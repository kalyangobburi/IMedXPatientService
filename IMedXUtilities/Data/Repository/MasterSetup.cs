
using IMedXUtilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXUtilities.Repository
{
    public class MasterSetup : IMasterTransaction
    {
        public DataTable GetChartMasterData(List<string> columnNames)
        {
            DataTable result = new DataTable();
            try
            {
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SetMasterData(string companyName, DataTable fileData)
        {
            try
            {
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string SetMasterStructure(string companyname, List<string> columnNames)
        {
            string companyCode = "";
            try
            {
                return companyCode;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
