using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class download_attachment2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MemoAttachmentEO att = new MemoAttachmentEO();
        
        string att_id = Request.QueryString["ID"];

        int id = Convert.ToInt32(att_id);

        if (att_id != null)
        {
            att.Load(id);

            byte[] xmlb = att.Content;

            var path = Server.MapPath("~/Documents/") + att.MemoNumber;

            var file = path + "\\" + att.Filename;


            //if (File.Exists(file))
            //{
            //    xmlb = File.ReadAllBytes(file);
            //}

            // Download File
            try
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + att.Filename + "\"");
                Response.ContentType = "application/XLSX";

                Response.AddHeader("content-length", Convert.ToString(xmlb.Length));
                Response.BinaryWrite(xmlb);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.Write("Downlaod Error. Please contact your administrator.");
                Response.End();
            }
        }else
        {
            Response.Clear();
            Response.Write("Download attachment notfoud.");
            Response.End();
        }

        
        
    }
}