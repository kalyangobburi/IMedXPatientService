using IMedXModels.Input;
using IMedXUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TestCodes
{
    static class IMedXDataTableTest
    {
        public static void RunDataTableTest()
        {
            List<string> icdData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\ICDData.txt");
            List<string> icdColumnNames = new List<string>() { "PA", "DOC", "ICD" };
            DataTable icdDataTab = DataUtility.BuildDataTableForTabbedData(icdData, "ICDCodes", icdColumnNames);
            Console.WriteLine("The ICD Data in the table is as below");
            Console.WriteLine("PA\tDOC\tICD");
            for (int i = 0; i < icdDataTab.Rows.Count; i++)
            {
                DataRow dr = icdDataTab.Rows[i];
                Console.WriteLine("{0}\t{1}\t{2}", dr["PA"], dr["DOC"], dr["ICD"]);
            }

            List<string> ndcData = DataUtility.ReadFeedDataFileLines(@"F:\MyPKSolutions\TEST\IMEDEX\TestFiles\NDCData.txt");
            System.Console.WriteLine("Contents of NDCData   ");
            foreach (string line in ndcData)
            {
                // Use a tab to indent each line of the file.
                //Console.WriteLine("\t" + line);
                Console.WriteLine(line);
            }
            List<string> ndcColumnNames = new List<string>() { "PA", "NDC", "AMT" };
            //DataTable ndcDataTab = DataUtility.BuildDataTableForDelimitedData(ndcData, "NDCCodes", ndcColumnNames);
            
            DataTable ndcDataTab = IMedXUtility.PrepareNDCDataTable(ndcData, ndcColumnNames);

            Console.WriteLine("The NDC Data in the table is as below");
            Console.WriteLine("MEM\tNDC\tAMT");
            for (int i = 0; i < ndcDataTab.Rows.Count; i++)
            {
                DataRow dr = ndcDataTab.Rows[i];
                Console.WriteLine("{0}\t{1}\t{2}", dr[0], dr[1], dr[2]);
            }
        }
    }
}
