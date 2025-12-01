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
                LogHelper.Error("Kullanýcý doðrulanýrken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý doðrulanýrken hata oluþtu: {ex.Message}", ex);
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
                    throw new ArgumentException("Kullanýcý adý boþ olamaz");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("Email boþ olamaz");

                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Þifre boþ olamaz");

                if (_unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanýcý adý zaten kullanýlmaktadýr");

                if (_unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanýlmaktadýr");

                // Hash password
                user.PasswordHash = PasswordHelper.HashPassword(password);

                _unitOfWork.Users.Add(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User created: username='{user.Username}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CreateUser yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý oluþturulurken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý oluþturulurken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                // Authorization: only Admin
                AuthorizationHelper.EnsureAdmin();

                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new ArgumentException("Kullanýcý adý boþ olamaz");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("Email boþ olamaz");

                if (_unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanýcý adý zaten kullanýlmaktadýr");

                if (_unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanýlmaktadýr");

                _unitOfWork.Users.Add(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User created (no password): username='{user.Username}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("CreateUser (no password) yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý oluþturulurken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý oluþturulurken hata oluþtu: {ex.Message}", ex);
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
                    throw new InvalidOperationException("Kullanýcý bulunamadý");

                // Centralized authorization: admin or self
                AuthorizationHelper.EnsureAdminOrSelf(user.Id);

                // If not admin, prevent role change
                if (!SessionManager.IsAdmin && existingUser.Role != user.Role)
                    throw new UnauthorizedAccessException("Rol deðiþikliði yapma yetkiniz yok.");

                // Check if username is changed and already exists
                if (existingUser.Username != user.Username && _unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanýcý adý zaten kullanýlmaktadýr");

                // Check if email is changed and already exists
                if (existingUser.Email != user.Email && _unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanýlmaktadýr");

                _unitOfWork.Users.Update(user);
                var result = _unitOfWork.Complete();

                LogHelper.Information($"User updated: id={user.Id}, username='{user.Username}', result={result}.");
                return result > 0;
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("UpdateUser yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý güncellenirken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý güncellenirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("DeleteUser yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý silinirken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý silinirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("GetUserById yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý getirilirken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return null;

                AuthorizationHelper.EnsureStaff();
                return _unitOfWork.Users.GetByUsername(username);
            }
            catch (UnauthorizedAccessException)
            {
                LogHelper.Information("GetUserByUsername yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý getirilirken hata oluþtu.", ex);
                throw new Exception($"Kullanýcý getirilirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("GetAllUsers yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcýlar getirilirken hata oluþtu.", ex);
                throw new Exception($"Kullanýcýlar getirilirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("GetDoctors yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Doktorlar getirilirken hata oluþtu.", ex);
                throw new Exception($"Doktorlar getirilirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("GetUsersByRole yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Rol bazlý kullanýcýlar getirilirken hata oluþtu.", ex);
                throw new Exception($"Rol bazlý kullanýcýlar getirilirken hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("IsUsernameExists yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Kullanýcý adý kontrolü sýrasýnda hata oluþtu.", ex);
                throw new Exception($"Kullanýcý adý kontrolü sýrasýnda hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("IsEmailExists yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Email kontrolü sýrasýnda hata oluþtu.", ex);
                throw new Exception($"Email kontrolü sýrasýnda hata oluþtu: {ex.Message}", ex);
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
                LogHelper.Information("ChangePassword yetki hatasý.");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Þifre deðiþtirilirken hata oluþtu.", ex);
                throw new Exception($"Þifre deðiþtirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        // Backward compatibility method
        public User AuthenticateUser(string username, string password)
        {
            return Authenticate(username, password);
        }
    }
}