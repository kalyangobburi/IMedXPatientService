using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMedXModels.Input
{
    public class IMedXPatientData
    {
        //[Key]
        //public long RowId { get; set; }
        public string PA { get; set; }
        public string DOC { get; set; }
        public string ICD { get; set; }
        public string NDC { get; set; }
        public double AMT { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
