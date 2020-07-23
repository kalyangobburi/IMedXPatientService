using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMedXUtilities.Interfaces
{
    public interface IMasterTransaction
    {
        string SetMasterStructure(string companyname, List<string> columnNames);
        bool SetMasterData(string companyName, DataTable fileData);
        DataTable GetChartMasterData(List<string> columnNames);
    }
}
