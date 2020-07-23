using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMedXUtilities
{
    public static class DBConnectify
    {
        public static string DbConnectionString { get; set; } = @"Server=.\SQLEXPRESS;Database=TestDB;UID=sa;PWD=Test@123";
        public static System.Data.DataTable graphResultTable { get; set; }

        public static bool InsertBulk(System.Data.DataTable dataTable, string connectionstring, string destinationtable, List<string> sourceColumns, List<string> destinationColumns)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionstring))
            {
                System.Data.SqlClient.SqlTransaction transaction = null;
                connection.Open();
                try
                {
                    if (sourceColumns.Count == destinationColumns.Count)
                    {
                        int count = sourceColumns.Count;
                        transaction = connection.BeginTransaction();
                        using (var sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection, System.Data.SqlClient.SqlBulkCopyOptions.TableLock | System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity, transaction))
                        {
                            sqlBulkCopy.DestinationTableName = destinationtable;
                            for (int i = 0; i < count; i++)
                            {
                                sqlBulkCopy.ColumnMappings.Add(sourceColumns[i], destinationColumns[i]);
                            }
                            sqlBulkCopy.WriteToServer(dataTable);
                        }
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception("Source Columns Do not match with Destination Columns");
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static long MoveTempData(string tmpTableName,string destinationTableName)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(DbConnectionString);                
                SqlCommand cmd = new SqlCommand(DbConnectionString);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_MoveTempData";
                cmd.Parameters.Add(new SqlParameter("@tmpTableName", tmpTableName));
                cmd.Parameters.Add(new SqlParameter("@destinationTableName", destinationTableName));
                sqlConn.Open();
                int count= cmd.ExecuteNonQuery();
                sqlConn.Close();
                return count;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
