<%@ Page Title="Manage Information" Language="C#" MasterPageFile="~/std/std.master" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="std_Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-5" style="max-width: 720px;">
            <h2 class="text-center text-primary mb-4">Manage Your Information</h2>

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-danger"></asp:Label>
            <asp:HiddenField ID="hfPersonID" runat="server" />

            <!-- Form -->
            <div class="row g-3">
                <!-- Name -->
                <div class="col-md-6">
                    <label for="txtQualification" class="form-label">Highest Qualification</label>
                    <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtOccupation" class="form-label">Current Occupation / Job</label>
                    <asp:TextBox ID="txtOccupation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtCompany" class="form-label">Organisation / Company and location</label>
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </div>

                <!-- Phone -->
                <div class="col-md-6">
                    <label for="txtAchievement" class="form-label">Special Achievement</label>
                    <asp:TextBox ID="txtAchievement" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </div>

                <!-- Alumnus Register -->
                <div class="col-md-6">
                    <label for="rblAlumnus" class="form-label">Register as Alumni</label>
                    <asp:RadioButtonList ID="rblAlumnus" runat="server" CssClass="form-control" Enabled="false">
                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="no"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <!-- Alumnus Register -->
                <div class="col-md-6">
                    <label for="rblAchiever" class="form-label">Register as Achiever</label>
                    <asp:RadioButtonList ID="rblAchiever" runat="server" CssClass="form-control" Enabled="false">
                        <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="no"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <!-- Save/Update Button -->
                <div class="col-md-6 text-end">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary me-2" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnSave_Click" Enabled="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary me-2" OnClick="btnCancel_Click" Enabled="false" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
