using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsDbHelper
    {
        private clsDbHelper()
        {
            
        }

        public static SqlDataAdapter GetAdapter(object tableAdapter)
        {
            Type tableAdapterType = tableAdapter.GetType();
            SqlDataAdapter adapter = (SqlDataAdapter)tableAdapterType.GetProperty("Adapter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tableAdapter, null);

            return adapter;
        }

        public static SqlTransaction OpenTransaction(SqlConnection Conn)
        {
            if (Conn.State == 0)
                Conn.Open();
            return Conn.BeginTransaction();
        }

        public static void CloseTransaction(SqlConnection Conn, SqlTransaction Trn)
        {
            if (Conn.State != 0)
                Conn.Close();
            Trn.Dispose();
            Conn.Dispose();
        }

        public static void CloseTransaction(SqlConnection Conn)
        {
            if (Conn.State != 0)
                Conn.Close();

            Conn.Dispose();

        }
    }
}
