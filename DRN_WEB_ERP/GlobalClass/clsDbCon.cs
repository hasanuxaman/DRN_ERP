using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsDbCon
    {
        string ConnectionString;
        SqlDataReader reader;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public void ExecuteSQLStmt(string sql)
        {
            // Open the connection
            if (conn.State == ConnectionState.Open) conn.Close();

            ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString(); //ConfigurationSettings.AppSettings["DALConStr"].ToString();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace + "Source==" + ex.Source + "===End Message==" + ex.Message);
            }
        }//ExecuteSQLStmt

        public void ExecuteSysSQLStmt(string sql)
        {
            // Open the connection
            if (conn.State == ConnectionState.Open) conn.Close();

            ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SYSConStr"].ToString(); //ConfigurationSettings.AppSettings["SYSConStr"].ToString();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace + "Source==" + ex.Source + "===End Message==" + ex.Message);
            }
        }//ExecuteSysSQLStmt
    }        
}