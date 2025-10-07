using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Tools
{
    public static class DatabaseFixerTool
    {
        public static void ShowDatabaseManager()
        {
            var form = new DatabaseManagerForm();
            form.ShowDialog();
        }
    }

    public partial class DatabaseManagerForm : Form
    {
        private TextBox _txtLog;

        public DatabaseManagerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Veritabaný Yöneticisi";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblTitle = new Label
            {
                Text = "HASTANE OTOMASYON SÝSTEMÝ - VERÝTABANI YÖNETÝCÝSÝ",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkBlue,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(550, 25),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            var btnTestConnection = new Button
            {
                Text = "?? Veritabaný Baðlantýsýný Test Et",
                Location = new System.Drawing.Point(50, 70),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightBlue
            };
            btnTestConnection.Click += BtnTestConnection_Click;

            var btnShowInfo = new Button
            {
                Text = "?? Veritabaný Bilgilerini Göster",
                Location = new System.Drawing.Point(300, 70),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightGreen
            };
            btnShowInfo.Click += BtnShowInfo_Click;

            var btnRecreateDatabase = new Button
            {
                Text = "?? Veritabanýný Yeniden Oluþtur",
                Location = new System.Drawing.Point(50, 130),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.Orange
            };
            btnRecreateDatabase.Click += BtnRecreateDatabase_Click;

            var btnInitializeDatabase = new Button
            {
                Text = "? Veritabanýný Baþlat",
                Location = new System.Drawing.Point(300, 130),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightYellow
            };
            btnInitializeDatabase.Click += BtnInitializeDatabase_Click;

            var btnShowCredentials = new Button
            {
                Text = "?? Varsayýlan Giriþ Bilgileri",
                Location = new System.Drawing.Point(50, 190),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightCyan
            };
            btnShowCredentials.Click += BtnShowCredentials_Click;

            var btnFixAllProblems = new Button
            {
                Text = "??? Tüm Sorunlarý Otomatik Çöz",
                Location = new System.Drawing.Point(300, 190),
                Size = new System.Drawing.Size(200, 40),
                BackColor = System.Drawing.Color.LightSalmon,
                Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
            };
            btnFixAllProblems.Click += BtnFixAllProblems_Click;

            var txtLog = new TextBox
            {
                Location = new System.Drawing.Point(50, 250),
                Size = new System.Drawing.Size(500, 150),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Text = "Veritabaný yöneticisine hoþ geldiniz!\n\n" +
                       "• Baðlantý testi için 'Test Et' butonunu kullanýn\n" +
                       "• Sorun yaþýyorsanýz 'Tüm Sorunlarý Otomatik Çöz' butonunu deneyin\n" +
                       "• Varsayýlan kullanýcý bilgilerini görmek için '??' butonunu kullanýn"
            };

            var btnClose = new Button
            {
                Text = "Kapat",
                Location = new System.Drawing.Point(250, 420),
                Size = new System.Drawing.Size(100, 30),
                DialogResult = DialogResult.OK
            };

            this.Controls.AddRange(new Control[] {
                lblTitle, btnTestConnection, btnShowInfo, btnRecreateDatabase,
                btnInitializeDatabase, btnShowCredentials, btnFixAllProblems,
                txtLog, btnClose
            });

            this._txtLog = txtLog;
        }

        private void LogMessage(string message)
        {
            if (_txtLog.InvokeRequired)
            {
                _txtLog.Invoke(new Action(() => LogMessage(message)));
                return;
            }

            _txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            _txtLog.SelectionStart = _txtLog.Text.Length;
            _txtLog.ScrollToCaret();
            Application.DoEvents();
        }

        private void BtnTestConnection_Click(object sender, EventArgs e)
        {
            LogMessage("Veritabaný baðlantýsý test ediliyor...");
            
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var exists = context.Database.Exists();
                    LogMessage($"Veritabaný durumu: {(exists ? "Mevcut" : "Mevcut deðil")}");

                    if (exists)
                    {
                        var userCount = context.Users.Count();
                        var patientCount = context.Patients.Count();
                        var departmentCount = context.Departments.Count();

                        LogMessage($"Kullanýcý sayýsý: {userCount}");
                        LogMessage($"Hasta sayýsý: {patientCount}");
                        LogMessage($"Bölüm sayýsý: {departmentCount}");
                        LogMessage("? Baðlantý baþarýlý!");
                    }
                    else
                    {
                        LogMessage("? Veritabaný mevcut deðil!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"? Baðlantý hatasý: {ex.Message}");
            }
        }

        private void BtnShowInfo_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.ShowDatabaseInfo();
        }

        private void BtnRecreateDatabase_Click(object sender, EventArgs e)
        {
            LogMessage("Veritabanýný yeniden oluþturma iþlemi baþlatýlýyor...");
            
            var result = MessageBox.Show(
                "Veritabanýný yeniden oluþturmak istediðinizden emin misiniz?\n\n" +
                "Bu iþlem tüm mevcut verileri silecektir!",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var context = new HospitalDbContext())
                    {
                        if (context.Database.Exists())
                        {
                            LogMessage("Mevcut veritabaný siliniyor...");
                            context.Database.Delete();
                        }

                        LogMessage("Yeni veritabaný oluþturuluyor...");
                        context.Database.Initialize(true);

                        LogMessage("Baþlangýç veriler ekleniyor...");
                        var userCount = context.Users.Count();
                        var patientCount = context.Patients.Count();
                        var departmentCount = context.Departments.Count();

                        LogMessage($"? Veritabaný baþarýyla oluþturuldu!");
                        LogMessage($"Oluþturulan kayýtlar: {userCount} kullanýcý, {patientCount} hasta, {departmentCount} bölüm");
                    }
                }
                catch (Exception ex)
                {
                    LogMessage($"? Hata: {ex.Message}");
                }
            }
        }

        private void BtnInitializeDatabase_Click(object sender, EventArgs e)
        {
            LogMessage("Veritabaný baþlatýlýyor...");
            
            try
            {
                using (var context = new HospitalDbContext())
                {
                    context.Database.Initialize(force: true);
                    LogMessage("? Veritabaný baþarýyla baþlatýldý!");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"? Baþlatma hatasý: {ex.Message}");
            }
        }

        private void BtnShowCredentials_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.ShowDefaultLoginCredentials();
        }

        private void BtnFixAllProblems_Click(object sender, EventArgs e)
        {
            LogMessage("??? Otomatik sorun çözme baþlatýlýyor...");
            
            try
            {
                // Step 1: Check connection
                LogMessage("1. Baðlantý kontrol ediliyor...");
                
                using (var context = new HospitalDbContext())
                {
                    bool needsRecreation = false;

                    // Check if database exists
                    if (!context.Database.Exists())
                    {
                        LogMessage("? Veritabaný mevcut deðil");
                        needsRecreation = true;
                    }
                    else
                    {
                        LogMessage("? Veritabaný mevcut");

                        // Check if model is compatible
                        try
                        {
                            if (!context.Database.CompatibleWithModel(throwIfNoMetadata: false))
                            {
                                LogMessage("? Model uyumsuzluðu tespit edildi");
                                needsRecreation = true;
                            }
                            else
                            {
                                LogMessage("? Model uyumlu");
                            }
                        }
                        catch
                        {
                            LogMessage("? Model uyumluluk kontrolü baþarýsýz");
                            needsRecreation = true;
                        }

                        // Check if we have essential data
                        if (!needsRecreation)
                        {
                            try
                            {
                                var userCount = context.Users.Count();
                                var departmentCount = context.Departments.Count();

                                if (userCount == 0 || departmentCount == 0)
                                {
                                    LogMessage("? Temel veriler eksik");
                                    needsRecreation = true;
                                }
                                else
                                {
                                    LogMessage($"? Temel veriler mevcut ({userCount} kullanýcý, {departmentCount} bölüm)");
                                }
                            }
                            catch (Exception ex)
                            {
                                LogMessage($"? Veri kontrolü baþarýsýz: {ex.Message}");
                                needsRecreation = true;
                            }
                        }
                    }

                    // Recreate if needed
                    if (needsRecreation)
                    {
                        LogMessage("2. Veritabaný yeniden oluþturuluyor...");

                        if (context.Database.Exists())
                        {
                            LogMessage("Mevcut veritabaný siliniyor...");
                            context.Database.Delete();
                        }

                        LogMessage("Yeni veritabaný oluþturuluyor...");
                        context.Database.Initialize(true);

                        // Verify creation
                        var userCount = context.Users.Count();
                        var patientCount = context.Patients.Count();
                        var departmentCount = context.Departments.Count();

                        LogMessage($"? Veritabaný baþarýyla oluþturuldu!");
                        LogMessage($"Oluþturulan kayýtlar:");
                        LogMessage($"  • {userCount} Kullanýcý");
                        LogMessage($"  • {patientCount} Örnek Hasta");
                        LogMessage($"  • {departmentCount} Bölüm");
                    }
                    else
                    {
                        LogMessage("? Veritabaný zaten saðlýklý durumda");
                    }
                }

                LogMessage("");
                LogMessage("?? TÜM SORUNLAR ÇÖZÜLDÜ!");
                LogMessage("");
                LogMessage("Varsayýlan giriþ bilgileri:");
                LogMessage("• Admin: admin / admin123");
                LogMessage("• Doktor: dr.mehmet.kardiyoloji / 123456");
                LogMessage("• Hasta: 12345678901 / 1505");

                MessageBox.Show(
                    "Tüm veritabaný sorunlarý baþarýyla çözüldü!\n\n" +
                    "Þimdi ana uygulamaya dönüp giriþ yapabilirsiniz.\n\n" +
                    "Varsayýlan giriþ bilgileri için '??' butonunu kullanabilirsiniz.",
                    "Baþarýlý",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogMessage($"? Kritik hata: {ex.Message}");
                LogMessage($"Stack Trace: {ex.StackTrace}");
                
                MessageBox.Show(
                    $"Otomatik çözüm sýrasýnda hata oluþtu:\n{ex.Message}\n\nLütfen manuel olarak veritabanýný yeniden oluþturun.",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}