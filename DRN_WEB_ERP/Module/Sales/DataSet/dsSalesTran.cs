using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Sales.DataSet {
    
    
    public partial class dsSalesTran {
    }
}

namespace DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters
{
    #region tblSalesOrderHdr
    partial class tblSalesOrderHdrTableAdapter
    {
    }

    public partial class tblSalesOrderHdrTableAdapter
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

    #region tblSalesOrderDet
    partial class tblSalesOrderDetTableAdapter
    {
    }

    public partial class tblSalesOrderDetTableAdapter
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

    #region tblSalesOrdDelHdr
    partial class tblSalesOrdDelHdrTableAdapter
    {
    }

    public partial class tblSalesOrdDelHdrTableAdapter
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

    #region tblSalesOrdDelDet
    partial class tblSalesOrdDelDetTableAdapter
    {
    }

    public partial class tblSalesOrdDelDetTableAdapter
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

    #region tblSalesCreditRealize
    partial class tblSalesCreditRealizeTableAdapter
    {
    }

    public partial class tblSalesCreditRealizeTableAdapter
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

    #region tblSalesByRetailer
    partial class tblSalesByRetailerTableAdapter
    {
    }

    public partial class tblSalesByRetailerTableAdapter
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

    #region tblSalesTarget
    partial class tblSalesTargetTableAdapter
    {
    }

    public partial class tblSalesTargetTableAdapter
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

    #region tblSalesTargetSp
    partial class tblSalesTargetSpTableAdapter
    {
    }

    public partial class tblSalesTargetSpTableAdapter
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

    #region tblSalesTargetDealer
    partial class tblSalesTargetDealerTableAdapter
    {
    }

    public partial class tblSalesTargetDealerTableAdapter
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