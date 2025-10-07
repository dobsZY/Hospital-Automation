using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalAutomation.Utilities;
using HospitalAutomation.Views.NurseManagement;

namespace HospitalAutomation.Views.NurseManagement
{
    public partial class NurseMainForm : Form
    {
        private Timer _timer;

        public NurseMainForm()
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void UpdateWelcomeMessage()
        {
            if (SessionManager.IsStaffLogin)
            {
                lblWelcome.Text = $"Hoþ Geldiniz, Hemþire {SessionManager.CurrentUser.FirstName} {SessionManager.CurrentUser.LastName}";
                toolStripStatusLabel1.Text = $"Hemþire: {SessionManager.CurrentUser.FullName}";
            }

            lblDateTime.Text = DateTime.Now.ToString("dd MMMM yyyy, dddd");
        }

        // MDI form açma metodu
        private void OpenMdiForm(Form form)
        {
            // Ayný tipte form zaten açýksa onu öne getir
            foreach (Form child in this.MdiChildren)
            {
                if (child.GetType() == form.GetType())
                {
                    child.Activate();
                    child.WindowState = FormWindowState.Maximized;
                    form.Dispose();
                    return;
                }
            }

            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            form.BringToFront();
        }

        // Menu Events
        private void VitalBulgularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new VitalSignsForm());
        }

        private void IlacTakibiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new MedicationTrackingForm());
        }

        private void HemþireNotlarýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new NursingNotesForm());
        }

        private void HastalarýmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.PatientManagement.PatientListForm());
        }

        private void GünlükRandevularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.Reports.DailyAppointmentsReportForm());
        }

        private void ProfilimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMdiForm(new Views.Authentication.UserProfileForm());
        }

        private void ÞifreDeðiþtirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var changePasswordForm = new Views.Authentication.ChangePasswordForm();
            changePasswordForm.ShowDialog();
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

        private void NurseMainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void NurseMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            
            // ShowDialog() kullanýldýðý için, form kapandýðýnda LoginForm otomatik olarak görünecek
        }

        // Button Events
        private void BtnVitalSigns_Click(object sender, EventArgs e)
        {
            VitalBulgularToolStripMenuItem_Click(sender, e);
        }

        private void BtnMedicationTracking_Click(object sender, EventArgs e)
        {
            IlacTakibiToolStripMenuItem_Click(sender, e);
        }

        private void BtnNursingNotes_Click(object sender, EventArgs e)
        {
            HemþireNotlarýToolStripMenuItem_Click(sender, e);
        }

        private void BtnPatientList_Click(object sender, EventArgs e)
        {
            HastalarýmToolStripMenuItem_Click(sender, e);
        }
    }
}