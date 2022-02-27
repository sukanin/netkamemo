using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotBambooBLL.Framework;
using System.Web.UI.HtmlControls;

namespace FrameworkControls
{
    [ToolboxData("<{0}:ValidationErrorMessages runat=server></{0}:ValidationErrorMessages>")]
    public class ValidationErrorMessages : WebControl
    {
        [Bindable(false), Browsable(false)]
        public ValidationErrors ValidationErrors { get; set; }

        public ValidationErrorMessages()
        {
            ValidationErrors = new ValidationErrors();
        }


        protected override void RenderContents(HtmlTextWriter output)
        {
            if (ValidationErrors.Count != 0)
            {
                HtmlTable table = new HtmlTable();

                HtmlTableRow trHeader = new HtmlTableRow();
                HtmlTableCell tcHeader = new HtmlTableCell();
                tcHeader.InnerText = "Cannot save or submit request. Please check the following:";
                tcHeader.Attributes.Add("class", "validationErroMessageHeader");
                trHeader.Cells.Add(tcHeader);
                table.Rows.Add(trHeader);

                foreach (ValidationError ve in ValidationErrors)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlTableCell tc = new HtmlTableCell();
                    tc.InnerText = ve.ErrorMessage;
                    tc.Attributes.Add("class", "validationErrorMessage");
                    tr.Cells.Add(tc);
                    table.Rows.Add(tr);
                    tc = null;
                    tr = null;
                }

                table.RenderControl(output);
                tcHeader = null;
                trHeader = null;
                table = null;
            }
            else
            {
                output.Write("");
            }
        }
    }
}
