using IMedXModels.Input;
using IMedXUtilities;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TestCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<InputPatientICD> inputICDList = IMedXListTest.GetPatientICDDataList();
                foreach(InputPatientICD p in inputICDList)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", p.PA, p.DOC, p.ICD);
                }

                List<InputPatientNDC> inputNDCList = IMedXListTest.GetPatientNDCDataList();
                foreach (InputPatientNDC p in inputNDCList)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", p.PA, p.NDC, p.AMT);
                }

                List<IMedXModels.Input.IMedXPatientData> patientData= IMedXUtility.MergePatientData(inputICDList, inputNDCList);
                foreach(var x in patientData)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", x.PA, x.DOC, x.ICD, x.NDC, x.AMT);
                }

                DataTable patdt = IMedXUtility.MakePatientDataTable(patientData);
                string dataConnection = "Server=.\\SQLEXPRESS;Database=IMedXHR;Trusted_Connection=True";
                List<string> patientDataColumns = new List<string>() { "PA", "DOC", "ICD", "NDC", "AMT", "CreatedDate" };
                DBConnectify.InsertBulk(patdt, dataConnection, "IMedXPatientData", patientDataColumns, patientDataColumns);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static List<IMedXPatientData> MergePatientData(List<InputPatientICD> inputICDList, List<InputPatientNDC> inputNDCList)
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
                               AMT = (string.IsNullOrEmpty(j.AMT)) ? Convert.ToDouble(j.AMT) : 0.00
                           });
                return list.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
