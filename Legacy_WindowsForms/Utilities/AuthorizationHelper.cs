using System;

namespace HospitalAutomation.Utilities
{
    public static class AuthorizationHelper
    {
        /// <summary>
        /// Yalnýzca Admin kullanýcýlar için izin kontrolü. Baþarýsýzsa UnauthorizedAccessException fýrlatýr.
        /// </summary>
        public static void EnsureAdmin()
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.IsAdmin)
                throw new UnauthorizedAccessException("Admin yetkisi gereklidir.");
        }

        /// <summary>
        /// Giriþ yapmýþ personel (staff) gereklidir.
        /// </summary>
        public static void EnsureStaff()
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.IsStaffLogin)
                throw new UnauthorizedAccessException("Giriþ yapmýþ personel olmalýsýnýz.");
        }

        /// <summary>
        /// Admin veya kendi hesabý olmasý durumunu doðrular.
        /// </summary>
        public static void EnsureAdminOrSelf(int userId)
        {
            if (SessionManager.IsAdmin) return;
            if (SessionManager.IsStaffLogin && SessionManager.CurrentUser != null && SessionManager.CurrentUser.Id == userId) return;

            throw new UnauthorizedAccessException("Bu iþlemi gerçekleþtirmek için yetkiniz yok.");
        }

        /// <summary>
        /// Hasta verisini deðiþtirme yetkisini kontrol eder:
        /// - Hasta kendi kaydýný güncelleyebilir.
        /// - Personel için SessionManager.CanModifyPatientData() kontrolü uygulanýr.
        /// </summary>
        public static void EnsureCanModifyPatient(int patientId)
        {
            if (SessionManager.IsPatientLogin)
            {
                if (SessionManager.CurrentPatient == null || SessionManager.CurrentPatient.Id != patientId)
                    throw new UnauthorizedAccessException("Bu hastayý güncelleme yetkiniz yok.");
                return;
            }

            if (SessionManager.IsStaffLogin && SessionManager.CanModifyPatientData())
                return;

            throw new UnauthorizedAccessException("Bu iþlemi gerçekleþtirmek için yetkiniz yok.");
        }

        /// <summary>
        /// Hasta oluþturma kontekstini doðrular:
        /// - Anonim (giriþ yapmamýþ) kullanýcý self-register yapabilir.
        /// - Giriþ yapan personel için sadece Admin veya Receptionist oluþturabilir.
        /// </summary>
        public static void EnsureCanCreatePatient()
        {
            if (!SessionManager.IsLoggedIn) return; // anonymous self-register allowed
            if (SessionManager.IsStaffLogin && (SessionManager.IsAdmin || SessionManager.IsReceptionist)) return;

            throw new UnauthorizedAccessException("Bu iþlemi gerçekleþtirmek için yetkiniz yok.");
        }
    }
}