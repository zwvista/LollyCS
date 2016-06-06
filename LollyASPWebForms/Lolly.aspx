<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lolly.aspx.cs" Inherits="LollyASPWebForms.Lolly" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <link href="/Styles/lolly.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal" role="form">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Language:" CssClass="col-sm-1 control-label"></asp:Label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" DataSourceID="odsLanguage" DataTextField="LANGNAME" DataValueField="LANGID" CssClass="form-control">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsLanguage" runat="server" SelectMethod="Languages_GetDataNonChinese" TypeName="LollyShared.LollyDB"></asp:ObjectDataSource>
            </div>
            <asp:Label ID="Label2" runat="server" Text="Dictionary:" CssClass="col-sm-1 control-label"></asp:Label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlDictionary" runat="server" DataSourceID="odsDictionary" DataTextField="DICTNAME" DataValueField="DICTNAME" CssClass="form-control" >
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsDictionary" runat="server" SelectMethod="Dictionaries_GetDataByLang" TypeName="LollyShared.LollyDB">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlLanguage" Name="langid" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Word:" CssClass="col-sm-1 control-label"></asp:Label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtWord" runat="server" CssClass="form-control">一人</asp:TextBox>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnSearchRedirect" runat="server" Text="Search(redirect)" OnClick="btnSearchRedirect_Click" CssClass="btn btn-primary" />
            <asp:ObjectDataSource ID="odsDictAll" runat="server" SelectMethod="DictAll_GetDataByLangDict" TypeName="LollyShared.LollyDB">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlLanguage" Name="langid" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlDictionary" Name="dict" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <div class="col-sm-3 error vcenter" id='wordError'></div>
        </div>
    </div>
<iframe id='dictframe' runat="server">
</iframe>
</asp:Content>
