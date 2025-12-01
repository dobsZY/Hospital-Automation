using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Views.AppointmentManagement
{
    public partial class AppointmentAddForm : Form
    {
        private IAppointmentService _appointmentService;
        private IPatientService _patientService;
        private IUserService _userService;

        public AppointmentAddForm()
        {
            InitializeComponent();
            InitializeServices();
            LoadComboBoxes();
        }

        private void InitializeServices()
        {
            var context = new HospitalDbContext();
            var unitOfWork = new UnitOfWork(context);
            _appointmentService = new AppointmentService(unitOfWork);
            _patientService = new PatientService(unitOfWork);
            _userService = new UserService(unitOfWork);
        }

        private void LoadComboBoxes()
        {
            LoadPatients();
            LoadDepartments();
            LoadTimeSlots();
        }

        private void LoadPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients().ToList();
                cmbPatient.Items.Clear();
                foreach (var patient in patients)
                {
                    cmbPatient.Items.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hastalar yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDepartments()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var departments = context.Departments.Where(d => d.IsActive).ToList();
                    cmbDepartment.Items.Clear();
                    foreach (var dept in departments)
                    {
                        cmbDepartment.Items.Add(dept);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bölümler yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTimeSlots()
        {
            cmbAppointmentTime.Items.Clear();
            for (int hour = 9; hour <= 17; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    var time = new TimeSpan(hour, minute, 0);
                    cmbAppointmentTime.Items.Add(time.ToString(@"hh\:mm"));
                }
            }
        }

        private void CmbPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatient.SelectedItem != null)
            {
                var patientId = ((Patient)cmbPatient.SelectedItem).Id;
                var patient = _patientService.GetPatientById(patientId);
                if (patient != null)
                {
                    lblPatientDetails.Text = $"TC: {patient.NationalId}\nTelefon: {patient.Phone}\nYaþ: {patient.Age}\nCinsiyet: {patient.Gender}";
                }
            }
        }

        private void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedItem != null)
            {
                LoadDoctorsByDepartment();
            }
        }

        private void LoadDoctorsByDepartment()
        {
            try
            {
                // For now, load all doctors - in a real system, you'd filter by department
                var doctors = _userService.GetUsersByRole(UserRole.Doctor).ToList();
                cmbDoctor.Items.Clear();
                foreach (var doctor in doctors)
                {
                    cmbDoctor.Items.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Doktorlar yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearchPatient_Click(object sender, EventArgs e)
        {
            var searchForm = new PatientManagement.PatientSearchForm();
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                // Select the chosen patient
                for (int i = 0; i < cmbPatient.Items.Count; i++)
                {
                    var item = (Patient)cmbPatient.Items[i];
                    if (item.Id == searchForm.SelectedPatientId)
                    {
                        cmbPatient.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                var appointment = new Appointment
                {
                    PatientId = (int)((Patient)cmbPatient.SelectedItem).Id,
                    DoctorId = (int)((User)cmbDoctor.SelectedItem).Id,
                    DepartmentId = cmbDepartment.SelectedItem != null ? (int?)((Department)cmbDepartment.SelectedItem).Id : null,
                    AppointmentDate = dtpAppointmentDate.Value.Date,
                    AppointmentTime = TimeSpan.Parse(cmbAppointmentTime.SelectedItem.ToString()),
                    Symptoms = txtSymptoms.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    Status = AppointmentStatus.Scheduled,
                    CreatedBy = SessionManager.CurrentUser?.Username ?? "System"
                };

                if (_appointmentService.CreateAppointment(appointment))
                {
                    MessageBox.Show("Randevu baþarýyla oluþturuldu!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Randevu oluþturulamadý!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevu kaydedilirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbPatient.SelectedItem == null)
            {
                MessageBox.Show("Hasta seçimi yapýnýz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbDoctor.SelectedItem == null)
            {
                MessageBox.Show("Doktor seçimi yapýnýz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpAppointmentDate.Value < DateTime.Today)
            {
                MessageBox.Show("Geçmiþ tarihli randevu oluþturulamaz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbAppointmentTime.SelectedItem == null)
            {
                MessageBox.Show("Randevu saati seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public partial class AppointmentListForm : Form
    {
        private IAppointmentService _appointmentService;

        public AppointmentListForm()
        {
            InitializeComponent();
            InitializeServices();
            LoadAppointments();
        }

        private void InitializeServices()
        {
            var context = new HospitalDbContext();
            var unitOfWork = new UnitOfWork(context);
            _appointmentService = new AppointmentService(unitOfWork);
        }

        private void SetupFilterCombo()
        {
            cmbFilterStatus.Items.Add(new ComboBoxItem("Tümü", null));
            foreach (AppointmentStatus status in Enum.GetValues(typeof(AppointmentStatus)))
            {
                string statusText;
                switch (status)
                {
                    case AppointmentStatus.Scheduled:
                        statusText = "Planlandý";
                        break;
                    case AppointmentStatus.Completed:
                        statusText = "Tamamlandý";
                        break;
                    case AppointmentStatus.Cancelled:
                        statusText = "Ýptal Edildi";
                        break;
                    case AppointmentStatus.NoShow:
                        statusText = "Gelmedi";
                        break;
                    default:
                        statusText = status.ToString();
                        break;
                }
                cmbFilterStatus.Items.Add(new ComboBoxItem(statusText, status));
            }
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void LoadAppointments()
        {
            try
            {
                var appointments = _appointmentService.GetAllAppointments().Select(a => new
                {
                    Id = a.Id,
                    HastaAdý = a.Patient.FullName,
                    DoktorAdý = a.Doctor.FullName,
                    Bölüm = a.Department?.Name ?? "-",
                    Tarih = a.AppointmentDate.ToString("dd/MM/yyyy"),
                    Saat = a.AppointmentTime.ToString(@"hh\:mm"),
                    Durum = GetStatusText(a.Status),
                    Þikayetler = a.Symptoms,
                    Notlar = a.Notes
                }).ToList();

                dgvAppointments.DataSource = appointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randevular yüklenirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStatusText(AppointmentStatus status)
        {
            switch (status)
            {
                case AppointmentStatus.Scheduled:
                    return "Planlandý";
                case AppointmentStatus.Completed:
                    return "Tamamlandý";
                case AppointmentStatus.Cancelled:
                    return "Ýptal Edildi";
                case AppointmentStatus.NoShow:
                    return "Gelmedi";
                default:
                    return status.ToString();
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var appointments = _appointmentService.GetAllAppointments().AsEnumerable();

                // Date filter
                appointments = appointments.Where(a => a.AppointmentDate.Date == dtpFilterDate.Value.Date);

                // Status filter
                if (cmbFilterStatus.SelectedItem != null)
                {
                    var selectedStatus = ((ComboBoxItem)cmbFilterStatus.SelectedItem).Value;
                    if (selectedStatus != null)
                    {
                        appointments = appointments.Where(a => a.Status == (AppointmentStatus)selectedStatus);
                    }
                }

                var filteredAppointments = appointments.Select(a => new
                {
                    Id = a.Id,
                    HastaAdý = a.Patient.FullName,
                    DoktorAdý = a.Doctor.FullName,
                    Bölüm = a.Department?.Name ?? "-",
                    Tarih = a.AppointmentDate.ToString("dd/MM/yyyy"),
                    Saat = a.AppointmentTime.ToString(@"hh\:mm"),
                    Durum = GetStatusText(a.Status),
                    Þikayetler = a.Symptoms,
                    Notlar = a.Notes
                }).ToList();

                dgvAppointments.DataSource = filteredAppointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sýrasýnda hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            dtpFilterDate.Value = DateTime.Today;
            cmbFilterStatus.SelectedIndex = 0;
            LoadAppointments();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AppointmentAddForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAppointments();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Düzenlenecek randevuyu seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Düzenleme özelliði yakýnda eklenecek!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Tamamlanacak randevuyu seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seçili randevuyu tamamlamak istediðinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var selectedId = (int)dgvAppointments.SelectedRows[0].Cells["Id"].Value;
                    if (_appointmentService.CompleteAppointment(selectedId))
                    {
                        MessageBox.Show("Randevu tamamlandý!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAppointments();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Randevu tamamlanýrken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Ýptal edilecek randevuyu seçiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seçili randevuyu iptal etmek istediðinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var selectedId = (int)dgvAppointments.SelectedRows[0].Cells["Id"].Value;
                    if (_appointmentService.CancelAppointment(selectedId, "Kullanýcý tarafýndan iptal edildi"))
                    {
                        MessageBox.Show("Randevu iptal edildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAppointments();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Randevu iptal edilirken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}