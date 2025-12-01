using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  
using System.Windows.Forms;
using HospitalAutomation.Views;
using HospitalAutomation.Utilities;
using HospitalAutomation.Tools;

namespace HospitalAutomation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                System.Diagnostics.Debug.WriteLine("Starting Hospital Automation System...");
                
                // Basit veritabaný kontrolü - hýzlý baþlatma için
                bool dbReady = false;
                try
                {
                    dbReady = DatabaseTestHelper.EnsureDatabaseReady();
                    System.Diagnostics.Debug.WriteLine($"Database ready: {dbReady}");
                }
                catch (Exception dbEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Database initialization error: {dbEx.Message}");
                    
                    // Kullanýcýya hýzlý çözüm sun
                    var result = MessageBox.Show(
                        "Veritabaný baþlatma sorunu!\n\n" +
                        "Hýzlý çözüm için 'Evet' seçin.\n" +
                        "(Veritabaný otomatik olarak düzeltilecek)\n\n" +
                        "'Hayýr' = Sýnýrlý modda devam et\n" +
                        "'Ýptal' = Uygulamayý kapat",
                        "Veritabaný Sorunu",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                            try
                            {
                                MessageBox.Show("Veritabaný otomatik düzeltiliyor...\nLütfen bekleyin.", "Düzeltme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DatabaseTestHelper.RecreateDatabase();
                                dbReady = true;
                            }
                            catch (Exception fixEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Database fix failed: {fixEx.Message}");
                                MessageBox.Show($"Otomatik düzeltme baþarýsýz: {fixEx.Message}\n\nSýnýrlý modda devam ediliyor.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dbReady = false;
                            }
                            break;
                            
                        case DialogResult.No:
                            dbReady = false;
                            break;
                            
                        case DialogResult.Cancel:
                        default:
                            return; // Uygulamayý kapat
                    }
                }

                System.Diagnostics.Debug.WriteLine("Starting login form...");
                
                // Login formunu oluþtur ve baþlat
                var loginForm = new LoginForm();
                
                // LoginForm ile uygulamayý baþlat
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Uygulama baþlatýlýrken kritik hata oluþtu:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDetay: {ex.InnerException.Message}";
                }

                System.Diagnostics.Debug.WriteLine($"Application startup error: {ex}");

                MessageBox.Show(errorMessage, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Otomatik giriþ denemesi
        /// </summary>
        private static bool TryAutoLogin(string username, string password)
        {
            try
            {
                using (var context = new HospitalAutomation.Data.HospitalDbContext())
                {
                    var unitOfWork = new HospitalAutomation.Data.UnitOfWork(context);
                    var userService = new HospitalAutomation.Services.UserService(unitOfWork);
                    var patientService = new HospitalAutomation.Services.PatientService(unitOfWork);

                    // Önce staff/doktor giriþini dene
                    var user = userService.Authenticate(username, password);
                    if (user != null)
                    {
                        SessionManager.Login(user);
                        
                        // Rol bazlý form yönlendirmesi
                        switch (user.Role)
                        {
                            case HospitalAutomation.Models.Enums.UserRole.Doctor:
                                var doctorMainForm = new HospitalAutomation.Views.DoctorManagement.DoctorMainForm();
                                Application.Run(doctorMainForm);
                                break;

                            case HospitalAutomation.Models.Enums.UserRole.Nurse:
                                var nurseMainForm = new HospitalAutomation.Views.NurseManagement.NurseMainForm();
                                Application.Run(nurseMainForm);
                                break;

                            case HospitalAutomation.Models.Enums.UserRole.Admin:
                            case HospitalAutomation.Models.Enums.UserRole.Receptionist:
                            case HospitalAutomation.Models.Enums.UserRole.Staff:
                            default:
                                var mainForm = new MainForm();
                                Application.Run(mainForm);
                                break;
                        }
                        return true;
                    }

                    // Hasta giriþini dene
                    var patient = patientService.GetPatientByNationalId(username);
                    if (patient != null)
                    {
                        var expectedPassword = patient.BirthDate.ToString("ddMM");
                        if (password == expectedPassword || password == patient.NationalId)
                        {
                            SessionManager.LoginPatient(patient);
                            var patientMainForm = new HospitalAutomation.Views.PatientPortal.PatientMainForm();
                            Application.Run(patientMainForm);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Auto-login failed: {ex.Message}");
            }

            return false;
        }
    }
}