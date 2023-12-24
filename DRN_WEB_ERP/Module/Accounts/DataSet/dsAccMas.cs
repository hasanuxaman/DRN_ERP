using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Accounts.DataSet {
    
    
    public partial class dsAccMas {
    }
}

namespace DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters
{
    #region tbl_Acc_Fa_Gl_Coa
    partial class tbl_Acc_Fa_Gl_CoaTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Gl_CoaTableAdapter
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

    #region GL_TALLY_NEW
    partial class GL_TALLY_NEWTableAdapter
    {
    }

    public partial class GL_TALLY_NEWTableAdapter
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

    #region tbl_Acc_Grp_Def
    partial class tbl_Acc_Grp_DefTableAdapter
    {
    }

    public partial class tbl_Acc_Grp_DefTableAdapter
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

    #region tbl_Acc_Grp_Code
    partial class tbl_Acc_Grp_CodeTableAdapter
    {
    }

    public partial class tbl_Acc_Grp_CodeTableAdapter
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

    #region tbl_Acc_Fa_Gl_Coa_Grp
    partial class tbl_Acc_Fa_Gl_Coa_GrpTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Gl_Coa_GrpTableAdapter
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

    #region tbl_Acc_Gl_Coa_Grp
    partial class tbl_Acc_Gl_Coa_GrpTableAdapter
    {
    }

    public partial class tbl_Acc_Gl_Coa_GrpTableAdapter
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

    #region tbl_Acc_User_Perm
    partial class tbl_Acc_User_PermTableAdapter
    {
    }

    public partial class tbl_Acc_User_PermTableAdapter
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
