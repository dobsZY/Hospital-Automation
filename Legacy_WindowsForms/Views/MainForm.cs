using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;
using HospitalAutomation.Views.PatientManagement;
using HospitalAutomation.Views.AppointmentManagement;
using HospitalAutomation.Views.UserManagement;
using HospitalAutomation.Views.Reports;
using HospitalAutomation.Views.Authentication;

namespace HospitalAutomation.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateUIBasedOnUserRole();
            UpdateWelcomeMessage();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hastaYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastaEkleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hastaListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevuYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevuEkleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randevuListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanýcýYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanýcýEkleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanýcýListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raporlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.günlükRandevularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.þifreDeðiþtirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çýkýþToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblQuickActions = new System.Windows.Forms.Label();
            this.btnQuickPatientAdd = new System.Windows.Forms.Button();
            this.btnQuickAppointment = new System.Windows.Forms.Button();
            this.btnQuickPatientList = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            
            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hastane Otomasyon Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MdiChildActivate += new EventHandler(this.MainForm_MdiChildActivate);
            
            // menuStrip1
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hastaYönetimiToolStripMenuItem,
            this.randevuYönetimiToolStripMenuItem,
            this.kullanýcýYönetimiToolStripMenuItem,
            this.raporlarToolStripMenuItem,
            this.ayarlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            
            // hastaYönetimiToolStripMenuItem
            this.hastaYönetimiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hastaEkleToolStripMenuItem,
            this.hastaListesiToolStripMenuItem});
            this.hastaYönetimiToolStripMenuItem.Name = "hastaYönetimiToolStripMenuItem";
            this.hastaYönetimiToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.hastaYönetimiToolStripMenuItem.Text = "Hasta Yönetimi";
            
            // hastaEkleToolStripMenuItem
            this.hastaEkleToolStripMenuItem.Name = "hastaEkleToolStripMenuItem";
            this.hastaEkleToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.hastaEkleToolStripMenuItem.Text = "Hasta Ekle";
            this.hastaEkleToolStripMenuItem.Click += new System.EventHandler(this.HastaEkleToolStripMenuItem_Click);
            
            // hastaListesiToolStripMenuItem
            this.hastaListesiToolStripMenuItem.Name = "hastaListesiToolStripMenuItem";
            this.hastaListesiToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.hastaListesiToolStripMenuItem.Text = "Hasta Listesi";
            this.hastaListesiToolStripMenuItem.Click += new System.EventHandler(this.HastaListesiToolStripMenuItem_Click);
            
            // randevuYönetimiToolStripMenuItem
            this.randevuYönetimiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randevuEkleToolStripMenuItem,
            this.randevuListesiToolStripMenuItem});
            this.randevuYönetimiToolStripMenuItem.Name = "randevuYönetimiToolStripMenuItem";
            this.randevuYönetimiToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.randevuYönetimiToolStripMenuItem.Text = "Randevu Yönetimi";
            
            // randevuEkleToolStripMenuItem
            this.randevuEkleToolStripMenuItem.Name = "randevuEkleToolStripMenuItem";
            this.randevuEkleToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.randevuEkleToolStripMenuItem.Text = "Randevu Ekle";
            this.randevuEkleToolStripMenuItem.Click += new System.EventHandler(this.RandevuEkleToolStripMenuItem_Click);
            
            // randevuListesiToolStripMenuItem
            this.randevuListesiToolStripMenuItem.Name = "randevuListesiToolStripMenuItem";
            this.randevuListesiToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.randevuListesiToolStripMenuItem.Text = "Randevu Listesi";
            this.randevuListesiToolStripMenuItem.Click += new System.EventHandler(this.RandevuListesiToolStripMenuItem_Click);
            
            // kullanýcýYönetimiToolStripMenuItem
            this.kullanýcýYönetimiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullanýcýEkleToolStripMenuItem,
            this.kullanýcýListesiToolStripMenuItem});
            this.kullanýcýYönetimiToolStripMenuItem.Name = "kullanýcýYönetimiToolStripMenuItem";
            this.kullanýcýYönetimiToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.kullanýcýYönetimiToolStripMenuItem.Text = "Kullanýcý Yönetimi";
            
            // kullanýcýEkleToolStripMenuItem
            this.kullanýcýEkleToolStripMenuItem.Name = "kullanýcýEkleToolStripMenuItem";
            this.kullanýcýEkleToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.kullanýcýEkleToolStripMenuItem.Text = "Kullanýcý Ekle";
            this.kullanýcýEkleToolStripMenuItem.Click += new System.EventHandler(this.KullanýcýEkleToolStripMenuItem_Click);
            
            // kullanýcýListesiToolStripMenuItem
            this.kullanýcýListesiToolStripMenuItem.Name = "kullanýcýListesiToolStripMenuItem";
            this.kullanýcýListesiToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.kullanýcýListesiToolStripMenuItem.Text = "Kullanýcý Listesi";
            this.kullanýcýListesiToolStripMenuItem.Click += new System.EventHandler(this.KullanýcýListesiToolStripMenuItem_Click);
            
            // raporlarToolStripMenuItem
            this.raporlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.günlükRandevularToolStripMenuItem});
            this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
            this.raporlarToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.raporlarToolStripMenuItem.Text = "Raporlar";
            
            // günlükRandevularToolStripMenuItem
            this.günlükRandevularToolStripMenuItem.Name = "günlükRandevularToolStripMenuItem";
            this.günlükRandevularToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.günlükRandevularToolStripMenuItem.Text = "Günlük Randevular";
            
            // ayarlarToolStripMenuItem
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilimToolStripMenuItem,
            this.þifreDeðiþtirToolStripMenuItem,
            this.çýkýþToolStripMenuItem});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            
            // profilimToolStripMenuItem
            this.profilimToolStripMenuItem.Name = "profilimToolStripMenuItem";
            this.profilimToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.profilimToolStripMenuItem.Text = "Profilim";
            this.profilimToolStripMenuItem.Click += new System.EventHandler(this.ProfilimToolStripMenuItem_Click);
            
            // þifreDeðiþtirToolStripMenuItem
            this.þifreDeðiþtirToolStripMenuItem.Name = "þifreDeðiþtirToolStripMenuItem";
            this.þifreDeðiþtirToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.þifreDeðiþtirToolStripMenuItem.Text = "Þifre Deðiþtir";
            this.þifreDeðiþtirToolStripMenuItem.Click += new System.EventHandler(this.ÞifreDeðiþtirToolStripMenuItem_Click);
            
            // çýkýþToolStripMenuItem
            this.çýkýþToolStripMenuItem.Name = "çýkýþToolStripMenuItem";
            this.çýkýþToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.çýkýþToolStripMenuItem.Text = "Çýkýþ";
            this.çýkýþToolStripMenuItem.Click += new System.EventHandler(this.ÇýkýþToolStripMenuItem_Click);
            
            // statusStrip1
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 674);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            
            // toolStripStatusLabel1
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(111, 20);
            this.toolStripStatusLabel1.Text = "Kullanýcý: Admin";
            
            // toolStripStatusLabel2
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabel2.Text = "Tarih:";
            
            // panel1
            this.panel1.BackColor = Color.White;
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.lblQuickActions);
            this.panel1.Controls.Add(this.btnQuickPatientAdd);
            this.panel1.Controls.Add(this.btnQuickAppointment);
            this.panel1.Controls.Add(this.btnQuickPatientList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 646);
            this.panel1.TabIndex = 2;
            
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(0, 120, 180);
            this.lblWelcome.Location = new System.Drawing.Point(300, 100);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(600, 37);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Hastane Otomasyon Sistemine Hoþgeldiniz";
            
            // lblDateTime
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblDateTime.ForeColor = Color.Gray;
            this.lblDateTime.Location = new System.Drawing.Point(500, 150);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(200, 24);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "Tarih ve Saat";
            
            // lblQuickActions
            this.lblQuickActions.AutoSize = true;
            this.lblQuickActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblQuickActions.ForeColor = Color.FromArgb(0, 120, 180);
            this.lblQuickActions.Location = new System.Drawing.Point(500, 230);
            this.lblQuickActions.Name = "lblQuickActions";
            this.lblQuickActions.Size = new System.Drawing.Size(200, 26);
            this.lblQuickActions.TabIndex = 2;
            this.lblQuickActions.Text = "Hýzlý Ýþlemler";
            
            // btnQuickPatientAdd
            this.btnQuickPatientAdd.BackColor = Color.FromArgb(40, 167, 69);
            this.btnQuickPatientAdd.FlatStyle = FlatStyle.Flat;
            this.btnQuickPatientAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnQuickPatientAdd.ForeColor = Color.White;
            this.btnQuickPatientAdd.Location = new System.Drawing.Point(350, 300);
            this.btnQuickPatientAdd.Name = "btnQuickPatientAdd";
            this.btnQuickPatientAdd.Size = new System.Drawing.Size(150, 50);
            this.btnQuickPatientAdd.TabIndex = 3;
            this.btnQuickPatientAdd.Text = "Hasta Ekle";
            this.btnQuickPatientAdd.UseVisualStyleBackColor = false;
            this.btnQuickPatientAdd.Click += new System.EventHandler(this.HastaEkleToolStripMenuItem_Click);
            
            // btnQuickAppointment
            this.btnQuickAppointment.BackColor = Color.FromArgb(0, 123, 255);
            this.btnQuickAppointment.FlatStyle = FlatStyle.Flat;
            this.btnQuickAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnQuickAppointment.ForeColor = Color.White;
            this.btnQuickAppointment.Location = new System.Drawing.Point(525, 300);
            this.btnQuickAppointment.Name = "btnQuickAppointment";
            this.btnQuickAppointment.Size = new System.Drawing.Size(150, 50);
            this.btnQuickAppointment.TabIndex = 4;
            this.btnQuickAppointment.Text = "Randevu Ekle";
            this.btnQuickAppointment.UseVisualStyleBackColor = false;
            this.btnQuickAppointment.Click += new System.EventHandler(this.RandevuEkleToolStripMenuItem_Click);
            
            // btnQuickPatientList
            this.btnQuickPatientList.BackColor = Color.FromArgb(108, 117, 125);
            this.btnQuickPatientList.FlatStyle = FlatStyle.Flat;
            this.btnQuickPatientList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnQuickPatientList.ForeColor = Color.White;
            this.btnQuickPatientList.Location = new System.Drawing.Point(700, 300);
            this.btnQuickPatientList.Name = "btnQuickPatientList";
            this.btnQuickPatientList.Size = new System.Drawing.Size(150, 50);
            this.btnQuickPatientList.TabIndex = 5;
            this.btnQuickPatientList.Text = "Hasta Listesi";
            this.btnQuickPatientList.UseVisualStyleBackColor = false;
            this.btnQuickPatientList.Click += new System.EventHandler(this.HastaListesiToolStripMenuItem_Click);
            
            // timer1
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void UpdateUIBasedOnUserRole()
        {
            if (!SessionManager.IsStaffLogin)
                return;

            // Kullanýcý rolüne göre menü öðelerini göster/gizle
            kullanýcýYönetimiToolStripMenuItem.Visible = SessionManager.IsAdmin;
            
            // Hemþire ve diðer roller için ek menü öðeleri göster
            if (SessionManager.CurrentUser.Role == UserRole.Nurse)
            {
                // Hemþire menüsünü ekle
                AddNurseMenu();
            }
        }

        private void AddNurseMenu()
        {
            var nurseMenu = new ToolStripMenuItem("Hemþire Ýþlemleri");
            
            var vitalSigns = new ToolStripMenuItem("Vital Bulgular");
            vitalSigns.Click += VitalSigns_Click;
            
            var medicationAdmin = new ToolStripMenuItem("Ýlaç Takibi");
            medicationAdmin.Click += MedicationAdmin_Click;
            
            var nursingNotes = new ToolStripMenuItem("Hemþire Notlarý");
            nursingNotes.Click += NursingNotes_Click;
            
            nurseMenu.DropDownItems.AddRange(new ToolStripItem[] { vitalSigns, medicationAdmin, nursingNotes });
            menuStrip1.Items.Insert(menuStrip1.Items.Count - 1, nurseMenu);
        }

        private void VitalSigns_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.NurseManagement.VitalSignsForm());
        }

        private void MedicationAdmin_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.NurseManagement.MedicationTrackingForm());
        }

        private void NursingNotes_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.NurseManagement.NursingNotesForm());
        }

        // MDI form açma metodunu iyileþtir
        private void OpenMdiForm(Form form)
        {
            try
            {
                // Ayný tipte form zaten açýksa onu öne getir
                foreach (Form child in this.MdiChildren)
                {
                    if (child.GetType() == form.GetType())
                    {
                        child.Activate();
                        child.BringToFront();
                        child.Focus();
                        child.WindowState = FormWindowState.Maximized;
                        form.Dispose();
                        return;
                    }
                }

                // Ana paneli gizle
                panel1.Visible = false;

                // Yeni form aç
                form.MdiParent = this;
                form.Show();
                form.WindowState = FormWindowState.Maximized;
                form.BringToFront();
                form.Activate();
                form.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form açýlýrken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form?.Dispose();
            }
        }

        private void UpdateWelcomeMessage()
        {
            if (SessionManager.IsStaffLogin)
            {
                lblWelcome.Text = $"Hoþ Geldiniz, {SessionManager.CurrentUser.FirstName} {SessionManager.CurrentUser.LastName}";
                toolStripStatusLabel1.Text = $"{SessionManager.GetUserType()}: {SessionManager.CurrentUser.FullName}";
            }

            lblDateTime.Text = DateTime.Now.ToString("dd MMMM yyyy, dddd");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        // Hasta Yönetimi Menu Events
        private void HastaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientAddForm());
        }

        private void HastaListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientListForm());
        }

        // Randevu Yönetimi Menu Events
        private void RandevuEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new AppointmentAddForm());
        }

        private void RandevuListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new AppointmentListForm());
        }

        // Kullanýcý Yönetimi Menu Events
        private void KullanýcýEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Bu iþlem için yetkiniz bulunmamaktadýr!", "Yetki Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenMdiForm(new UserAddForm());
        }

        private void KullanýcýListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Bu iþlem için yetkiniz bulunmamaktadýr!", "Yetki Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenMdiForm(new UserListForm());
        }

        // Raporlar Menu Events
        private void GünlükRandevularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Reports.DailyAppointmentsReportForm());
        }

        // Ayarlar Menu Events
        private void ProfilimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.Authentication.UserProfileForm());
        }

        private void ÞifreDeðiþtirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var changePasswordForm = new ChangePasswordForm();
            changePasswordForm.ShowDialog();
        }

        private void ÇýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Çýkýþ yapmak istediðinizden emin misiniz?", "Çýkýþ Onayý", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                timer1?.Stop();
                timer1?.Dispose();
                
                // Form kapatýlacak, FormClosed event'inde login formu açýlacak
                this.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1?.Stop();
            timer1?.Dispose();
            
            // ShowDialog() kullanýldýðý için, form kapandýðýnda LoginForm otomatik olarak görünecek
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Çýkýþ onayý sadece X butonuna basýldýðýnda sor
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Çýkýþ yapmak istediðinizden emin misiniz?", "Çýkýþ Onayý",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            
            // Çýkýþ kabul edildi
            SessionManager.Logout();
            timer1?.Stop();
            timer1?.Dispose();
        }

        // MDI child form kapatýldýðýnda welcome panel'i tekrar göster
        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                // Hiçbir child form yoksa welcome panel'i göster
                panel1.Visible = true;
            }
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hastaYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastaEkleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hastaListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevuYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevuEkleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randevuListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanýcýYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanýcýEkleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanýcýListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raporlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem günlükRandevularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem þifreDeðiþtirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çýkýþToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblQuickActions;
        private System.Windows.Forms.Button btnQuickPatientAdd;
        private System.Windows.Forms.Button btnQuickAppointment;
        private System.Windows.Forms.Button btnQuickPatientList;
        private System.Windows.Forms.Timer timer1;
    }
}