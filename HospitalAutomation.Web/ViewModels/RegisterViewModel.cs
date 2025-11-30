using System;
using System.ComponentModel.DataAnnotations;
using HospitalAutomation.Models.Enums;

namespace HospitalAutomation.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "TC Kimlik No gereklidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır")]
        [Display(Name = "TC Kimlik No")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Ad gereklidir")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi gereklidir")]
        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }
    }
}

