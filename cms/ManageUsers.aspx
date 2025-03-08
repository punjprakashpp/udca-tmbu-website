<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Account_Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Manage Users</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Heading -->
        <h2 class="text-center mb-4">Manage Users</h2>

        <!-- Message Display -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success fw-bold d-block mb-3"></asp:Label>

        <!-- Users Table -->
        <div class="row">
            <div class="col-md-8 m-auto">
                <div class="table-responsive">
                    <asp:GridView ID="gridUsers" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="UserId,UserName" OnRowDeleting="gridUsers_RowDeleting"
                        CssClass="table table-hover table-bordered table-striped align-middle">
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="Username"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Role" HeaderText="User Type"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center fw-bold" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="MidName" HeaderText="Middle Name"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:CommandField HeaderText="Action" ShowDeleteButton="True"
                                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                ButtonType="Button" ControlStyle-CssClass="btn btn-danger" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
