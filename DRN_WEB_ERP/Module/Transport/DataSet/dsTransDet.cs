using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Transport.DataSet {       
    public partial class dsTransDet {
    }
}

namespace DRN_WEB_ERP.Module.Transport.DataSet.dsTransDetTableAdapters
{
    #region tbl_TrTr_TC_Hdr
    partial class tbl_TrTr_TC_HdrTableAdapter
    {
    }

    public partial class tbl_TrTr_TC_HdrTableAdapter
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

    #region tbl_Weighbridge_Data
    partial class tbl_Weighbridge_DataTableAdapter
    {
    }

    public partial class tbl_Weighbridge_DataTableAdapter
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