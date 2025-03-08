<%@ Page Title="Chnage Password" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <h2 class="text-center mb-4">Change Your Password</h2>

                    <!-- Panel for Checking Current Password -->
                    <asp:Panel ID="CheckPasswordPanel" runat="server" Visible="true">
                        <div class="card shadow-sm p-4 mb-4">
                            <div class="mb-3">
                                <label for="txtCurrentPassword" class="form-label">Type Your Current Password:</label>
                                <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="d-flex justify-content-between">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
                                <asp:Button ID="btnCheck" runat="server" Text="Check Password" CssClass="btn btn-primary" OnClick="btnCheck_Click" />
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel for Changing Password -->
                    <asp:Panel ID="ChangePasswordPanel" runat="server" Visible="false">
                        <div class="card shadow-sm p-4 mb-4">
                            <div class="mb-3">
                                <label for="txtNewPassword" class="form-label">Type New Password:</label>
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtPasswordCheck" class="form-label">Retype New Password:</label>
                                <asp:TextBox ID="txtPasswordCheck" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="d-flex justify-content-between">
                                <asp:Button ID="BtnClear" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
                                <asp:Button ID="btnChange" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnChange_Click" />
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Message Section -->
                    <div class="info card shadow-sm p-4">
                        <asp:Label ID="lblMessage" runat="server" CssClass=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
