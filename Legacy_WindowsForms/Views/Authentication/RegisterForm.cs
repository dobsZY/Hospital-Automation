using System;
using System.Windows.Forms;

namespace HospitalAutomation.Views.Authentication
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void BtnRegisterPatient_Click(object sender, EventArgs e)
        {
            // Minimal implementation
            MessageBox.Show("Register Patient functionality not implemented yet.");
        }

        private void BtnRegisterDoctor_Click(object sender, EventArgs e)
        {
            // Minimal implementation
            MessageBox.Show("Register Doctor functionality not implemented yet.");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}