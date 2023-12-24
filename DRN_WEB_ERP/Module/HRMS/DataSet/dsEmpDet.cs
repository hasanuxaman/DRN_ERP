using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.HRMS.DataSet {
    
    
    public partial class dsEmpDet {
    }
}

namespace DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters
{
    #region tblHrmsEmp
    partial class tblHrmsEmpTableAdapter
    {
    }

    public partial class tblHrmsEmpTableAdapter
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

    #region tblHrmsEmpAdr
    partial class tblHrmsEmpAdrTableAdapter
    {
    }

    public partial class tblHrmsEmpAdrTableAdapter
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

    #region tblHrmsEmpExt
    partial class tblHrmsEmpExtTableAdapter
    {
    }

    public partial class tblHrmsEmpExtTableAdapter
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

    #region tblHrmsEmpQual
    partial class tblHrmsEmpQualTableAdapter
    {
    }

    public partial class tblHrmsEmpQualTableAdapter
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

    #region tblHrmsEmpExp
    partial class tblHrmsEmpExpTableAdapter
    {
    }

    public partial class tblHrmsEmpExpTableAdapter
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

    #region tblHrmsEmpOffice
    partial class tblHrmsEmpOfficeTableAdapter
    {
    }

    public partial class tblHrmsEmpOfficeTableAdapter
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

    #region tblHrmsEmpImg
    partial class tblHrmsEmpImgTableAdapter
    {
    }

    public partial class tblHrmsEmpImgTableAdapter
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

    #region tblHrmsEmpServLog
    partial class tblHrmsEmpServLogTableAdapter
    {
    }

    public partial class tblHrmsEmpServLogTableAdapter
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