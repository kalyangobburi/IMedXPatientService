using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.ComponentModel;

namespace IMedXUtilities
{
    public static class DataUtility
    {
        public static DataTable ToDataTable<T>(this IQueryable items)
        {
            Type type = typeof(T);

            var props = TypeDescriptor.GetProperties(type)
                                      .Cast<PropertyDescriptor>()
                                      .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                      .Where(propertyInfo => propertyInfo.IsReadOnly == false)
                                      .ToArray();

            var table = new DataTable();

            foreach (var propertyInfo in props)
            {
                table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            foreach (var item in items)
            {
                table.Rows.Add(props.Select(property => property.GetValue(item)).ToArray());
            }

            return table;
        }
        public static List<string> ReadFeedDataFileLines(string feedDataFile)
        {
            try
            {
                return ReadAsLines(feedDataFile).Cast<string>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static IEnumerable<string> ReadAsLines(string filename)
        {
            using (var reader = new System.IO.StreamReader(filename))
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
        }

        public static string GetJSONFormattedDataForTabbed(List<string> feedDataLines, List<string> columnNames)
        {
            string formattedData=" ";
            List<string> newColumnNames = new List<string>();
            try
            {
                int count = columnNames.Count;
                for(int x=0;x<count;x++)
                {
                    string highlet = columnNames[x].Substring(0, 1);
                    string newcolval=columnNames[x].Replace(columnNames[x].Substring(0, 1), highlet.ToLower());
                    newColumnNames.Add(newcolval);
                }
                foreach (string s in feedDataLines)
                {
                    
                    string pastr = (s.Contains(columnNames[0] + ":")) ? s : s.Replace(columnNames[0], columnNames[0] + ":");
                    pastr = pastr.Replace(columnNames[0] + ":", newColumnNames[0] + ":");
                    int cnameslen = columnNames.Count;
                    for (int j = 1; j < cnameslen; j++)
                    {
                        pastr = (pastr.Contains(columnNames[j] + ":")) ? pastr.Replace(columnNames[j], ";" + columnNames[j]) : pastr.Replace(columnNames[j], ";" + columnNames[j] + ":");
                        pastr = pastr.Replace(columnNames[j] + ":", newColumnNames[j] + ":");
                    }
                    pastr = pastr.Replace(";", ",");
                    pastr = pastr.Replace(",", "\",\"").Replace(":", "\":\"");
                    pastr = "{\"" + pastr + "\"},";
                    formattedData += pastr;
                }
                formattedData = formattedData.Replace(" ", "");
                formattedData = "[" + formattedData + "]";
                return formattedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetJSONFormattedData(List<string> feedDataLines, List<string> columnNames)
        {
            string formattedData = " ";
            List<string> newColumnNames = new List<string>();
            try
            {
                int count = columnNames.Count;
                for (int x = 0; x < count; x++)
                {
                    string highlet = columnNames[x].Substring(0, 1);
                    string newcolval = columnNames[x].Replace(columnNames[x].Substring(0, 1), highlet.ToLower());
                    newColumnNames.Add(newcolval);
                }
                int i = 0;
                long leng = feedDataLines.Count;
                foreach (string s in feedDataLines)
                {

                    string pastr = (s.Contains(columnNames[0] + ":")) ? s :(s.Contains(columnNames[0]+";")? s.Replace(columnNames[0]+";", columnNames[0] + ":"):(s.Contains(columnNames[0])?s.Replace(columnNames[0],columnNames[0]+":"):s));
                    pastr = pastr.Replace(columnNames[0] + ":", newColumnNames[0] + ":");
                    int cnameslen = columnNames.Count;
                    for (int j = 1; j < cnameslen; j++)
                    {
                        //pastr = (pastr.Contains(columnNames[j] + ":")) ? (pastr.Contains(";" + columnNames[j]) ? pastr : pastr.Replace(columnNames[j], ";" + columnNames[j])): (pastr.Contains(";" + columnNames[j]) ? pastr.Replace(columnNames[j], columnNames[j] + ":") : pastr.Replace(columnNames[j], ";" + columnNames[j] + ";"));
                        pastr = (pastr.Contains(";" + columnNames[j] + ";")) ? pastr.Replace(";" + columnNames[j] + ";", ";" + columnNames[j] + ":") : (pastr.Contains(columnNames[j] + ";")) ? pastr.Replace(columnNames[j] + ";", columnNames[j] + ":") : pastr;
                        pastr = pastr.Replace(columnNames[j] + ":", newColumnNames[j] + ":");
                    }
                    pastr = pastr.Remove(pastr.Length - 1, 1);
                    if (i < leng)
                    {
                        pastr = pastr.Replace(";", ",");
                        pastr = pastr.Replace(",", "\",\"").Replace(":", "\":\"");
                        pastr = "{\"" + pastr + "\"},";
                    }
                    formattedData += pastr;
                    i++;
                }
                formattedData = formattedData.Replace(" ", "");
                formattedData = "[" + formattedData + "]";
                return formattedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<string> GetFormattedData(List<string> feedDataLines,List<string> columnNames)
        {
            List<string> formattedData = new List<string>();
            try
            {
                foreach (string s in feedDataLines)
                {
                    string pastr = (s.Contains(columnNames[0] + ":")) ? s : s.Replace(columnNames[0], columnNames[0] + ":");
                    int cnameslen = columnNames.Count;
                    for (int j = 1; j < cnameslen; j++)
                    {
                        pastr = (pastr.Contains(columnNames[j] + ":")) ? pastr.Replace(columnNames[j], ";" + columnNames[j]) : pastr.Replace(columnNames[j], ";" + columnNames[j] + ":");
                    }
                    formattedData.Add(pastr);
                }

                return formattedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static System.Data.DataTable BuildDataTableForTabbedData(List<string> feedDataLines, string dataTableName, List<string> columnNames)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable(dataTableName);
                int cnameslen = columnNames.Count;
                for (int i = 0; i < cnameslen; i++)
                {
                    dt.Columns.Add(columnNames[i], typeof(string));
                }

                foreach (string s in feedDataLines)
                {
                    string pastr = (s.Contains(columnNames[0] + ":")) ? s : s.Replace(columnNames[0], columnNames[0] + ":");

                    for (int j = 1; j < cnameslen; j++)
                    {
                        pastr = (pastr.Contains(columnNames[j] + ":")) ? pastr.Replace(columnNames[j], ";" + columnNames[j]) : pastr.Replace(columnNames[j], ";" + columnNames[j] + ":");
                    }
                    string[] insstr = pastr.Split(';');
                    System.Data.DataRow dr = dt.NewRow();
                    for (int k = 0; k < cnameslen; k++)
                    {
                        dr[columnNames[k]] = insstr[k].Split(':')[1].Trim();
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable BuildDataTableForDelimitedData(List<string> feedDataLines, string dataTableName, List<string> columnNames)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable(dataTableName);
                int cnameslen = columnNames.Count;
                for (int i = 0; i < cnameslen; i++)
                {
                    dt.Columns.Add(columnNames[i], typeof(string));
                }

                foreach (string s in feedDataLines)
                {
                    string pastr = (s.Contains(columnNames[0] + ":")) ? s : ((s.Contains(columnNames[0] + ";")) ? s.Replace(columnNames[0] + ";", columnNames[0] + ":") : s.Replace(columnNames[0], columnNames[0] + ":"));

                    for (int j = 1; j < cnameslen; j++)
                    {
                        pastr = (pastr.Contains(columnNames[j] + ":")) ? pastr : ((pastr.Contains(columnNames[j] + ";")) ? pastr.Replace(columnNames[j] + ";", columnNames[j] + ":") : pastr.Replace(columnNames[j], columnNames[j] + ":"));
                    }
                    string[] insstr = pastr.Split(';');
                    System.Data.DataRow dr = dt.NewRow();
                    for (int k = 0; k < cnameslen; k++)
                    {
                        dr[columnNames[k]] = insstr[k].Split(':')[1].Trim();
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
