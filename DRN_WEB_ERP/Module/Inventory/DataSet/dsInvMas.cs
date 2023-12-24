using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Inventory.DataSet {
    
    
    public partial class dsInvMas {
    }
}

namespace DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters
{
    #region tbl_InMa_Item_Det
    partial class tbl_InMa_Item_DetTableAdapter
    {
    }

    public partial class tbl_InMa_Item_DetTableAdapter
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

    #region tbl_InMa_Item_Spec
    partial class tbl_InMa_Item_SpecTableAdapter
    {
    }

    public partial class tbl_InMa_Item_SpecTableAdapter
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

    #region tbl_InMa_Str_Loc_Perm
    partial class tbl_InMa_Str_Loc_PermTableAdapter
    {
    }

    public partial class tbl_InMa_Str_Loc_PermTableAdapter
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