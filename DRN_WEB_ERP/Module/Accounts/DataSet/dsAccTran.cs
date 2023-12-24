using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Accounts.DataSet {
    
    
    public partial class dsAccTran {
        
    }
}
namespace DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters
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

    #region tbl_Acc_Jv_Hold
    partial class tbl_Acc_Jv_HoldTableAdapter
    {
    }

    public partial class tbl_Acc_Jv_HoldTableAdapter
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

    #region Customer_Opening_Balance
    partial class Customer_Opening_BalanceTableAdapter
    {
    }

    public partial class Customer_Opening_BalanceTableAdapter
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

    #region tbl_Acc_Fa_Te_Wh
    partial class tbl_Acc_Fa_Te_WhTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Te_WhTableAdapter
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

    #region tbl_Acc_Fa_Te_Wd
    partial class tbl_Acc_Fa_Te_WdTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Te_WdTableAdapter
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

    #region tbl_Acc_Chq_Voucher
    partial class tbl_Acc_Chq_VoucherTableAdapter
    {
    }

    public partial class tbl_Acc_Chq_VoucherTableAdapter
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

    #region tbl_Acc_Fa_Te_Wh_Mrr
    partial class tbl_Acc_Fa_Te_Wh_MrrTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Te_Wh_MrrTableAdapter
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

    #region tbl_Acc_Fa_Te_Wd_Mrr
    partial class tbl_Acc_Fa_Te_Wd_MrrTableAdapter
    {
    }

    public partial class tbl_Acc_Fa_Te_Wd_MrrTableAdapter
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

    #region Tally_Bank_Closing_Bal_31_10_19
    partial class Tally_Bank_Closing_Bal_31_10_19TableAdapter
    {
    }

    public partial class Tally_Bank_Closing_Bal_31_10_19TableAdapter
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

    #region TALLY_ALL_GL_CLOSE_BAL_31_10_19
    partial class TALLY_ALL_GL_CLOSE_BAL_31_10_19TableAdapter
    {
    }

    public partial class TALLY_ALL_GL_CLOSE_BAL_31_10_19TableAdapter
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

    #region TALLY_ALL_GL_CLOSE_BAL_31_10_19_Ext
    partial class TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtTableAdapter
    {
    }

    public partial class TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtTableAdapter
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

    #region View_GL_Close_Bal
    partial class View_GL_Close_BalTableAdapter
    {
    }

    public partial class View_GL_Close_BalTableAdapter
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

    #region Tally_Overhead_GL_List
    partial class Tally_Overhead_GL_ListTableAdapter
    {
    }

    public partial class Tally_Overhead_GL_ListTableAdapter
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

    #region Tally_Overhead_GL_List_Ext
    partial class Tally_Overhead_GL_List_ExtTableAdapter
    {
    }

    public partial class Tally_Overhead_GL_List_ExtTableAdapter
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

    #region Tally_Accounts_Payable_31_10_19
    partial class Tally_Accounts_Payable_31_10_19TableAdapter
    {
    }

    public partial class Tally_Accounts_Payable_31_10_19TableAdapter
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

    #region Tally_Accounts_Payable_31_10_19_Ext
    partial class Tally_Accounts_Payable_31_10_19_ExtTableAdapter
    {
    }

    public partial class Tally_Accounts_Payable_31_10_19_ExtTableAdapter
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

    #region Temp_View_Acc_Fa_Gl_Coa_Unassigned_Grp
    partial class Temp_View_Acc_Fa_Gl_Coa_Unassigned_GrpTableAdapter
    {
    }

    public partial class Temp_View_Acc_Fa_Gl_Coa_Unassigned_GrpTableAdapter
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

    #region Supplier_Balance_Tally_31_10_2019
    partial class Supplier_Balance_Tally_31_10_2019TableAdapter
    {
    }

    public partial class Supplier_Balance_Tally_31_10_2019TableAdapter
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
