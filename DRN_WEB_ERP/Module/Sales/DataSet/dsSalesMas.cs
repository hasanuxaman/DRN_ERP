using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Sales.DataSet {
    
    
    public partial class dsSalesMas {
    }
}

namespace DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters
{
    #region tblSalesPartyAcc
    partial class tblSalesPartyAccTableAdapter
    {
    }

    public partial class tblSalesPartyAccTableAdapter
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

    #region tblSalesPartyAdr
    partial class tblSalesPartyAdrTableAdapter
    {
    }

    public partial class tblSalesPartyAdrTableAdapter
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

    #region tblSalesPartyRtl
    partial class tblSalesPartyRtlTableAdapter
    {
    }

    public partial class tblSalesPartyRtlTableAdapter
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

    #region tblSalesPartyCrLimit
    partial class tblSalesPartyCrLimitTableAdapter
    {
    }

    public partial class tblSalesPartyCrLimitTableAdapter
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

    #region tblSalesCrLimitSecurity
    partial class tblSalesCrLimitSecurityTableAdapter
    {
    }

    public partial class tblSalesCrLimitSecurityTableAdapter
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

    #region tblSalesPriceCust
    partial class tblSalesPriceCustTableAdapter
    {
    }

    public partial class tblSalesPriceCustTableAdapter
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

    #region tblSalesDsmMas
    partial class tblSalesDsmMasTableAdapter
    {
    }

    public partial class tblSalesDsmMasTableAdapter
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

    #region tblSalesPersonMas
    partial class tblSalesPersonMasTableAdapter
    {
    }

    public partial class tblSalesPersonMasTableAdapter
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

    #region tbl_Sms_Body
    partial class tbl_Sms_BodyTableAdapter
    {
    }

    public partial class tbl_Sms_BodyTableAdapter
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

    #region tbl_Sms_Config
    partial class tbl_Sms_ConfigTableAdapter
    {
    }

    public partial class tbl_Sms_ConfigTableAdapter
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