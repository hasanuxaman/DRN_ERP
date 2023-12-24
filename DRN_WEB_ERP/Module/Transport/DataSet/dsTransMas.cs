using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Transport.DataSet {
    
    
    public partial class dsTransMas {
    }
}

namespace DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters
{
    #region tbl_TrTr_Loc_Mas
    partial class tbl_TrTr_Loc_MasTableAdapter
    {
    }

    public partial class tbl_TrTr_Loc_MasTableAdapter
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

    #region tbl_Trn_Vsl_Type
    partial class tbl_TrTr_Vsl_TypeTableAdapter
    {
    }

    public partial class tbl_TrTr_Vsl_TypeTableAdapter
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

    #region tbl_TrTr_Vsl_Mas_Ext
    partial class tbl_TrTr_Vsl_Mas_ExtTableAdapter
    {
    }

    public partial class tbl_TrTr_Vsl_Mas_ExtTableAdapter
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

    #region tbl_TrTr_Vsl_Pic
    partial class tbl_TrTr_Vsl_PicTableAdapter
    {
    }

    public partial class tbl_TrTr_Vsl_PicTableAdapter
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

    #region tbl_TrTr_Load_Slip
    partial class tbl_TrTr_Load_SlipTableAdapter
    {
    }

    public partial class tbl_TrTr_Load_SlipTableAdapter
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

