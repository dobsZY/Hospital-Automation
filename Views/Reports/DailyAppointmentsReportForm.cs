using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.Reports
{
    public partial class DailyAppointmentsReportForm : Form
    {
        private UnitOfWork _unitOfWork;

        public DailyAppointmentsReportForm()
        {
            InitializeComponent();
            try
            {
                var context = new HospitalDbContext();
                _unitOfWork = new UnitOfWork(context);
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
                // Default to today
                if (dtpReportDate != null)
                    dtpReportDate.Value = DateTime.Today;

                this.Load += DailyAppointmentsReportForm_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form init hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DailyAppointmentsReportForm_Load(object sender, EventArgs e)
        {
            LoadAppointmentReport();
        }

        private void LoadAppointmentReport()
        {
            try
            {
                var selectedDate = dtpReportDate?.Value ?? DateTime.Today;
                string reportTitle = $"Günlük Randevu Raporu ({selectedDate:dd.MM.yyyy})";

                lblTitle.Text = reportTitle;

                // Basit örnek veri oluþtur - gerçek uygulamada veritabanýndan gelecek
                var sampleAppointments = new[]
                {
                    new { 
                        Saat = "09:00", 
                        Hasta = "Ahmet YILMAZ", 
                        Doktor = "Dr. Mehmet KARDÝYOLOJÝ", 
                        Bölüm = "Kardiyoloji", 
                        Durum = "Tamamlandý" 
                    },
                    new { 
                        Saat = "10:30", 
                        Hasta = "Ayþe KAYA", 
                        Doktor = "Dr. Zeynep DAHÝLÝYE", 
                        Bölüm = "Dahiliye", 
                        Durum = "Planlandý" 
                    },
                    new { 
                        Saat = "14:00", 
                        Hasta = "Mehmet DEMÝR", 
                        Doktor = "Dr. Ali ORTOPEDÝ", 
                        Bölüm = "Ortopedi", 
                        Durum = "Ýptal Edildi" 
                    }
                };

                dgvAppointments.DataSource = sampleAppointments.ToList();

                // Ýstatistikleri güncelle
                UpdateStatistics(sampleAppointments);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics(dynamic[] appointments)
        {
            try
            {
                lblTotalAppointments.Text = $"Toplam Randevu: {appointments.Length}";
                
                var completed = appointments.Count(a => a.Durum == "Tamamlandý");
                lblCompletedAppointments.Text = $"Tamamlanan: {completed}";
                
                var cancelled = appointments.Count(a => a.Durum == "Ýptal Edildi");
                lblCancelledAppointments.Text = $"Ýptal Edilen: {cancelled}";
                
                var scheduled = appointments.Count(a => a.Durum == "Planlandý");
                lblScheduledAppointments.Text = $"Bekleyen: {scheduled}";

                // Bölümlere göre daðýlým
                var departmentStats = appointments
                    .GroupBy(a => a.Bölüm)
                    .Select(g => $"{g.Key}: {g.Count()}")
                    .ToList();

                txtDepartmentStats.Text = departmentStats.Count > 0 
                    ? string.Join(Environment.NewLine, departmentStats)
                    : "Veri bulunamadý";

                // Doktorlara göre daðýlým
                var doctorStats = appointments
                    .GroupBy(a => a.Doktor)
                    .Select(g => $"{g.Key}: {g.Count()}")
                    .ToList();

                txtDoctorStats.Text = doctorStats.Count > 0 
                    ? string.Join(Environment.NewLine, doctorStats)
                    : "Veri bulunamadý";
            }
            catch (Exception ex)
            {
                lblTotalAppointments.Text = "Hata: Ýstatistik hesaplanamadý";
                System.Diagnostics.Debug.WriteLine($"Statistics calculation error: {ex.Message}");
            }
        }

        private void DtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            LoadAppointmentReport();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointmentReport();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yazdýrma özelliði geliþtirilmektedir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dýþa aktarma özelliði geliþtirilmektedir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnToday_Click(object sender, EventArgs e)
        {
            if (dtpReportDate != null)
            {
                dtpReportDate.Value = DateTime.Today;
            }
        }

        private void BtnYesterday_Click(object sender, EventArgs e)
        {
            if (dtpReportDate != null)
            {
                dtpReportDate.Value = DateTime.Today.AddDays(-1);
            }
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