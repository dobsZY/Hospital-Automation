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
                new { Text = "Hemï¿½ire", Value = UserRole.Nurse },
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

                departments.Insert(0, new { Text = "-- Seï¿½iniz --", Value = 0 });
                
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Text";
                cmbDepartment.ValueMember = "Value";
                cmbDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bï¿½lï¿½mler yï¿½klenirken hata oluï¿½tu:\n{ex.Message}", "Hata", 
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
                    Role = (UserRole)cmbRole.SelectedValue,
                    CreatedDate = DateTime.Now,
                    CreatedBy = SessionManager.CurrentUser?.Username ?? "System",
                    IsActive = true
                };

                // Doktor ï¿½zel alanlarï¿½
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

                MessageBox.Show("Kullanï¿½cï¿½ baï¿½arï¿½yla eklendi!", "Baï¿½arï¿½lï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanï¿½cï¿½ eklenirken hata oluï¿½tu:\n{ex.Message}", "Hata", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Kullanï¿½cï¿½ adï¿½ boï¿½ olamaz!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("ï¿½ifre boï¿½ olamaz!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Ad boï¿½ olamaz!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Soyad boï¿½ olamaz!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-mail boï¿½ olamaz!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Rol seï¿½imi yapï¿½lmalï¿½dï¿½r!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            // Doktor iï¿½in ek validasyonlar
            var selectedRole = (UserRole)cmbRole.SelectedValue;
            if (selectedRole == UserRole.Doctor)
            {
                if (cmbDepartment.SelectedIndex <= 0)
                {
                    MessageBox.Show("Doktor iï¿½in bï¿½lï¿½m seï¿½imi yapï¿½lmalï¿½dï¿½r!", "Uyarï¿½", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbDepartment.Focus();
                    return false;
                }
            }

            // Username benzersizliï¿½i kontrolï¿½
            var existingUser = _unitOfWork.UserRepository.GetByUsername(txtUsername.Text.Trim());
            if (existingUser != null)
            {
                MessageBox.Show("Bu kullanï¿½cï¿½ adï¿½ zaten kullanï¿½lï¿½yor!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Email benzersizliï¿½i kontrolï¿½
            var existingEmail = _unitOfWork.UserRepository.GetAll()
                .FirstOrDefault(u => u.Email.ToLower() == txtEmail.Text.Trim().ToLower());
            if (existingEmail != null)
            {
                MessageBox.Show("Bu e-mail adresi zaten kullanï¿½lï¿½yor!", "Uyarï¿½", 
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
                new { Text = "Tï¿½mï¿½", Value = -1 },
                new { Text = "Admin", Value = (int)UserRole.Admin },
                new { Text = "Doktor", Value = (int)UserRole.Doctor },
                new { Text = "Hemï¿½ire", Value = (int)UserRole.Nurse },
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
                        
                        Rol = u.Role.ToString(),
                        Bolum = u.Department != null ? u.Department.Name : "-",
                        Uzmanlik = !string.IsNullOrEmpty(u.Specialization) ? u.Specialization : "-",
                        KayitTarihi = u.CreatedDate.ToString("dd.MM.yyyy"),
                        u.Role
                    }).ToList();

                // Filtre uygula
                var selectedRoleValue = (int)cmbRoleFilter.SelectedValue;
                if (selectedRoleValue >= 0)
                {
                    users = users.Where(u => (int)u.Role == selectedRoleValue).ToList();
                }

                dgvUsers.DataSource = users;

                // Sï¿½tun baï¿½lï¿½klarï¿½nï¿½ ayarla
                if (dgvUsers.Columns["Id"] != null)
                    dgvUsers.Columns["Id"].Visible = false;
                if (dgvUsers.Columns["Role"] != null)
                    dgvUsers.Columns["Role"].Visible = false;
                if (dgvUsers.Columns["Username"] != null)
                    dgvUsers.Columns["Username"].HeaderText = "Kullanï¿½cï¿½ Adï¿½";
                if (dgvUsers.Columns["AdSoyad"] != null)
                    dgvUsers.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                if (dgvUsers.Columns["Email"] != null)
                    dgvUsers.Columns["Email"].HeaderText = "E-mail";
                if (dgvUsers.Columns["Phone"] != null)
                    dgvUsers.Columns["Phone"].HeaderText = "Telefon";
                if (dgvUsers.Columns["Kayï¿½tTarihi"] != null)
                    dgvUsers.Columns["Kayï¿½tTarihi"].HeaderText = "Kayï¿½t Tarihi";

                // Satï¿½r sayï¿½sï¿½nï¿½ gï¿½ster
                this.Text = $"Kullanï¿½cï¿½ Listesi ({users.Count} kayï¿½t)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanï¿½cï¿½lar yï¿½klenirken hata oluï¿½tu:\n{ex.Message}", "Hata", 
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
                MessageBox.Show("Lï¿½tfen dï¿½zenlemek iï¿½in bir kullanï¿½cï¿½ seï¿½in!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Kullanï¿½cï¿½ dï¿½zenleme ï¿½zelliï¿½i yakï¿½nda eklenecek!", "Bilgi", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lï¿½tfen silmek iï¿½in bir kullanï¿½cï¿½ seï¿½in!", "Uyarï¿½", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seï¿½ili kullanï¿½cï¿½yï¿½ silmek istediï¿½inizden emin misiniz?", 
                                        "Silme Onayï¿½", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
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
                        
                        MessageBox.Show("Kullanï¿½cï¿½ baï¿½arï¿½yla silindi!", "Baï¿½arï¿½lï¿½", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kullanï¿½cï¿½ silinirken hata oluï¿½tu:\n{ex.Message}", "Hata", 
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

