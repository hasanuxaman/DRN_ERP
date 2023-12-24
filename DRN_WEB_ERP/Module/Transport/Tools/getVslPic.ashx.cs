using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    /// <summary>
    /// Summary description for getVslPic
    /// </summary>
    public class getVslPic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            try
            {
                if (context.Request.QueryString["VslRegNo"] != null)
                {
                    string imgId = "";
                    imgId = context.Request.QueryString["VslRegNo"];
                    MemoryStream memoryStream = new MemoryStream(GetImageFromDB(imgId), false);
                    System.Drawing.Image imgFromGB = System.Drawing.Image.FromStream(memoryStream);
                    imgFromGB.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex) { }
        }

        private byte[] GetImageFromDB(string VslRegNo)
        {
            System.Data.SqlClient.SqlDataReader rdr = null;
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand selcmd = null;
            byte[] btImage = null;
            try
            {
                string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ToString();
                conn = new System.Data.SqlClient.SqlConnection(connStr);
                selcmd = new System.Data.SqlClient.SqlCommand("select VslImgPic1 from tbl_TrTr_Vsl_Pic where VslRegNo=" + VslRegNo, conn);
                conn.Open();
                rdr = selcmd.ExecuteReader();
                while (rdr.Read())
                {
                    btImage = ((byte[])rdr["VslImgPic1"]);
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