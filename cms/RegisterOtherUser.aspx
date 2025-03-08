<%@ Page Title="Register Other User" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="RegisterOtherUser.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Heading -->
        <h2 class="text-center mb-4">Register Other User</h2>

        <div class="row">
            <div class="col-md-6 m-auto">
                <!-- Registration Form -->
                <div class="mb-3">
                    <label for="txtFirstName" class="form-label">First Name:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtMidName" class="form-label">Middle Name (if any):</label>
                    <asp:TextBox ID="txtMidName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtLastName" class="form-label">Last Name:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtUserName" class="form-label">Username:</label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtPasswordCheck" class="form-label">Retype Password:</label>
                    <asp:TextBox ID="txtPasswordCheck" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="drpRole" class="form-label">User Type (Role):</label>
                    <asp:DropDownList ID="drpRole" runat="server" CssClass="form-select">
                        <asp:ListItem>User</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-danger"></asp:Label>
                </div>

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

