using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalAutomation.Views.DoctorManagement
{
    public partial class DoctorNewMedicalRecordForm : Form
    {
        public DoctorNewMedicalRecordForm()
        {
            InitializeComponent();
        }

        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}