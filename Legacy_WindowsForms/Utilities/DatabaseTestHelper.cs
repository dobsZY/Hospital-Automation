using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using System.Data.SqlClient;

namespace HospitalAutomation.Utilities
{
    public static class DatabaseTestHelper
    {
        private static readonly object _lock = new object();

        public static void TestDatabaseConnection()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Force database creation if not exists
                    context.Database.Initialize(force: false);
                    
                    // Test connection
                    var canConnect = context.Database.Exists();
                    
                    if (canConnect)
                    {
                        // Get table counts
                        var userCount = context.Users.Count();
                        var patientCount = context.Patients.Count();
                        var departmentCount = context.Departments.Count();
                        var appointmentCount = context.Appointments.Count();
                        
                        var message = $"Veritabaný Baðlantýsý: BAÞARILI\n\n" +
                                     $"Tablolar ve Kayýt Sayýlarý:\n" +
                                     $"• Users: {userCount}\n" +
                                     $"• Patients: {patientCount}\n" +
                                     $"• Departments: {departmentCount}\n" +
                                     $"• Appointments: {appointmentCount}\n\n" +
                                     $"Varsayýlan Giriþ Bilgileri:\n" +
                                     $"Admin: admin / admin123\n" +
                                     $"Doktor: dr.mehmet.kardiyoloji / 123456\n" +
                                     $"Hasta: 12345678901 / 1505\n\n" +
                                     $"Veritabaný Konumu:\n" +
                                     $"(LocalDB)\\MSSQLLocalDB - HospitalDatabase";
                        
                        MessageBox.Show(message, "Veritabaný Test Sonucu", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Veritabanýna baðlanýlamýyor!", "Hata", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný test hatasý:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public static void ShowDatabaseInfo()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var connectionString = context.Database.Connection.ConnectionString;
                    var databaseName = context.Database.Connection.Database;
                    
                    var info = $"Veritabaný Bilgileri:\n\n" +
                              $"Database Name: {databaseName}\n" +
                              $"Connection String: {connectionString}\n\n" +
                              $"Eriþim Yollarý:\n" +
                              $"1. Visual Studio ? View ? SQL Server Object Explorer\n" +
                              $"2. Server Explorer ? Data Connections\n" +
                              $"3. SQL Server Management Studio (SSMS)\n" +
                              $"   Server: (LocalDB)\\MSSQLLocalDB";
                    
                    MessageBox.Show(info, "Veritabaný Bilgileri", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilgi alma hatasý:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RecreateDatabase()
        {
            lock (_lock)
            {
                try
                {
                    var result = MessageBox.Show(
                        "Veritabanýný yeniden oluþturmak istediðinizden emin misiniz?\n\n" +
                        "Bu iþlem mevcut tüm verileri silecektir!\n\n" +
                        "Yeni veritabanýnda þu varsayýlan kullanýcýlar oluþturulacak:\n" +
                        "• Admin: admin / admin123\n" +
                        "• 10 Doktor (Þifre: 123456)\n" +
                        "• 3 Hemþire (Þifre: 123456)\n" +
                        "• 2 Resepsiyon (Þifre: 123456)\n" +
                        "• 5 Örnek Hasta",
                        "Veritabanýný Yeniden Oluþtur",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Tüm baðlantýlarý kapat
                        ForceCloseAllConnections();
                        
                        using (var context = new HospitalDbContext())
                        {
                            // Güvenli delete
                            SafeDeleteDatabase();
                            
                            // Kýsa bekleme
                            System.Threading.Thread.Sleep(1000);
                            
                            // Create database - this will trigger the initializer
                            context.Database.Initialize(true);
                            
                            // Test by counting records
                            var userCount = context.Users.Count();
                            var patientCount = context.Patients.Count();
                            var departmentCount = context.Departments.Count();
                            
                            MessageBox.Show(
                                $"Veritabaný baþarýyla yeniden oluþturuldu!\n\n" +
                                $"Oluþturulan kayýtlar:\n" +
                                $"• {userCount} Kullanýcý (Admin, Doktor, Hemþire, Resepsiyon)\n" +
                                $"• {patientCount} Örnek Hasta\n" +
                                $"• {departmentCount} Bölüm\n\n" +
                                $"Giriþ yapmak için uygulamayý yeniden baþlatýn.",
                                "Baþarýlý",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabaný yeniden oluþturma hatasý:\n{ex.Message}\n\nInner Exception: {ex.InnerException?.Message}", "Hata", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void InitializeDatabase()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Force database initialization
                    context.Database.Initialize(force: true);
                    
                    MessageBox.Show(
                        "Veritabaný baþarýyla baþlatýldý!",
                        "Baþarýlý",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný baþlatma hatasý:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Model deðiþiklikleri durumunda veritabanýný otomatik olarak yeniden oluþturur
        /// </summary>
        public static bool HandleModelChanges()
        {
            lock (_lock)
            {
                try
                {
                    using (var context = new HospitalDbContext())
                    {
                        // Model uyumluluðunu kontrol et
                        if (context.Database.CompatibleWithModel(throwIfNoMetadata: false))
                        {
                            return true; // Model uyumlu, sorun yok
                        }
                        
                        var result = MessageBox.Show(
                            "Veritabaný modeli deðiþmiþ!\n\n" +
                            "Model deðiþiklikleri nedeniyle veritabanýnýn yeniden oluþturulmasý gerekiyor.\n" +
                            "Bu iþlem mevcut verileri silecektir.\n\n" +
                            "Devam etmek istiyor musunuz?",
                            "Model Deðiþikliði Tespit Edildi",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Tüm baðlantýlarý kapat
                            ForceCloseAllConnections();
                            
                            // Veritabanýný sil ve yeniden oluþtur
                            SafeDeleteDatabase();
                            
                            System.Threading.Thread.Sleep(1000);
                            
                            context.Database.Initialize(true);
                            
                            MessageBox.Show(
                                "Veritabaný baþarýyla yeniden oluþturuldu!\n\n" +
                                "Yeni doktorlar ve örnek veriler eklendi.",
                                "Baþarýlý",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            
                            return true;
                        }
                        
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Veritabaný model deðiþikliði iþlemi sýrasýnda hata:\n{ex.Message}\n\n" +
                        "Lütfen uygulamayý yeniden baþlatýn ve tekrar deneyin.",
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Uygulama baþlangýcýnda veritabanýný kontrol eder ve gerekirse düzeltir
        /// </summary>
        public static bool EnsureDatabaseReady()
        {
            lock (_lock)
            {
                try
                {
                    using (var context = new HospitalDbContext())
                    {
                        // Veritabaný var mý kontrol et
                        if (!context.Database.Exists())
                        {
                            System.Diagnostics.Debug.WriteLine("Database does not exist, creating...");
                            context.Database.Initialize(true);
                            return true;
                        }
                        
                        // Model uyumlu mu kontrol et
                        try
                        {
                            if (!context.Database.CompatibleWithModel(throwIfNoMetadata: false))
                            {
                                System.Diagnostics.Debug.WriteLine("Database model is not compatible, recreating...");
                                ForceCloseAllConnections();
                                SafeDeleteDatabase();
                                System.Threading.Thread.Sleep(1000);
                                context.Database.Initialize(true);
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Model compatibility check failed: {ex.Message}");
                            // Model check failed, assume incompatible
                            ForceCloseAllConnections();
                            SafeDeleteDatabase();
                            System.Threading.Thread.Sleep(1000);
                            context.Database.Initialize(true);
                            return true;
                        }
                        
                        // Check if we have essential data
                        try
                        {
                            var userCount = context.Users.Count();
                            var departmentCount = context.Departments.Count();
                            
                            if (userCount == 0 || departmentCount == 0)
                            {
                                System.Diagnostics.Debug.WriteLine("Database exists but lacks essential data, recreating...");
                                ForceCloseAllConnections();
                                SafeDeleteDatabase();
                                System.Threading.Thread.Sleep(1000);
                                context.Database.Initialize(true);
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Data check failed: {ex.Message}");
                            // Data check failed, recreate database
                            ForceCloseAllConnections();
                            SafeDeleteDatabase();
                            System.Threading.Thread.Sleep(1000);
                            context.Database.Initialize(true);
                            return true;
                        }
                        
                        System.Diagnostics.Debug.WriteLine("Database is ready!");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error ensuring database ready: {ex.Message}");
                    try
                    {
                        // Last resort - try to create a fresh database
                        ForceCloseAllConnections();
                        SafeDeleteDatabase();
                        System.Threading.Thread.Sleep(1000);
                        using (var context = new HospitalDbContext())
                        {
                            context.Database.Initialize(true);
                            return true;
                        }
                    }
                    catch (Exception innerEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create fresh database: {innerEx.Message}");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Tüm baðlantýlarý zorla kapat
        /// </summary>
        private static void ForceCloseAllConnections()
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.Collect();
                
                // Entity Framework connection pool'unu temizle
                SqlConnection.ClearAllPools();
                
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error closing connections: {ex.Message}");
            }
        }

        /// <summary>
        /// Güvenli veritabaný silme iþlemi
        /// </summary>
        private static void SafeDeleteDatabase()
        {
            try
            {
                // Connection pool'larý temizle
                SqlConnection.ClearAllPools();
                
                using (var masterConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
                {
                    masterConnection.Open();
                    
                    var killConnectionsCommand = new SqlCommand(@"
                        IF DB_ID('HospitalDatabase') IS NOT NULL
                        BEGIN
                            ALTER DATABASE [HospitalDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            DROP DATABASE [HospitalDatabase];
                        END", masterConnection);
                    
                    killConnectionsCommand.ExecuteNonQuery();
                    masterConnection.Close();
                }
                
                System.Diagnostics.Debug.WriteLine("Database deleted successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Safe delete failed: {ex.Message}");
                // Son çare olarak baþka yöntemler denenebilir ama genelde bu çalýþýr
            }
        }

        public static void ShowDefaultLoginCredentials()
        {
            var message = "VARSAYILAN GÝRÝÞ BÝLGÝLERÝ\n\n" +
                         "?? ADMIN\n" +
                         "Kullanýcý Adý: admin\n" +
                         "Þifre: admin123\n\n" +
                         "??????? DOKTORLAR (Þifre: 123456)\n" +
                         "• dr.mehmet.kardiyoloji\n" +
                         "• dr.ayse.noroloji\n" +
                         "• dr.ali.ortopedi\n" +
                         "• dr.zeynep.dahiliye\n" +
                         "• dr.can.goz\n" +
                         "• dr.fatma.dermatoloji\n" +
                         "• dr.emre.psikiyatri\n" +
                         "• dr.selin.kbb\n" +
                         "• dr.burak.uroloji\n" +
                         "• dr.elif.jinekolog\n\n" +
                         "??????? HEMÞÝRELER (Þifre: 123456)\n" +
                         "• hemþire.fatma\n" +
                         "• hemþire.elif\n" +
                         "• hemþire.ayþe\n\n" +
                         "?? RESEPSÝYON (Þifre: 123456)\n" +
                         "• resepsiyon.ali\n" +
                         "• resepsiyon.sema\n\n" +
                         "?? ÖRNEK HASTALAR\n" +
                         "TC: 12345678901, Þifre: 1505\n" +
                         "TC: 98765432109, Þifre: 2208";

            MessageBox.Show(message, "Varsayýlan Giriþ Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}