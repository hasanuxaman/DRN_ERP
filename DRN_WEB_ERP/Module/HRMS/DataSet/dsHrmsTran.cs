using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.HRMS.DataSet
{


	public partial class dsHrmsTran
	{
	}
}

namespace DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters
{
    #region tblHrmsHoliday
    partial class tblHrmsHolidayTableAdapter
    {
    }

    public partial class tblHrmsHolidayTableAdapter
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

    #region tblHrmsEmpDayAttnd
    partial class tblHrmsEmpDayAttndTableAdapter
    {
    }

    public partial class tblHrmsEmpDayAttndTableAdapter
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

    #region tblHrmsImportAttndDet
    partial class tblHrmsImportAttndDetTableAdapter
    {
    }

    public partial class tblHrmsImportAttndDetTableAdapter
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

    #region tblHrmsImportAttndDetEcilFactory
    partial class tblHrmsImportAttndDetEcilFactTableAdapter
    {
    }

    public partial class tblHrmsImportAttndDetEcilFactTableAdapter
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

    #region tblHrmsEmpLeave
    partial class tblHrmsEmpLeaveTableAdapter
    {
    }

    public partial class tblHrmsEmpLeaveTableAdapter
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

    #region tblHrmsEmpLeaveApp
    partial class tblHrmsEmpLeaveApp111TableAdapter
    {
    }

    public partial class tblHrmsEmpLeaveAppTableAdapter
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

    #region tblHrmsEmpPaySet
    partial class tblHrmsEmpPaySetTableAdapter
    {
    }

    public partial class tblHrmsEmpPaySetTableAdapter
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

    #region tblHrmsTrialSalary
    partial class tblHrmsTrialSalaryTableAdapter
    {
    }

    public partial class tblHrmsTrialSalaryTableAdapter
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