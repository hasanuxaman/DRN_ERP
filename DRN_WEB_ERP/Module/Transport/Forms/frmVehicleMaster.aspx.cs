using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransDetTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmVehicleMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            hfVslRegNo.Value = "0";

            var taVslCat = new tbl_TrTr_Vsl_TypeTableAdapter();
            ddlVslType.DataSource = taVslCat.GetData();
            ddlVslType.DataValueField = "VslTypeRef";
            ddlVslType.DataTextField = "VslTypeName";
            ddlVslType.DataBind();
            ddlVslType.Items.Insert(0, new ListItem("---Select---", "0"));

            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();
            var dtVslMas = taVslMas.GetDataByDeliveryTrans();
            gvVslDet.DataSource = dtVslMas;
            gvVslDet.DataBind();
            gvVslDet.SelectedIndex = -1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            var taVslMasExt = new tbl_TrTr_Vsl_Mas_ExtTableAdapter();
            var taVslPic = new tbl_TrTr_Vsl_PicTableAdapter();

            try
            {
                var dtVslMas = taVslMas.GetDataByVslRegNo(txtSearch.Text.Trim().ToString());
                if (dtVslMas.Rows.Count > 0)
                {
                    hfVslRegNo.Value = dtVslMas[0].Vsl_Mas_No.ToString();
                    txtVslRegNo.Text = dtVslMas[0].Vsl_Mas_No.ToString();

                    var dtVslMasExt = taVslMasExt.GetDataByVslRegNo(txtSearch.Text.Trim().ToString());
                    if (dtVslMasExt.Rows.Count > 0)
                    {
                        ddlVslType.SelectedValue = dtVslMasExt[0].Vsl_Mas_Type.ToString();
                        txtBrandName.Text = dtVslMasExt[0].Vsl_Mas_Brand.ToString();
                        txtModelNo.Text = dtVslMasExt[0].Vsl_Mas_Model.ToString();
                        txtGrade.Text = dtVslMasExt[0].Vsl_Mas_Grade.ToString();
                        txtShape.Text = dtVslMasExt[0].Vsl_Mas_Shape.ToString();
                        txtEngNo.Text = dtVslMasExt[0].Vsl_Mas_Engine_No.ToString();
                        txtChasisNo.Text = dtVslMasExt[0].Vsl_Mas_Chasis_No.ToString();
                        txtEngCC.Text = dtVslMasExt[0].Vsl_Mas_Engine_CC.ToString();
                        txtBaterySize.Text = dtVslMasExt[0].Vsl_Mas_Batery_Size.ToString();
                        txtTyreSize.Text = dtVslMasExt[0].Vsl_Mas_Tyre_Size.ToString();
                        txtGearType.Text = dtVslMasExt[0].Vsl_Mas_Gear_Type.ToString();
                        txtFuelType.Text = dtVslMasExt[0].Vsl_Mas_Fuel_Type.ToString();
                        txtMadeBy.Text = dtVslMasExt[0].Vsl_Mas_Made_By.ToString();
                        txtColor.Text = dtVslMasExt[0].Vsl_Mas_Color.ToString();
                        txtMfgYear.Text = dtVslMasExt[0].Vsl_Mas_MF_Year.ToString();
                        txtMilageUsed.Text = dtVslMasExt[0].Vsl_Mas_Milage.ToString();
                        txtRegDate.Text = dtVslMasExt[0].IsVsl_Mas_Reg_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Reg_Date.ToString("dd/MM/yyyy");
                        txtSuppName.Text = dtVslMasExt[0].Vsl_Mas_Sup_Name.ToString();
                        txtSuppAddr.Text = dtVslMasExt[0].Vsl_Mas_Sup_Addr.ToString();
                        txtPurAmt.Text = dtVslMasExt[0].Vsl_Mas_Pur_Amt.ToString("N2");
                        txtInsNo.Text = dtVslMasExt[0].Vsl_Mas_Ins_No.ToString();
                        txtInsComp.Text = dtVslMasExt[0].Vsl_Mas_Ins_Comp.ToString();
                        txtInsDate.Text = dtVslMasExt[0].IsVsl_Mas_Ins_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Ins_Date.ToString("dd/MM/yyyy");
                        txtLeaseComp.Text = dtVslMasExt[0].Vsl_Mas_Lease_Comp.ToString();
                        txtLeaseCompAdr.Text = dtVslMasExt[0].Vsl_Mas_Lease_Comp_Addr.ToString();
                        txtLeaseDate.Text = dtVslMasExt[0].IsVsl_Mas_Lease_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Lease_Date.ToString("dd/MM/yyyy");
                    }

                    var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                    if (dtVslMasPic.Rows.Count > 0)
                    {
                        hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                        imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                    }
                    else
                    {
                        hlVslPic.NavigateUrl = "~/Image/NoImage.gif";
                        imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                    }
                }
                else
                {
                    btnClearSrch.Visible = true;

                    ClearData();

                    tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex) { }
        }

        private void ClearData()
        {
            hfVslRegNo.Value = "0";

            txtVslRegNo.Text = "";
            ddlVslType.SelectedIndex = 0;
            txtBrandName.Text = "";
            txtModelNo.Text = "";
            txtGrade.Text = "";
            txtShape.Text = "";
            txtEngNo.Text = "";
            txtChasisNo.Text = "";
            txtEngCC.Text = "";
            txtBaterySize.Text = "";
            txtTyreSize.Text = "";
            txtGearType.Text = "";
            txtFuelType.Text = "";
            txtMadeBy.Text = "";
            txtColor.Text = "";
            txtMfgYear.Text = "";
            txtMilageUsed.Text = "";
            txtRegDate.Text = "";
            txtSuppName.Text = "";
            txtSuppAddr.Text = "";
            txtPurAmt.Text = "";
            txtInsNo.Text = "";
            txtInsComp.Text = "";
            txtInsDate.Text = "";
            txtLeaseComp.Text = "";
            txtLeaseCompAdr.Text = "";
            txtLeaseDate.Text = "";

            txtSearch.Text = "";
            btnClearSrch.Visible = false;

            //hlVslPic.ImageUrl = "~/Image/NoImage.gif";
            hlVslPic.ImageUrl = "";
            imgVslPic.ImageUrl = "~/Image/NoImage.gif";

            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();
            var dtVslMas = taVslMas.GetDataByDeliveryTrans();
            gvVslDet.DataSource = dtVslMas;
            gvVslDet.DataBind();
            gvVslDet.SelectedIndex = -1;
        }

        #region GridData
        public string GetVslType(string VslTypeRef)
        {
            var taVslType = new tbl_TrTr_Vsl_TypeTableAdapter();

            string vslTypeName = "";
            try
            {
                var dtVslType = taVslType.GetDataByVslTypeRef(Convert.ToInt32(VslTypeRef.ToString()));
                if (dtVslType.Rows.Count > 0)
                    vslTypeName = dtVslType[0].IsVslTypeNameNull() ? "" : dtVslType[0].VslTypeName.ToString();

                return vslTypeName;
            }
            catch (Exception ex) { return vslTypeName; }
        }
        #endregion

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnUpdatePic_Click(object sender, EventArgs e)
        {
            var taVslImg = new tbl_TrTr_Vsl_PicTableAdapter();

            if (picUpload.HasFile)
            {
                try
                {
                    System.Drawing.Image imag = System.Drawing.Image.FromStream(picUpload.PostedFile.InputStream);

                    var dtVslImg = taVslImg.GetDataByVslRegNo(hfVslRegNo.Value.ToString());

                    if (dtVslImg.Rows.Count > 0)
                    {
                        taVslImg.UpdateVslPic(ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg), hfVslRegNo.Value.ToString());
                    }
                    else
                    {
                        taVslImg.InsertVslPic(hfVslRegNo.Value.ToString(), ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg),
                            null, null, "", "", "", "1", "");
                    }

                    var dtVslMasPic = taVslImg.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                    if (dtVslMasPic.Rows.Count > 0)
                    {
                        hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                        imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                    }
                    else
                    {
                        hlVslPic.NavigateUrl = "~/Image/NoImage.gif";
                        imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                    }           
                }
                catch (Exception ex) { }
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Select file first.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnDeletePic_Click(object sender, EventArgs e)
        {
            var taVslImg = new tbl_TrTr_Vsl_PicTableAdapter();

            try
            {
                var dtVslImg = taVslImg.GetDataByVslRegNo(hfVslRegNo.Value.ToString());

                //if (dtVslImg.Rows.Count > 0)
                //{
                //    taVslImg.UpdateVslPic(null, hfVslRegNo.Value.ToString());
                //}

                taVslImg.DeleteVslPic(hfVslRegNo.Value.ToString());

                var dtVslMasPic = taVslImg.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                if (dtVslMasPic.Rows.Count > 0)
                {
                    hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                    imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                }
                else
                {
                    hlVslPic.NavigateUrl = "~/Image/NoImage.gif";
                    imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                }           

                tblMsg.Rows[0].Cells[0].InnerText = "Picture deleted successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception ex) { throw; }
            return Ret;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            var taVslMasExt = new tbl_TrTr_Vsl_Mas_ExtTableAdapter();
            var taVslPic = new tbl_TrTr_Vsl_PicTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taVslMasExt.Connection);

            try
            {
                taVslMasExt.AttachTransaction(myTran);
                taVslPic.AttachTransaction(myTran);

                System.Drawing.Image PicImg = null;

                if (picUpload.HasFile)
                    PicImg = System.Drawing.Image.FromStream(picUpload.PostedFile.InputStream);

                var vslPurAmt = (txtPurAmt.Text.Trim() == "" || txtPurAmt.Text.Trim().Length <= 0) ? 0 : Convert.ToDecimal(txtPurAmt.Text.Trim());

                DateTime? regDate = null;
                if (!String.IsNullOrEmpty(txtRegDate.Text.Trim()))
                    regDate = Convert.ToDateTime(txtRegDate.Text.Trim());

                DateTime? insuDate = null;
                if (!String.IsNullOrEmpty(txtInsDate.Text.Trim()))
                    insuDate = Convert.ToDateTime(txtInsDate.Text.Trim());

                DateTime? leaseDate = null;
                if (!String.IsNullOrEmpty(txtLeaseDate.Text.Trim()))
                    leaseDate = Convert.ToDateTime(txtLeaseDate.Text.Trim());

                var dtVslMas = taVslMas.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                if (dtVslMas.Rows.Count > 0)
                {
                    taVslMas.UpdateVslMas("", ddlVslType.SelectedValue.ToString(), "", "", "", "", "", DateTime.Now, "user", "1", "", hfVslRegNo.Value.ToString());

                    //if (picUpload.HasFile)
                    //{
                    //    var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                    //    if (dtVslMasPic.Rows.Count > 0)
                    //    {
                    //        //update pic
                    //        taVslPic.UpdateVslPic(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfVslRegNo.Value.ToString());
                    //    }
                    //    else
                    //    {
                    //        //insert pic
                    //        taVslPic.InsertVslPic(hfVslRegNo.Value.ToString(), ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                    //            null, null, "", "", "", "1", "");
                    //    }
                    //}
                }
                else
                {
                    var dtGetMaxVslRef = taVslMas.GetMaxVslRef();
                    var nextVslRef = dtGetMaxVslRef == null ? 1 : Convert.ToInt32(dtGetMaxVslRef) + 1;

                    taVslMas.InsertVslMas(nextVslRef, txtVslRegNo.Text.Trim(), "", ddlVslType.SelectedValue.ToString(), "", "", "", "", "", DateTime.Now, "user", "1", "");

                    //if (picUpload.HasFile)
                    //{
                    //    //insert pic
                    //    taVslPic.InsertVslPic(hfVslRegNo.Value.ToString(), ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                    //        null, null, "", "", "", "1", "");
                    //}
                }

                if (picUpload.HasFile)
                {
                    var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                    if (dtVslMasPic.Rows.Count > 0)
                    {
                        //update pic
                        taVslPic.UpdateVslPic(ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg), hfVslRegNo.Value.ToString());
                    }
                    else
                    {
                        //insert pic
                        taVslPic.InsertVslPic(hfVslRegNo.Value.ToString(), ConvertImageToByteArray(PicImg, System.Drawing.Imaging.ImageFormat.Jpeg),
                            null, null, "", "", "", "1", "");
                    }
                }

                var dtVslMasExt = taVslMasExt.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                if (dtVslMasExt.Rows.Count > 0)
                {
                    taVslMasExt.UpdateVslMasExt(ddlVslType.SelectedValue.ToString(), "", txtBrandName.Text.Trim(), txtModelNo.Text.Trim(), txtGrade.Text.Trim(),
                        txtShape.Text.Trim(), txtEngNo.Text.Trim(), txtChasisNo.Text.Trim(), txtEngCC.Text.Trim(), txtBaterySize.Text.Trim(), txtTyreSize.Text.Trim(),
                        txtGearType.Text.Trim(), txtFuelType.Text.Trim(), txtMadeBy.Text.Trim(), txtColor.Text.Trim(), txtMfgYear.Text.Trim(), txtMilageUsed.Text.Trim(),
                        regDate, txtSuppName.Text.Trim(), txtSuppAddr.Text.Trim(), vslPurAmt, txtInsNo.Text.Trim(),
                        txtInsComp.Text.Trim(), insuDate, txtLeaseComp.Text.Trim(), txtLeaseCompAdr.Text.Trim(), leaseDate,
                        "", "", "", "", "", DateTime.Now, "userid", "1", "", hfVslRegNo.Value.ToString());
                   
                    
                }
                else
                {
                    taVslMasExt.InsertVslMasExt(txtVslRegNo.Text.Trim(), ddlVslType.SelectedValue.ToString(), "", txtBrandName.Text.Trim(), txtModelNo.Text.Trim(), txtGrade.Text.Trim(),
                        txtShape.Text.Trim(), txtEngNo.Text.Trim(), txtChasisNo.Text.Trim(), txtEngCC.Text.Trim(), txtBaterySize.Text.Trim(), txtTyreSize.Text.Trim(),
                        txtGearType.Text.Trim(), txtFuelType.Text.Trim(), txtMadeBy.Text.Trim(), txtColor.Text.Trim(), txtMfgYear.Text.Trim(), txtMilageUsed.Text.Trim(),
                        regDate, txtSuppName.Text.Trim(), txtSuppAddr.Text.Trim(), vslPurAmt, txtInsNo.Text.Trim(),
                        txtInsComp.Text.Trim(), insuDate, txtLeaseComp.Text.Trim(), txtLeaseCompAdr.Text.Trim(), leaseDate,
                        "", "", "", "", "", DateTime.Now, "userid", "1", "");
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                ClearData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processig error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvVslDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvVslDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();         
            var taVslPic = new tbl_TrTr_Vsl_PicTableAdapter();

            int indx = gvVslDet.SelectedIndex;

            if (indx != -1)
            {                          
                try
                {
                    //ClearData();

                    var lblVslRegNo = ((Label)gvVslDet.Rows[indx].FindControl("lblVslRegNo")).Text.Trim();

                    var dtVslMasExt = taVslMas.GetDataByVslRegNo(lblVslRegNo.ToString());
                    if (dtVslMasExt.Rows.Count > 0)
                    {
                        hfVslRegNo.Value = dtVslMasExt[0].Vsl_Mas_No.ToString();
                        txtVslRegNo.Text = dtVslMasExt[0].Vsl_Mas_No.ToString();

                        ddlVslType.SelectedValue = dtVslMasExt[0].Vsl_Mas_Type.ToString();
                        txtBrandName.Text = dtVslMasExt[0].IsVsl_Mas_BrandNull() ? "" : dtVslMasExt[0].Vsl_Mas_Brand.ToString();
                        txtModelNo.Text = dtVslMasExt[0].IsVsl_Mas_ModelNull() ? "" : dtVslMasExt[0].Vsl_Mas_Model.ToString();
                        txtGrade.Text = dtVslMasExt[0].IsVsl_Mas_GradeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Grade.ToString();
                        txtShape.Text = dtVslMasExt[0].IsVsl_Mas_ShapeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Shape.ToString();
                        txtEngNo.Text = dtVslMasExt[0].IsVsl_Mas_Engine_NoNull() ? "" : dtVslMasExt[0].Vsl_Mas_Engine_No.ToString();
                        txtChasisNo.Text = dtVslMasExt[0].IsVsl_Mas_Chasis_NoNull() ? "" : dtVslMasExt[0].Vsl_Mas_Chasis_No.ToString();
                        txtEngCC.Text = dtVslMasExt[0].IsVsl_Mas_Engine_CCNull() ? "" : dtVslMasExt[0].Vsl_Mas_Engine_CC.ToString();
                        txtBaterySize.Text = dtVslMasExt[0].IsVsl_Mas_Batery_SizeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Batery_Size.ToString();
                        txtTyreSize.Text = dtVslMasExt[0].IsVsl_Mas_Tyre_SizeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Tyre_Size.ToString();
                        txtGearType.Text = dtVslMasExt[0].IsVsl_Mas_Gear_TypeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Gear_Type.ToString();
                        txtFuelType.Text = dtVslMasExt[0].IsVsl_Mas_Fuel_TypeNull() ? "" : dtVslMasExt[0].Vsl_Mas_Fuel_Type.ToString();
                        txtMadeBy.Text = dtVslMasExt[0].IsVsl_Mas_Made_ByNull() ? "" : dtVslMasExt[0].Vsl_Mas_Made_By.ToString();
                        txtColor.Text = dtVslMasExt[0].IsVsl_Mas_ColorNull() ? "" : dtVslMasExt[0].Vsl_Mas_Color.ToString();
                        txtMfgYear.Text = dtVslMasExt[0].IsVsl_Mas_MF_YearNull() ? "" : dtVslMasExt[0].Vsl_Mas_MF_Year.ToString();
                        txtMilageUsed.Text = dtVslMasExt[0].IsVsl_Mas_MilageNull() ? "" : dtVslMasExt[0].Vsl_Mas_Milage.ToString();
                        txtRegDate.Text = dtVslMasExt[0].IsVsl_Mas_Reg_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Reg_Date.ToString("dd/MM/yyyy");
                        txtSuppName.Text = dtVslMasExt[0].IsVsl_Mas_Sup_NameNull() ? "" : dtVslMasExt[0].Vsl_Mas_Sup_Name.ToString();
                        txtSuppAddr.Text = dtVslMasExt[0].IsVsl_Mas_Sup_AddrNull() ? "" : dtVslMasExt[0].Vsl_Mas_Sup_Addr.ToString();
                        txtPurAmt.Text = dtVslMasExt[0].IsVsl_Mas_Pur_AmtNull() ? "" : dtVslMasExt[0].Vsl_Mas_Pur_Amt.ToString("N2");
                        txtInsNo.Text = dtVslMasExt[0].IsVsl_Mas_Ins_NoNull() ? "" : dtVslMasExt[0].Vsl_Mas_Ins_No.ToString();
                        txtInsComp.Text = dtVslMasExt[0].IsVsl_Mas_Ins_CompNull() ? "" : dtVslMasExt[0].Vsl_Mas_Ins_Comp.ToString();
                        txtInsDate.Text = dtVslMasExt[0].IsVsl_Mas_Ins_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Ins_Date.ToString("dd/MM/yyyy");
                        txtLeaseComp.Text = dtVslMasExt[0].IsVsl_Mas_Lease_CompNull() ? "" : dtVslMasExt[0].Vsl_Mas_Lease_Comp.ToString();
                        txtLeaseCompAdr.Text = dtVslMasExt[0].IsVsl_Mas_Lease_Comp_AddrNull() ? "" : dtVslMasExt[0].Vsl_Mas_Lease_Comp_Addr.ToString();
                        txtLeaseDate.Text = dtVslMasExt[0].IsVsl_Mas_Lease_DateNull() ? "" : dtVslMasExt[0].Vsl_Mas_Lease_Date.ToString("dd/MM/yyyy");

                        var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                        if (dtVslMasPic.Rows.Count > 0)
                        {
                            hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + lblVslRegNo.ToString() + "'";
                            imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + lblVslRegNo.ToString() + "'";
                        }
                        else
                        {
                            hlVslPic.NavigateUrl = "~/Image/NoImage.gif";
                            imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                        }

                        btnClearSrch.Visible = true;
                    }
                    else
                    {
                        ClearData();
                        tblMsg.Rows[0].Cells[0].InnerText = "No data found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                catch (Exception ex) 
                { 
                    ClearData();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data processig error. " + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}