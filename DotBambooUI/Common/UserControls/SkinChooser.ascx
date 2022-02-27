<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SkinChooser.ascx.cs" Inherits="Common_UserControls_SkinChooser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadSkinManager ID="QsfSkinManager" runat="server" />

<span class="sc-activator">
	<span class="thumb <%= SelectedSkin.ToLower() %>"></span>
	<span class="sc-current"><%= SelectedSkin %></span> skin
</span>

<div class="animation-container">
	<div class="skin-list">
		<h4>skins</h4>
		<ul class="skin-grid">
			<li><a href="<%= Url %>Black"><span class="thumb black"></span> Black</a></li>
			<li><a href="<%= Url %>BlackMetroTouch"><span class="thumb blackmetrotouch"></span> BlackMetroTouch</a></li>
            <li><a href="<%= Url %>Bootstrap"><span class="thumb bootstrap"></span> Bootstrap</a></li>
			<li><a href="<%= Url %>Default"><span class="thumb default"></span> Default</a></li>
			<li><a href="<%= Url %>Glow"><span class="thumb glow"></span> Glow</a></li>
			<li><a href="<%= Url %>Metro"><span class="thumb metro"></span> Metro</a></li>
			<li><a href="<%= Url %>MetroTouch"><span class="thumb metrotouch"></span> MetroTouch</a></li>
			<li><a href="<%= Url %>Office2007"><span class="thumb office2007"></span> Office2007</a></li>
			<li><a href="<%= Url %>Office2010Black"><span class="thumb office2010black"></span> Office2010Black</a></li>
			<li><a href="<%= Url %>Office2010Blue"><span class="thumb office2010blue"></span> Office2010Blue</a></li>
			<li><a href="<%= Url %>Office2010Silver"><span class="thumb office2010silver"></span> Office2010Silver</a></li>
			<li><a href="<%= Url %>Outlook"><span class="thumb outlook"></span> Outlook</a></li>
			<li><a href="<%= Url %>Silk"><span class="thumb silk"></span> Silk</a></li>
			<li><a href="<%= Url %>Simple"><span class="thumb simple"></span> Simple</a></li>
			<li><a href="<%= Url %>Sunset"><span class="thumb sunset"></span> Sunset</a></li>
			<li><a href="<%= Url %>Telerik"><span class="thumb telerik"></span> Telerik</a></li>
			<li><a href="<%= Url %>Vista"><span class="thumb vista"></span> Vista</a></li>
			<li><a href="<%= Url %>Web20"><span class="thumb web20"></span> Web20</a></li>
			<li><a href="<%= Url %>WebBlue"><span class="thumb webblue"></span> WebBlue</a></li>
			<li><a href="<%= Url %>Windows7"><span class="thumb windows7"></span> Windows7</a></li>
		</ul>
	</div>
</div>