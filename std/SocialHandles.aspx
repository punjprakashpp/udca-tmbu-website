<%@ Page Title="Manage Social Handles" Language="C#" MasterPageFile="~/std/std.master" AutoEventWireup="true" CodeFile="SocialHandles.aspx.cs" Inherits="std_SocialHandles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-5" style="max-width: 720px;">
            <h2 class="text-center text-primary mb-4">Manage Your Social Handles</h2>

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-danger"></asp:Label>
            <asp:HiddenField ID="hfPersonID" runat="server" />

            <!-- Form -->
            <div class="row g-3">
                <!-- Name -->
                <div class="col-md-6">
                    <label for="txtLinkedIn" class="form-label"><i class="fab fa-linkedin-in"></i>&nbsp;Linkedin Id: @</label>
                    <asp:TextBox ID="txtLinkedIn" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtGitHub" class="form-label"><i class="fab fa-github"></i>&nbsp;GitHub Id: @</label>
                    <asp:TextBox ID="txtGitHub" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtFacebook" class="form-label"><i class="fab fa-facebook-f"></i>&nbsp;Facebook Id: @</label>
                    <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>

                <!-- Phone -->
                <div class="col-md-6">
                    <label for="txtInstagram" class="form-label"><i class="fab fa-instagram"></i>&nbsp;Instagram Id: @</label>
                    <asp:TextBox ID="txtInstagram" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>              

                <!-- Alumnus Register -->
                <div class="col-md-6">
                    <label for="txtTwitter" class="form-label"><i class="fab fa-x-twitter"></i>&nbsp;Twitter / X Id: @</label>
                    <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>

                <!-- Save/Update Button -->
                <div class="col-md-6 text-end">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary me-2" OnClick="btnEdit_Click"/>
                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnSave_Click" Enabled="false"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary me-2" OnClick="btnCancel_Click" Enabled="false" />                    
                </div>
            </div>
        </div>
    </section>
</asp:Content>