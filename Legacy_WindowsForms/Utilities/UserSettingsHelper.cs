using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace HospitalAutomation.Utilities
{
    public static class UserSettingsHelper
    {
        private const string REGISTRY_KEY = @"SOFTWARE\HospitalAutomation";
        private const string USERNAME_VALUE = "RememberedUsername";
        private const string PASSWORD_VALUE = "RememberedPassword";
        private const string REMEMBER_VALUE = "RememberMe";

        /// <summary>
        /// Kullanýcý bilgilerini DPAPI (CryptProtectData) ile þifreleyip registry'de saklar (CurrentUser scope).
        /// </summary>
        public static void SaveUserCredentials(string username, string password, bool remember)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY))
                {
                    if (remember)
                    {
                        var encrypted = ProtectString(password);
                        key.SetValue(USERNAME_VALUE, username ?? string.Empty);
                        key.SetValue(PASSWORD_VALUE, encrypted ?? string.Empty);
                        key.SetValue(REMEMBER_VALUE, "true");
                    }
                    else
                    {
                        key.DeleteValue(USERNAME_VALUE, false);
                        key.DeleteValue(PASSWORD_VALUE, false);
                        key.SetValue(REMEMBER_VALUE, "false");
                    }
                }
            }
            catch (Exception)
            {
                // Registry eriþim veya þifreleme hatasý durumunda sessizce geç.
            }
        }

        /// <summary>
        /// Kayýtlý kullanýcý bilgilerini geri döner. Baþarýsýzsa boþ döner.
        /// </summary>
        public static (string username, string password, bool remember) GetSavedCredentials()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY))
                {
                    if (key != null)
                    {
                        string remember = key.GetValue(REMEMBER_VALUE, "false").ToString();
                        if (remember == "true")
                        {
                            string username = key.GetValue(USERNAME_VALUE, "").ToString();
                            string encryptedPassword = key.GetValue(PASSWORD_VALUE, "").ToString();

                            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(encryptedPassword))
                            {
                                var password = UnprotectString(encryptedPassword);
                                if (password == null) return ("", "", false);
                                return (username, password, true);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda hassas veri döndürmüyoruz
            }

            return ("", "", false);
        }

        public static void ClearSavedCredentials()
        {
            SaveUserCredentials("", "", false);
        }

        /// <summary>
        /// DPAPI ile þifrele (CryptProtectData). Dönen Base64 string.
        /// P/Invoke kullanýlarak ProtectedData'ya olan baðýmlýlýk kaldýrýldý.
        /// </summary>
        private static string ProtectString(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return string.Empty;
                var data = Encoding.UTF8.GetBytes(text);
                var encrypted = CryptProtect(data);
                return encrypted == null ? null : Convert.ToBase64String(encrypted);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DPAPI ile çöz (CryptUnprotectData). Baþarýsýzsa null döner.
        /// </summary>
        private static string UnprotectString(string protectedBase64)
        {
            try
            {
                if (string.IsNullOrEmpty(protectedBase64)) return string.Empty;
                var protectedData = Convert.FromBase64String(protectedBase64);
                var decrypted = CryptUnprotect(protectedData);
                return decrypted == null ? null : Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return null;
            }
        }

        #region DPAPI P/Invoke (CryptProtectData / CryptUnprotectData)

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;

            public DATA_BLOB(byte[] data)
            {
                cbData = data?.Length ?? 0;
                pbData = IntPtr.Zero;
                if (cbData > 0)
                {
                    pbData = Marshal.AllocHGlobal(cbData);
                    Marshal.Copy(data, 0, pbData, cbData);
                }
            }

            public void Free()
            {
                if (pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pbData);
                    pbData = IntPtr.Zero;
                }
                cbData = 0;
            }
        }

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CryptProtectData(
            ref DATA_BLOB pDataIn,
            string pszDataDescr,
            IntPtr pOptionalEntropy,
            IntPtr pvReserved,
            IntPtr pPromptStruct,
            int dwFlags,
            out DATA_BLOB pDataOut);

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CryptUnprotectData(
            ref DATA_BLOB pDataIn,
            StringBuilder ppszDataDescr,
            IntPtr pOptionalEntropy,
            IntPtr pvReserved,
            IntPtr pPromptStruct,
            int dwFlags,
            out DATA_BLOB pDataOut);

        private static byte[] CryptProtect(byte[] plainData)
        {
            if (plainData == null || plainData.Length == 0) return new byte[0];

            var inBlob = new DATA_BLOB(plainData);
            try
            {
                DATA_BLOB outBlob;
                bool success = CryptProtectData(ref inBlob, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, out outBlob);
                if (!success || outBlob.cbData == 0)
                {
                    // attempt cleanup
                    outBlob.Free();
                    return null;
                }

                try
                {
                    var encrypted = new byte[outBlob.cbData];
                    Marshal.Copy(outBlob.pbData, encrypted, 0, outBlob.cbData);
                    return encrypted;
                }
                finally
                {
                    outBlob.Free();
                }
            }
            finally
            {
                inBlob.Free();
            }
        }

        private static byte[] CryptUnprotect(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length == 0) return new byte[0];

            var inBlob = new DATA_BLOB(encryptedData);
            try
            {
                DATA_BLOB outBlob;
                bool success = CryptUnprotectData(ref inBlob, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, out outBlob);
                if (!success || outBlob.cbData == 0)
                {
                    outBlob.Free();
                    return null;
                }

                try
                {
                    var decrypted = new byte[outBlob.cbData];
                    Marshal.Copy(outBlob.pbData, decrypted, 0, outBlob.cbData);
                    return decrypted;
                }
                finally
                {
                    outBlob.Free();
                }
            }
            finally
            {
                inBlob.Free();
            }
        }

        #endregion
    }
}