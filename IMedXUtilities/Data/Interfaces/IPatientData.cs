using IMedXModels;
using IMedXModels.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXUtilities.Interfaces
{
    public interface IPatientData
    {
        //List<PatientICD> GetPatientICDData();
        List<IMedXPatientData> GetPatientData();
        List<IMedXPatientData> GetPatientDataRange(DateTime startDate, DateTime endTime);
        List<ICDOUT> GetPatientICDDataRange(DateTime startDate, DateTime endDate);
        List<NDCOUT> GetPatientNDCDataRange(DateTime startDate, DateTime endDate);
    }
}
