using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;

namespace HospitalAutomation.Views.PatientManagement
{
    public partial class PatientListForm : Form
    {
        private IPatientService _patientService;

        public PatientListForm()
        {
            InitializeComponent();
            InitializeServices();
            LoadPatients();
        }

        private void InitializeServices()
        {
            var context = new HospitalDbContext();
            var unitOfWork = new UnitOfWork(context);
            _patientService = new PatientService(unitOfWork);
        }

        private void LoadPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();
                dgvPatients.DataSource = patients.Select(p => new
                {
                    p.Id,
                    TCKimlikNo = p.NationalId,
                    Ad = p.FirstName,
                    Soyad = p.LastName,
                    DoðumTarihi = p.BirthDate.ToString("dd.MM.yyyy"),
                    Yaþ = p.Age,
                    Cinsiyet = p.Gender.ToString(),
                    Telefon = p.Phone ?? "-",
                    Email = p.Email ?? "-"
                }).ToList();

                // Sütun baþlýklarýný ayarla
                if (dgvPatients.Columns.Count > 0)
                {
                    dgvPatients.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hastalar yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchPatients();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchPatients();
            }
        }

        private void SearchPatients()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    LoadPatients();
                    return;
                }

                var patients = _patientService.SearchPatients(txtSearch.Text.Trim()).ToList();
                dgvPatients.DataSource = patients.Select(p => new
                {
                    p.Id,
                    TCKimlikNo = p.NationalId,
                    Ad = p.FirstName,
                    Soyad = p.LastName,
                    DoðumTarihi = p.BirthDate.ToString("dd.MM.yyyy"),
                    Yaþ = p.Age,
                    Cinsiyet = p.Gender.ToString(),
                    Telefon = p.Phone ?? "-",
                    Email = p.Email ?? "-"
                }).ToList();

                if (dgvPatients.Columns.Count > 0)
                {
                    dgvPatients.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Arama sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadPatients();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new PatientAddForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Düzenlemek için bir hasta seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["Id"].Value);
                var editForm = new PatientEditForm(patientId);
                
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients(); // Listeyi yenile
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hasta düzenleme formu açýlýrken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silmek için bir hasta seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seçili hastayý silmek istediðinizden emin misiniz?", 
                "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["Id"].Value);
                    if (_patientService.DeletePatient(patientId))
                    {
                        MessageBox.Show("Hasta baþarýyla silindi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatients();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hasta silinirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvPatients_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
    }
}