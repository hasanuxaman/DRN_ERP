using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Procurement.DataSets
{
    
    
    public partial class dsProcTran {
    }
}

namespace DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters
{
    #region tbl_PuTr_Pr_Hdr
    partial class tbl_PuTr_Pr_HdrTableAdapter
    {
    }

    public partial class tbl_PuTr_Pr_HdrTableAdapter
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

    #region tbl_PuTr_Pr_Det
    partial class tbl_PuTr_Pr_DetTableAdapter
    {
    }

    public partial class tbl_PuTr_Pr_DetTableAdapter
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

    #region tbl_Qtn_Log
    partial class tbl_Qtn_LogTableAdapter
    {
    }

    public partial class tbl_Qtn_LogTableAdapter
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

    #region tbl_Qtn_Det
    partial class tbl_Qtn_DetTableAdapter
    {
    }

    public partial class tbl_Qtn_DetTableAdapter
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

    #region tbl_TaC_Log
    partial class tbl_TaC_LogTableAdapter
    {
    }

    public partial class tbl_TaC_LogTableAdapter
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

    #region tbl_Tran_Com
    partial class tbl_Tran_ComTableAdapter
    {
    }

    public partial class tbl_Tran_ComTableAdapter
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

    #region tbl_PuTr_CS_Sum
    partial class tbl_PuTr_CS_SumTableAdapter
    {
    }

    public partial class tbl_PuTr_CS_SumTableAdapter
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

    #region tbl_PuTr_PO_Hdr
    partial class tbl_PuTr_PO_HdrTableAdapter
    {
    }

    public partial class tbl_PuTr_PO_HdrTableAdapter
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

    #region tbl_PuTr_PO_Det
    partial class tbl_PuTr_PO_DetTableAdapter
    {
    }

    public partial class tbl_PuTr_PO_DetTableAdapter
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

    #region tbl_PuTr_Bill_Hdr
    partial class tbl_PuTr_Bill_HdrTableAdapter
    {
    }

    public partial class tbl_PuTr_Bill_HdrTableAdapter
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

    #region tbl_PuTr_Bill_Det
    partial class tbl_PuTr_Bill_DetTableAdapter
    {
    }

    public partial class tbl_PuTr_Bill_DetTableAdapter
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

    #region View_Qtn_Val_Sum
    partial class View_Qtn_Val_SumTableAdapter
    {
    }

    public partial class View_Qtn_Val_SumTableAdapter
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