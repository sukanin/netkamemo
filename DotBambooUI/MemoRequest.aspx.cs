using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DotBambooBLL.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
public partial class PrGeneralRequest : BaseEditPage<MemoEO>
{
    private const string VIEW_STATE_KEY = "MemoRequest";

    #region Override
    protected override void LoadObjectFromScreen(MemoEO baseEO)
    {

        baseEO.MemoNumber = MemoNumber.Text;
        baseEO.MemoDate = MemoDate.SelectedDate.Value;
        baseEO.MemoYear = baseEO.MemoDate.Year;
        baseEO.MemoMonth = baseEO.MemoDate.Month;
        baseEO.MemoRunnum = baseEO.MemoRunnum;
        
        baseEO.ApplicantName = ApplicantName.Text;
        baseEO.Department = Department.Text;
        
        baseEO.Subject = Subject.Text;
        baseEO.EmailCC = EmailCC.Text;
        baseEO.Detail = Detail.Text;

        baseEO.ApproveRemark1 = ApproveRemark1.Text;
        baseEO.ApproveRemark2 = ApproveRemark2.Text;
        baseEO.ApproveRemark3 = ApproveRemark3.Text;
        baseEO.ApproveRemark4 = ApproveRemark4.Text;

        // A1 confirm
        if (Approver1Confirm.Checked)
        {
            baseEO.Approver1confirmStatus = 1;
        }else if (Approver1Reject.Checked)
        {
            baseEO.Approver1confirmStatus = 2;
        }
        else
        {
            baseEO.Approver1confirmStatus = 0;
        }
        baseEO.Approver1confirmDate = DateTime.Parse(Approver1ConfirmDate.Text);
        if (!string.IsNullOrEmpty(Approver1ConfirmBy.Text))
        {
            UserAccountEO user = new UserAccountEO();
            if (user.LoadByUsername(Approver1ConfirmBy.Text))
            {
                baseEO.Approver1confirmBy = user.ID;
            }else
            {
                baseEO.Approver1confirmBy = 0;
            }
        }else
        {
            baseEO.Approver1confirmBy = 0;
        }

        // A2 confirm
        if (Approver2Confirm.Checked)
        {
            baseEO.Approver2confirmStatus = 1;
        }
        else if (Approver2Reject.Checked)
        {
            baseEO.Approver2confirmStatus = 2;
        }
        else
        {
            baseEO.Approver2confirmStatus = 0;
        }
        baseEO.Approver2confirmDate = DateTime.Parse(Approver2ConfirmDate.Text);
        if (!string.IsNullOrEmpty(Approver2ConfirmBy.Text))
        {
            UserAccountEO user = new UserAccountEO();
            if (user.LoadByUsername(Approver2ConfirmBy.Text))
            {
                baseEO.Approver2confirmBy = user.ID;
            }
            else
            {
                baseEO.Approver2confirmBy = 0;
            }
        }
        else
        {
            baseEO.Approver2confirmBy = 0;
        }

        // A3 confirm
        if (Approver3Confirm.Checked)
        {
            baseEO.Approver3confirmStatus = 1;
        }
        else if (Approver3Reject.Checked)
        {
            baseEO.Approver3confirmStatus = 2;
        }
        else
        {
            baseEO.Approver3confirmStatus = 0;
        }
        baseEO.Approver3confirmDate = DateTime.Parse(Approver3ConfirmDate.Text);
        if (!string.IsNullOrEmpty(Approver3ConfirmBy.Text))
        {
            UserAccountEO user = new UserAccountEO();
            if (user.LoadByUsername(Approver3ConfirmBy.Text))
            {
                baseEO.Approver3confirmBy = user.ID;
            }
            else
            {
                baseEO.Approver3confirmBy = 0;
            }
        }
        else
        {
            baseEO.Approver3confirmBy = 0;
        }

        // A4 confirm
        if (Approver4Confirm.Checked)
        {
            baseEO.Approver4confirmStatus = 1;
        }
        else if (Approver4Reject.Checked)
        {
            baseEO.Approver4confirmStatus = 2;
        }
        else
        {
            baseEO.Approver4confirmStatus = 0;
        }
        baseEO.Approver4confirmDate = DateTime.Parse(Approver4ConfirmDate.Text);
        if (!string.IsNullOrEmpty(Approver4ConfirmBy.Text))
        {
            UserAccountEO user = new UserAccountEO();
            if (user.LoadByUsername(Approver4ConfirmBy.Text))
            {
                baseEO.Approver4confirmBy = user.ID;
            }
            else
            {
                baseEO.Approver4confirmBy = 0;
            }
        }
        else
        {
            baseEO.Approver4confirmBy = 0;
        }
    }

    protected override void LoadScreenFromObject(MemoEO baseEO)
    {

        // Disable Print and add new button
        Master.HideBtnPrint();
        Master.HideBtnAddNew();

        if (baseEO.ID > 0)
        {
            Master.HideBtnSave();
            Master.HideBtnPrint();

            // HIDE SELECT APPROVER
            SelectApprover1.Visible = false;
            SelectApprover2.Visible = false;
            SelectApprover3.Visible = false;
            SelectApprover4.Visible = false;

            Approver1ConfirmBy.ReadOnly = true;
            Approver2ConfirmBy.ReadOnly = true;
            Approver3ConfirmBy.ReadOnly = true;
            Approver4ConfirmBy.ReadOnly = true;

            MemoDate.Enabled = false;
            Subject.ReadOnly = true;
            Detail.ReadOnly = true;

            ApproveRemark1.Text = baseEO.ApproveRemark1;
            ApproveRemark2.Text = baseEO.ApproveRemark2;
            ApproveRemark3.Text = baseEO.ApproveRemark3;
            ApproveRemark4.Text = baseEO.ApproveRemark4;

            if (baseEO.MemoStatus == 2)
            {
                RadAsyncUpload1.Visible = false;

                Approver1Confirm.Enabled = true;
                Approver1Reject.Enabled = true;
                ApproveRemark1.Enabled = true;
                Approver1ConfirmDate.Visible = false;

                Approver2Confirm.Enabled = false;
                Approver2Reject.Enabled = false;
                ApproveRemark2.Enabled = false;
                Approver2ConfirmDate.Visible = false;

                Approver3Confirm.Enabled = false;
                Approver3Reject.Enabled = false;
                ApproveRemark3.Enabled = false;
                Approver3ConfirmDate.Visible = false;

                Approver4Confirm.Enabled = false;
                Approver4Reject.Enabled = false;
                ApproveRemark4.Enabled = false;
                Approver4ConfirmDate.Visible = false;

            }
            if (baseEO.MemoStatus == 3)
            {
                RadAsyncUpload1.Visible = false;

                Approver1Confirm.Enabled = false;
                Approver1Reject.Enabled = false;
                ApproveRemark1.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver2Confirm.Enabled = true;
                Approver2Reject.Enabled = true;
                ApproveRemark2.Enabled = true;
                Approver2ConfirmDate.Visible = false;

                Approver3Confirm.Enabled = false;
                Approver3Reject.Enabled = false;
                ApproveRemark3.Enabled = false;
                Approver3ConfirmDate.Visible = false;

                Approver4Confirm.Enabled = false;
                Approver4Reject.Enabled = false;
                ApproveRemark4.Enabled = false;
                Approver4ConfirmDate.Visible = false;
            }
            if (baseEO.MemoStatus == 4)
            {
                RadAsyncUpload1.Visible = false;

                Approver1Confirm.Enabled = false;
                Approver1Reject.Enabled = false;
                ApproveRemark1.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver2Confirm.Enabled = false;
                Approver2Reject.Enabled = false;
                ApproveRemark2.Enabled = false;
                Approver2ConfirmDate.Visible = true;

                Approver3Confirm.Enabled = true;
                Approver3Reject.Enabled = true;
                ApproveRemark3.Enabled = true;
                Approver3ConfirmDate.Visible = false;

                Approver4Confirm.Enabled = false;
                Approver4Reject.Enabled = false;
                ApproveRemark4.Enabled = false;
                Approver4ConfirmDate.Visible = false;
            }
            if (baseEO.MemoStatus == 5)
            {
                RadAsyncUpload1.Visible = false;

                Approver1Confirm.Enabled = false;
                Approver1Reject.Enabled = false;
                ApproveRemark1.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver2Confirm.Enabled = false;
                Approver2Reject.Enabled = false;
                ApproveRemark2.Enabled = false;
                Approver2ConfirmDate.Visible = true;

                Approver3Confirm.Enabled = false;
                Approver3Reject.Enabled = false;
                ApproveRemark3.Enabled = false;
                Approver3ConfirmDate.Visible = true;

                Approver4Confirm.Enabled = true;
                Approver4Reject.Enabled = true;
                ApproveRemark4.Enabled = true;
                Approver4ConfirmDate.Visible = false;
            }
            if (baseEO.MemoStatus == 7)
            {
                RadAsyncUpload1.Visible = false;

                Approver1Confirm.Enabled = false;
                Approver1Reject.Enabled = false;
                ApproveRemark1.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver2Confirm.Enabled = false;
                Approver2Reject.Enabled = false;
                ApproveRemark2.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver3Confirm.Enabled = false;
                Approver3Reject.Enabled = false;
                ApproveRemark3.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Approver4Confirm.Enabled = false;
                Approver4Reject.Enabled = false;
                ApproveRemark4.Enabled = false;
                Approver1ConfirmDate.Visible = true;

                Master.HideBtnSave2();
                Master.ShowBtnPrint();
            }


            if (baseEO.CancelRejectStatus > 0)
            {
                Master.HideBtnSave2();
                RadAsyncUpload1.Visible = false;

                if (baseEO.CancelRejectStatus == 1)
                {
                    UpdatePanel10.Visible = true;
                    CancelDate.Text = baseEO.CancelDate.ToString();
                    if (baseEO.CancelBy > 0)
                    {
                        UserAccountEO tempUser = new UserAccountEO();
                        tempUser.Load(baseEO.CancelBy);
                        CancelBy.Text = tempUser.Name;
                    }
                    else
                    {
                        CancelBy.Text = "";
                    }
                }

            }

            if (!CheckAuthorization(baseEO.InsertUserAccountId, baseEO.Approver1confirmBy, baseEO.Approver2confirmBy, baseEO.Approver3confirmBy, baseEO.Approver4confirmBy, baseEO.MemoStatus, baseEO.EmailCC))
            {
                Master.HideBtnSave2();
                ErrorMsg.Text = "This user not have authorization to view this request";
                UpdatePanel1.Visible = false;
                UpdatePanel2.Visible = false;
                UpdatePanel3.Visible = false;
                UpdatePanel4.Visible = false;
                UpdatePanel5.Visible = false;
                UpdatePanel6.Visible = false;
                return;
            }

            if (ValidateUser(baseEO))
            {
                
            }else
            {
                Master.HideBtnSave2();
            }
        }
        else
        {
            Approver1Confirm.Visible = false;
            Approver1Reject.Visible = false;
            ApproveRemark1.Visible = false;
            Approver1ConfirmDate.Visible = false;

            Approver2Confirm.Visible = false;
            Approver2Reject.Visible = false;
            ApproveRemark2.Visible = false;
            Approver2ConfirmDate.Visible = false;

            Approver3Confirm.Visible = false;
            Approver3Reject.Visible = false;
            ApproveRemark3.Visible = false;
            Approver3ConfirmDate.Visible = false;

            Approver4Confirm.Visible = false;
            Approver4Reject.Visible = false;
            ApproveRemark4.Visible = false;
            Approver4ConfirmDate.Visible = false;
        }

        MemoNumber.Text = baseEO.MemoNumber;
        Status.Text = "[" + baseEO.MemoStatusText + "]";

        if (baseEO.MemoDate.Ticks < MemoDate.MinDate.Ticks)
        {
            MemoDate.SelectedDate = DateTime.Now;
        }
        else
        {
            MemoDate.SelectedDate = baseEO.MemoDate;
        }
        
        if (baseEO.ID > 0)
        {
            ApplicantName.Text = baseEO.ApplicantName;
            Department.Text = baseEO.Department;
        
        }
        else
        {
            ApplicantName.Text = CurrentUser.Name;
            Department.Text = CurrentUser.Section;
            //DeliveryAt.Text = CurrentUser.Section;
        }

        Subject.Text = baseEO.Subject;
        EmailCC.Text = baseEO.EmailCC;
        Detail.Text = baseEO.Detail;

        // pr confirma
        if (baseEO.Approver1confirmStatus > 0)
        {
            if (baseEO.Approver1confirmStatus == 1)
            {
                Approver1Confirm.Checked = true;
            }
            else if (baseEO.Approver1confirmStatus == 2)
            {
                Approver1Reject.Checked = true;
            }
        }
        Approver1ConfirmDate.Text = baseEO.Approver1confirmDate.ToString();
        if (baseEO.Approver1confirmBy > 0)
        {
            UserAccountEO tempUser = new UserAccountEO();
            tempUser.Load(baseEO.Approver1confirmBy);
            Approver1ConfirmBy.Text = tempUser.Username;
            Approver1ConfirmByID.Value = tempUser.ID.ToString();
            ApproveRemark1.Text = baseEO.ApproveRemark1;
        }
        else
        {
            Approver1ConfirmBy.Text = "";
            Approver1ConfirmByID.Value = "0";
            ApproveRemark1.Text = "";
        }

        // requestor confirma
        if (baseEO.Approver2confirmStatus > 0)
        {
            if (baseEO.Approver2confirmStatus == 1)
            {
                Approver2Confirm.Checked = true;
            }
            else if (baseEO.Approver2confirmStatus == 2)
            {
                Approver2Reject.Checked = true;
            }
        }
        Approver2ConfirmDate.Text = baseEO.Approver1confirmDate.ToString();
        if (baseEO.Approver2confirmBy > 0)
        {
            UserAccountEO tempUser = new UserAccountEO();
            tempUser.Load(baseEO.Approver2confirmBy);
            Approver2ConfirmBy.Text = tempUser.Username;
            Approver2ConfirmByID.Value = tempUser.ID.ToString();
            ApproveRemark2.Text = baseEO.ApproveRemark2;
        }
        else
        {
            Approver2ConfirmBy.Text = "";
            Approver2ConfirmByID.Value = "0";
            ApproveRemark2.Text = "";
        }

        // review confirma
        if (baseEO.Approver3confirmStatus > 0)
        {
            if (baseEO.Approver3confirmStatus == 1)
            {
                Approver3Confirm.Checked = true;
            }
            else if (baseEO.Approver3confirmStatus == 2)
            {
                Approver3Reject.Checked = true;
            }
        }
        Approver3ConfirmDate.Text = baseEO.Approver3confirmDate.ToString();
        if (baseEO.Approver3confirmBy > 0)
        {
            UserAccountEO tempUser = new UserAccountEO();
            tempUser.Load(baseEO.Approver3confirmBy);
            Approver3ConfirmBy.Text = tempUser.Username;
            Approver3ConfirmByID.Value = tempUser.ID.ToString();
            ApproveRemark3.Text = baseEO.ApproveRemark3;
        }
        else
        {
            Approver3ConfirmBy.Text = "";
            Approver3ConfirmByID.Value = "0";
            ApproveRemark3.Text = "";
        }

        // approver confirma
        if (baseEO.Approver4confirmStatus > 0)
        {
            if (baseEO.Approver4confirmStatus == 1)
            {
                Approver4Confirm.Checked = true;
            }
            else if (baseEO.Approver4confirmStatus == 2)
            {
                Approver4Reject.Checked = true;
            }
        }
        Approver4ConfirmDate.Text = baseEO.Approver4confirmDate.ToString();
        if (baseEO.Approver4confirmBy > 0)
        {
            UserAccountEO tempUser = new UserAccountEO();
            tempUser.Load(baseEO.Approver4confirmBy);
            Approver4ConfirmBy.Text = tempUser.Username;
            Approver4ConfirmByID.Value = tempUser.ID.ToString();
            ApproveRemark4.Text = baseEO.ApproveRemark4; ;
        }
        else
        {
            Approver4ConfirmBy.Text = "";
            Approver4ConfirmByID.Value = "0";
            ApproveRemark4.Text = "";
        }

        MemoStatus.Text = baseEO.MemoStatus.ToString();

        ViewState[VIEW_STATE_KEY] = baseEO;
    }

    private bool CheckAuthorization(int insertUserAccountId, int a1, int a2, int a3, int a4, int MemoStatus, string Email)
    {
        // Admin role
        RoleEO role = new RoleEO();
        role.Load(1);
        if (role.RoleUserAccounts.IsUserInRole(CurrentUser.ID))
        {
            return true;
        }


        if (a1 == CurrentUser.ID)
        {
            return true;
        }else
        {
            Approver1Confirm.Enabled = false;
            Approver1Reject.Enabled = false;
        }

        if (a2 == CurrentUser.ID)
        {
            return true;
        }else
        {
            Approver2Confirm.Enabled = false;
            Approver2Reject.Enabled = false;
        }

        if (a3 == CurrentUser.ID)
        {
            return true;
        }else
        {
            Approver3Confirm.Enabled = false;
            Approver3Reject.Enabled = false;
        }

        if (a4 == CurrentUser.ID)
        {
            return true;
        }else
        {
            Approver4Confirm.Enabled = false;
            Approver4Reject.Enabled = false;
        }


        // User that created have right
        if (insertUserAccountId == CurrentUser.ID)
        {
            return true;
        }

        if (Email.ToLower().Contains(CurrentUser.Email.ToLower()))
        {
            return true;
        }

        return false;
    }

    private bool ValidateUser(MemoEO baseEO)
    {
        if (baseEO.MemoStatus == 2)
        {
            if (baseEO.Approver1confirmBy == CurrentUser.ID)
            {
                return true;
            }
        }
        if (baseEO.MemoStatus == 3)
        {
            if (baseEO.Approver2confirmBy == CurrentUser.ID)
            {
                return true;
            }
        }
        if (baseEO.MemoStatus == 4)
        {
            if (baseEO.Approver3confirmBy == CurrentUser.ID)
            {
                return true;
            }
        }
        if (baseEO.MemoStatus == 5)
        {
            if (baseEO.Approver4confirmBy == CurrentUser.ID)
            {
                return true;
            }
        }
        
        // Admin role
        RoleEO role = new RoleEO();
        role.Load(1);
        if (role.RoleUserAccounts.IsUserInRole(CurrentUser.ID))
        {
            return true;
        }

        if (baseEO.EmailCC.ToLower().Contains(CurrentUser.Email.ToLower()))
        {
            return true;
        }

        return false;
    }

    protected override void LoadControls()
    {

    }

    protected override void GoToGridPage()
    {
        if (ViewState["PreviousPageUrl"] != null)
        {
            string PreviousPageUrl = Convert.ToString(ViewState["PreviousPageUrl"]);
            if (!string.IsNullOrEmpty(PreviousPageUrl))
            {
                Response.Redirect(PreviousPageUrl);
            }
        }

        Response.Redirect("OverallMemoRequests.aspx");
    }

    public override string MenuItemName()
    {
        return "PR Requests";
    }

    public override string[] CapabilityNames()
    {
        return new string[] { "PR Requests" };
    }
    #endregion Override

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Master.SaveButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        Master.PrintButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_PrintButton_Click);
        Master.AddButton_Click += new DotBambooEditPage.ButtonClickedHandler(Master_AddButton_Click);
        
    }

    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        ValidationErrors validationErrors = new ValidationErrors();

        MemoEO pr = (MemoEO)ViewState[VIEW_STATE_KEY];
        LoadObjectFromScreen(pr);

        if (pr.ID == 0)
        {
            pr.MemoNumber = pr.GetNextMemoNumber(pr.MemoDate.Year, pr.MemoDate.Month);
            Match match = Regex.Match(pr.MemoNumber, @"MEMO\d+-\d+-(\d+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                pr.MemoRunnum = Convert.ToInt32(match.Groups[1].Value);
            }
            else
            {
                pr.MemoRunnum = 0;
            }
        }
        

        if (pr.MemoStatus == 2)
        {
            pr.Approver1confirmDate = DateTime.Now;
            pr.Approver1confirmBy = CurrentUser.ID;
        }
        if (pr.MemoStatus == 3)
        {
            pr.Approver2confirmDate = DateTime.Now;
            pr.Approver2confirmBy = CurrentUser.ID;
        }
        if (pr.MemoStatus == 4)
        {
            pr.Approver3confirmDate = DateTime.Now;
            pr.Approver3confirmBy = CurrentUser.ID;
        }
        if (pr.MemoStatus == 5 || pr.MemoStatus == 15)
        {
            pr.Approver4confirmDate = DateTime.Now;
            pr.Approver4confirmBy = CurrentUser.ID;
        }

        foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
        {
            MemoAttachmentEO attachment = new MemoAttachmentEO();

            byte[] fileData = new byte[file.InputStream.Length];
            file.InputStream.Read(fileData, 0, (int)file.InputStream.Length);
            
            attachment.MemoNumber = pr.MemoNumber;
            attachment.Filename = file.FileName;
            attachment.Content = fileData;

            pr.Attachments.Add(attachment);
        }
        
        if (!pr.Save(ref validationErrors, CurrentUser.ID))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            foreach (UploadedFile file in RadAsyncUpload1.UploadedFiles)
            {
                var path = Server.MapPath("~/Documents/") + pr.MemoNumber;
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                
                if (!File.Exists(path + "/" + file.FileName))
                {
                    using (var output = new FileStream(path + "/" + file.FileName, FileMode.CreateNew))
                    {
                        byte[] data = new byte[file.InputStream.Length];
                        file.InputStream.Read(data, 0, (int)file.InputStream.Length);
                        output.Write(data, 0, data.Length);
                    }
                }
            }

            // Reload the globals
            // Page.Response.Redirect(string.Format("PrGeneralRequest.aspx?ID={0}", pr.ID));
            GoToGridPage();
        }
    }



    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        GoToGridPage();
    }

    void Master_AddButton_Click(Object sender, EventArgs e)
    {
        Response.Redirect("MemoRequest.aspx?id=0");
    }

    void Master_PrintButton_Click(object sender, EventArgs e)
    {
        ReportDocument report = new ReportDocument();

        MemoEO pr = new MemoEO();
        pr.Load(GetId());

        if (pr.ID > 0)
        {
            string report_name = "MemoRequest.rpt";
            report.Load(Server.MapPath(report_name));
            
            UserAccountEO user = new UserAccountEO();
            user.Load(pr.Approver1confirmBy);

            if (user.ID > 0)
            {
                report.SetParameterValue("Signature1", user.Username);
                report.SetParameterValue("Approver1", user.Name);
                report.SetParameterValue("Approver1Date", pr.Approver1confirmDate.ToString("dd/MM/yyyy"));
            }
            else
            {
                report.SetParameterValue("Signature1", "");
                report.SetParameterValue("Approver1", "");
                report.SetParameterValue("Approver1Date", "");
            }

            if (pr.Approver2confirmBy > 0)
            {
                user.Load(pr.Approver2confirmBy);

                if (user.ID > 0)
                {
                    report.SetParameterValue("Signature2", user.Username);
                    report.SetParameterValue("Approver2", user.Name);
                    report.SetParameterValue("Approver2Date", pr.Approver2confirmDate.ToString("dd/MM/yyyy"));
                }
                else
                {
                    report.SetParameterValue("Signature2", "");
                    report.SetParameterValue("Approver2", "");
                    report.SetParameterValue("Approver2Date", "");
                }
            }else
            {
                report.SetParameterValue("Signature2", "");
                report.SetParameterValue("Approver2", "N/A");
                report.SetParameterValue("Approver2Date", "-");
            }
            

            if (pr.Approver3confirmBy > 0)
            {
                user.Load(pr.Approver3confirmBy);

                if (user.ID > 0)
                {
                    report.SetParameterValue("Signature3", user.Username);
                    report.SetParameterValue("Approver3", user.Name);
                    report.SetParameterValue("Approver3Date", pr.Approver3confirmDate.ToString("dd/MM/yyyy"));
                }
                else
                {
                    report.SetParameterValue("Signature3", "");
                    report.SetParameterValue("Approver3", "");
                    report.SetParameterValue("Approver3Date", "");
                }
            } else
            {
                report.SetParameterValue("Signature3", "");
                report.SetParameterValue("Approver3", "N/A");
                report.SetParameterValue("Approver3Date", "-");
            }
            
            if (pr.Approver4confirmBy > 0)
            {
                user.Load(pr.Approver4confirmBy);

                if (user.ID > 0)
                {
                    report.SetParameterValue("Signature4", user.Username);
                    report.SetParameterValue("Approver4", user.Name);
                    report.SetParameterValue("Approver4Date", pr.Approver4confirmDate.ToString("dd/MM/yyyy"));
                }
                else
                {
                    report.SetParameterValue("Signature4", "");
                    report.SetParameterValue("Approver4", "");
                    report.SetParameterValue("Approver4Date", "");
                }
            }else
            {
                report.SetParameterValue("Signature4", "");
                report.SetParameterValue("Approver4", "N/A");
                report.SetParameterValue("Approver4Date", "-");
            }
           

            user.Load(pr.InsertUserAccountId);

            if (user.ID > 0)
            {
                report.SetParameterValue("RequesterName", user.Name);
                report.SetParameterValue("Department", user.Section);
            }
            else
            {
                report.SetParameterValue("RequesterName", "");
                report.SetParameterValue("Department", "");
            }

            report.SetParameterValue("RequesterDate", pr.MemoDate.ToString("dd MMM yyyy"));
            report.SetParameterValue("MemoNumber", pr.MemoNumber);
            report.SetParameterValue("Subject", pr.Subject);
            report.SetParameterValue("Detail", pr.Detail);


            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

            //Cleanup the report after that!
            report.Close();
            report.Dispose();
        }else
        {
            //Cleanup the report after that!
            report.Close();
            report.Dispose();

            Response.Clear();
            Response.Write("No data for this report");
            Response.End();
        }
    }

    #endregion Page Events

    
    protected void RadGrid3_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        MemoAttachmentEOList attachments = new MemoAttachmentEOList();
        attachments.LoadByMemoId(GetId());
        RadGrid3.DataSource = attachments;
    }

    protected void RadGrid3_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "DownloadMyFile")
        {
            GridDataItem item = e.Item as GridDataItem;
            // ACCESS KEy
            string strID = item.GetDataKeyValue("ID").ToString();
            //ACCESS COLUMN
            string strName = item["Name"].Text;

            MemoAttachmentEO att = new MemoAttachmentEO();
            att.Load(1);
            byte[] xmlb = att.Content;
            // Download File
            try
            {
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + att.Filename);
                Response.OutputStream.Write(xmlb, 0, xmlb.Length);
                Response.Flush();
            }
            catch (Exception ex)
            {
                // An error occurred.. 
            }
        }
    }
    
    private int getClientPrCount()
    {
        var count = 0;
        
        return count;
    }
    
}