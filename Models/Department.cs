using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAutomation.Models
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "Bölüm adý gereklidir")]
        [StringLength(100, ErrorMessage = "Bölüm adý en fazla 100 karakter olabilir")]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(10, ErrorMessage = "Bölüm kodu en fazla 10 karakter olabilir")]
        public string Code { get; set; }

        // Navigation Properties
        public virtual ICollection<User> Doctors { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}