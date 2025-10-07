using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.NurseManagement
{
    public partial class VitalSignsForm : Form
    {
        private UnitOfWork _unitOfWork;

        public VitalSignsForm()
        {
            InitializeComponent();
            try
            {
                var context = new HospitalDbContext();
                _unitOfWork = new UnitOfWork(context);
                LoadPatients();
                InitializeForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form baþlatýlýrken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeForm()
        {
            try
            {
                this.Load += VitalSignsForm_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form init hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VitalSignsForm_Load(object sender, EventArgs e)
        {
            LoadVitalSigns();
        }

        private void LoadPatients()
        {
            try
            {
                if (_unitOfWork?.PatientRepository == null) return;
                
                var patients = _unitOfWork.PatientRepository.GetAll().Where(p => p.IsActive).ToList();
                cmbPatient.DataSource = patients;
                cmbPatient.DisplayMember = "FullName";
                cmbPatient.ValueMember = "PatientId";
                cmbPatient.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hastalar yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVitalSigns();
        }

        private void LoadVitalSigns()
        {
            if (cmbPatient.SelectedValue == null || _unitOfWork?.VitalSignsRepository == null) return;

            try
            {
                var patientId = (int)cmbPatient.SelectedValue;
                var vitalSigns = _unitOfWork.VitalSignsRepository.GetAll()
                    .Where(v => v.PatientId == patientId)
                    .OrderByDescending(v => v.MeasurementDateTime)
                    .Select(v => new
                    {
                        Tarih = v.MeasurementDateTime.ToString("dd.MM.yyyy HH:mm"),
                        Tansiyon = v.BloodPressure,
                        Nabýz = v.HeartRate,
                        Ateþ = v.Temperature,
                        Solunum = v.RespiratoryRate,
                        O2 = v.OxygenSaturation,
                        Kilo = v.Weight,
                        Boy = v.Height,
                        Hemþire = v.Nurse.FullName
                    }).ToList();

                dgvVitalSigns.DataSource = vitalSigns;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vital bulgular yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPatient.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SessionManager.IsStaffLogin)
                {
                    MessageBox.Show("Giriþ yapýlmamýþ!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var vitalSigns = new VitalSigns
                {
                    PatientId = (int)cmbPatient.SelectedValue,
                    NurseId = SessionManager.CurrentUser.Id, // UserId yerine Id kullanýldý
                    MeasurementDateTime = DateTime.Now,
                    BloodPressureSystolic = ParseDouble(txtSystolic.Text),
                    BloodPressureDiastolic = ParseDouble(txtDiastolic.Text),
                    HeartRate = ParseDouble(txtHeartRate.Text),
                    Temperature = ParseDouble(txtTemperature.Text),
                    RespiratoryRate = ParseDouble(txtRespiratoryRate.Text),
                    OxygenSaturation = ParseDouble(txtOxygenSaturation.Text),
                    Weight = ParseDouble(txtWeight.Text),
                    Height = ParseDouble(txtHeight.Text),
                    Notes = txtNotes.Text
                };

                _unitOfWork.VitalSignsRepository.Add(vitalSigns);
                _unitOfWork.Save();

                MessageBox.Show("Vital bulgular baþarýyla kaydedildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnClear_Click(sender, e);
                LoadVitalSigns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayýt sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double? ParseDouble(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (double.TryParse(value, out double result))
                return result;

            return null;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSystolic.Clear();
            txtDiastolic.Clear();
            txtHeartRate.Clear();
            txtTemperature.Clear();
            txtRespiratoryRate.Clear();
            txtOxygenSaturation.Clear();
            txtWeight.Clear();
            txtHeight.Clear();
            txtNotes.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}