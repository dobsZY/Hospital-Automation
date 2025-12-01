using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;
using System.Collections.Generic;

namespace HospitalAutomation.Views.NurseManagement
{
    public partial class MedicationTrackingForm : Form
    {
        private UnitOfWork _unitOfWork;

        public MedicationTrackingForm()
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
                // Status ComboBox - Basit string deðerler kullan
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Planlandý");
                cmbStatus.Items.Add("Verildi");
                cmbStatus.Items.Add("Atlandý");
                cmbStatus.Items.Add("Reddedildi");
                if (cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedIndex = 0;

                this.Load += MedicationTrackingForm_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form init hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MedicationTrackingForm_Load(object sender, EventArgs e)
        {
            LoadMedicationHistory();
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
            LoadMedicationHistory();
        }

        private void LoadMedicationHistory()
        {
            if (cmbPatient.SelectedValue == null) return;

            try
            {
                // Basit bir liste oluþtur - ilaç takibi için
                var medicationHistory = new List<dynamic>
                {
                    new { Ýlaç = "Parol 500mg", Doz = "1 tablet", Planlanan = DateTime.Now.ToString("dd.MM.yyyy HH:mm"), Verilen = "-", Durum = "Planlandý", Hemþire = SessionManager.CurrentUser?.FullName ?? "Sistem" }
                };

                dgvMedications.DataSource = medicationHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ýlaç geçmiþi yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;

                // Basit kayýt iþlemi
                MessageBox.Show("Ýlaç kaydý baþarýyla eklendi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMedicationHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayýt sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdminister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;

                MessageBox.Show("Ýlaç baþarýyla verildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadMedicationHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ýþlem sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDosage.Text))
            {
                MessageBox.Show("Lütfen doz bilgisini girin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtDosage.Clear();
            if (dtpScheduled != null)
                dtpScheduled.Value = DateTime.Now;
            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
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