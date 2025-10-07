using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalAutomation.Views.PatientPortal
{
    public partial class PatientMedicalRecordsForm : Form
    {
        public PatientMedicalRecordsForm()
        {
            InitializeComponent();
        }

        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}