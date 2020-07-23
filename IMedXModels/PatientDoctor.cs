using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXModels
{
    public class PatientDoctor
    {
        [Key]
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
        public string DoctorName { get; set; }
        //public bool IsActive { get; set; }
    }
}
