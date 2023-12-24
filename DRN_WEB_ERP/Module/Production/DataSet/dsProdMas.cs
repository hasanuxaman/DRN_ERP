using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Production.DataSet {
    
    
    public partial class dsProdMas {
    }
}

namespace DRN_WEB_ERP.Module.Production.DataSet.dsProdMasTableAdapters
{
    #region tbl_Prod_Item
    partial class tbl_Prod_ItemTableAdapter
    {
    }

    public partial class tbl_Prod_ItemTableAdapter
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

    #region tbl_Prod_Recipe
    partial class tbl_Prod_RecipeTableAdapter
    {
    }

    public partial class tbl_Prod_RecipeTableAdapter
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