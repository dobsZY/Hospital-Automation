using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Utilities;
using HospitalAutomation.Views.PatientPortal;

namespace HospitalAutomation.Views.PatientPortal
{
    public partial class PatientMainForm : Form
    {
        private Timer _timer;
        private Panel _welcomePanel;

        public PatientMainForm()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateWelcomeMessage();
            FindWelcomePanel();
        }

        private void FindWelcomePanel()
        {
            // Welcome panel'i bul ve referansýný sakla
            foreach (Control control in this.Controls)
            {
                if (control is Panel && control.Dock == DockStyle.Fill)
                {
                    _welcomePanel = control as Panel;
                    break;
                }
            }
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
            if (SessionManager.IsPatientLogin)
            {
                lblWelcome.Text = $"Hoþ Geldiniz, {SessionManager.CurrentPatient.FirstName} {SessionManager.CurrentPatient.LastName}";
                toolStripStatusLabel1.Text = $"Hasta: {SessionManager.CurrentPatient.FullName}";
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

                // Welcome panel'i gizle
                if (_welcomePanel != null)
                {
                    _welcomePanel.Visible = false;
                }

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

        private void RandevularýmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientAppointmentsForm());
        }

        private void YeniRandevuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientNewAppointmentForm());
        }

        private void TýbbiKayýtlarýmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientMedicalRecordsForm());
        }

        private void BilgileriniGüncelle_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new PatientProfileForm());
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

        private void BtnMyAppointments_Click(object sender, EventArgs e)
        {
            RandevularýmToolStripMenuItem_Click(sender, e);
        }

        private void BtnNewAppointment_Click(object sender, EventArgs e)
        {
            YeniRandevuToolStripMenuItem_Click(sender, e);
        }

        private void BtnMedicalRecords_Click(object sender, EventArgs e)
        {
            TýbbiKayýtlarýmToolStripMenuItem_Click(sender, e);
        }

        private void PatientMainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void PatientMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            
            // ShowDialog() kullanýldýðý için, form kapandýðýnda LoginForm otomatik olarak görünecek
        }

        // MDI child form kapatýldýðýnda welcome panel'i tekrar göster
        private void PatientMainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                // Hiçbir child form yoksa welcome panel'i göster
                if (_welcomePanel != null)
                {
                    _welcomePanel.Visible = true;
                }
            }
        }
    }
}