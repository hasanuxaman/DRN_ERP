using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Procurement.DataSets
{
    
    
    public partial class dsProcMas {
    }
}

namespace DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters
{
    #region tbl_PuMa_Par_Acc
    partial class tbl_PuMa_Par_AccTableAdapter
    {
    }

    public partial class tbl_PuMa_Par_AccTableAdapter
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

    #region tbl_PuMa_Par_Adr
    partial class tbl_PuMa_Par_AdrTableAdapter
    {
    }

    public partial class tbl_PuMa_Par_AdrTableAdapter
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

    #region Accounts_Payable
    partial class Accounts_PayableTableAdapter
    {
    }

    public partial class Accounts_PayableTableAdapter
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

    #region tbl_PuMa_Par_Adr_Qtn
    partial class tbl_PuMa_Par_Adr_QtnTableAdapter
    {
    }

    public partial class tbl_PuMa_Par_Adr_QtnTableAdapter
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