<%@ Page Title="Manage Your Account" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ManageAccount.aspx.cs" Inherits="cms_ManageAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <h2 class="text-center mb-4">Manage Your Account</h2>

                    <!-- Check Password Panel -->
                    <asp:Panel ID="CheckPasswordPanel" runat="server" Visible="true" class="mb-4">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Verify Your Password</h5>
                                <div class="mb-3">
                                    <label for="txtCurrentPassword" class="form-label">Type Current Password</label>
                                    <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-between">
                                    <asp:Button ID="btnClear" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
                                    <asp:Button ID="btnCheck" runat="server" Text="Check Password" CssClass="btn btn-primary" OnClick="btnCheck_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Manage Account Panel -->
                    <asp:Panel ID="ManageAccountPanel" runat="server" Visible="false" class="mb-4">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Manage Your Details</h5>
                                <div class="mb-3">
                                    <label for="txtFirstName" class="form-label">First Name</label>
                                    <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtMidName" class="form-label">Middle Name (if any)</label>
                                    <asp:TextBox ID="txtMidName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtLastName" class="form-label">Last Name</label>
                                    <asp:TextBox ID="txtLastName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtUserName" class="form-label">Username</label>
                                    <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-between">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
                                    <asp:Button ID="btnChange" runat="server" Text="Change" CssClass="btn btn-primary" OnClick="btnChange_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Visible="false" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Message Section -->
                    <div class="info card shadow-sm p-4">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
