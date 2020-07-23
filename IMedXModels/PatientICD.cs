using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXModels
{
    public class PatientICD
    {
        [Key]
        public long PAICDId { get; set; }
        public long PAID { get; set; }
        public string DOC { get; set; }
        public string ICD { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
