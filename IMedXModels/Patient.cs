using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXModels
{
    public class Patient
    {
        [Key]
        public long PAID { get; set; }
        public string PA { get; set; }
        public bool IsActive { get; set; }
    }
}
