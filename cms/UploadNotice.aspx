<%@ Page Title="Upload Notice" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="UploadNotice.aspx.cs" Inherits="cms_UploadNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../Content/flatpickr.min.css">
    <script type="text/javascript" src="../Scripts/flatpickr.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Initialize Flatpickr on the TextBox
            const fp = flatpickr('.flatpickr-input', {
                enableTime: false,       // Disable time
                dateFormat: "d-m-Y",     // Date format
                allowInput: true,        // Allow manual input
                clickOpens: true,       // Calendar opens on click
                disableMobile: true     // Force Flatpickr on mobile
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Title -->
        <h2 class="text-center mb-4">Upload Notice Image/PDF</h2>

        <div class="row">
            <div class="col-md-6 m-auto">
                <!-- Upload Form -->
                <!-- Notice No -->
                <div class="mb-3">
                    <label for="txtNoticeNo" class="form-label">Notice No.:</label>
                    <asp:TextBox ID="txtNoticeNo" ToolTip="Notice No." CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <!-- Notice Date -->
                <div class="mb-3">
                    <label for="txtNoticeDate" class="form-label">Notice Date:</label>
                    <asp:TextBox ID="txtNoticeDate" ToolTip="Notice Date" CssClass="form-control flatpickr-input" runat="server"></asp:TextBox>
                </div>

                <!-- Notice Title -->
                <div class="mb-3">
                    <label for="txtNoticeText" class="form-label">Notice Title:</label>
                    <asp:TextBox ID="txtNoticeText" ToolTip="Notice Text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <!-- Notice Image/PDF -->
                <div class="mb-3">
                    <label for="fileUpload" class="form-label">Select Notice Image/PDF:</label>
                    <asp:FileUpload ID="fileUpload" ToolTip="Notice File" CssClass="form-control" runat="server"></asp:FileUpload>
                </div>

                <!-- Message Label -->
                <div class="mb-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-danger"></asp:Label>
                </div>

                <!-- Buttons -->
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnManage" CssClass="btn btn-secondary" runat="server" Text="Manage Notice" OnClick="btnManage_Click" />
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
