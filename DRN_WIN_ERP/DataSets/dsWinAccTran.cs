using System.Data;
using System.Data.SqlClient;

namespace DRN_WIN_ERP.DataSets {
    
    
    public partial class dsWinAccTran {
    }
}

namespace DRN_WIN_ERP.DataSets.dsWinAccTranTableAdapters
{
    #region tbl_Acc_Fa_Te
    partial class tbl_Acc_Fa_TeTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_TeTableAdapter
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