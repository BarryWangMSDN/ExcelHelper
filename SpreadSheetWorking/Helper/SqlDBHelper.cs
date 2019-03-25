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

        // Connection string for using Windows Authentication.(AzureDB)
        //private static string connectionString =
        //    @"Server=tcp:barryengineering.database.windows.net,1433;Initial Catalog=EngineeringDB;Persist Security Info=False;User ID=azureuser;Password=Azure1234567;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //switch to use local network database. My Azure database now time out...such a pity
        private static string connectionString = @"Data Source=172.17.16.133;Initial Catalog=ServerDB;User ID=sa;Password=Password001!";
        // This is an example connection string for using SQL Server Authentication.
        // private string connectionString =
        //     @"Data Source=YourServerName\YourInstanceName;Initial Catalog=DatabaseName; User Id=XXXXX; Password=XXXXX";

        public static string ConnectionString { get => connectionString; set => connectionString = value; }

        private static ObservableCollection<MemberInfo> commonmemlist;

        public static ObservableCollection<MemberInfo> CommonMemList
        {
            get { return commonmemlist; }
            set { commonmemlist = value; }
        }


        public static ObservableCollection<MemberInfo> QueryDataInDB()
        {
            const string GetMembersQuery = "select ChineseName, MSAlias, WSAlias, Technology, TeamGroup, Hour" +
                                           " from dbo.EngineerDaysOff ";
            var members = new ObservableCollection<MemberInfo>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetMembersQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while(reader.Read())
                                {
                                    var memberinfo = new MemberInfo();
                                    memberinfo.UserName = reader.GetString(0);
                                    memberinfo.Alias = reader.GetString(1);
                                    memberinfo.WsAlias = reader.GetString(2);
                                    memberinfo.Technology = reader.GetString(3);
                                    memberinfo.Group = reader.GetString(4);
                                    memberinfo.VacationHour = reader.GetInt32(5);
                                    members.Add(memberinfo);
                                }
                            }
                        }
                    }
                 
                }
                return members;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
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

        public static void DeleteItemFromDB(string alias)
        {
             string DeleteMemberQuery = "delete from dbo.EngineerDaysOff" +
                "where MSAlias='" + alias + "'";
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = DeleteMemberQuery;
                            cmd.ExecuteNonQuery();
                        }
                    }

                }              
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }         
        }

        public static void DeleteItemFromMemory(string alias)
        {
            var found = commonmemlist.Where(s => s.Alias == alias);
            foreach (var item in found)
            commonmemlist.Remove(item);
        }

        public static void UpdateItemFromDB(string alias,int hour)
        {
            string UpdateHourQuery= "Update dbo.EngineerDaysOff set Hour=(Hour-"+hour+")"+
                "where MSAlias='" + alias + "'";
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = UpdateHourQuery;
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
        }

        public static void UpdateItemFromCollection(int index, int hour,ObservableCollection<MemberInfo> col)
        {
            col[index].VacationHour -= hour;                    
        }

    }
}
