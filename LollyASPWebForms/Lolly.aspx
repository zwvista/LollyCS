<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lolly.aspx.cs" Inherits="LollyASPWebForms.Lolly" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table >
        <tr>
            <td style="height: 21px">
                <asp:Label ID="Label1" runat="server" Text="Language:"></asp:Label>
            </td>
            <td style="height: 21px">
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" DataSourceID="odsLanguage" DataTextField="LANGNAME" DataValueField="LANGID" >
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsLanguage" runat="server" SelectMethod="Languages_GetDataNonChinese" TypeName="LollyBase.LollyDB"></asp:ObjectDataSource>
            </td>
            <td><asp:Label ID="Label2" runat="server" Text="Dictionary:"></asp:Label></td>
            <td><asp:DropDownList ID="ddlDictionary" runat="server" DataSourceID="odsDictionary" DataTextField="DICTNAME" DataValueField="DICTNAME" >
    </asp:DropDownList></td>
                <asp:ObjectDataSource ID="odsDictionary" runat="server" SelectMethod="Dictionaries_GetDataByLang" TypeName="LollyBase.LollyDB">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlLanguage" Name="langid" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
            </asp:ObjectDataSource>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Word:"></asp:Label></td>
            <td colspan="3">
                <asp:TextBox ID="txtWord" runat="server">一人</asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:ObjectDataSource ID="odsDictAll" runat="server" SelectMethod="DictAll_GetDataByLangDict" TypeName="LollyBase.LollyDB">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlLanguage" Name="langid" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlDictionary" Name="dict" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
<iframe id='dictframe' runat="server" width='100%' height='500'>
</iframe>
</asp:Content>
