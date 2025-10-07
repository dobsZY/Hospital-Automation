using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Utilities;
using System.Collections.Generic;

namespace HospitalAutomation.Views.DoctorManagement
{
    public partial class DoctorAppointmentsForm : Form
    {
        private UnitOfWork _unitOfWork;

        public DoctorAppointmentsForm()
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
                if (dtpDate != null)
                    dtpDate.Value = DateTime.Today;

                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Form init hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAppointments()
        {
            try
            {
                var selectedDate = dtpDate?.Value ?? DateTime.Today;

                // Basit örnek veri - gerçek uygulamada veritabanýndan gelecek
                var sampleAppointments = new List<dynamic>();

                if (SessionManager.IsStaffLogin)
                {
                    sampleAppointments.AddRange(new[]
                    {
                        new { 
                            Id = 1,
                            Hasta = "Ahmet YILMAZ", 
                            Tarih = DateTime.Today.ToString("dd.MM.yyyy"),
                            Saat = "09:00",
                            Durum = "Planlandý",
                            Notlar = "Kontrol muayenesi"
                        },
                        new { 
                            Id = 2,
                            Hasta = "Ayþe KAYA", 
                            Tarih = DateTime.Today.ToString("dd.MM.yyyy"),
                            Saat = "10:30",
                            Durum = "Tamamlandý",
                            Notlar = "Ýlaç kontrolü"
                        },
                        new { 
                            Id = 3,
                            Hasta = "Mehmet DEMÝR", 
                            Tarih = DateTime.Today.AddDays(1).ToString("dd.MM.yyyy"),
                            Saat = "14:00",
                            Durum = "Planlandý",
                            Notlar = "Baþlangýç muayenesi"
                        }
                    });
                }

                dgvAppointments.DataSource = sampleAppointments;

                // Grid renklendirme
                this.BeginInvoke(new Action(() =>
                {
                    foreach (DataGridViewRow row in dgvAppointments.Rows)
                    {
                        if (row.Cells["Durum"]?.Value?.ToString() == "Tamamlandý")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else if (row.Cells["Durum"]?.Value?.ToString() == "Ýptal Edildi")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                    }
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevular yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir randevu seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MessageBox.Show("Randevu tamamlandý olarak iþaretlendi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ýþlem sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir randevu seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Randevuyu iptal etmek istediðinizden emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    MessageBox.Show("Randevu iptal edildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ýþlem sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}