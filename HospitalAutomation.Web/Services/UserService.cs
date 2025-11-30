using System;
using System.Collections.Generic;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;

namespace HospitalAutomation.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public User Authenticate(string username, string password)
        {
            try
            {
                LogHelper.Information($"Authenticate attempt for username='{username ?? "<null>"}'.");

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return null;

                var user = _unitOfWork.Users.Authenticate(username, password);

                if (user != null)
                    LogHelper.Information($"Authenticate success for username='{username}'.");
                else
                    LogHelper.Information($"Authenticate failed for username='{username}'.");

                return user;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı doğrulanırken hata oluştu.", ex);
                throw new Exception($"Kullanıcı doğrulanırken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool CreateUser(User user, string password)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                // Authorization: only Admin
                AuthorizationHelper.EnsureAdmin();

                // Validate user data
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new ArgumentException("Kullanıcı adı boş olamaz");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("Email boş olamaz");

                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Şifre boş olamaz");

                if (_unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanıcı adı zaten kullanılmaktadır");

                if (_unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanılmaktadır");

                // Hash password
                user.PasswordHash = PasswordHelper.HashPassword(password);

                _unitOfWork.Users.Add(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User created: username='{user.Username}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CreateUser yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı oluşturulurken hata oluştu.", ex);
                throw new Exception($"Kullanıcı oluşturulurken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var existingUser = _unitOfWork.Users.GetById(user.Id);
                if (existingUser == null)
                    throw new InvalidOperationException("Kullanıcı bulunamadı");

                // Centralized authorization: admin or self
                AuthorizationHelper.EnsureAdminOrSelf(user.Id);

                // If not admin, prevent role change
                if (!SessionManager.IsAdmin && existingUser.Role != user.Role)
                    throw new UnauthorizedAccessException("Rol değişikliği yapma yetkiniz yok.");

                // Check if username is changed and already exists
                if (existingUser.Username != user.Username && _unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanıcı adı zaten kullanılmaktadır");

                // Check if email is changed and already exists
                if (existingUser.Email != user.Email && _unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanılmaktadır");

                _unitOfWork.Users.Update(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User updated: id={user.Id}, username='{user.Username}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("UpdateUser yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı güncellenirken hata oluştu.", ex);
                throw new Exception($"Kullanıcı güncellenirken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                AuthorizationHelper.EnsureAdmin();

                var user = _unitOfWork.Users.GetById(userId);
                if (user == null)
                    return false;

                _unitOfWork.Users.Remove(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User deleted: id={userId}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("DeleteUser yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı silinirken hata oluştu.", ex);
                throw new Exception($"Kullanıcı silinirken hata oluştu: {ex.Message}", ex);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                AuthorizationHelper.EnsureAdminOrSelf(userId);
                return _unitOfWork.Users.GetById(userId);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetUserById yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı getirilirken hata oluştu.", ex);
                throw new Exception($"Kullanıcı getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.GetAll();
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetAllUsers yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcılar getirilirken hata oluştu.", ex);
                throw new Exception($"Kullanıcılar getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetDoctors()
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.GetDoctors();
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetDoctors yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Doktorlar getirilirken hata oluştu.", ex);
                throw new Exception($"Doktorlar getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetUsersByRole(UserRole role)
        {
            try
            {
                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.GetByRole(role);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetUsersByRole yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Rol bazlı kullanıcılar getirilirken hata oluştu.", ex);
                throw new Exception($"Rol bazlı kullanıcılar getirilirken hata oluştu: {ex.Message}", ex);
            }
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return false;

                // Allow check for registration scenarios (public)
                if (!SessionManager.IsLoggedIn)
                    return _unitOfWork.Users.IsUsernameExists(username);

                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.IsUsernameExists(username);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("IsUsernameExists yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanıcı adı kontrolü sırasında hata oluştu.", ex);
                throw new Exception($"Kullanıcı adı kontrolü sırasında hata oluştu: {ex.Message}", ex);
            }
        }

        public bool IsEmailExists(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                if (!SessionManager.IsLoggedIn)
                    return _unitOfWork.Users.IsEmailExists(email);

                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.IsEmailExists(email);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("IsEmailExists yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Email kontrolü sırasında hata oluştu.", ex);
                throw new Exception($"Email kontrolü sırasında hata oluştu: {ex.Message}", ex);
            }
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = _unitOfWork.Users.GetById(userId);
                if (user == null)
                    return false;

                AuthorizationHelper.EnsureAdminOrSelf(userId);

                if (!PasswordHelper.VerifyPassword(oldPassword, user.PasswordHash) && !SessionManager.IsAdmin)
                    return false;

                user.PasswordHash = PasswordHelper.HashPassword(newPassword);
                _unitOfWork.Users.Update(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"Password changed for userId={userId}, result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("ChangePassword yetki hatası.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Şifre değiştirilirken hata oluştu.", ex);
                throw new Exception($"Şifre değiştirilirken hata oluştu: {ex.Message}", ex);
            }
        }
    }
}

