using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Transport.DataSet {
    
    
    public partial class dsStaffMas {
    }
}

namespace DRN_WEB_ERP.Module.Transport.DataSet.dsStaffMasTableAdapters
{
    #region tbl_TrTr_Staff_Mas
    partial class tbl_TrTr_Staff_MasTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_MasTableAdapter
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

    #region tbl_TrTr_Staff_Adr
    partial class tbl_TrTr_Staff_AdrTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_AdrTableAdapter
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

    #region tbl_TrTr_Staff_Ext
    partial class tbl_TrTr_Staff_ExtTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_ExtTableAdapter
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

    #region tbl_TrTr_Staff_Qual
    partial class tbl_TrTr_Staff_QualTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_QualTableAdapter
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

    #region tbl_TrTr_Staff_Exp
    partial class tbl_TrTr_Staff_ExpTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_ExpTableAdapter
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

    #region tbl_TrTr_Staff_Office
    partial class tbl_TrTr_Staff_OfficeTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_OfficeTableAdapter
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

    #region tbl_TrTr_Staff_Img
    partial class tbl_TrTr_Staff_ImgTableAdapter
    {
    }

    public partial class tbl_TrTr_Staff_ImgTableAdapter
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