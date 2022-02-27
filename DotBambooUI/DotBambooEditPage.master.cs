﻿using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DotBambooEditPage : System.Web.UI.MasterPage
{
    public delegate void ButtonClickedHandler(object sender, EventArgs e);

    public event ButtonClickedHandler SaveButton_Click;
    public event ButtonClickedHandler CancelButton_Click;
    public event ButtonClickedHandler PrintButton_Click;
    public event ButtonClickedHandler AddButton_Click;

    public void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveButton_Click != null)
        {
            SaveButton_Click(sender, e);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (CancelButton_Click != null)
        {
            CancelButton_Click(sender, e);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (PrintButton_Click != null)
        {
            PrintButton_Click(sender, e);
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (AddButton_Click != null)
        {
            AddButton_Click(sender, e);
        }
    }

    public ValidationErrors ValidationErrors
    {
        get
        {
            return ValidationErrorMessages1.ValidationErrors;
        }

        set
        {
            ValidationErrorMessages1.ValidationErrors = value;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (((BasePage)this.Page).ReadOnly)
        {
            //Hide the save button
            btnSave.Visible = false;
            btnPrint.Visible = false;
            btnAddNew.Visible = false;
        }

        btnPrint.Attributes.Add("onclick", GetPrintButtonScript(btnPrint));
    }

    public string GetPrintButtonScript(Button btn)
    {
        StringBuilder printButtonScript = new StringBuilder();

        //Get the postback script.
        string postback = this.Page.ClientScript.GetPostBackEventReference(btn, "");

        //Change target to a new window.  Name the window the current date and time so multiple windows can
        //be opened.
        printButtonScript.Append("var today = new Date();");
        printButtonScript.Append("var newWindowName = today.getFullYear().toString() + today.getMonth().toString() + today.getDate().toString() + today.getHours().toString() + today.getHours().toString() + today.getMinutes().toString() + today.getSeconds().toString() + today.getMilliseconds().toString();");

        printButtonScript.Append("document.forms[0].target = newWindowName;");

        //TODO: Added root path after this was turned in.
        //Show the please wait screen.
        printButtonScript.Append("window.open('PleaseWait.html', newWindowName, 'scrollbars=yes,status=no,resizable=yes,height=768,width=1024,top=0,left=0');");

        //Add the postback script.
        printButtonScript.Append(postback + ";");

        //Reset target back to itself so other controls will post back to this form.
        printButtonScript.Append("document.forms[0].target='_self';");
        //Return false to stop page submitting.
        printButtonScript.Append("return false;" + Environment.NewLine);

        return printButtonScript.ToString();
    }

    public void HideBtnSave()
    {
        btnSave.Visible = true;
        btnPrint.Visible = true;
    }

    public void HideBtnPrint()
    {
        btnPrint.Visible = false;
    }

    public void ShowBtnPrint()
    {
        btnPrint.Visible = true;
    }

    public void HideBtnAddNew()
    {
        btnAddNew.Visible = false;
    }

    public void HideBtnSave2()
    {
        btnSave.Visible = false;
    }
    public void ShowBtnSave2()
    {
        btnSave.Visible = true;
    }

}
