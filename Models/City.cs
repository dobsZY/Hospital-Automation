using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HospitalAutomation.Models
{
    public class City : BaseEntity
    {
        [Required(ErrorMessage = "Þehir adý gereklidir")]
        [StringLength(100, ErrorMessage = "Þehir adý en fazla 100 karakter olabilir")]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "Þehir kodu en fazla 10 karakter olabilir")]
        public string Code { get; set; }

        public string Region { get; set; } // Bölge (Marmara, Akdeniz, vs.)

        // Navigation Properties
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public City()
        {
            Districts = new HashSet<District>();
            Patients = new HashSet<Patient>();
        }
    }
}