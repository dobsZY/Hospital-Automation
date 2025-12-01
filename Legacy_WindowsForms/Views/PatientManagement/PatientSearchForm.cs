using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;

namespace HospitalAutomation.Views.PatientManagement
{
    public partial class PatientSearchForm : Form
    {
        private IPatientService _patientService;
        public int SelectedPatientId { get; private set; }

        public PatientSearchForm()
        {
            InitializeComponent();
            InitializeServices();
            LoadAllPatients();
        }

        private void InitializeServices()
        {
            var context = new HospitalDbContext();
            var unitOfWork = new UnitOfWork(context);
            _patientService = new PatientService(unitOfWork);
        }

        private void LoadAllPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients().Select(p => new
                {
                    Id = p.Id,
                    TCKimlikNo = p.NationalId,
                    AdSoyad = p.FullName,
                    Yaþ = p.Age,
                    Cinsiyet = p.Gender.ToString(),
                    Telefon = p.Phone ?? "-",
                    Email = p.Email ?? "-"
                }).ToList();

                dgvPatients.DataSource = patients;

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

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadAllPatients();
                    return;
                }

                var patients = _patientService.SearchPatients(searchTerm).Select(p => new
                {
                    Id = p.Id,
                    TCKimlikNo = p.NationalId,
                    AdSoyad = p.FullName,
                    Yaþ = p.Age,
                    Cinsiyet = p.Gender.ToString(),
                    Telefon = p.Phone ?? "-",
                    Email = p.Email ?? "-"
                }).ToList();

                dgvPatients.DataSource = patients;

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

        private void DgvPatients_DoubleClick(object sender, EventArgs e)
        {
            SelectPatient();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SelectPatient();
        }

        private void SelectPatient()
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir hasta seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedPatientId = (int)dgvPatients.SelectedRows[0].Cells["Id"].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PatientSearchForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiðinde yapýlacak iþlemler
        }
    }
}