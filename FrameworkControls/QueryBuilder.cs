using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DotBambooCommon;
using System.Reflection;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using DotBambooBLL.Framework;

namespace FrameworkControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:QueryBuilder runat=server></{0}:QueryBuilder>")]
    public class QueryBuilder : WebControl
    {
        #region Constants

        private const string COMPARISON_EQUALS = "Equals";
        private const string COMPARISON_NOT_EQUALS = "Is Not Equal To";
        private const string COMPARISON_IN = "Is In List";
        private const string COMPARISON_NOT_IN = "Is Not In List";
        private const string COMPARISON_IS_BLANK = "Is Blank";
        private const string COMPARISON_IS_NOT_BLANK = "Is Not Blank";
        private const string COMPARISON_IS_GREATER_THAN = "Is Greater Than";
        private const string COMPARISON_IS_GREATER_THAN_EQUAL = "Is Greater Than Or Equal To";
        private const string COMPARISON_IS_LESS_THAN = "Is Less Than";
        private const string COMPARISON_IS_LESS_THAN_OR_EQUAL = "Is Less Than Or Equal To";

        #endregion Constants

        #region Members

        private DropDownList _ddlFields;
        private DropDownList _ddlComparison;
        private DropDownList _ddlValues;
        private ListBox _lstValues;
        private ListBox _lstWhereClause;

        private Button _btnAdd;
        private Button _btnAddAnd;
        private Button _btnAddOr;
        private Button _btnRemoveLine;
        private Button _btnLeftParen;
        private Button _btnRigthParen;
        private Button _btnRemoveLeftParen;
        private Button _btnRemoveRigthParen;

        private UpdatePanel _updatePanel;
        private Panel _pleaseWaitPanel;
        HiddenField _hidDummyField;
        private ModalPopupExtender _mde;

        #endregion Members

        #region Properties

        public object[] QueryFields
        {
            get
            {
                return (object[])ViewState["QueryFields"];
            }

            set
            {
                ViewState["QueryFields"] = value;
            }
        }

        public string QueryObjectName { get; set; }

        #endregion Properties

        #region Overrides

        protected override void CreateChildControls()
        {
            _updatePanel = new UpdatePanel();

            CreateDropDowns();

            CreateWhereList();

            Controls.Add(_updatePanel);

            CreatePleaseWait();

            base.CreateChildControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                Type objectType = Type.GetType(QueryObjectName);
                object listObject = Activator.CreateInstance(objectType);

                //Call the method to load the object
                QueryFields = (object[])objectType.InvokeMember("GetCustomAttributes", BindingFlags.InvokeMethod, null, listObject, new object[] { });

                foreach (object attribute in QueryFields)
                {
                    if (attribute is QueryFieldAttribute)
                    {
                        QueryFieldAttribute qfa = (QueryFieldAttribute)attribute;

                        _ddlFields.Items.Add(new ListItem(qfa.FriendlyFieldName, qfa.FieldName));
                    }
                }

                _ddlFields.Items.Insert(0, "");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            EnsureChildControls();

            //Add custom java script
            if (Page.ClientScript.IsClientScriptBlockRegistered("PleaseWait") == false)
            {
                StringBuilder sb1 = new StringBuilder();

                sb1.Append("function pageLoad(sender, args){" + Environment.NewLine);
                sb1.Append("    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);" + Environment.NewLine);
                sb1.Append("    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);" + Environment.NewLine);
                sb1.Append("} " + Environment.NewLine);
                sb1.Append(Environment.NewLine);
                sb1.Append("function beginRequest(sender, args){" + Environment.NewLine);
                sb1.Append("    var modalPopupBehavior = $find('" + _mde.ClientID + "');" + Environment.NewLine);
                sb1.Append("    modalPopupBehavior.show();" + Environment.NewLine);
                sb1.Append("}" + Environment.NewLine);
                sb1.Append(Environment.NewLine);
                sb1.Append("function endRequest(sender, args) {" + Environment.NewLine);
                sb1.Append("    var modalPopupBehavior = $find('" + _mde.ClientID + "');" + Environment.NewLine);
                sb1.Append("    modalPopupBehavior.hide();" + Environment.NewLine);
                sb1.Append("}" + Environment.NewLine);

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "PleaseWait", sb1.ToString(), true);
            }

            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            //Add the style
            StringBuilder sb = new StringBuilder();
            sb.Append("<style type='text/css'>" + Environment.NewLine);
            sb.Append(".modalBackground {" + Environment.NewLine);
            sb.Append("    background-color:Gray;" + Environment.NewLine);
            sb.Append("    filter:alpha(opacity=70);" + Environment.NewLine);
            sb.Append("    opacity:0.7;" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(".modalPopup {" + Environment.NewLine);
            sb.Append("    background-color:#ffffdd;" + Environment.NewLine);
            sb.Append("    border-width:3px;" + Environment.NewLine);
            sb.Append("    border-style:solid;" + Environment.NewLine);
            sb.Append("    border-color:Gray;" + Environment.NewLine);
            sb.Append("    padding:3px;" + Environment.NewLine);
            sb.Append("    width:250px;" + Environment.NewLine);
            sb.Append("}" + Environment.NewLine);
            sb.Append("</style>");

            writer.Write(sb.ToString());

            base.RenderContents(writer);
        }

        #endregion Overrides

        #region Event Handlers

        void _ddlComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ddlComparison.SelectedIndex > 0)
            {
                //Get the field name
                string valueFieldName = _ddlFields.SelectedValue;
                string lookupFieldName = "";

                //Get the values for this field in the database.
                foreach (object attribute in QueryFields)
                {
                    if (attribute is QueryFieldAttribute)
                    {
                        QueryFieldAttribute qfa = (QueryFieldAttribute)attribute;

                        if (qfa.FieldName == valueFieldName)
                        {
                            if (qfa.LookupFieldName == null)
                            {
                                lookupFieldName = valueFieldName;
                            }
                            else
                            {
                                lookupFieldName = qfa.LookupFieldName;
                            }
                        }
                    }
                }

                Type objectType = Type.GetType(QueryObjectName);
                object listObject = Activator.CreateInstance(objectType);
                List<LookupData> data = (List<LookupData>)objectType.InvokeMember("GetLookup", BindingFlags.InvokeMethod, null, listObject, new object[] { lookupFieldName, valueFieldName });

                switch (_ddlComparison.SelectedItem.Text)
                {
                    case COMPARISON_EQUALS:
                    case COMPARISON_NOT_EQUALS:
                    case COMPARISON_IS_GREATER_THAN:
                    case COMPARISON_IS_GREATER_THAN_EQUAL:
                    case COMPARISON_IS_LESS_THAN:
                    case COMPARISON_IS_LESS_THAN_OR_EQUAL:
                        _ddlValues.Visible = true;
                        _lstValues.Visible = false;

                        _ddlValues.DataSource = data;
                        _ddlValues.DataTextField = "Text";
                        _ddlValues.DataValueField = "Value";
                        _ddlValues.DataBind();

                        break;
                    case COMPARISON_IN:
                    case COMPARISON_NOT_IN:
                        _ddlValues.Visible = false;
                        _lstValues.Visible = true;

                        _lstValues.DataSource = data;
                        _lstValues.DataTextField = "Text";
                        _lstValues.DataValueField = "Value";
                        _lstValues.DataBind();

                        break;
                    case COMPARISON_IS_BLANK:
                    case COMPARISON_IS_NOT_BLANK:
                        _ddlValues.Visible = false;
                        _lstValues.Visible = false;
                        break;
                }
            }
        }

        void _ddlFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ddlFields.SelectedIndex > 0)
            {
                _ddlComparison.Visible = true;
                _ddlComparison.Items.Clear();

                _ddlValues.Items.Clear();
                _ddlValues.Visible = true;
                _lstValues.Items.Clear();
                _lstValues.Visible = false;

                //Get the field name
                string friendlyFieldName = _ddlFields.SelectedItem.Text;
                QueryFieldAttribute.QueryFieldTypeEnum queryFieldType = QueryFieldAttribute.QueryFieldTypeEnum.NotSet;

                foreach (object attribute in QueryFields)
                {
                    if (attribute is QueryFieldAttribute)
                    {
                        QueryFieldAttribute qfa = (QueryFieldAttribute)attribute;

                        if (qfa.FriendlyFieldName == friendlyFieldName)
                        {
                            queryFieldType = qfa.FieldType;
                        }
                    }
                }

                switch (queryFieldType)
                {
                    case QueryFieldAttribute.QueryFieldTypeEnum.NotSet:
                        //hide drop downs
                        _ddlComparison.Visible = false;
                        break;
                    case QueryFieldAttribute.QueryFieldTypeEnum.Boolean:
                        _ddlComparison.Items.Add(COMPARISON_EQUALS);
                        _ddlComparison.Items.Add(COMPARISON_NOT_EQUALS);
                        break;
                    case QueryFieldAttribute.QueryFieldTypeEnum.String:
                    case QueryFieldAttribute.QueryFieldTypeEnum.Lookup:
                        _ddlComparison.Items.Add(COMPARISON_EQUALS);
                        _ddlComparison.Items.Add(COMPARISON_NOT_EQUALS);
                        _ddlComparison.Items.Add(COMPARISON_IN);
                        _ddlComparison.Items.Add(COMPARISON_NOT_IN);
                        _ddlComparison.Items.Add(COMPARISON_IS_BLANK);
                        _ddlComparison.Items.Add(COMPARISON_IS_NOT_BLANK);
                        break;
                    case QueryFieldAttribute.QueryFieldTypeEnum.Date:
                    case QueryFieldAttribute.QueryFieldTypeEnum.Number:
                        _ddlComparison.Items.Add(COMPARISON_EQUALS);
                        _ddlComparison.Items.Add(COMPARISON_NOT_EQUALS);
                        _ddlComparison.Items.Add(COMPARISON_IN);
                        _ddlComparison.Items.Add(COMPARISON_NOT_IN);
                        _ddlComparison.Items.Add(COMPARISON_IS_GREATER_THAN);
                        _ddlComparison.Items.Add(COMPARISON_IS_GREATER_THAN_EQUAL);
                        _ddlComparison.Items.Add(COMPARISON_IS_LESS_THAN);
                        _ddlComparison.Items.Add(COMPARISON_IS_LESS_THAN_OR_EQUAL);
                        _ddlComparison.Items.Add(COMPARISON_IS_BLANK);
                        _ddlComparison.Items.Add(COMPARISON_IS_NOT_BLANK);
                        break;
                }

                _ddlComparison.Items.Insert(0, "");
            }
            else
            {
                _ddlComparison.Visible = false;
            }
        }

        void _btnRemoveRigthParen_Click(object sender, EventArgs e)
        {
            if (_lstWhereClause.SelectedIndex > -1)
            {
                if (_lstWhereClause.SelectedItem.Text.EndsWith(")"))
                {
                    _lstWhereClause.SelectedItem.Text = _lstWhereClause.SelectedItem.Text.Substring(0, _lstWhereClause.SelectedItem.Text.Length - 1);
                    _lstWhereClause.SelectedItem.Value = _lstWhereClause.SelectedItem.Value.Substring(0, _lstWhereClause.SelectedItem.Value.Length - 1);
                }
            }
        }

        void _btnRemoveLeftParen_Click(object sender, EventArgs e)
        {
            if (_lstWhereClause.SelectedIndex > -1)
            {
                int lengthOfPrefix = -1;

                //Check if this is the first item
                if (_lstWhereClause.SelectedIndex == 0)
                {
                    if (_lstWhereClause.SelectedItem.Text.Trim().StartsWith("("))
                    {
                        lengthOfPrefix = 0;
                    }
                }
                else
                {
                    //Check if this is an AND statement
                    if (_lstWhereClause.SelectedItem.Text.Trim().Substring(0, 3) == "AND")
                    {
                        if (_lstWhereClause.SelectedItem.Text.Substring(5, 1) == "(")
                        {
                            lengthOfPrefix = 5;
                        }
                    }
                    else
                    {
                        //OR                        
                        if (_lstWhereClause.SelectedItem.Text.Substring(4, 1) == "(")
                        {
                            lengthOfPrefix = 4;
                        }
                    }
                }

                if (lengthOfPrefix > -1)
                {
                    ListItem li = _lstWhereClause.SelectedItem;
                    li.Text = li.Text.Substring(0, lengthOfPrefix) + _lstWhereClause.SelectedItem.Text.Substring(lengthOfPrefix + 1);
                    li.Value = li.Value.Substring(0, lengthOfPrefix) + _lstWhereClause.SelectedItem.Value.Substring(lengthOfPrefix + 1);
                }
            }
        }

        void _btnRigthParen_Click(object sender, EventArgs e)
        {
            if (_lstWhereClause.SelectedIndex > -1)
            {
                _lstWhereClause.Items[_lstWhereClause.SelectedIndex].Text = _lstWhereClause.SelectedItem.Text + ")";
                _lstWhereClause.Items[_lstWhereClause.SelectedIndex].Value = _lstWhereClause.SelectedItem.Value + ")";
            }
        }

        void _btnLeftParen_Click(object sender, EventArgs e)
        {
            if (_lstWhereClause.SelectedIndex > -1)
            {
                int insertParenIndex;

                //if this is the first line then just add the paren to the beginning.
                if (_lstWhereClause.SelectedIndex == 0)
                {
                    insertParenIndex = 0;
                }
                else
                {
                    //Check if this is an AND statement
                    if (_lstWhereClause.SelectedItem.Text.Trim().Substring(0, 3) == "AND")
                    {
                        insertParenIndex = 5;
                    }
                    else
                    {
                        //Must be an OR clause
                        insertParenIndex = 4;
                    }
                }

                ListItem li = _lstWhereClause.SelectedItem;
                li.Text = li.Text.Insert(insertParenIndex, "(");
                li.Value = li.Value.Insert(insertParenIndex, "(");
            }
        }

        void _btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (_lstWhereClause.SelectedIndex > -1)
            {
                bool firstLineRemoved = (_lstWhereClause.SelectedIndex == 0);

                _lstWhereClause.Items.RemoveAt(_lstWhereClause.SelectedIndex);

                if (_lstWhereClause.Items.Count == 0)
                {
                    _btnAdd.Visible = true;
                    _btnAddAnd.Visible = false;
                    _btnAddOr.Visible = false;
                    _btnRemoveLine.Visible = false;
                    _btnLeftParen.Visible = false;
                    _btnRigthParen.Visible = false;
                    _btnRemoveLeftParen.Visible = false;
                    _btnRemoveRigthParen.Visible = false;
                }
                else
                {
                    if (firstLineRemoved)
                    {
                        //Remove the and/or from the new first line.
                        if (_lstWhereClause.Items[0].Text.Trim().Substring(0, 3) == "AND")
                        {
                            _lstWhereClause.Items[0].Text = _lstWhereClause.Items[0].Text.Substring(4);
                            _lstWhereClause.Items[0].Value = _lstWhereClause.Items[0].Value.Substring(4);
                        }
                        else
                        {
                            _lstWhereClause.Items[0].Text = _lstWhereClause.Items[0].Text.Substring(3);
                            _lstWhereClause.Items[0].Value = _lstWhereClause.Items[0].Value.Substring(3);
                        }
                    }
                }
            }
        }

        void _btnAddOr_Click(object sender, EventArgs e)
        {
            AddWhereSQL(" OR ");
        }

        void _btnAddAnd_Click(object sender, EventArgs e)
        {
            AddWhereSQL(" AND ");
        }

        void _btnAdd_Click(object sender, EventArgs e)
        {
            AddWhereSQL("");

            if (_lstWhereClause.Items.Count > 0)
            {
                _btnAdd.Visible = false;
                _btnAddAnd.Visible = true;
                _btnAddOr.Visible = true;
                _btnRemoveLine.Visible = true;
                _btnLeftParen.Visible = true;
                _btnRigthParen.Visible = true;
                _btnRemoveLeftParen.Visible = true;
                _btnRemoveRigthParen.Visible = true;
            }
        }

        #endregion Event Handlers

        #region Private Methods

        private void CreateDropDowns()
        {
            //Create the table that contains the dropdown lists
            Table tblDropDowns = new Table();
            tblDropDowns.BorderWidth = 1;
            tblDropDowns.CellPadding = 1;
            tblDropDowns.CellSpacing = 1;

            //Add header
            TableRow trHeader = new TableRow();
            TableCell tcField = new TableCell();
            tcField.Text = "Select a field";
            trHeader.Cells.Add(tcField);

            TableCell tcComparison = new TableCell();
            tcComparison.Text = "Comparison";
            trHeader.Cells.Add(tcComparison);

            TableCell tcValue = new TableCell();
            tcValue.Text = "Select a value";
            trHeader.Cells.Add(tcValue);
            tblDropDowns.Rows.Add(trHeader);

            //Add drop downs
            TableRow tr = new TableRow();
            tr.VerticalAlign = VerticalAlign.Top;

            TableCell tc1 = new TableCell();
            _ddlFields = new DropDownList();
            _ddlFields.ID = "ddlFields";
            _ddlFields.AutoPostBack = true;
            _ddlFields.SelectedIndexChanged += new EventHandler(_ddlFields_SelectedIndexChanged);

            tc1.Controls.Add(_ddlFields);
            tr.Cells.Add(tc1);

            TableCell tc2 = new TableCell();
            _ddlComparison = new DropDownList();
            _ddlComparison.ID = "ddlComparison";
            _ddlComparison.AutoPostBack = true;
            _ddlComparison.SelectedIndexChanged += new EventHandler(_ddlComparison_SelectedIndexChanged);

            tc2.Controls.Add(_ddlComparison);
            tr.Cells.Add(tc2);

            TableCell tc3 = new TableCell();
            _ddlValues = new DropDownList();
            _ddlValues.ID = "ddlValues";
            tc3.Controls.Add(_ddlValues);

            _lstValues = new ListBox();
            _lstValues.ID = "lstValues";
            _lstValues.Visible = false;
            _lstValues.SelectionMode = ListSelectionMode.Multiple;
            _lstValues.Rows = 10;
            tc3.Controls.Add(_lstValues);

            tr.Cells.Add(tc3);

            CreateButtons(tr);

            tblDropDowns.Rows.Add(tr);

            _updatePanel.ContentTemplateContainer.Controls.Add(tblDropDowns);
        }

        private void CreateButtons(TableRow tr)
        {
            //Create the buttons                                    
            TableCell tc1 = new TableCell();
            _btnAdd = new Button();
            _btnAdd.ID = "btnAdd";
            _btnAdd.Text = "+";
            _btnAdd.ToolTip = "Add to filter";
            _btnAdd.Click += new EventHandler(_btnAdd_Click);
            tc1.Controls.Add(_btnAdd);
            tr.Cells.Add(tc1);

            TableCell tc2 = new TableCell();
            _btnAddAnd = new Button();
            _btnAddAnd.ID = "btnAddAnd";
            _btnAddAnd.Text = "And";
            _btnAddAnd.ToolTip = "Add And Clause";
            _btnAddAnd.Visible = false;
            _btnAddAnd.Click += new EventHandler(_btnAddAnd_Click);
            tc2.Controls.Add(_btnAddAnd);
            tr.Cells.Add(tc2);

            TableCell tc3 = new TableCell();
            _btnAddOr = new Button();
            _btnAddOr.ID = "btnAddOr";
            _btnAddOr.Text = "Or";
            _btnAddOr.ToolTip = "Add Or Clause";
            _btnAddOr.Visible = false;
            _btnAddOr.Click += new EventHandler(_btnAddOr_Click);
            tc3.Controls.Add(_btnAddOr);
            tr.Cells.Add(tc3);

            TableCell tc4 = new TableCell();
            _btnRemoveLine = new Button();
            _btnRemoveLine.ID = "btnRemoveLine";
            _btnRemoveLine.Text = "-";
            _btnRemoveLine.ToolTip = "Remove Line";
            _btnRemoveLine.Visible = false;
            _btnRemoveLine.Click += new EventHandler(_btnRemoveLine_Click);
            tc4.Controls.Add(_btnRemoveLine);
            tr.Cells.Add(tc4);

            TableCell tc5 = new TableCell();
            _btnLeftParen = new Button();
            _btnLeftParen.ID = "btnLeftParen";
            _btnLeftParen.Text = "(";
            _btnLeftParen.ToolTip = "Add Left Parenthesis";
            _btnLeftParen.Visible = false;
            _btnLeftParen.Click += new EventHandler(_btnLeftParen_Click);
            tc5.Controls.Add(_btnLeftParen);
            tr.Cells.Add(tc5);

            TableCell tc6 = new TableCell();
            _btnRigthParen = new Button();
            _btnRigthParen.ID = "btnRightParen";
            _btnRigthParen.Text = ")";
            _btnRigthParen.ToolTip = "Add Right Parenthesis";
            _btnRigthParen.Visible = false;
            _btnRigthParen.Click += new EventHandler(_btnRigthParen_Click);
            tc6.Controls.Add(_btnRigthParen);
            tr.Cells.Add(tc6);

            TableCell tc7 = new TableCell();
            _btnRemoveLeftParen = new Button();
            _btnRemoveLeftParen.ID = "btnRemoveLeftParen";
            _btnRemoveLeftParen.Text = "(-";
            _btnRemoveLeftParen.ToolTip = "Remove Left Parenthesis";
            _btnRemoveLeftParen.Visible = false;
            _btnRemoveLeftParen.Click += new EventHandler(_btnRemoveLeftParen_Click);
            tc7.Controls.Add(_btnRemoveLeftParen);
            tr.Cells.Add(tc7);

            TableCell tc8 = new TableCell();
            _btnRemoveRigthParen = new Button();
            _btnRemoveRigthParen.ID = "btnRemoveRightParen";
            _btnRemoveRigthParen.Text = ")-";
            _btnRemoveRigthParen.ToolTip = "Remove Right Parenthesis";
            _btnRemoveRigthParen.Visible = false;
            _btnRemoveRigthParen.Click += new EventHandler(_btnRemoveRigthParen_Click);
            tc8.Controls.Add(_btnRemoveRigthParen);
            tr.Cells.Add(tc8);
        }

        private void CreateWhereList()
        {
            Table tbl = new Table();
            tbl.BorderWidth = 1;

            TableRow tr1 = new TableRow();
            TableCell tc1 = new TableCell();
            tc1.Text = "Filters To Apply";
            tr1.Cells.Add(tc1);
            tbl.Rows.Add(tr1);

            TableRow tr2 = new TableRow();
            TableCell tc2 = new TableCell();
            _lstWhereClause = new ListBox();
            _lstWhereClause.ID = "lstWhereClause";

            tc2.Controls.Add(_lstWhereClause);
            tr2.Cells.Add(tc2);
            tbl.Rows.Add(tr2);

            _updatePanel.ContentTemplateContainer.Controls.Add(tbl);
        }

        private void CreatePleaseWait()
        {
            //Add the please wait panel
            _pleaseWaitPanel = new Panel();
            _pleaseWaitPanel.ID = "pnlPleaseWait";
            _pleaseWaitPanel.Style.Add("display", "none");
            _pleaseWaitPanel.CssClass = "modalPopup";
            _pleaseWaitPanel.Controls.Add(new LiteralControl("Please Wait..."));

            Controls.Add(_pleaseWaitPanel);

            //Add a dummy control for the popup extender
            _hidDummyField = new HiddenField();
            _hidDummyField.ID = "hidDummyField";

            Controls.Add(_hidDummyField);

            //Add the pop up extender.
            _mde = new ModalPopupExtender();
            _mde.ID = "mdePleaseWait";
            _mde.PopupControlID = _pleaseWaitPanel.ID;
            //_mde.TargetControlID = _hidDummyField.ClientID;
            _mde.TargetControlID = _hidDummyField.ID;
            _mde.DropShadow = true;
            _mde.BackgroundCssClass = "modalBackground";

            Controls.Add(_mde);
        }

        private void AddWhereSQL(string andOr)
        {
            //The field, comparison, and value must have a value selected
            if (_ddlFields.SelectedIndex > 0)
            {
                if (_ddlComparison.SelectedIndex > 0)
                {
                    string friendlyText = andOr + " " + _ddlFields.SelectedItem.Text + " " + _ddlComparison.SelectedItem.Text + " ";

                    string sqlValue = "";
                    string friendlyValue = "";

                    string comparison = "";
                    string beginParen = "";
                    string endParen = "";

                    GetValueText(ref friendlyValue, ref sqlValue);

                    if (sqlValue.Length > 0)
                    {
                        switch (_ddlComparison.SelectedItem.Text)
                        {
                            case COMPARISON_EQUALS:
                                comparison = " = ";
                                break;
                            case COMPARISON_NOT_EQUALS:
                                comparison = " <> ";
                                break;
                            case COMPARISON_IN:
                                comparison = " IN ";
                                beginParen = "(";
                                endParen = ")";
                                break;
                            case COMPARISON_NOT_IN:
                                comparison = " NOT IN ";
                                beginParen = "(";
                                endParen = ")";
                                break;
                            case COMPARISON_IS_BLANK:
                                comparison = " IS ";
                                break;
                            case COMPARISON_IS_NOT_BLANK:
                                comparison = " IS NOT ";
                                break;
                            case COMPARISON_IS_GREATER_THAN:
                                comparison = " > ";
                                break;
                            case COMPARISON_IS_GREATER_THAN_EQUAL:
                                comparison = " >= ";
                                break;

                            case COMPARISON_IS_LESS_THAN:
                                comparison = " < ";
                                break;

                            case COMPARISON_IS_LESS_THAN_OR_EQUAL:
                                comparison = " <= ";
                                break;
                        }

                        friendlyText += beginParen + friendlyValue + endParen;
                        _lstWhereClause.Items.Add(new ListItem(friendlyText, andOr + _ddlFields.SelectedValue + comparison + beginParen + sqlValue + endParen));
                        if (_lstWhereClause.Items.Count > 1)
                        {
                            _lstWhereClause.Rows = _lstWhereClause.Items.Count;
                        }
                    }
                }
            }
        }

        private void GetValueText(ref string friendlyValue, ref string sqlValue)
        {
            //The comparison type will determine which list is displayed
            switch (_ddlComparison.SelectedItem.Text)
            {
                case COMPARISON_EQUALS:
                case COMPARISON_NOT_EQUALS:
                case COMPARISON_IS_GREATER_THAN:
                case COMPARISON_IS_GREATER_THAN_EQUAL:
                case COMPARISON_IS_LESS_THAN:
                case COMPARISON_IS_LESS_THAN_OR_EQUAL:
                    friendlyValue = _ddlValues.SelectedItem.Text;
                    sqlValue = FormatValue(_ddlValues.SelectedValue);
                    break;

                case COMPARISON_IN:
                case COMPARISON_NOT_IN:
                    string tempValue = "";
                    string tempFriendlyValue = "";
                    foreach (ListItem li in _lstValues.Items)
                    {
                        if (li.Selected)
                        {
                            tempFriendlyValue += li.Text + ", ";
                            tempValue += FormatValue(li.Value) + ", ";
                        }
                    }

                    if (tempValue.Length > 0)
                    {
                        friendlyValue = tempFriendlyValue.Substring(0, tempFriendlyValue.Length - 2);
                        sqlValue = tempValue.Substring(0, tempValue.Length - 2);
                    }
                    else
                    {
                        friendlyValue = tempFriendlyValue;
                        sqlValue = tempValue;
                    }
                    break;

                case COMPARISON_IS_BLANK:
                case COMPARISON_IS_NOT_BLANK:
                    sqlValue = " NULL ";
                    friendlyValue = "";
                    break;

                default:
                    sqlValue = "";
                    friendlyValue = "";
                    break;
            }
        }

        private string FormatValue(string value)
        {
            //Get the field name
            string valueFieldName = _ddlFields.SelectedValue;

            //Get the values for this field in the database.
            foreach (object attribute in QueryFields)
            {
                if (attribute is QueryFieldAttribute)
                {
                    QueryFieldAttribute qfa = (QueryFieldAttribute)attribute;

                    if (qfa.FieldName == valueFieldName)
                    {
                        switch (qfa.FieldType)
                        {
                            case QueryFieldAttribute.QueryFieldTypeEnum.String:
                                return "'" + value.Replace("'", "''") + "'";

                            case QueryFieldAttribute.QueryFieldTypeEnum.Date:
                                return "'" + value + "'";

                            case QueryFieldAttribute.QueryFieldTypeEnum.Boolean:
                            case QueryFieldAttribute.QueryFieldTypeEnum.Lookup:
                            case QueryFieldAttribute.QueryFieldTypeEnum.Number:
                                return value;
                        }
                    }
                }
            }
            return value;
        }

        private int CountParens(char paren, string text)
        {
            int tempCount = text.Split(new char[] { paren }).Count();

            if (tempCount > 1)
            {
                return (tempCount - 1);
            }
            else
            {
                return 0;
            }
        }

        #endregion Private Methods

        #region Public Methods

        public bool GetWhereClause(ref string whereClause, ref ValidationErrors validationErrors)
        {
            //The left and right parens must equal each other.
            int leftParenCount = 0;
            int rightParenCount = 0;

            whereClause = "";

            //Append any required where clauses
            foreach (object attribute in QueryFields)
            {
                if (attribute is RequiredQueryFieldAttribute)
                {
                    if (whereClause == "") whereClause = " WHERE ";

                    whereClause += ((RequiredQueryFieldAttribute)attribute).Clause + " AND ";
                }
            }

            //If there were any required where clauses then the rest of the query must be surrounded by parens (  )
            bool requiredWhereClauseExists = (whereClause != "");
            if (requiredWhereClauseExists)
            {
                whereClause += "( ";
            }

            //Add a line for each item in the list box.
            if (_lstWhereClause.Items.Count > 0)
            {
                if (whereClause == "") whereClause = " WHERE ";

                foreach (ListItem li in _lstWhereClause.Items)
                {
                    leftParenCount += CountParens('(', li.Value);
                    rightParenCount += CountParens(')', li.Value);

                    whereClause += li.Value;
                }
            }

            //Close out the parenthesis if a required where clause existed.
            if (requiredWhereClauseExists)
            {
                whereClause += ")";
            }

            //Validate that the left and right parens were equal.
            if (leftParenCount == rightParenCount)
            {
                return true;
            }
            else
            {
                validationErrors.Add("Error In Filter: The nubmer of left and right parenthesis must match");
                return false;
            }
        }

        #endregion Public Methods
    }
}
