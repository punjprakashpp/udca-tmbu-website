<%@ Page Title="Upload Gallery Images" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="UploadGalleryImage.aspx.cs" Inherits="cms_UploadGalleryImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Title -->
        <h2 class="text-center mb-4">Upload Gallery Images</h2>

        <div class="row">
            <div class="col-md-6 m-auto">
                <!-- Gallery Image Form -->
                <!-- Occasion Input -->
                <div class="mb-3">
                    <label for="txtOccasion" class="form-label">Occasion:</label>
                    <asp:TextBox ID="txtOccasion" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <!-- Image Upload -->
                <div class="mb-3">
                    <label for="fileUpload" class="form-label">Select Images of Occasion:</label>
                    <asp:FileUpload ID="fileUpload" CssClass="form-control" runat="server" AllowMultiple="true" />
                </div>

                <!-- Message Display -->
                <div class="mb-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="form-text"></asp:Label>
                </div>

                <!-- Action Buttons -->
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="Delete Images" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload Images" OnClick="btnUpload_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
