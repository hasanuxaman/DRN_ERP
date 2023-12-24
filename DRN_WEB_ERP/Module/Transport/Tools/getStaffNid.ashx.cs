using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Transport.Tools
{
    /// <summary>
    /// Summary description for getEmpNid
    /// </summary>
    public class getStaffNid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            try
            {
                if (context.Request.QueryString["StaffRefNo"] != null)
                {
                    string imgId = "";
                    imgId = context.Request.QueryString["StaffRefNo"];
                    MemoryStream memoryStream = new MemoryStream(GetImageFromDB(imgId), false);
                    System.Drawing.Image imgFromGB = System.Drawing.Image.FromStream(memoryStream);
                    imgFromGB.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex) { }
        }

        private byte[] GetImageFromDB(string EmpRefNo)
        {
            System.Data.SqlClient.SqlDataReader rdr = null;
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand selcmd = null;
            byte[] btImage = null;
            try
            {
                string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ToString();
                conn = new System.Data.SqlClient.SqlConnection(connStr);
                selcmd = new System.Data.SqlClient.SqlCommand("select Staff_Nid from tbl_TrTr_Staff_Img where Staff_Ref_No=" + EmpRefNo, conn);
                conn.Open();
                rdr = selcmd.ExecuteReader();
                while (rdr.Read())
                {
                    btImage = ((byte[])rdr["Staff_Nid"]);
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return btImage;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}