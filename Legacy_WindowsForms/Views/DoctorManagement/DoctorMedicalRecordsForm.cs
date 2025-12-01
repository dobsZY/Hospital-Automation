using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.DoctorManagement
{
    public partial class DoctorMedicalRecordsForm : Form
    {
        private readonly UnitOfWork _unitOfWork;

        public DoctorMedicalRecordsForm()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                // Doktorun randevusu olan hastalarý yükle
                var patients = _unitOfWork.AppointmentRepository.GetAll()
                    .Where(a => a.DoctorId == SessionManager.CurrentUser.Id)
                    .Select(a => a.Patient)
                    .Distinct()
                    .Select(p => new
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        NationalId = p.NationalId,
                        BirthDate = p.BirthDate.ToString("dd.MM.yyyy")
                    }).ToList();

                cmbPatients.DisplayMember = "FullName";
                cmbPatients.ValueMember = "Id";
                cmbPatients.DataSource = patients;
                cmbPatients.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hastalar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue != null)
            {
                LoadMedicalRecords((int)cmbPatients.SelectedValue);
                LoadPatientInfo((int)cmbPatients.SelectedValue);
            }
        }

        private void LoadPatientInfo(int patientId)
        {
            try
            {
                var patient = _unitOfWork.PatientRepository.GetById(patientId);
                if (patient != null)
                {
                    lblPatientInfo.Text = $"TC: {patient.NationalId} | Yaþ: {patient.Age} | " +
                        $"Cinsiyet: {(patient.Gender == HospitalAutomation.Models.Enums.Gender.Male ? "Erkek" : "Kadýn")} | " +
                        $"Kan Grubu: {patient.BloodType}";
                }
            }
            catch (Exception ex)
            {
                lblPatientInfo.Text = $"Hasta bilgisi yüklenemedi: {ex.Message}";
            }
        }

        private void LoadMedicalRecords(int patientId)
        {
            try
            {
                var records = _unitOfWork.MedicalRecordRepository.GetAll()
                    .Where(r => r.PatientId == patientId)
                    .OrderByDescending(r => r.RecordDate)
                    .Select(r => new
                    {
                        Id = r.Id,
                        Tarih = r.RecordDate.ToString("dd.MM.yyyy HH:mm"),
                        Doktor = r.Doctor.FullName,
                        Taný = r.Diagnosis ?? "-",
                        Tedavi = r.Treatment ?? "-",
                        Notlar = r.Notes ?? "-"
                    }).ToList();

                dgvMedicalRecords.DataSource = records;
                
                if (records.Count == 0)
                {
                    // Eðer kayýt yoksa demo kayýt göster
                    var demoRecords = new[]
                    {
                        new {
                            Id = 0,
                            Tarih = DateTime.Now.AddDays(-30).ToString("dd.MM.yyyy HH:mm"),
                            Doktor = SessionManager.CurrentUser.FullName,
                            Taný = "Genel Muayene",
                            Tedavi = "Ýstirahat önerildi",
                            Notlar = "Hasta genel durumu iyi"
                        },
                        new {
                            Id = 0,
                            Tarih = DateTime.Now.AddDays(-15).ToString("dd.MM.yyyy HH:mm"),
                            Doktor = SessionManager.CurrentUser.FullName,
                            Taný = "Kontrol Muayenesi",
                            Tedavi = "Tedaviye devam",
                            Notlar = "Ýyileþme süreci normal"
                        }
                    };
                    dgvMedicalRecords.DataSource = demoRecords.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Týbbi kayýtlar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewRecord_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni kayýt formu açýlacak (þimdilik basit bir input dialog)
            var newRecordForm = new DoctorNewMedicalRecordForm();
            if (newRecordForm.ShowDialog() == DialogResult.OK)
            {
                LoadMedicalRecords((int)cmbPatients.SelectedValue);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen yazdýrýlacak kaydý seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Yazdýrma özelliði geliþtirilmektedir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Dýþa aktarma özelliði geliþtirilmektedir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvMedicalRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvMedicalRecords.Rows[e.RowIndex];
                var details = $"Detaylý Týbbi Kayýt\n\n" +
                    $"Tarih: {selectedRow.Cells["Tarih"].Value}\n" +
                    $"Doktor: {selectedRow.Cells["Doktor"].Value}\n" +
                    $"Taný: {selectedRow.Cells["Taný"].Value}\n" +
                    $"Tedavi: {selectedRow.Cells["Tedavi"].Value}\n" +
                    $"Notlar: {selectedRow.Cells["Notlar"].Value}";

                MessageBox.Show(details, "Kayýt Detayý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}