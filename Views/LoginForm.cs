using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;
using HospitalAutomation.Views.Authentication;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Infrastructure;

namespace HospitalAutomation.Views
{
    public partial class LoginForm : Form
    {
        private IUserService _userService;
        private IPatientService _patientService;

        // Global LoginForm referansý
        public static LoginForm Instance { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            Instance = this; // Static referansý ayarla
            
            // Veritabanýný kontrol et ama baþarýsýz olursa uygulamayý kapatma
            try
            {
                if (!DatabaseTestHelper.EnsureDatabaseReady())
                {
                    var result = MessageBox.Show(
                        "Veritabaný baþlatýlamadý!\n\n" +
                        "Sýnýrlý modda devam etmek ister misiniz?\n" +
                        "(Bazý özellikler çalýþmayabilir)\n\n" +
                        "'Hayýr' seçerseniz uygulama kapatýlacak.",
                        "Veritabaný Uyarýsý",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.No)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database check failed in LoginForm: {ex.Message}");
                
                var result = MessageBox.Show(
                    $"Veritabaný kontrol hatasý:\n{ex.Message}\n\n" +
                    "Sýnýrlý modda devam etmek ister misiniz?",
                    "Veritabaný Hatasý",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result == DialogResult.No)
                {
                    Application.Exit();
                    return;
                }
            }
            
            InitializeServices();
            LoadSavedCredentials();
        }

        /// <summary>
        /// Yeni giriþ için login formunu tekrar göster
        /// </summary>
        public void ShowForNewLogin()
        {
            // Kullanýcý bilgilerini temizle
            txtUsername.Text = "";
            txtPassword.Text = "";
            chkRememberMe.Checked = false;
            
            // Servisleri yeniden baþlat
            InitializeServices();
            
            // Formu göster
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Focus();
            this.Activate();
            
            // Ýlk kontrole focus ver
            txtUsername.Focus();
        }

        /// <summary>
        /// Kayýtlý kullanýcý bilgilerini yükle
        /// </summary>
        private void LoadSavedCredentials()
        {
            try
            {
                var (username, password, remember) = UserSettingsHelper.GetSavedCredentials();
                
                if (remember)
                {
                    txtUsername.Text = username;
                    txtPassword.Text = password;
                    chkRememberMe.Checked = true;
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce geç
                System.Diagnostics.Debug.WriteLine($"Kayýtlý bilgiler yüklenirken hata: {ex.Message}");
            }
        }

        private void InitializeServices()
        {
            try
            {
                var context = new HospitalDbContext();
                var unitOfWork = new UnitOfWork(context);
                
                // SimpleContainer'a kaydet
                SimpleContainer.Instance.RegisterSingleton(unitOfWork);
                SimpleContainer.Instance.RegisterSingleton<HospitalDbContext>(context);
                
                _userService = new UserService(unitOfWork);
                _patientService = new PatientService(unitOfWork);
                
                System.Diagnostics.Debug.WriteLine("Services initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Service initialization failed: {ex.Message}");
                
                // Servisler baþlatýlamazsa null býrak, BtnLogin_Click'te kontrol edilecek
                _userService = null;
                _patientService = null;
                
                MessageBox.Show(
                    $"Servisler baþlatýlýrken hata oluþtu:\n{ex.Message}\n\n" +
                    "Bazý özellikler çalýþmayabilir.",
                    "Servis Baþlatma Uyarýsý",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.lblHospitalName = new System.Windows.Forms.Label();
            this.btnShowDefaultCredentials = new System.Windows.Forms.Button();
            this.btnTestDatabase = new System.Windows.Forms.Button();
            this.btnRecreateDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(200, 90);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(159, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "GÝRÝÞ YAP";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUsername.Location = new System.Drawing.Point(80, 150);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(106, 20);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Kullanýcý Adý:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUsername.Location = new System.Drawing.Point(200, 147);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(220, 26);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPassword.Location = new System.Drawing.Point(80, 190);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(49, 20);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Þifre:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPassword.Location = new System.Drawing.Point(200, 187);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(220, 26);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPassword_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(200, 270);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Giriþ Yap";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(320, 270);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 35);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.Text = "Kayýt Ol";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(200, 330);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(220, 30);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Çýkýþ";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkRememberMe.Location = new System.Drawing.Point(200, 230);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(105, 22);
            this.chkRememberMe.TabIndex = 6;
            this.chkRememberMe.Text = "Beni Hatýrla";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // lblHospitalName
            // 
            this.lblHospitalName.AutoSize = true;
            this.lblHospitalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblHospitalName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.lblHospitalName.Location = new System.Drawing.Point(50, 30);
            this.lblHospitalName.Name = "lblHospitalName";
            this.lblHospitalName.Size = new System.Drawing.Size(581, 39);
            this.lblHospitalName.TabIndex = 0;
            this.lblHospitalName.Text = "HASTANE OTOMASYON SÝSTEMÝ";
            // 
            // btnShowDefaultCredentials
            // 
            this.btnShowDefaultCredentials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnShowDefaultCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowDefaultCredentials.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowDefaultCredentials.ForeColor = System.Drawing.Color.White;
            this.btnShowDefaultCredentials.Location = new System.Drawing.Point(428, 150);
            this.btnShowDefaultCredentials.Name = "btnShowDefaultCredentials";
            this.btnShowDefaultCredentials.Size = new System.Drawing.Size(190, 40);
            this.btnShowDefaultCredentials.TabIndex = 10;
            this.btnShowDefaultCredentials.Text = "Varsayýlan Giriþ Bilgileri";
            this.btnShowDefaultCredentials.UseVisualStyleBackColor = false;
            this.btnShowDefaultCredentials.Click += new System.EventHandler(this.BtnShowDefaultCredentials_Click);
            // 
            // btnTestDatabase
            // 
            this.btnTestDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.btnTestDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestDatabase.ForeColor = System.Drawing.Color.White;
            this.btnTestDatabase.Location = new System.Drawing.Point(428, 206);
            this.btnTestDatabase.Name = "btnTestDatabase";
            this.btnTestDatabase.Size = new System.Drawing.Size(190, 40);
            this.btnTestDatabase.TabIndex = 11;
            this.btnTestDatabase.Text = "Veritabaný Testi";
            this.btnTestDatabase.UseVisualStyleBackColor = false;
            this.btnTestDatabase.Click += new System.EventHandler(this.BtnTestDatabase_Click);
            // 
            // btnRecreateDatabase
            // 
            this.btnRecreateDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRecreateDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecreateDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnRecreateDatabase.ForeColor = System.Drawing.Color.White;
            this.btnRecreateDatabase.Location = new System.Drawing.Point(428, 262);
            this.btnRecreateDatabase.Name = "btnRecreateDatabase";
            this.btnRecreateDatabase.Size = new System.Drawing.Size(190, 40);
            this.btnRecreateDatabase.TabIndex = 12;
            this.btnRecreateDatabase.Text = "Veritabanýný Yeniden Oluþtur";
            this.btnRecreateDatabase.UseVisualStyleBackColor = false;
            this.btnRecreateDatabase.Click += new System.EventHandler(this.BtnRecreateDatabase_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(670, 444);
            this.Controls.Add(this.btnRecreateDatabase);
            this.Controls.Add(this.btnTestDatabase);
            this.Controls.Add(this.btnShowDefaultCredentials);
            this.Controls.Add(this.lblHospitalName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giriþ - Hastane Otomasyon Sistemi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Kullanýcý adý ve þifre boþ býrakýlamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Servisler hazýr mý kontrol et
                if (_userService == null || _patientService == null)
                {
                    MessageBox.Show(
                        "Veritabaný servisleri hazýr deðil!\n\n" +
                        "Lütfen uygulamayý yeniden baþlatýn veya\n" +
                        "veritabaný yöneticisinden sorunlarý giderin.",
                        "Servis Hatasý",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Disable login button to prevent multiple clicks
                btnLogin.Enabled = false;
                btnLogin.Text = "Giriþ yapýlýyor...";
                Application.DoEvents();

                bool loginSuccessful = false;
                Form nextForm = null;

                // Önce doktor giriþini kontrol et
                var user = _userService.Authenticate(username, password);
                if (user != null)
                {
                    SessionManager.Login(user);
                    loginSuccessful = true;
                    
                    // Rol bazlý form yönlendirmesi
                    switch (user.Role)
                    {
                        case UserRole.Doctor:
                            nextForm = new Views.DoctorManagement.DoctorMainForm();
                            break;

                        case UserRole.Nurse:
                            nextForm = new Views.NurseManagement.NurseMainForm();
                            break;

                        case UserRole.Admin:
                        case UserRole.Receptionist:
                        case UserRole.Staff:
                        default:
                            nextForm = new MainForm();
                            break;
                    }
                }
                else
                {
                    // Doktor deðilse hasta giriþini kontrol et (TC kimlik no ile)
                    var patient = _patientService.GetPatientByNationalId(username);
                    if (patient != null)
                    {
                        // Basit bir hasta doðrulamasý - gerçek uygulamada daha güvenli olmalý
                        // Þu an için doðum tarihi son 4 hanesi þifre olarak kullanýlýyor
                        var expectedPassword = patient.BirthDate.ToString("ddMM");
                        if (password == expectedPassword || password == patient.NationalId) // Geçici: TC kimlik no da þifre olarak kabul edilsin
                        {
                            SessionManager.LoginPatient(patient);
                            loginSuccessful = true;
                            nextForm = new Views.PatientPortal.PatientMainForm();
                        }
                    }
                }

                if (loginSuccessful && nextForm != null)
                {
                    // "Beni Hatýrla" iþaretliyse bilgileri kaydet
                    UserSettingsHelper.SaveUserCredentials(username, password, chkRememberMe.Checked);
                    
                    // Form geçiþi yap
                    this.Hide();
                    
                    // Yeni formu göster ve Application.Run() ile baþlat
                    nextForm.FormClosed += (s, args) => {
                        // Ana form kapatýldýðýnda login formunu tekrar göster
                        this.ShowForNewLogin();
                    };
                    
                    nextForm.ShowDialog(); // ShowDialog kullanarak modal göster
                    return;
                }

                // Giriþ baþarýsýz
                MessageBox.Show("Kullanýcý adý veya þifre hatalý!\n\nVarsayýlan giriþ bilgileri için 'Yardým' butonuna basabilirsiniz.", 
                    "Giriþ Baþarýsýz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex}");
                MessageBox.Show($"Giriþ sýrasýnda hata oluþtu:\n{ex.Message}\n\nLütfen veritabaný baðlantýsýný kontrol edin.", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Giriþ Yap";
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Sadece hasta kaydý
            var registerForm = new Views.Authentication.PatientRegisterForm();
            registerForm.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnShowDefaultCredentials_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.ShowDefaultLoginCredentials();
        }

        private void BtnTestDatabase_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.TestDatabaseConnection();
        }

        private void BtnRecreateDatabase_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.RecreateDatabase();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Login form kapatýlýrsa uygulamayý tamamen kapat
            Application.Exit();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Label lblHospitalName;
        private System.Windows.Forms.Button btnShowDefaultCredentials;
        private System.Windows.Forms.Button btnTestDatabase;
        private System.Windows.Forms.Button btnRecreateDatabase;
    }
}