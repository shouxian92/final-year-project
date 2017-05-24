<%@ Page Title="FYP - Examination Attendance" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Device.aspx.cs" Inherits="FYP_WEB._Device" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
        <AnonymousTemplate>
            <p>
                Please log in to view attendance taken for examination.
            </p>
        </AnonymousTemplate>
        <LoggedInTemplate>
        </LoggedInTemplate>
    </asp:LoginView>
    <asp:ScriptManager ID="scriptMgr" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="pnlAttendance" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCreateForm" runat="server" Visible="false">
                <asp:Label runat="server" Text="IMEI Number"></asp:Label>
                <asp:TextBox ID="txtImeiNumber" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddlStaff" runat="server"></asp:DropDownList>
                <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlDevice" runat="server">
                <asp:Button ID="btnNew" runat="server" Text="New Device" OnClick="btnNew_Click" />
                <asp:GridView ID="gvDevice" runat="server" AutoGenerateColumns="false" OnRowCommand="gvDevice_RowCommand" DataKeyNames="DeviceId" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField DataField="DeviceId" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ImeiNumber" HeaderText="IMEI Number" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Staff.Username" HeaderText="Owner" ItemStyle-HorizontalAlign="Center" />
                        <asp:ButtonField ButtonType="Button" HeaderText="Delete" CommandName="DeleteDevice" Text="Delete" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="pnlProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="imgLoader" runat="server" ImageUrl="~/files/icons/loader.gif" AlternateText="Loading ..."
                ToolTip="Loading..." />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
