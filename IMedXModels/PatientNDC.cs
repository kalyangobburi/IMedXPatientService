using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXModels
{
    public class PatientNDC
    {
        [Key]
        public long PANDCId { get; set; }
        public long PAID { get; set; }
        public string NDC { get; set; }
        public double AMT { get; set; }
    }
}
