<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavigation.ascx.cs" Inherits="Common_UserControls_LeftNavigation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div id="nav-switch">
	<span class="back-nav">All Menus</span>
</div>
<div id="nav">
	<div class="nav-wrap">

		<div id="root-nav">
			<telerik:RadTreeView runat="server" ID="CategoryControls" Skin="" CssClass="treeview"
				EnableEmbeddedSkins="false" ShowLineImages="false">
			</telerik:RadTreeView>
		</div>

		<div id="sub-nav">
			<telerik:RadTreeView runat="server" ID="ControlDemos" Skin="" CssClass="treeview"
				EnableEmbeddedSkins="false" ShowLineImages="false"
				OnClientNodeClicked="controlDemos_OnClientNodeClicked"
				OnClientNodeClicking="controlDemos_OnClientNodeClicking"
				OnClientNodeCollapsing="controlDemos_OnClientNodeCollapsing"
				OnClientLoad="controlDemos_OnClientLoad">
				<Nodes>
					<telerik:RadTreeNode CssClass="rtRootNode"></telerik:RadTreeNode>
				</Nodes>
			</telerik:RadTreeView>
		</div>

	</div>
</div>