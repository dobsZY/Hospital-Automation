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
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return null;

                return _unitOfWork.Users.Authenticate(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý doðrulanýrken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool CreateUser(User user, string password)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

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
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý oluþturulurken hata oluþtu: {ex.Message}", ex);
            }
        }

        // Overloaded method for backward compatibility
        public bool CreateUser(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                // Validate user data
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new ArgumentException("Kullanýcý adý boþ olamaz");

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("Email boþ olamaz");

                if (_unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanýcý adý zaten kullanýlmaktadýr");

                if (_unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanýlmaktadýr");

                _unitOfWork.Users.Add(user);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
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

                // Check if username is changed and already exists
                if (existingUser.Username != user.Username && _unitOfWork.Users.IsUsernameExists(user.Username))
                    throw new InvalidOperationException("Bu kullanýcý adý zaten kullanýlmaktadýr");

                // Check if email is changed and already exists
                if (existingUser.Email != user.Email && _unitOfWork.Users.IsEmailExists(user.Email))
                    throw new InvalidOperationException("Bu email adresi zaten kullanýlmaktadýr");

                _unitOfWork.Users.Update(user);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý güncellenirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var user = _unitOfWork.Users.GetById(userId);
                if (user == null)
                    return false;

                _unitOfWork.Users.Remove(user);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý silinirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return _unitOfWork.Users.GetById(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return null;

                return _unitOfWork.Users.GetByUsername(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _unitOfWork.Users.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcýlar getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetDoctors()
        {
            try
            {
                return _unitOfWork.Users.GetDoctors();
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktorlar getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public IEnumerable<User> GetUsersByRole(UserRole role)
        {
            try
            {
                return _unitOfWork.Users.GetByRole(role);
            }
            catch (Exception ex)
            {
                throw new Exception($"Rol bazlý kullanýcýlar getirilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return false;

                return _unitOfWork.Users.IsUsernameExists(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanýcý adý kontrolü sýrasýnda hata oluþtu: {ex.Message}", ex);
            }
        }

        public bool IsEmailExists(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return _unitOfWork.Users.IsEmailExists(email);
            }
            catch (Exception ex)
            {
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

                if (!PasswordHelper.VerifyPassword(oldPassword, user.PasswordHash))
                    return false;

                user.PasswordHash = PasswordHelper.HashPassword(newPassword);
                _unitOfWork.Users.Update(user);
                return _unitOfWork.Complete() > 0;
            }
            catch (Exception ex)
            {
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