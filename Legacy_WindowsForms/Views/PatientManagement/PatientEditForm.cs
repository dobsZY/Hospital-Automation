using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.PatientManagement
{
    public partial class PatientEditForm : Form
    {
        private IPatientService _patientService;
        private Patient _patient;
        private int _patientId;

        public PatientEditForm(int patientId)
        {
            _patientId = patientId;
            InitializeComponent();
            InitializeServices();
            InitializeComboBoxes();
            LoadPatient();
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
        }

        private void LoadPatient()
        {
            try
            {
                _patient = _patientService.GetPatientById(_patientId);
                if (_patient != null)
                {
                    txtNationalId.Text = _patient.NationalId;
                    txtFirstName.Text = _patient.FirstName;
                    txtLastName.Text = _patient.LastName;
                    dtpBirthDate.Value = _patient.BirthDate;
                    cmbGender.SelectedItem = _patient.Gender;
                    txtPhone.Text = _patient.Phone ?? "";
                    txtEmail.Text = _patient.Email ?? "";
                    txtAddress.Text = _patient.Address ?? "";
                    txtEmergencyContactName.Text = _patient.EmergencyContactName ?? "";
                    txtEmergencyContactPhone.Text = _patient.EmergencyContactPhone ?? "";
                    txtMedicalHistory.Text = _patient.MedicalHistory ?? "";
                    txtAllergies.Text = _patient.Allergies ?? "";

                    if (_patient.BloodType.HasValue)
                        cmbBloodType.SelectedItem = _patient.BloodType.Value;
                    else
                        cmbBloodType.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Hasta bulunamadý!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hasta bilgileri yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                _patient.NationalId = txtNationalId.Text.Trim();
                _patient.FirstName = txtFirstName.Text.Trim();
                _patient.LastName = txtLastName.Text.Trim();
                _patient.BirthDate = dtpBirthDate.Value;
                _patient.Gender = (Gender)cmbGender.SelectedItem;
                _patient.Phone = txtPhone.Text.Trim();
                _patient.Email = txtEmail.Text.Trim();
                _patient.Address = txtAddress.Text.Trim();
                _patient.BloodType = cmbBloodType.SelectedIndex == -1 ? (BloodType?)null : (BloodType)cmbBloodType.SelectedItem;
                _patient.EmergencyContactName = txtEmergencyContactName.Text.Trim();
                _patient.EmergencyContactPhone = txtEmergencyContactPhone.Text.Trim();
                _patient.MedicalHistory = txtMedicalHistory.Text.Trim();
                _patient.Allergies = txtAllergies.Text.Trim();

                if (_patientService.UpdatePatient(_patient))
                {
                    MessageBox.Show("Hasta bilgileri baþarýyla güncellendi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void PatientEditForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiðinde yapýlacak iþlemler
        }
    }
}