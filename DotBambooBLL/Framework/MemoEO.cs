using DotBambooDAL;
using DotBambooDAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace DotBambooBLL.Framework
{
	[Serializable()]
    public class MemoEO: BaseEO
    {
        public MemoEO()
		{
            // 1 - new
            // 2 - approv1
            // 3 - approv2
            // 4 - approv3
            // 5 - approv4
            // 7 - done
            // 10 - cancel
            // 11 - reject

            MemoStatus = 1;
            Attachments = new MemoAttachmentEOList();
        }

        public static int PRTodoByUserID(int ID)
        {
            return MemoData.GetTodoByUserID(ID);
        }

        public static int PRPendingByUserID(int ID)
        {
            return MemoData.GetPendingByUserID(ID);
        }

        public static int PRDoneByUserID(int ID)
        {
            return MemoData.GetDoneByUserID(ID);
        }

        public static int PRTotalByUserID(int ID)
        {
            return MemoData.GetTotalByUserID(ID);
        }

        #region Properties
        public int MemoId { get; set; }
		public string MemoNumber { get; set; }
		public DateTime MemoDate { get; set; }
		public int MemoYear { get; set; }
		public int MemoMonth { get; set; }
		public int MemoRunnum { get; set; }
		public int MemoType { get; set; }
		public int MemoStatus { get; set; }
		public string ApplicantName { get; set; }
		public string Department { get; set; }
		public string Subject { get; set; }
        public string EmailCC { get; set; }
		public string Detail { get; set; }
		public int CancelRejectStatus { get; set; }
		public int Approver1confirmStatus { get; set; }
		public DateTime Approver1confirmDate { get; set; }
		public int Approver1confirmBy { get; set; }
        public string ApproveRemark1 { get; set; }
        public string ApproveRemark2 { get; set; }
        public string ApproveRemark3 { get; set; }
        public string ApproveRemark4 { get; set; }
        public int Approver2confirmStatus { get; set; }
		public DateTime Approver2confirmDate { get; set; }
		public int Approver2confirmBy { get; set; }
		public int Approver3confirmStatus { get; set; }
		public DateTime Approver3confirmDate { get; set; }
		public int Approver3confirmBy { get; set; }
		public int Approver4confirmStatus { get; set; }
		public DateTime Approver4confirmDate { get; set; }
		public int Approver4confirmBy { get; set; }
		public DateTime CancelDate { get; set; }
		public int CancelBy { get; set; }

        public MemoAttachmentEOList Attachments { get; set; }

        public string MemoStatusText
        {
            get
            {
                if (CancelRejectStatus == 1)
                {
                    return "Cancel";
                }
                if (CancelRejectStatus == 2)
                {
                    return "Reject 1";
                }
                if (CancelRejectStatus == 3)
                {
                    return "Reject 2";
                }
                if (CancelRejectStatus == 4)
                {
                    return "Reject 3";
                }
                if (CancelRejectStatus == 5)
                {
                    return "Reject 4";
                }

                if (MemoStatus == 1)
                {
                    return "New Memo";
                }
                if (MemoStatus == 2)
                {
                    return "Waiting Approver 1 approve";
                }

                if (MemoStatus == 3)
                {
                    return "Waiting Approver 2 approve";
                }

                if (MemoStatus == 4)
                {
                    return "Waiting Approver 3 approve";
                }

                if (MemoStatus == 5)
                {
                    return "Waiting Approver 4 approve";
                }

                if (MemoStatus == 7)
                {
                    return "Memo Approved";
                }
                
                return "Inprogress";
            }
        }

        #endregion Properties

        #region Overrides
        public override bool Load(int id)
        {
            Memo memo = new MemoData().Select(id);
            if (memo != null)
            {
                MapEntityToProperties(memo);
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public override bool Save(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors, int userAccountId)
        {
			if (DBAction == DBActionEnum.Save)
            {
                Validate(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    if (IsNewRecord())
                    {
                        if (MemoStatus == 1)
                        {
                            MemoStatus = 2;
                        }

                        ID = new MemoData().Insert(db, MemoNumber, MemoDate, MemoYear, MemoMonth, MemoRunnum, MemoType, MemoStatus, ApplicantName, Department, Subject, EmailCC, Detail, CancelRejectStatus, Approver1confirmStatus, Approver1confirmDate, Approver1confirmBy, Approver2confirmStatus, Approver2confirmDate, Approver2confirmBy, Approver3confirmStatus, Approver3confirmDate, Approver3confirmBy, Approver4confirmStatus, Approver4confirmDate, Approver4confirmBy, CancelDate, CancelBy, ApproveRemark1, ApproveRemark2, ApproveRemark3, ApproveRemark4, userAccountId);

                        if (MemoStatus == 2)
                        {
                            // Insert into purchase_todo
                            PurchaseTodoEO todo = new PurchaseTodoEO();
                            todo.PurchaseId = ID;
                            todo.UserAccountId = Approver1confirmBy;
                            todo.Save(ref validationErrors, userAccountId);

                            NotificationEO myNotification = new NotificationEO();
                            if (myNotification.Load(db, MemoStatus))
                            {
                                //Get the new owner's email address
                                UserAccountEO newOwner = new UserAccountEO();
                                newOwner.Load(db, todo.UserAccountId);

                                EmailEO email = new EmailEO
                                {
                                    FromEmailAddress = myNotification.FromEmailAddress,
                                    Subject = ReplaceTokens(myNotification.Subject, this),
                                    Body = ReplaceTokens(myNotification.Body, this),
                                    EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                    ToEmailAddress = newOwner.Email,
                                    CcEmailAddress = EmailCC,
                                };

                                email.Save(db, ref validationErrors, userAccountId);
                            }
                        }
                    }
                    else
                    {
                        if (Approver1confirmStatus == 2)
                        {
                            CancelRejectStatus = 2;
                            MemoStatus = 12;
                            
                        }
                        if (Approver2confirmStatus == 2)
                        {
                            CancelRejectStatus = 3;
                            MemoStatus = 12;
                            
                        }
                        if (Approver3confirmStatus == 2)
                        {
                            CancelRejectStatus = 4;
                            MemoStatus = 12;
                            
                        }
                        if (Approver4confirmStatus == 2)
                        {
                            CancelRejectStatus = 5;
                            MemoStatus = 12;
                            
                        }

                        if (CancelRejectStatus == 1)
                        {
                            CancelBy = userAccountId;
                            CancelDate = DateTime.Now;
                            MemoStatus = 11;
                        }

                        if (CancelRejectStatus > 0)
                        {
                            PurchaseTodoData.DeletePurchaseId(ID);

                            NotificationEO myNotification = new NotificationEO();
                            if (myNotification.Load(db, MemoStatus))
                            {
                                //Get the new owner's email address
                                UserAccountEO newOwner = new UserAccountEO();
                                newOwner.Load(db, InsertUserAccountId);

                                EmailEO email = new EmailEO
                                {
                                    FromEmailAddress = myNotification.FromEmailAddress,
                                    Subject = ReplaceTokens(myNotification.Subject, this),
                                    Body = ReplaceTokens(myNotification.Body, this),
                                    EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                    ToEmailAddress = newOwner.Email,
                                    CcEmailAddress = EmailCC,
                                };

                                email.Save(db, ref validationErrors, userAccountId);
                            }
                        }

                        if (CancelRejectStatus == 0)
                        {
                            if (MemoStatus == 2)
                            {

                            }
                            if (MemoStatus == 1)
                            {
                                MemoStatus = 2;

                                PurchaseTodoData.DeletePurchaseId(ID);

                                // Insert into purchase_todo
                                PurchaseTodoEO todo = new PurchaseTodoEO();
                                todo.PurchaseId = ID;
                                todo.UserAccountId = Approver1confirmBy;
                                todo.Save(ref validationErrors, userAccountId);

                                NotificationEO myNotification = new NotificationEO();
                                if (myNotification.Load(db, MemoStatus))
                                {
                                    //Get the new owner's email address
                                    UserAccountEO newOwner = new UserAccountEO();
                                    newOwner.Load(db, todo.UserAccountId);

                                    EmailEO email = new EmailEO
                                    {
                                        FromEmailAddress = myNotification.FromEmailAddress,
                                        Subject = ReplaceTokens(myNotification.Subject, this),
                                        Body = ReplaceTokens(myNotification.Body, this),
                                        EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                        ToEmailAddress = newOwner.Email,
                                        CcEmailAddress = EmailCC,
                                    };

                                    email.Save(db, ref validationErrors, userAccountId);
                                }
                            }
                            else if (MemoStatus == 2)
                            {
                                MemoStatus = 3;

                                PurchaseTodoData.DeletePurchaseId(ID);

                                if (Approver2confirmBy > 0)
                                {
                                    // Insert into purchase_todo
                                    PurchaseTodoEO todo = new PurchaseTodoEO();
                                    todo.PurchaseId = ID;
                                    todo.UserAccountId = Approver2confirmBy;
                                    todo.Save(ref validationErrors, userAccountId);

                                    NotificationEO myNotification = new NotificationEO();
                                    if (myNotification.Load(db, MemoStatus))
                                    {
                                        //Get the new owner's email address
                                        UserAccountEO newOwner = new UserAccountEO();
                                        newOwner.Load(db, todo.UserAccountId);

                                        EmailEO email = new EmailEO
                                        {
                                            FromEmailAddress = myNotification.FromEmailAddress,
                                            Subject = ReplaceTokens(myNotification.Subject, this),
                                            Body = ReplaceTokens(myNotification.Body, this),
                                            EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                            ToEmailAddress = newOwner.Email,
                                            CcEmailAddress = EmailCC,
                                        };

                                        email.Save(db, ref validationErrors, userAccountId);
                                    }
                                }
                                
                            }
                            else if (MemoStatus == 3)
                            {
                                MemoStatus = 4;

                                PurchaseTodoData.DeletePurchaseId(ID);

                                if (Approver3confirmBy > 0)
                                {
                                    // Insert into purchase_todo
                                    PurchaseTodoEO todo = new PurchaseTodoEO();
                                    todo.PurchaseId = ID;
                                    todo.UserAccountId = Approver3confirmBy;
                                    todo.Save(ref validationErrors, userAccountId);

                                    NotificationEO myNotification = new NotificationEO();
                                    if (myNotification.Load(db, MemoStatus))
                                    {
                                        //Get the new owner's email address
                                        UserAccountEO newOwner = new UserAccountEO();
                                        newOwner.Load(db, todo.UserAccountId);

                                        EmailEO email = new EmailEO
                                        {
                                            FromEmailAddress = myNotification.FromEmailAddress,
                                            Subject = ReplaceTokens(myNotification.Subject, this),
                                            Body = ReplaceTokens(myNotification.Body, this),
                                            EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                            ToEmailAddress = newOwner.Email,
                                            CcEmailAddress = EmailCC,
                                        };

                                        email.Save(db, ref validationErrors, userAccountId);
                                    }
                                }
                                
                            }
                            else if (MemoStatus == 4)
                            {
                                MemoStatus = 5;

                                PurchaseTodoData.DeletePurchaseId(ID);

                                if (Approver4confirmBy > 0)
                                {
                                    // Insert into purchase_todo
                                    PurchaseTodoEO todo = new PurchaseTodoEO();
                                    todo.PurchaseId = ID;
                                    todo.UserAccountId = Approver4confirmBy;
                                    todo.Save(ref validationErrors, userAccountId);

                                    NotificationEO myNotification = new NotificationEO();
                                    if (myNotification.Load(db, MemoStatus))
                                    {
                                        //Get the new owner's email address
                                        UserAccountEO newOwner = new UserAccountEO();
                                        newOwner.Load(db, todo.UserAccountId);

                                        EmailEO email = new EmailEO
                                        {
                                            FromEmailAddress = myNotification.FromEmailAddress,
                                            Subject = ReplaceTokens(myNotification.Subject, this),
                                            Body = ReplaceTokens(myNotification.Body, this),
                                            EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                            ToEmailAddress = newOwner.Email,
                                            CcEmailAddress = EmailCC,
                                        };

                                        email.Save(db, ref validationErrors, userAccountId);
                                    }
                                }
                                
                            }
                            else if (MemoStatus == 5)
                            {
                                MemoStatus = 7;

                                /*
                                PurchaseTodoData.DeletePurchaseId(ID);

                                NotificationEO myNotification = new NotificationEO();
                                if (myNotification.Load(db, MemoStatus))
                                {
                                    //Get the new owner's email address
                                    UserAccountEO newOwner = new UserAccountEO();
                                    newOwner.Load(db, InsertUserAccountId);

                                    EmailEO email = new EmailEO
                                    {
                                        FromEmailAddress = myNotification.FromEmailAddress,
                                        Subject = ReplaceTokens(myNotification.Subject, this),
                                        Body = ReplaceTokens(myNotification.Body, this),
                                        EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                        ToEmailAddress = newOwner.Email
                                    };

                                    email.Save(db, ref validationErrors, userAccountId);
                                }
                                */
                            }

                            if (Approver2confirmBy > 0 && Approver2confirmStatus == 0 && MemoStatus != 7)
                            {
                                MemoStatus = 3;
                            }
                            else
                            {
                                if (Approver3confirmBy > 0 && Approver3confirmStatus == 0 && MemoStatus != 7)
                                {
                                    MemoStatus = 4;
                                }
                                else
                                {
                                    if (Approver4confirmBy > 0 && Approver4confirmStatus == 0 && MemoStatus != 7)
                                    {
                                        MemoStatus = 5;

                                        // Insert into purchase_todo
                                        PurchaseTodoEO todo = new PurchaseTodoEO();
                                        todo.PurchaseId = ID;
                                        todo.UserAccountId = Approver4confirmBy;
                                        todo.Save(ref validationErrors, userAccountId);

                                        NotificationEO myNotification = new NotificationEO();
                                        if (myNotification.Load(db, MemoStatus))
                                        {
                                            //Get the new owner's email address
                                            UserAccountEO newOwner = new UserAccountEO();
                                            newOwner.Load(db, todo.UserAccountId);

                                            EmailEO email = new EmailEO
                                            {
                                                FromEmailAddress = myNotification.FromEmailAddress,
                                                Subject = ReplaceTokens(myNotification.Subject, this),
                                                Body = ReplaceTokens(myNotification.Body, this),
                                                EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                                ToEmailAddress = newOwner.Email,
                                                CcEmailAddress = EmailCC
                                               
                                            };

                                            email.Save(db, ref validationErrors, userAccountId);
                                        }
                                    }
                                    else
                                    {
                                        MemoStatus = 7;

                                        PurchaseTodoData.DeletePurchaseId(ID);

                                        NotificationEO myNotification = new NotificationEO();
                                        if (myNotification.Load(db, MemoStatus))
                                        {
                                            //Get the new owner's email address
                                            UserAccountEO newOwner = new UserAccountEO();
                                            newOwner.Load(db, InsertUserAccountId);

                                            EmailEO email = new EmailEO
                                            {
                                                FromEmailAddress = myNotification.FromEmailAddress,
                                                Subject = ReplaceTokens(myNotification.Subject, this),
                                                Body = ReplaceTokens(myNotification.Body, this),
                                                EmailStatusFlag = EmailEO.EmailStatusFlagEnum.NotSent,
                                                ToEmailAddress = newOwner.Email,
                                                CcEmailAddress = EmailCC
                                            };

                                            email.Save(db, ref validationErrors, userAccountId);
                                        }
                                    }
                                }
                            }
                        }

                        if (!new MemoData().Update(db, ID, MemoNumber, MemoDate, MemoYear, MemoMonth, MemoRunnum, MemoType, MemoStatus, ApplicantName, Department, Subject, EmailCC, Detail, CancelRejectStatus, Approver1confirmStatus, Approver1confirmDate, Approver1confirmBy, Approver2confirmStatus, Approver2confirmDate, Approver2confirmBy, Approver3confirmStatus, Approver3confirmDate, Approver3confirmBy, Approver4confirmStatus, Approver4confirmDate, Approver4confirmBy, CancelDate, CancelBy, ApproveRemark1, ApproveRemark2, ApproveRemark3, ApproveRemark4, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }

                    foreach (MemoAttachmentEO item in Attachments)
                    {
                        item.MemoId = ID;
                        item.MemoNumber = MemoNumber;
                    }

                    if (Attachments.Save(db, ref validationErrors, userAccountId))
                    {

                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
		}

        private bool ValidateEmail(string mail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail);
            if (!match.Success)
            {
                return false;
            }
            return true;
        }
		
		protected override void Validate(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
            if (string.IsNullOrEmpty(Department))
            {
                validationErrors.Add("Department can't be blank.");
            }

            if (string.IsNullOrEmpty(Subject))
            {
                validationErrors.Add("Subject can't be blank.");
            }

            if (Approver1confirmBy <= 0)
            {
                validationErrors.Add("Approver 1 can't be blank.");
            }

            if (!string.IsNullOrEmpty(EmailCC))
            {
                string[] email_list = EmailCC.Split(';');
                foreach (var mail in email_list)
                {
                    if (!ValidateEmail(mail))
                    {
                        validationErrors.Add("Email CC is not valid");
                    }
                }
            }

            if (MemoStatus == 2)
            {
                if (Approver1confirmStatus == 0 && CancelRejectStatus == 0)
                {
                    validationErrors.Add("Please select Approve or Reject option (1)");
                }
            }

            if (MemoStatus == 3)
            {
                if (Approver2confirmStatus == 0 && CancelRejectStatus == 0)
                {
                    validationErrors.Add("Please select Approve or Reject option (2)");
                }
            }

            if (MemoStatus == 4)
            {
                if (Approver3confirmStatus == 0 && CancelRejectStatus == 0)
                {
                    validationErrors.Add("Please select Approve or Reject option (3)");
                }
            }

            if (MemoStatus == 5)
            {
                if (Approver4confirmStatus == 0 && CancelRejectStatus == 0)
                {
                    validationErrors.Add("Please select Approve or Reject option (4)");
                }
            }
        }
		
		public override void Init()
        {
		}
		
		protected override void DeleteForReal(DotBambooDAL.DotBambooDataContext db)
        {
		}
		
		protected override void ValidateDelete(DotBambooDAL.DotBambooDataContext db, ref ValidationErrors validationErrors)
        {
		}
		
		protected override string GetDisplayText()
        {
			return "NOT SET";
		}
		
		protected override void MapEntityToCustomProperties(DotBambooDAL.Framework.IBaseEntity entity)
        {
			Memo memo = (Memo)entity;
			
			ID = memo.MemoId;
			MemoId = memo.MemoId;
			MemoNumber = memo.MemoNumber;
			MemoDate = memo.MemoDate;
			MemoYear = memo.MemoYear;
			MemoMonth = memo.MemoMonth;
			MemoRunnum = memo.MemoRunnum;
			MemoType = memo.MemoType;
			MemoStatus = memo.MemoStatus;
			ApplicantName = memo.ApplicantName;
			Department = memo.Department;
			Subject = memo.Subject;
            EmailCC = memo.CcEmailAddress;
			Detail = memo.Detail;
			CancelRejectStatus = memo.CancelRejectStatus;
			Approver1confirmStatus = memo.Approver1ConfirmStatus;
			Approver1confirmDate = memo.Approver1ConfirmDate;
			Approver1confirmBy = memo.Approver1ConfirmBy;
			Approver2confirmStatus = memo.Approver2ConfirmStatus;
			Approver2confirmDate = memo.Approver2ConfirmDate;
			Approver2confirmBy = memo.Approver2ConfirmBy;
			Approver3confirmStatus = memo.Approver3ConfirmStatus;
			Approver3confirmDate = memo.Approver3ConfirmDate;
			Approver3confirmBy = memo.Approver3ConfirmBy;
			Approver4confirmStatus = memo.Approver4ConfirmStatus;
			Approver4confirmDate = memo.Approver4ConfirmDate;
			Approver4confirmBy = memo.Approver4ConfirmBy;
			CancelDate = memo.CancelDate;
			CancelBy = memo.CancelBy;
            ApproveRemark1 = memo.ApproveRemark1;
            ApproveRemark2 = memo.ApproveRemark2;
            ApproveRemark3 = memo.ApproveRemark3;
            ApproveRemark4 = memo.ApproveRemark4;

        }
        #endregion Overrides

        private string ReplaceTokens(string text, BaseEO baseEO)
        {
            List<Token> tokens = new List<Token>();

            UserAccountEO user = new UserAccountEO();
            if (user.Load(Approver1confirmBy))
            {
                tokens.Add(new Token { TokenString = "<APPROVER1>", Value = user.Name });
            }
            if (user.Load(Approver2confirmBy))
            {
                tokens.Add(new Token { TokenString = "<APPROVER2>", Value = user.Name });
            }
            if (user.Load(Approver3confirmBy))
            {
                tokens.Add(new Token { TokenString = "<APPROVER3>", Value = user.Name });
            }
            if (user.Load(Approver4confirmBy))
            {
                tokens.Add(new Token { TokenString = "<APPROVER4>", Value = user.Name });
            }

            tokens.Add(new Token { TokenString = "<SUBJECT>", Value = Subject });

            //state
            tokens.Add(new Token { TokenString = "<MEMONUMBER>", Value = MemoNumber });

            //requestor
            tokens.Add(new Token { TokenString = "<REQUESTOR>", Value = ApplicantName });

            //section
            tokens.Add(new Token { TokenString = "<DEPARTMENT>", Value = Department });

            tokens.Add(new Token { TokenString = "<DETAIL>", Value = Detail.Replace(Environment.NewLine, "<br />") });

            //state
            tokens.Add(new Token { TokenString = "<WFSTATE>", Value = MemoStatusText });

            //submit date            
            tokens.Add(new Token { TokenString = "<WFSUBMITDATE>", Value = (baseEO.InsertDate == DateTime.MinValue ? DateTime.Now.ToString("MM/dd/yyyy") : baseEO.InsertDate.ToString("MM/dd/yyyy")) });

            //link, get the page from the workflow.            
            tokens.Add(new Token { TokenString = "<LINK>", Value = "http://10.1.1.195/MemoRequest.aspx?id=" + baseEO.ID.ToString() });

            return NotificationEO.ReplaceTokens(tokens, text);
        }

        public string GetNextMemoNumber(int year, int month)
        {
            int max = new MemoData().GetMaxMemoNumber(year, month);

            int next = max + 1;

            return string.Format("MEMO{0:D4}-{1:D2}-{2:D3}", year, month, next);
        }
    }
	
	[Serializable()]
    public class MemoEOList :BaseEOList<MemoEO>
    {
		#region Overrides
        public override void Load()
        {
            LoadFromList(new MemoData().Select());
        }
        #endregion Overrides
		
		#region Private Methods

        protected void LoadFromList(List<Memo> Memos)
        {
            foreach (Memo memo in Memos)
            {
                MemoEO newMemoEO = new MemoEO();
                newMemoEO.MapEntityToProperties(memo);

                this.Add(newMemoEO);
            }
        }

        #endregion  Private Methods
	}
}
