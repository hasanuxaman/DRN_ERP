using System.Data;
using System.Data.SqlClient;

namespace DRN_WIN_ERP.DataSets {
    
    
    public partial class dsWinInvTran {
    }
}


namespace DRN_WIN_ERP.DataSets.dsWinInvTranTableAdapters
{
    #region tbl_InTr_Trn_Hdr
    partial class tbl_InTr_Trn_HdrTableAdapter
    {
    }

    public partial class tbl_InTr_Trn_HdrTableAdapter
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

    #region tbl_InTr_Trn_Det
    partial class tbl_InTr_Trn_DetTableAdapter
    {
    }

    public partial class tbl_InTr_Trn_DetTableAdapter
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

    #region tbl_InMa_Stk_Ctl
    partial class tbl_InMa_Stk_CtlTableAdapter
    {
    }

    public partial class tbl_InMa_Stk_CtlTableAdapter
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