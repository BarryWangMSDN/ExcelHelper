using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetWorking.Helper
{
    public class SqlDBHelper
    { 
        // Connection string for using Windows Authentication.
        private static string connectionString =
            @"Server=tcp:barryengineering.database.windows.net,1433;Initial Catalog=EngineeringDB;Persist Security Info=False;User ID=azureuser;Password=Azure1234567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // This is an example connection string for using SQL Server Authentication.
        // private string connectionString =
        //     @"Data Source=YourServerName\YourInstanceName;Initial Catalog=DatabaseName; User Id=XXXXX; Password=XXXXX";

        public static string ConnectionString { get => connectionString; set => connectionString = value; }

        public static void QueryDataInDB()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    String sql = "select * from SalesLT.Address";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        public static async Task insertcollection(ObservableCollection<MemberInfo> membercol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    
                    String sql = "insert into dbo.EngineerDaysOff(ChineseName, MSAlias, WSAlias, Technology, TeamGroup, Hour) " +
                  "VALUES (@ChineseName, @MSAlias, @WSAlias, @Technology, @TeamGroup, @Hour) ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@ChineseName", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@MSAlias", System.Data.SqlDbType.NChar, 10);
                    cmd.Parameters.Add("@WSAlias", System.Data.SqlDbType.NChar, 10);
                    cmd.Parameters.Add("@Technology", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@TeamGroup", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@Hour", System.Data.SqlDbType.Int, 50);
                    conn.Open();
                    foreach (var member in membercol)
                    {
                       cmd.Parameters["@ChineseName"].Value = member.UserName;
                       cmd.Parameters["@MSAlias"].Value = member.Alias;
                       cmd.Parameters["@WSAlias"].Value = member.WsAlias;
                       cmd.Parameters["@Technology"].Value = member.Technology;
                       cmd.Parameters["@TeamGroup"].Value = member.Group;
                       cmd.Parameters["@Hour"].Value = member.VacationHour;
                       await cmd.ExecuteNonQueryAsync();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
