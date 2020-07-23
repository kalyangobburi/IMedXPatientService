using IMedXModels.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IMedXUtilities
{
   public static class IMedXUtility
    {
        public static List<InputPatientICD> PrepareICDEntries(List<string> icdData, List<string> icdColumnNames)
        {
            try
            {
                string icdDataStr = IMedXUtilities.DataUtility.GetJSONFormattedDataForTabbed(icdData, icdColumnNames).Trim();
                //var settings = new JsonSerializerSettings
                //{
                //    NullValueHandling = NullValueHandling.Ignore,
                //    MissingMemberHandling = MissingMemberHandling.Ignore
                //};
                List<InputPatientICD> tmpPtData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InputPatientICD>>(icdDataStr);
                return tmpPtData;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable PrepareICDDataTable(List<string> icdData,List<string> icdColumnNames)
        {
            try
            {
                return DataUtility.BuildDataTableForTabbedData(icdData, "InputPatientICD", icdColumnNames);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //public static DataTable PrepareICDEntriesTable(List<string> icdData, List<string> icdColumnNames)
        //{
        //    DataTable dtICD = new DataTable();
        //    try
        //    {
        //        for(int i=0;i<icdColumnNames.Count;i++)
        //        {
        //            dtICD.Columns.Add(icdColumnNames[i], typeof(string));
        //        }
        //        string icdDataStr = IMedXUtilities.DataUtility.GetJSONFormattedData(icdData, icdColumnNames).Trim();
        //        var settings = new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Ignore,
        //            MissingMemberHandling = MissingMemberHandling.Ignore
        //        };
        //        dtICD = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(icdDataStr, settings);
        //        return dtICD;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static List<InputPatientNDC> PrepareNDCEntries(List<string> ndcData, List<string> ndcColumnNames)
        {
            try
            {
                string ndcDataStr = IMedXUtilities.DataUtility.GetJSONFormattedData(ndcData, ndcColumnNames).Trim();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<InputPatientNDC> tmpPtData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InputPatientNDC>>(ndcDataStr, settings);
                return tmpPtData;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public static DataTable PrepareNDCEntriesTable(List<string> ndcData, List<string> ndcColumnNames)
        //{
        //    DataTable dtNDC = new DataTable();
        //    try
        //    {
        //        for (int i = 0; i < ndcColumnNames.Count; i++)
        //        {
        //            dtNDC.Columns.Add(ndcColumnNames[i], typeof(string));
        //        }
        //        string ndcDataStr = IMedXUtilities.DataUtility.GetJSONFormattedData(ndcData, ndcColumnNames).Trim();
        //        var settings = new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Ignore,
        //            MissingMemberHandling = MissingMemberHandling.Ignore
        //        };
        //        dtNDC = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ndcDataStr, settings);
        //        return dtNDC;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static DataTable PrepareNDCDataTable(List<string> ndcData,List<string> ndcColumnNames)
        {
            try
            {
               return DataUtility.BuildDataTableForDelimitedData(ndcData, "InputPatientNDC", ndcColumnNames);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<IMedXPatientData> MergePatientData(List<InputPatientICD> inputICDList, List<InputPatientNDC> inputNDCList)
        {
            int x = 0;
            try
            {
                var list = from i in inputICDList
                           join j in inputNDCList on i.PA equals j.PA
                           select (new IMedXPatientData()
                           {
                               //RowId = x + 1,
                               PA = i.PA,
                               DOC = i.DOC,
                               ICD = i.ICD,
                               NDC = j.NDC,
                               AMT = (!string.IsNullOrEmpty(j.AMT)) ? Convert.ToDouble(j.AMT) : 0.00,
                               CreatedDate=DateTime.Now
                           });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable MakePatientDataTable(List<IMedXPatientData> patientData)
        {
            try
            {
                DataTable dt = new DataTable();
                //dt.Columns.Add("RowId");
                dt.Columns.Add("PA");
                dt.Columns.Add("DOC");
                dt.Columns.Add("ICD");
                dt.Columns.Add("NDC");
                dt.Columns.Add("AMT");
                dt.Columns.Add("CreatedDate");

                dt = DataUtility.ToDataTable<IMedXPatientData>(patientData.AsQueryable());
                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
