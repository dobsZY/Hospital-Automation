using System;
using Microsoft.AspNetCore.Http;

namespace HospitalAutomation.Utilities
{
    public static class AuthorizationHelper
    {
        /// <summary>
        /// Yalnızca Admin kullanıcılar için izin kontrolü. Başarısızsa UnauthorizedAccessException fırlatır.
        /// </summary>
        public static void EnsureAdmin(HttpContext? httpContext)
        {
            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated || !SessionManager.IsAdmin)
                throw new UnauthorizedAccessException("Admin yetkisi gereklidir.");
        }

        /// <summary>
        /// Giriş yapmış personel (staff) gereklidir.
        /// </summary>
        public static void EnsureStaff(HttpContext? httpContext)
        {
            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated || !SessionManager.IsStaffLogin)
                throw new UnauthorizedAccessException("Giriş yapmış personel olmalısınız.");
        }

        /// <summary>
        /// Admin veya kendi hesabı olması durumunu doğrular.
        /// </summary>
        public static void EnsureAdminOrSelf(HttpContext? httpContext, int userId)
        {
            if (SessionManager.IsAdmin) return;
            
            var currentUser = SessionManager.CurrentUser;
            if (currentUser != null && currentUser.Id == userId) return;

            throw new UnauthorizedAccessException("Bu işlemi gerçekleştirmek için yetkiniz yok.");
        }

        /// <summary>
        /// Hasta verisini değiştirme yetkisini kontrol eder:
        /// - Hasta kendi kaydını güncelleyebilir.
        /// - Personel için SessionManager.CanModifyPatientData() kontrolü uygulanır.
        /// </summary>
        public static void EnsureCanModifyPatient(HttpContext? httpContext, int patientId)
        {
            if (SessionManager.IsPatientLogin)
            {
                var currentPatient = SessionManager.CurrentPatient;
                if (currentPatient == null || currentPatient.Id != patientId)
                    throw new UnauthorizedAccessException("Bu hastayı güncelleme yetkiniz yok.");
                return;
            }

            if (SessionManager.IsStaffLogin && SessionManager.CanModifyPatientData())
                return;

            throw new UnauthorizedAccessException("Bu işlemi gerçekleştirmek için yetkiniz yok.");
        }

        /// <summary>
        /// Hasta oluşturma kontekstini doğrular:
        /// - Anonim (giriş yapmamış) kullanıcı self-register yapabilir.
        /// - Giriş yapan personel için sadece Admin veya Receptionist oluşturabilir.
        /// </summary>
        public static void EnsureCanCreatePatient(HttpContext? httpContext)
        {
            if (!SessionManager.IsLoggedIn) return; // anonymous self-register allowed
            
            if (SessionManager.IsStaffLogin && 
                (SessionManager.IsAdmin || SessionManager.IsReceptionist)) 
                return;

            throw new UnauthorizedAccessException("Bu işlemi gerçekleştirmek için yetkiniz yok.");
        }

        // Static methods for backward compatibility (will use HttpContextAccessor)
        // These will be called from services that don't have direct HttpContext access
        // We'll need to inject IHttpContextAccessor for these to work properly
        private static IHttpContextAccessor _httpContextAccessor;

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void EnsureAdmin()
        {
            EnsureAdmin(_httpContextAccessor?.HttpContext);
        }

        public static void EnsureStaff()
        {
            EnsureStaff(_httpContextAccessor?.HttpContext);
        }

        public static void EnsureAdminOrSelf(int userId)
        {
            EnsureAdminOrSelf(_httpContextAccessor?.HttpContext, userId);
        }

        public static void EnsureCanModifyPatient(int patientId)
        {
            EnsureCanModifyPatient(_httpContextAccessor?.HttpContext, patientId);
        }

        public static void EnsureCanCreatePatient()
        {
            EnsureCanCreatePatient(_httpContextAccessor?.HttpContext);
        }
    }
}

