<%@ Page Title="FYP - Examination Attendance" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="FYP_WEB._Attendance" %>

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
            <asp:Label ID="lblSearchHall" runat="server" Text="Hall: "></asp:Label>
            <asp:DropDownList ID="ddlHalls" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHalls_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="lblSearchCourse" runat="server" Text="Course: "></asp:Label>
            <asp:DropDownList ID="ddlCourses" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Panel ID="pnlStudentFilter" runat="server" Visible="false">
                <asp:Label ID="lblSearchStudentName" runat="server" Text="Student Name: "></asp:Label>
                <asp:TextBox ID="txtStudentName" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </asp:Panel>
            <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Student.MatricNumber" HeaderText="Matric Number" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Student.Name" HeaderText="Name" />
                    <asp:BoundField DataField="SeatNumber" HeaderText="Seat Number" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Attendance" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("SeatNumber").ToString().PadLeft(4, '0') %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attendance" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Image Height="16" Width="16" ID="img" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("Attendance")) == 1 ? "~/files/icons/tick.png" :"~/files/icons/cross.png" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="pnlProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="imgLoader" runat="server" ImageUrl="~/files/icons/loader.gif" AlternateText="Loading ..."
                ToolTip="Loading..." />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
