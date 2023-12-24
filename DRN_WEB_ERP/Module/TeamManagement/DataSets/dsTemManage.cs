using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.TeamManagement.DataSets {
    
    
    public partial class dsTemManage {
    }
}

namespace DRN_WEB_ERP.Module.TeamManagement.DataSets.dsTemManageTableAdapters
{
    #region tbl_Team_Tour_Plan
    partial class tbl_Team_Tour_PlanTableAdapter
    {
    }

    public partial class tbl_Team_Tour_PlanTableAdapter
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

    #region tbl_Team_Tour
    partial class tbl_Team_TourTableAdapter
    {
    }

    public partial class tbl_Team_TourTableAdapter
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