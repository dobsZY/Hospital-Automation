using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using HospitalAutomation.Data;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Utilities;
using HospitalAutomation.Infrastructure;

namespace HospitalAutomation.Views.UserManagement
{
    public partial class UserAddForm : Form
    {
        private readonly UnitOfWork _unitOfWork;
        
        public UserAddForm()
        {
            InitializeComponent();
            _unitOfWork = SimpleContainer.Instance.GetService<UnitOfWork>();
        }

        private void UserAddForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadDepartments();
            SetDoctorFieldsVisibility(false);
        }

        private void LoadRoles()
        {
            var roles = new[]
            {
                new { Text = "Admin", Value = UserRole.Admin },
                new { Text = "Doktor", Value = UserRole.Doctor },
                new { Text = "Hemþire", Value = UserRole.Nurse },
                new { Text = "Resepsiyonist", Value = UserRole.Receptionist }
            };

            cmbRole.DataSource = roles;
            cmbRole.DisplayMember = "Text";
            cmbRole.ValueMember = "Value";
            cmbRole.SelectedIndex = -1;
        }

        private void LoadDepartments()
        {
            try
            {
                var departments = _unitOfWork.DepartmentRepository.GetAll()
                    .Where(d => d.IsActive)
                    .Select(d => new { Text = d.Name, Value = d.Id })
                    .ToList();

                departments.Insert(0, new { Text = "-- Seçiniz --", Value = 0 });
                
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Text";
                cmbDepartment.ValueMember = "Value";
                cmbDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bölümler yüklenirken hata oluþtu:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedRole = (UserRole)cmbRole.SelectedValue;
            SetDoctorFieldsVisibility(selectedRole == UserRole.Doctor);
        }

        private void SetDoctorFieldsVisibility(bool isDoctorSelected)
        {
            lblDepartment.Visible = isDoctorSelected;
            cmbDepartment.Visible = isDoctorSelected;
            lblSpecialization.Visible = isDoctorSelected;
            txtSpecialization.Visible = isDoctorSelected;
            lblLicenseNumber.Visible = isDoctorSelected;
            txtLicenseNumber.Visible = isDoctorSelected;
            lblExperience.Visible = isDoctorSelected;
            txtExperience.Visible = isDoctorSelected;
            lblEducation.Visible = isDoctorSelected;
            txtEducation.Visible = isDoctorSelected;
        }

        private void btnTestDatabase_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.TestDatabaseConnection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var user = new User
                {
                    Username = txtUsername.Text.Trim(),
                    PasswordHash = PasswordHelper.HashPassword(txtPassword.Text),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Role = (UserRole)cmbRole.SelectedValue,
                    CreatedDate = DateTime.Now,
                    CreatedBy = SessionManager.CurrentUser?.Username ?? "System",
                    IsActive = true
                };

                // Doktor özel alanlarý
                if (user.Role == UserRole.Doctor)
                {
                    var selectedDeptId = (int)cmbDepartment.SelectedValue;
                    if (selectedDeptId > 0)
                        user.DepartmentId = selectedDeptId;
                        
                    user.Specialization = txtSpecialization.Text.Trim();
                    user.MedicalLicenseNumber = txtLicenseNumber.Text.Trim();
                    
                    if (int.TryParse(txtExperience.Text, out int experience))
                        user.ExperienceYears = experience;
                        
                    user.Education = txtEducation.Text.Trim();
                }

                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.SaveChanges();

                MessageBox.Show("Kullanýcý baþarýyla eklendi!", "Baþarýlý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanýcý eklenirken hata oluþtu:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Kullanýcý adý boþ olamaz!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Þifre boþ olamaz!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Ad boþ olamaz!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Soyad boþ olamaz!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-mail boþ olamaz!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Rol seçimi yapýlmalýdýr!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            // Doktor için ek validasyonlar
            var selectedRole = (UserRole)cmbRole.SelectedValue;
            if (selectedRole == UserRole.Doctor)
            {
                if (cmbDepartment.SelectedIndex <= 0)
                {
                    MessageBox.Show("Doktor için bölüm seçimi yapýlmalýdýr!", "Uyarý", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbDepartment.Focus();
                    return false;
                }
            }

            // Username benzersizliði kontrolü
            var existingUser = _unitOfWork.UserRepository.GetByUsername(txtUsername.Text.Trim());
            if (existingUser != null)
            {
                MessageBox.Show("Bu kullanýcý adý zaten kullanýlýyor!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Email benzersizliði kontrolü
            var existingEmail = _unitOfWork.UserRepository.GetAll()
                .FirstOrDefault(u => u.Email.ToLower() == txtEmail.Text.Trim().ToLower());
            if (existingEmail != null)
            {
                MessageBox.Show("Bu e-mail adresi zaten kullanýlýyor!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtSpecialization.Clear();
            txtLicenseNumber.Clear();
            txtExperience.Clear();
            txtEducation.Clear();
            cmbRole.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = 0;
            txtUsername.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public partial class UserListForm : Form
    {
        private readonly UnitOfWork _unitOfWork;
        
        public UserListForm()
        {
            InitializeComponent();
            _unitOfWork = SimpleContainer.Instance.GetService<UnitOfWork>();
        }

        private void UserListForm_Load(object sender, EventArgs e)
        {
            LoadRoleFilter();
            LoadUsers();
        }

        private void LoadRoleFilter()
        {
            var roles = new[]
            {
                new { Text = "Tümü", Value = -1 },
                new { Text = "Admin", Value = (int)UserRole.Admin },
                new { Text = "Doktor", Value = (int)UserRole.Doctor },
                new { Text = "Hemþire", Value = (int)UserRole.Nurse },
                new { Text = "Resepsiyonist", Value = (int)UserRole.Receptionist }
            };

            cmbRoleFilter.DataSource = roles;
            cmbRoleFilter.DisplayMember = "Text";
            cmbRoleFilter.ValueMember = "Value";
            cmbRoleFilter.SelectedIndex = 0;
        }

        private void LoadUsers()
        {
            try
            {
                var users = _unitOfWork.UserRepository.GetAll()
                    .Where(u => u.IsActive)
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        AdSoyad = u.FirstName + " " + u.LastName,
                        u.Email,
                        u.Phone,
                        Rol = u.Role.ToString(),
                        Bölüm = u.Department != null ? u.Department.Name : "-",
                        Uzmanlýk = !string.IsNullOrEmpty(u.Specialization) ? u.Specialization : "-",
                        KayýtTarihi = u.CreatedDate.ToString("dd.MM.yyyy"),
                        u.Role
                    }).ToList();

                // Filtre uygula
                var selectedRoleValue = (int)cmbRoleFilter.SelectedValue;
                if (selectedRoleValue >= 0)
                {
                    users = users.Where(u => (int)u.Role == selectedRoleValue).ToList();
                }

                dgvUsers.DataSource = users;

                // Sütun baþlýklarýný ayarla
                if (dgvUsers.Columns["Id"] != null)
                    dgvUsers.Columns["Id"].Visible = false;
                if (dgvUsers.Columns["Role"] != null)
                    dgvUsers.Columns["Role"].Visible = false;
                if (dgvUsers.Columns["Username"] != null)
                    dgvUsers.Columns["Username"].HeaderText = "Kullanýcý Adý";
                if (dgvUsers.Columns["AdSoyad"] != null)
                    dgvUsers.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                if (dgvUsers.Columns["Email"] != null)
                    dgvUsers.Columns["Email"].HeaderText = "E-mail";
                if (dgvUsers.Columns["Phone"] != null)
                    dgvUsers.Columns["Phone"].HeaderText = "Telefon";
                if (dgvUsers.Columns["KayýtTarihi"] != null)
                    dgvUsers.Columns["KayýtTarihi"].HeaderText = "Kayýt Tarihi";

                // Satýr sayýsýný göster
                this.Text = $"Kullanýcý Listesi ({users.Count} kayýt)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanýcýlar yüklenirken hata oluþtu:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void cmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var addForm = new UserAddForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek için bir kullanýcý seçin!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Kullanýcý düzenleme özelliði yakýnda eklenecek!", "Bilgi", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir kullanýcý seçin!", "Uyarý", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seçili kullanýcýyý silmek istediðinizden emin misiniz?", 
                                        "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    var userId = (int)dgvUsers.SelectedRows[0].Cells["Id"].Value;
                    var user = _unitOfWork.UserRepository.GetById(userId);
                    
                    if (user != null)
                    {
                        user.IsActive = false; // Soft delete
                        user.UpdatedDate = DateTime.Now;
                        user.UpdatedBy = SessionManager.CurrentUser?.Username ?? "System";
                        
                        _unitOfWork.SaveChanges();
                        
                        MessageBox.Show("Kullanýcý baþarýyla silindi!", "Baþarýlý", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kullanýcý silinirken hata oluþtu:\n{ex.Message}", "Hata", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTestDatabase_Click(object sender, EventArgs e)
        {
            DatabaseTestHelper.TestDatabaseConnection();
        }
    }
}