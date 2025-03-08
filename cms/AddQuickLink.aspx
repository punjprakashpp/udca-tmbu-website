<%@ Page Title="Add Quick Link" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="AddQuickLink.aspx.cs" Inherits="AddQuickLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:720px" class="container bg-white rounded shadow-sm p-5">
            <h1 class="text-center text-primary mb-4">Add Quick Link for Buttons</h1>
            <div class="mb-3 row">
                <label for="txtLinkText" class="col-sm-3 col-form-label">Link Text:</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtLinkText" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label class="col-sm-3 col-form-label">If link is external:</label>
                <div class="col-sm-9 d-flex align-items-center">
                    <asp:CheckBox ID="chkboxExt" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxExt_CheckedChanged" />
                    <span class="ms-2">Yes, Link is external.</span>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtLinkURL" class="col-sm-3 col-form-label">Link URL:</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="txtLinkURL" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="ddlLinkPicker" class="col-sm-3 col-form-label">Choose a Local Page Link:</label>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddlLinkPicker" CssClass="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLinkPicker_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="mb-3 row">
                <div class="col-sm-12 text-center">
                    <asp:Label ID="lblMessage" CssClass="alert alert-info d-none" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="d-flex justify-content-around">
                <asp:Button ID="btnManage" CssClass="btn btn-secondary mx-2" runat="server" Text="Manage Link" OnClick="btnManage_Click" />
                <asp:Button ID="btnSubmit" CssClass="btn btn-primary mx-2" runat="server" Text="Submit Link" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </section>
</asp:Content>