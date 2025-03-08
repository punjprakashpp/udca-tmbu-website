<%@ Page Title="Manage Staff" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="ManageStaff.aspx.cs" Inherits="ManageStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/Scripts/jquery-3.7.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/cropper.js"></script>
    <link rel="stylesheet" href="/Content/Cropper.min.css" />
    <style>
        .cropper-container {
            width: 300px;
            height: 300px;
            position: relative;
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4" style="max-width:840px">
        <!-- Page Heading -->
        <h2 class="text-center mb-4">Manage Staff</h2>

        <!-- Message Display -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

        <!-- Staff Management Form -->
        <div class="row mb-3">
            <div class="col-md-6">
                <label for="ddlFaculties" class="col-md-3 col-form-label fw-bold">Select Staff:</label>
                <asp:DropDownList ID="ddlFaculties" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculties_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label for="ddlType" class="col-md-3 col-form-label fw-bold">Staff Type:</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Office Staff" Value="Office"></asp:ListItem>
                    <asp:ListItem Text="Supporting Staff" Value="Support"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label for="txtName" class="col-md-3 col-form-label fw-bold">Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="txtQualification" class="col-md-3 col-form-label fw-bold">Qualification:</label>
                <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="txtPosition" class="col-md-3 col-form-label fw-bold">Position:</label>
                <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="txtPhone" class="col-md-3 col-form-label fw-bold">Phone:</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="txtEmail" class="col-md-3 col-form-label fw-bold">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label for="fileUpload" class="col-md-3 col-form-label fw-bold">Image:</label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                <asp:Label ID="lblFileTypeError" runat="server" CssClass="text-danger d-block mt-2" Visible="false">Invalid file type. Only .jpg, .jpeg, .png files are allowed.</asp:Label>
                <div class="mt-3">
                    <img id="currentImage" src="#" alt="Current Image" runat="server" class="img-thumbnail" style="display: none; max-width: 225px; max-height: 225px;" />
                    <asp:HiddenField ID="imagePreviewBase64" runat="server" />
                    <div id="cropperContainer" class="cropper-container mt-3" style="display: none;">
                        <img id="cropperImage" src="#" alt="Image for cropping" class="img-fluid" />
                    </div>
                    <asp:Button ID="btnCrop" runat="server" Text="Crop" OnClientClick="return cropImage();" CssClass="btn btn-outline-primary mt-3" Style="display: none;" />
                </div>
            </div>

            <div class="col-12 text-end">
                <div class="d-flex justify-content-end mt-4">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
        </div>


    <script>
        $(document).ready(function () {
            var cropper;
            var fileUpload = $("#<%= fileUpload.ClientID %>");
            var imagePreview = $("#imagePreview");
            var cropperContainer = $("#cropperContainer");
            var cropperImage = $("#cropperImage");
            var fileTypeError = $("#<%= lblFileTypeError.ClientID %>");
            var btnCrop = $("#<%= btnCrop.ClientID %>");

            fileUpload.change(function (e) {
                var file = e.target.files[0];
                if (file && (file.type === "image/jpeg" || file.type === "image/png")) {
                    fileTypeError.hide();
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        imagePreview.attr("src", e.target.result).show();
                        cropperImage.attr("src", e.target.result);
                        cropperContainer.show();
                        btnCrop.show();

                        if (cropper) {
                            cropper.destroy();
                        }
                        cropper = new Cropper(cropperImage[0], {
                            aspectRatio: 1,
                            viewMode: 1,
                            autoCropArea: 1,
                        });
                    }
                    reader.readAsDataURL(file);
                } else {
                    fileTypeError.show();
                    imagePreview.hide();
                    cropperContainer.hide();
                    btnCrop.hide();
                    if (cropper) {
                        cropper.destroy();
                    }
                }
            });

            window.cropImage = function () {
                var canvas = cropper.getCroppedCanvas({ width: 225, height: 225 });
                var base64String = canvas.toDataURL();
                $("#<%= imagePreviewBase64.ClientID %>").val(base64String); // Set the base64 string to the hidden field
                imagePreview.attr("src", base64String).show();
                cropperContainer.hide();
                btnCrop.hide();
                if (cropper) {
                    cropper.destroy();
                }
                return false;
            }
        });
    </script>
</asp:Content>
