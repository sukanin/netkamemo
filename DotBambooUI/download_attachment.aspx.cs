using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class download_attachment : System.Web.UI.Page
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
            // Download File
            try
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + att.Filename + "\"");
                Response.OutputStream.Write(xmlb, 0, xmlb.Length);
                Response.Flush();
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