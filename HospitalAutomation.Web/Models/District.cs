using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HospitalAutomation.Models
{
    public class District : BaseEntity
    {
        [Required(ErrorMessage = "İlçe adı gereklidir")]
        [StringLength(100, ErrorMessage = "İlçe adı en fazla 100 karakter olabilir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Şehir seçimi gereklidir")]
        public int CityId { get; set; }

        // Navigation Properties
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        public District()
        {
            Patients = new HashSet<Patient>();
        }
    }
}

