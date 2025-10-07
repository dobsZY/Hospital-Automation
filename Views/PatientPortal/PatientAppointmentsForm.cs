using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.PatientPortal
{
    public partial class PatientAppointmentsForm : Form
    {
        private readonly UnitOfWork _unitOfWork;

        public PatientAppointmentsForm()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            try
            {
                var appointments = _unitOfWork.AppointmentRepository.GetAll()
                    .Where(a => a.PatientId == SessionManager.CurrentPatient.Id)
                    .Select(a => new
                    {
                        Id = a.Id,
                        Doktor = a.Doctor.FullName,
                        Bölüm = a.Department.Name,
                        Tarih = a.AppointmentDate.ToString("dd.MM.yyyy"),
                        Saat = a.AppointmentTime.ToString(@"hh\:mm"),
                        Durum = GetStatusText(a.Status),
                        Notlar = a.Notes
                    })
                    .OrderBy(a => a.Tarih)
                    .ThenBy(a => a.Saat)
                    .ToList();

                dgvAppointments.DataSource = appointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevular yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStatusText(HospitalAutomation.Models.Enums.AppointmentStatus status)
        {
            switch (status)
            {
                case HospitalAutomation.Models.Enums.AppointmentStatus.Scheduled: return "Planlandý";
                case HospitalAutomation.Models.Enums.AppointmentStatus.Completed: return "Tamamlandý";
                case HospitalAutomation.Models.Enums.AppointmentStatus.Cancelled: return "Ýptal Edildi";
                case HospitalAutomation.Models.Enums.AppointmentStatus.InProgress: return "Devam Ediyor";
                default: return status.ToString();
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var query = _unitOfWork.AppointmentRepository.GetAll()
                    .Where(a => a.PatientId == SessionManager.CurrentPatient.Id);

                if (rbUpcoming.Checked)
                {
                    query = query.Where(a => a.AppointmentDate >= DateTime.Today);
                }
                else if (rbPast.Checked)
                {
                    query = query.Where(a => a.AppointmentDate < DateTime.Today);
                }

                var appointments = query
                    .Select(a => new
                    {
                        Id = a.Id,
                        Doktor = a.Doctor.FullName,
                        Bölüm = a.Department.Name,
                        Tarih = a.AppointmentDate.ToString("dd.MM.yyyy"),
                        Saat = a.AppointmentTime.ToString(@"hh\:mm"),
                        Durum = GetStatusText(a.Status),
                        Notlar = a.Notes
                    })
                    .OrderBy(a => a.Tarih)
                    .ThenBy(a => a.Saat)
                    .ToList();

                dgvAppointments.DataSource = appointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen iptal etmek istediðiniz randevuyu seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Randevuyu iptal etmek istediðinizden emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var appointmentId = (int)dgvAppointments.SelectedRows[0].Cells["Id"].Value;
                    var appointment = _unitOfWork.AppointmentRepository.GetById(appointmentId);
                    
                    if (appointment != null && appointment.AppointmentDate >= DateTime.Today)
                    {
                        appointment.Status = HospitalAutomation.Models.Enums.AppointmentStatus.Cancelled;
                        _unitOfWork.AppointmentRepository.Update(appointment);
                        _unitOfWork.Save();

                        MessageBox.Show("Randevunuz iptal edildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnFilter_Click(sender, e); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show("Geçmiþ randevular iptal edilemez!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ýþlem sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}