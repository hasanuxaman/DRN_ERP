using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.SYS.DataSet {
    
    
    public partial class dsSys {
    }
}
namespace DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters
{
    #region TBL_USER_INFO
    partial class TBL_USER_INFOTableAdapter
    {
    }

    public partial class TBL_USER_INFOTableAdapter
    {
        public void AttachTransaction(SqlTransaction SqlTrn)
        {
            this.Connection = SqlTrn.Connection;
            foreach (SqlCommand cmd in this.CommandCollection)
            {
                cmd.Transaction = SqlTrn;
            }
        }
    }
    #endregion
}