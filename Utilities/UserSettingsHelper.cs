using System;
using System.IO;
using System.Security.Cryptography;
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
        /// Kullanýcý bilgilerini kaydet
        /// </summary>
        public static void SaveUserCredentials(string username, string password, bool remember)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY))
                {
                    if (remember)
                    {
                        // Þifreyi þifrele ve kaydet
                        string encryptedPassword = EncryptString(password);
                        
                        key.SetValue(USERNAME_VALUE, username);
                        key.SetValue(PASSWORD_VALUE, encryptedPassword);
                        key.SetValue(REMEMBER_VALUE, "true");
                    }
                    else
                    {
                        // Kayýtlý bilgileri temizle
                        key.DeleteValue(USERNAME_VALUE, false);
                        key.DeleteValue(PASSWORD_VALUE, false);
                        key.SetValue(REMEMBER_VALUE, "false");
                    }
                }
            }
            catch (Exception)
            {
                // Registry eriþim hatasý durumunda sessizce geç
            }
        }

        /// <summary>
        /// Kayýtlý kullanýcý bilgilerini al
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
                                string password = DecryptString(encryptedPassword);
                                return (username, password, true);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Registry eriþim hatasý durumunda varsayýlan deðerleri döndür
            }

            return ("", "", false);
        }

        /// <summary>
        /// Kayýtlý bilgileri temizle
        /// </summary>
        public static void ClearSavedCredentials()
        {
            SaveUserCredentials("", "", false);
        }

        /// <summary>
        /// String þifreleme (basit Base64 + XOR)
        /// </summary>
        private static string EncryptString(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return text;

                // Basit XOR þifreleme ile Base64 kodlama
                byte[] data = Encoding.UTF8.GetBytes(text);
                byte[] key = Encoding.UTF8.GetBytes("HospitalAutomation2024"); // Sabit anahtar
                
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)(data[i] ^ key[i % key.Length]);
                }
                
                return Convert.ToBase64String(data);
            }
            catch
            {
                return text; // Þifreleme baþarýsýzsa orijinal metni döndür
            }
        }

        /// <summary>
        /// String þifre çözme (basit Base64 + XOR)
        /// </summary>
        private static string DecryptString(string encryptedText)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                    return encryptedText;

                byte[] data = Convert.FromBase64String(encryptedText);
                byte[] key = Encoding.UTF8.GetBytes("HospitalAutomation2024"); // Ayný sabit anahtar
                
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)(data[i] ^ key[i % key.Length]);
                }
                
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return encryptedText; // Þifre çözme baþarýsýzsa orijinal metni döndür
            }
        }
    }
}