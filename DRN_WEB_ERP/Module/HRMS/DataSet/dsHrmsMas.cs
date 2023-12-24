using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.HRMS.DataSet {
    
    
    public partial class dsHrmsMas {
    }
}

namespace DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters
{
    #region tblHrmsShift
    partial class tblHrmsShiftTableAdapter
    {
    }

    public partial class tblHrmsShiftTableAdapter
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

    #region tblHrmsLeaveMas
    partial class tblHrmsLeaveMasTableAdapter
    {
    }

    public partial class tblHrmsLeaveMasTableAdapter
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

    #region tblHrmsGradeDet
    partial class tblHrmsGradeDetTableAdapter
    {
    }

    public partial class tblHrmsGradeDetTableAdapter
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