using IMedXModels.Input;
using IMedXUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCodes
{
    static class IMedXListTest
    {
        static List<string> icdData = new List<string>();
        static List<string> ndcData = new List<string>();
        public static void RunListTest()
        {
            try
            {
                icdData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\ICDData.txt");
                System.Console.WriteLine("Contents of ICDData  = ");
                foreach (string line in icdData)
                {
                    // Use a tab to indent each line of the file.
                    //Console.WriteLine("\t" + line);
                    Console.WriteLine(line);
                }

                ndcData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\NDCData.txt");
                List<string> ndcColumnNames = new List<string>() { "PA", "NDC", "AMT" };
                List<InputPatientNDC> ndcDataList = IMedXUtility.PrepareNDCEntries(ndcData, ndcColumnNames);
                foreach (string line in ndcData)
                {
                    // Use a tab to indent each line of the file.
                    //Console.WriteLine("\t" + line);
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<InputPatientICD> GetPatientICDDataList()
        {
            try
            {
                icdData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\ICDData.txt");
                List<string> icdColumnNames = new List<string>() { "PA", "DOC", "ICD" };
                List<InputPatientICD> patientICDDataList = IMedXUtility.PrepareICDEntries(icdData, icdColumnNames);
                return patientICDDataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<InputPatientNDC> GetPatientNDCDataList()
        {
            try
            {
                ndcData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\NDCData.txt");
                List<string> ndcColumnNames = new List<string>() { "PA", "NDC", "AMT" };
                List<InputPatientNDC> patientNDCDataList = IMedXUtility.PrepareNDCEntries(ndcData, ndcColumnNames);
                return patientNDCDataList;
            }
            catch (Exception ex)
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
                               AMT = (string.IsNullOrEmpty(j.AMT)) ? Convert.ToDouble(j.AMT) : 0.00
                           });
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
