using System.Security.Claims;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace HospitalAutomation.Utilities
{
    public static class SessionManager
    {
        private const string UserIdClaim = "UserId";
        private const string UsernameClaim = "Username";
        private const string RoleClaim = "Role";
        private const string PatientIdClaim = "PatientId";
        private const string FullNameClaim = "FullName";
        private const string ProfilePictureClaim = "ProfilePicture";

        private static IHttpContextAccessor _httpContextAccessor;

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static HttpContext HttpContext => _httpContextAccessor?.HttpContext;

        public static User GetCurrentUser(HttpContext httpContext)
        {
            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated)
                return null;

            var userIdClaim = httpContext.User.FindFirst(UserIdClaim);
            if (userIdClaim == null)
                return null;

            // Note: In a real implementation, you might want to load the full user from database
            // For now, we'll create a minimal user object from claims
            var roleClaim = httpContext.User.FindFirst(RoleClaim);
            if (roleClaim == null || !Enum.TryParse<UserRole>(roleClaim.Value, out var role))
                return null;

            return new User
            {
                Id = int.Parse(userIdClaim.Value),
                Username = httpContext.User.FindFirst(UsernameClaim)?.Value,
                Role = role
            };
        }

        public static Patient GetCurrentPatient(HttpContext httpContext)
        {
            if (httpContext?.User == null || !httpContext.User.Identity.IsAuthenticated)
                return null;

            var patientIdClaim = httpContext.User.FindFirst(PatientIdClaim);
            if (patientIdClaim == null)
                return null;

            // Note: In a real implementation, you might want to load the full patient from database
            return new Patient
            {
                Id = int.Parse(patientIdClaim.Value),
                NationalId = httpContext.User.FindFirst(ClaimTypes.Name)?.Value
            };
        }

        public static string GetDisplayName(HttpContext httpContext)
        {
            if (httpContext?.User == null)
                return "Misafir";

            var fullNameClaim = httpContext.User.FindFirst(FullNameClaim);
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            var usernameClaim = httpContext.User.FindFirst(UsernameClaim);
            if (usernameClaim != null)
                return usernameClaim.Value;

            return "Misafir";
        }

        public static string GetUserType(HttpContext httpContext)
        {
            if (httpContext?.User == null)
                return "";
                
            if (httpContext.User.FindFirst(PatientIdClaim) != null)
                return "Hasta";
            
            if (httpContext.User.FindFirst(UserIdClaim) != null)
            {
                var roleClaim = httpContext.User.FindFirst(RoleClaim);
                if (roleClaim != null)
                    return roleClaim.Value;
            }

            return "";
        }

        // Static properties for backward compatibility
        public static User CurrentUser => GetCurrentUser(HttpContext);
        public static Patient CurrentPatient => GetCurrentPatient(HttpContext);
        
        public static bool IsLoggedIn
        {
            get
            {
                var context = HttpContext;
                return context?.User != null && context.User.Identity.IsAuthenticated;
            }
        }
        
        public static bool IsPatientLogin
        {
            get
            {
                var context = HttpContext;
                if (!IsLoggedIn || context == null) return false;
                return context.User.FindFirst(PatientIdClaim) != null;
            }
        }
        
        public static bool IsStaffLogin
        {
            get
            {
                var context = HttpContext;
                if (!IsLoggedIn || context == null) return false;
                return context.User.FindFirst(UserIdClaim) != null;
            }
        }
        
        public static bool IsAdmin
        {
            get
            {
                var context = HttpContext;
                return IsStaffLogin && context.User.HasClaim(RoleClaim, UserRole.Admin.ToString());
            }
        }
        
        public static bool IsDoctor
        {
            get
            {
                var context = HttpContext;
                return IsStaffLogin && context.User.HasClaim(RoleClaim, UserRole.Doctor.ToString());
            }
        }
        
        public static bool IsNurse
        {
            get
            {
                var context = HttpContext;
                return IsStaffLogin && context.User.HasClaim(RoleClaim, UserRole.Nurse.ToString());
            }
        }
        
        public static bool IsReceptionist
        {
            get
            {
                var context = HttpContext;
                return IsStaffLogin && context.User.HasClaim(RoleClaim, UserRole.Receptionist.ToString());
            }
        }

        public static string GetDisplayName() => GetDisplayName(HttpContext);
        public static string GetUserType() => GetUserType(HttpContext);

        public static string GetProfilePicture()
        {
            var context = HttpContext;
            if (context?.User == null) return null;
            return context.User.FindFirst(ProfilePictureClaim)?.Value;
        }

        public static bool HasPermission(UserRole requiredRole)
        {
            if (!IsStaffLogin)
                return false;

            // Admin tüm yetkilere sahip
            var currentUser = CurrentUser;
            if (currentUser?.Role == UserRole.Admin)
                return true;

            return currentUser?.Role == requiredRole;
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
            return (IsStaffLogin && (IsAdmin || IsReceptionist)) || IsPatientLogin;
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

        // Extension methods for easier access
        public static ClaimsPrincipal CreateClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(UserIdClaim, user.Id.ToString()),
                new Claim(UsernameClaim, user.Username),
                new Claim(RoleClaim, user.Role.ToString()),
                new Claim(FullNameClaim, user.FullName ?? $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ProfilePictureClaim, user.ProfilePicturePath ?? "")
            };

            var identity = new ClaimsIdentity(claims, "Cookie");
            return new ClaimsPrincipal(identity);
        }

        public static ClaimsPrincipal CreateClaimsPrincipal(Patient patient)
        {
            var claims = new List<Claim>
            {
                new Claim(PatientIdClaim, patient.Id.ToString()),
                new Claim(ClaimTypes.Name, patient.NationalId),
                new Claim(FullNameClaim, patient.FullName ?? $"{patient.FirstName} {patient.LastName}")
            };

            var identity = new ClaimsIdentity(claims, "Cookie");
            return new ClaimsPrincipal(identity);
        }
    }
}

