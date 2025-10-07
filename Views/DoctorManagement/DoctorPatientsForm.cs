using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.DoctorManagement
{
    public partial class DoctorPatientsForm : Form
    {
        private readonly UnitOfWork _unitOfWork;

        public DoctorPatientsForm()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                // Doktorun randevusu olan hastalarý getir
                var patients = _unitOfWork.AppointmentRepository.GetAll()
                    .Where(a => a.DoctorId == SessionManager.CurrentUser.Id)
                    .Select(a => a.Patient)
                    .Distinct()
                    .Select(p => new
                    {
                        Id = p.Id,
                        TC = p.NationalId,
                        Ad = p.FirstName,
                        Soyad = p.LastName,
                        DogumTarihi = p.BirthDate.ToString("dd.MM.yyyy"),
                        Telefon = p.Phone,
                        Email = p.Email,
                        KanGrubu = p.BloodType?.ToString()
                    }).ToList();

                dgvPatients.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hastalar yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadPatients();
                return;
            }

            try
            {
                var searchTerm = txtSearch.Text.ToLower();
                var patients = _unitOfWork.AppointmentRepository.GetAll()
                    .Where(a => a.DoctorId == SessionManager.CurrentUser.Id)
                    .Select(a => a.Patient)
                    .Distinct()
                    .Where(p => p.FirstName.ToLower().Contains(searchTerm) ||
                               p.LastName.ToLower().Contains(searchTerm) ||
                               p.NationalId.Contains(searchTerm))
                    .Select(p => new
                    {
                        Id = p.Id,
                        TC = p.NationalId,
                        Ad = p.FirstName,
                        Soyad = p.LastName,
                        DogumTarihi = p.BirthDate.ToString("dd.MM.yyyy"),
                        Telefon = p.Phone,
                        Email = p.Email,
                        KanGrubu = p.BloodType?.ToString()
                    }).ToList();

                dgvPatients.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Arama sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var patientId = (int)dgvPatients.SelectedRows[0].Cells["Id"].Value;
            var patient = _unitOfWork.PatientRepository.GetById(patientId);
            
            if (patient != null)
            {
                var details = $"Hasta Bilgileri:\n\n" +
                             $"TC: {patient.NationalId}\n" +
                             $"Ad Soyad: {patient.FullName}\n" +
                             $"Doðum Tarihi: {patient.BirthDate:dd.MM.yyyy}\n" +
                             $"Cinsiyet: {(patient.Gender == HospitalAutomation.Models.Enums.Gender.Male ? "Erkek" : "Kadýn")}\n" +
                             $"Telefon: {patient.Phone}\n" +
                             $"Email: {patient.Email}\n" +
                             $"Adres: {patient.Address}\n" +
                             $"Kan Grubu: {patient.BloodType}\n" +
                             $"Alerjiler: {patient.Allergies}\n" +
                             $"Kronik Hastalýklar: {patient.MedicalHistory}";

                MessageBox.Show(details, "Hasta Detaylarý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnViewMedicalRecords_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir hasta seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Týbbi kayýtlar formunu aç
            MessageBox.Show("Týbbi kayýtlar formu yakýnda eklenecek.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}