using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmGlGrpTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenuTreeData();
        }

        protected void SetMenuTreeData()
        {
            var taGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            TreeNode tnMain = new TreeNode();
            tnMain.Text = "ECIL";
            tnMain.Value = "ECIL";
            tvGlGrp.Nodes.Add(tnMain);

            var dtGrpOne = taGrpCode.GetDataByGrpDef("A01");
            foreach (dsAccMas.tbl_Acc_Grp_CodeRow drGrpOne in dtGrpOne.Rows)
            {
                TreeNode tnA01 = new TreeNode();
                tnA01.Text = drGrpOne.Grp_Code_Name.ToString();
                tnA01.Value = drGrpOne.Grp_Code_Id.ToString();
                //tnA01.ShowCheckBox = true;
                tnMain.ChildNodes.Add(tnA01);

                var dtGrpTwo = taGrpCode.GetDataByGrpSet1(drGrpOne.Grp_Code_Id.ToString());
                foreach (dsAccMas.tbl_Acc_Grp_CodeRow drGrpTwo in dtGrpTwo.Rows)
                {
                    if (drGrpTwo.Grp_Code_Def_Id == "A02")
                    {
                        TreeNode tnA02 = new TreeNode();
                        tnA02.Text = drGrpTwo.Grp_Code_Name.ToString();
                        tnA02.Value = drGrpTwo.Grp_Code_Id.ToString();
                        //tnA02.ShowCheckBox = true;
                        tnA01.ChildNodes.Add(tnA02);

                        var dtGrpThree = taGrpCode.GetDataByGrpSet2(drGrpTwo.Grp_Code_Id.ToString());
                        foreach (dsAccMas.tbl_Acc_Grp_CodeRow drGrpThree in dtGrpThree.Rows)
                        {
                            if (drGrpThree.Grp_Code_Def_Id == "A03")
                            {
                                TreeNode tnA03 = new TreeNode();
                                tnA03.Text = drGrpThree.Grp_Code_Name.ToString();
                                tnA03.Value = drGrpThree.Grp_Code_Id.ToString();
                                //tnA03.ShowCheckBox = true;
                                tnA02.ChildNodes.Add(tnA03);

                                var taGlCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();
                                var dtGlCoaGrp = taGlCoaGrp.GetDataByGrpDefGrpCode("A03", drGrpThree.Grp_Code_Id.ToString());
                                foreach (dsAccMas.tbl_Acc_Gl_Coa_GrpRow drCoaGrp in dtGlCoaGrp.Rows)
                                {
                                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                                    var dtCoa = taCoa.GetDataByCoaCode(drCoaGrp.Coa_Grp_Coa_Code.ToString());
                                    if (dtCoa.Rows.Count > 0)
                                    {
                                        TreeNode tnGL = new TreeNode();
                                        tnGL.Text = dtCoa[0].Gl_Coa_Name.ToString() + " - (" + dtCoa[0].Gl_Coa_Code.ToString() + ")";
                                        tnGL.Value = dtCoa[0].Gl_Coa_Code.ToString();
                                        //tnGL.ShowCheckBox = true;
                                        tnA03.ChildNodes.Add(tnGL);
                                        tnA03.Collapse();
                                    }
                                }
                            }
                        }
                    }                    
                }
            }
        }
    }
}