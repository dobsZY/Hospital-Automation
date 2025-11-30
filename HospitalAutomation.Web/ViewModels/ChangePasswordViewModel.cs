using System.ComponentModel.DataAnnotations;

namespace HospitalAutomation.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut şifre gereklidir")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre gereklidir")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string ConfirmPassword { get; set; }
    }
}

