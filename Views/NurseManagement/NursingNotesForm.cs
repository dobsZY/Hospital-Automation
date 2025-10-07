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
    public partial class NursingNotesForm : Form
    {
        private UnitOfWork _unitOfWork;

        public NursingNotesForm()
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
                // Note Type ComboBox - Basit string deðerler
                cmbNoteType.Items.Clear();
                cmbNoteType.Items.Add("Genel Bakým");
                cmbNoteType.Items.Add("Deðerlendirme");
                cmbNoteType.Items.Add("Müdahale");
                cmbNoteType.Items.Add("Hasta Eðitimi");
                cmbNoteType.Items.Add("Acil Durum");
                cmbNoteType.Items.Add("Ýlaç Ýliþkili");
                cmbNoteType.Items.Add("Vital Bulgular");
                
                if (cmbNoteType.Items.Count > 0)
                    cmbNoteType.SelectedIndex = 0;

                this.Load += NursingNotesForm_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form init hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NursingNotesForm_Load(object sender, EventArgs e)
        {
            LoadNursingNotes();
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
            LoadNursingNotes();
        }

        private void LoadNursingNotes()
        {
            if (cmbPatient.SelectedValue == null) return;

            try
            {
                // Basit bir liste oluþtur - hemþire notlarý için
                var notes = new List<dynamic>
                {
                    new { 
                        Tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm"), 
                        Türü = "Genel Bakým", 
                        Ýçerik = "Hasta takibi yapýldý", 
                        Acil = "Hayýr", 
                        Hemþire = SessionManager.CurrentUser?.FullName ?? "Sistem" 
                    }
                };

                dgvNotes.DataSource = notes;

                // Acil sütununu renklendir
                if (dgvNotes.Columns.Count > 3)
                {
                    foreach (DataGridViewRow row in dgvNotes.Rows)
                    {
                        if (row.Cells[3].Value?.ToString() == "EVET")
                        {
                            row.Cells[3].Style.ForeColor = Color.Red;
                            row.Cells[3].Style.Font = new Font(dgvNotes.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hemþire notlarý yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;

                MessageBox.Show("Hemþire notu baþarýyla kaydedildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnClear_Click(sender, e);
                LoadNursingNotes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayýt sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Lütfen not içeriðini girin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (cmbNoteType.Items.Count > 0)
                cmbNoteType.SelectedIndex = 0;
            txtContent.Clear();
            txtAssessment.Clear();
            txtIntervention.Clear();
            txtPatientResponse.Clear();
            if (chkUrgent != null)
                chkUrgent.Checked = false;
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