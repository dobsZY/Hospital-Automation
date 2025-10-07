using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Utilities
{
    public static class SessionManager
    {
        private static User _currentUser;
        private static Patient _currentPatient;

        public static User CurrentUser
        {
            get { return _currentUser; }
            private set { _currentUser = value; }
        }

        public static Patient CurrentPatient
        {
            get { return _currentPatient; }
            private set { _currentPatient = value; }
        }

        public static bool IsLoggedIn => CurrentUser != null || CurrentPatient != null;

        public static bool IsPatientLogin => CurrentPatient != null && CurrentUser == null;

        public static bool IsStaffLogin => CurrentUser != null && CurrentPatient == null;

        public static bool IsAdmin => IsStaffLogin && CurrentUser.Role == UserRole.Admin;

        public static bool IsDoctor => IsStaffLogin && CurrentUser.Role == UserRole.Doctor;

        public static bool IsNurse => IsStaffLogin && CurrentUser.Role == UserRole.Nurse;

        public static bool IsReceptionist => IsStaffLogin && CurrentUser.Role == UserRole.Receptionist;

        public static void Login(User user)
        {
            CurrentUser = user;
            CurrentPatient = null; // Personel giriþinde hasta session'ýný temizle
        }

        public static void LoginPatient(Patient patient)
        {
            CurrentPatient = patient;
            CurrentUser = null; // Hasta giriþinde personel session'ýný temizle
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentPatient = null;
        }

        public static bool HasPermission(UserRole requiredRole)
        {
            if (!IsStaffLogin)
                return false;

            // Admin tüm yetkilere sahip
            if (CurrentUser.Role == UserRole.Admin)
                return true;

            return CurrentUser.Role == requiredRole;
        }

        public static bool CanAccessPatientData()
        {
            return IsStaffLogin && (IsAdmin || IsDoctor || IsNurse || IsReceptionist);
        }

        public static bool CanModifyPatientData()
        {
            return IsStaffLogin && (IsAdmin || IsDoctor || IsNurse);
        }

        public static bool CanCreateAppointment()
        {
            return IsStaffLogin && (IsAdmin || IsReceptionist) || IsPatientLogin;
        }

        public static bool CanViewMedicalRecords()
        {
            return IsStaffLogin && (IsAdmin || IsDoctor);
        }

        public static bool CanCreateMedicalRecords()
        {
            return IsStaffLogin && (IsAdmin || IsDoctor);
        }

        public static bool CanViewOwnMedicalRecords()
        {
            return IsPatientLogin;
        }

        public static string GetDisplayName()
        {
            if (IsPatientLogin)
                return CurrentPatient.FullName;
            else if (IsStaffLogin)
                return CurrentUser.FullName;
            return "Misafir";
        }

        public static string GetUserType()
        {
            if (IsPatientLogin)
                return "Hasta";
            else if (IsStaffLogin)
                return CurrentUser.Role.ToString();
            return "";
        }
    }
}