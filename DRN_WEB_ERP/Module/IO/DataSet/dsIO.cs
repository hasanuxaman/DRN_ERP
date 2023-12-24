using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.IO.DataSet {
    
    
    public partial class dsIO {
    }
}

namespace DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters
{
    #region tblAccIoLimit
    partial class tblAccIoLimitTableAdapter
    {
    }

    public partial class tblAccIoLimitTableAdapter
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

    #region tblAccIoReqApp
    partial class tblAccIoReqAppTableAdapter
    {
    }

    public partial class tblAccIoReqAppTableAdapter
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

    #region tblAccIoReq
    partial class tblAccIoReqTableAdapter
    {
    }

    public partial class tblAccIoReqTableAdapter
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

    #region tblAccIoAdjApp
    partial class tblAccIoAdjAppTableAdapter
    {
    }

    public partial class tblAccIoAdjAppTableAdapter
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

    #region tblAccIoAdj
    partial class tblAccIoAdjTableAdapter
    {
    }

    public partial class tblAccIoAdjTableAdapter
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