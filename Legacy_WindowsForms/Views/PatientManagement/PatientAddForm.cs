using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.PatientManagement
{
    public partial class PatientAddForm : Form
    {
        private IPatientService _patientService;

        public PatientAddForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeComboBoxes();
        }

        private void InitializeServices()
        {
            var context = new HospitalDbContext();
            var unitOfWork = new UnitOfWork(context);
            _patientService = new PatientService(unitOfWork);
        }

        private void InitializeComboBoxes()
        {
            cmbGender.DataSource = Enum.GetValues(typeof(Gender));
            cmbBloodType.DataSource = Enum.GetValues(typeof(BloodType));
            cmbBloodType.SelectedIndex = -1;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var patient = new Patient
                {
                    NationalId = txtNationalId.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    BirthDate = dtpBirthDate.Value,
                    Gender = (Gender)cmbGender.SelectedItem,
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    BloodType = cmbBloodType.SelectedIndex == -1 ? (BloodType?)null : (BloodType)cmbBloodType.SelectedItem,
                    EmergencyContactName = txtEmergencyContactName.Text.Trim(),
                    EmergencyContactPhone = txtEmergencyContactPhone.Text.Trim(),
                    MedicalHistory = txtMedicalHistory.Text.Trim(),
                    Allergies = txtAllergies.Text.Trim()
                };

                if (_patientService.CreatePatient(patient))
                {
                    MessageBox.Show("Hasta baþarýyla kaydedildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtNationalId.Text))
            {
                MessageBox.Show("TC Kimlik No boþ olamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidTCKimlikNo(txtNationalId.Text))
            {
                MessageBox.Show("Geçersiz TC Kimlik No!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Ad boþ olamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Soyad boþ olamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Cinsiyet seçimi yapýnýz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !ValidationHelper.IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Geçersiz telefon numarasý!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Geçersiz email adresi!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PatientAddForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiðinde yapýlacak iþlemler
        }
    }
}