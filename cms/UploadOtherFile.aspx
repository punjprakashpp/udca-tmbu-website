<%@ Page Title="Upload Other Type File" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="UploadOtherFile.aspx.cs" Inherits="UploadOtherFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Title -->
        <h2 class="text-center mb-4">Upload Other Type File (Image/PDF)</h2>

        <div class="row">
            <div class="col-md-5 m-auto">
                <!-- Document Title -->
                <div class="mb-3">
                    <label for="txtNoticeText" class="form-label">Document Title:</label>
                    <asp:TextBox ID="txtNoticeText" ToolTip="Event Text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <!-- File Upload -->
                <div class="mb-3">
                    <label for="fileUpload" class="form-label">Select Document Image/PDF/ZIP:</label>
                    <asp:FileUpload ID="fileUpload" ToolTip="Event File" CssClass="form-control" runat="server" />
                </div>

                <!-- Message Display -->
                <div class="mb-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="form-text"></asp:Label>
                </div>

                <!-- Submit Button -->
                <div class="text-end">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
