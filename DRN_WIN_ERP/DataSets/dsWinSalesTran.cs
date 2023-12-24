using System.Data;
using System.Data.SqlClient;

namespace DRN_WIN_ERP.DataSets {
    
    
    public partial class dsWinSalesTran {
    }
}

namespace DRN_WIN_ERP.DataSets.dsWinSalesTranTableAdapters
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
}