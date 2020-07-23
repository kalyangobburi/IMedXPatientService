using IMedXModels;
using IMedXModels.Input;
using IMedXUtilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IMedXUtilities.Data.Repository
{
    public class PatientICDManager : IPatientData
    {
        readonly IMedXDBContext medXDBContext = new IMedXDBContext();

        public List<IMedXPatientData> GetPatientData()
        {
            try
            {
                return medXDBContext.IMedXPatientDatas.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<IMedXPatientData> GetPatientDataRange(DateTime startDate, DateTime endTime)
        {
            try
            {
                if (startDate > endTime)
                {
                    throw new Exception("Start Time should be Less than the End Time");
                }
                else
                {
                    return medXDBContext.IMedXPatientDatas.Where(m => (m.CreatedDate >= startDate) && (m.CreatedDate <= endTime)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ICDOUT> GetPatientICDDataRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var resset = medXDBContext.IMedXPatientDatas.Where(m => (m.CreatedDate >= startDate) && (m.CreatedDate <= endDate)).ToList();
                var result = (from i in resset
                              group i by i.ICD into g
                              select (new ICDOUT
                              {
                                  ICD = g.First().ICD,
                                  AMT = g.Sum(s => s.AMT)
                              })).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NDCOUT> GetPatientNDCDataRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var resset = medXDBContext.IMedXPatientDatas.Where(m => (m.CreatedDate >= startDate) && (m.CreatedDate <= endDate)).ToList();
                var result = (from i in resset
                              group i by i.NDC into g
                              select (new NDCOUT
                              {
                                  NDC = g.First().NDC,
                                  AMT = g.Sum(s => s.AMT)
                              })).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
