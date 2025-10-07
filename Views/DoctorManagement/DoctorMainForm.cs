using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Utilities;
using HospitalAutomation.Views.DoctorManagement;

namespace HospitalAutomation.Views.DoctorManagement
{
    public partial class DoctorMainForm : Form
    {
        private Timer _timer;

        public DoctorMainForm()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateWelcomeMessage();
        }

        private void InitializeTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // 1 saniye
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void UpdateWelcomeMessage()
        {
            if (SessionManager.IsStaffLogin)
            {
                lblWelcome.Text = $"Hoþ Geldiniz, Dr. {SessionManager.CurrentUser.FirstName} {SessionManager.CurrentUser.LastName}";
                toolStripStatusLabel1.Text = $"Doktor: {SessionManager.CurrentUser.FullName}";
            }
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

                // Yeni form aç
                form.MdiParent = this;
                form.Show();
                form.WindowState = FormWindowState.Maximized;
                form.BringToFront();
                form.Activate();
                form.Focus();
                
                // Ana formdaki welcome panel'i gizle
                if (this.Controls["panel1"] != null)
                {
                    this.Controls["panel1"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form açýlýrken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form?.Dispose();
            }
        }

        private void RandevularýmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new DoctorAppointmentsForm());
        }

        private void HastalarýmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new DoctorPatientsForm());
        }

        private void TýbbiKayýtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new DoctorMedicalRecordsForm());
        }

        private void YeniKayýtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new DoctorNewMedicalRecordForm());
        }

        private void ProfilimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new DoctorProfileForm());
        }

        private void ÇýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Çýkýþ yapmak istediðinizden emin misiniz?", "Çýkýþ Onayý", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                _timer?.Stop();
                _timer?.Dispose();
                
                // Form kapatýlacak, FormClosed event'inde login formu açýlacak
                this.Close();
            }
        }

        private void BtnTodayAppointments_Click(object sender, EventArgs e)
        {
            RandevularýmToolStripMenuItem_Click(sender, e);
        }

        private void BtnMyPatients_Click(object sender, EventArgs e)
        {
            HastalarýmToolStripMenuItem_Click(sender, e);
        }

        private void BtnNewMedicalRecord_Click(object sender, EventArgs e)
        {
            YeniKayýtToolStripMenuItem_Click(sender, e);
        }

        private void DoctorMainForm_FormClosing(object sender, FormClosingEventArgs e)
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
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void DoctorMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            
            // ShowDialog() kullanýldýðý için, form kapandýðýnda LoginForm otomatik olarak görünecek
        }

        // MDI child form kapatýldýðýnda welcome panel'i tekrar göster
        private void DoctorMainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                // Hiçbir child form yoksa welcome panel'i göster
                if (this.Controls["panel1"] != null)
                {
                    this.Controls["panel1"].Visible = true;
                }
            }
        }
    }
}