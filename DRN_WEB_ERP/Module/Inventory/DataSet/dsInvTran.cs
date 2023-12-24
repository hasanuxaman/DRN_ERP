using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Inventory.DataSet {
    public partial class dsInvTran {
    }
}

namespace DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters
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

    #region tbl_InTr_Pro_Sr_Hdr
    partial class tbl_InTr_Pro_Sr_HdrTableAdapter
    {
    }

    public partial class tbl_InTr_Pro_Sr_HdrTableAdapter
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

    #region tbl_InTr_Pro_Sr_Det
    partial class tbl_InTr_Pro_Sr_DetTableAdapter
    {
    }

    public partial class tbl_InTr_Pro_Sr_DetTableAdapter
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

    #region mechanical
    partial class mechanicalTableAdapter
    {
    }

    public partial class mechanicalTableAdapter
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

    #region View_InTr_Trn_Det_Stock_Bal
    partial class View_InTr_Trn_Det_Stock_BalTableAdapter
    {
    }

    public partial class View_InTr_Trn_Det_Stock_BalTableAdapter
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