<%@ Page Title="Update Administrative Person" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="ManageAdministration.aspx.cs" Inherits="ManageAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-3.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/cropper.js"></script>
    <link rel="stylesheet" href="../Content/Cropper.min.css" />
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
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-5" style="max-width: 720px;">
            <h2 class="text-center text-primary mb-4">Manage Administrative Person</h2>

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-danger"></asp:Label>
            <asp:HiddenField ID="hfPersonID" runat="server" />
            <asp:HiddenField ID="hfCurrentFilePath" runat="server" />

            <!-- Form -->
            <div class="row g-3">
                <!-- Position -->
                <div class="col-12">
                    <label for="ddlType" class="form-label">Position</label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">                        
                    </asp:DropDownList>
                </div>

                <!-- Name -->
                <div class="col-md-6">
                    <label for="txtName" class="form-label">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Phone -->
                <div class="col-md-6">
                    <label for="txtPhone" class="form-label">Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Email -->
                <div class="col-12">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Image Upload -->
                <div class="col-12">
                    <label for="fileUpload" class="form-label">Image</label>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                    <asp:Label ID="lblFileTypeError" runat="server" CssClass="form-text text-danger" Visible="false">Invalid file type. Only .jpg, .jpeg, .png files are allowed.</asp:Label>

                    <!-- Current Image Preview -->
                    <div class="mt-3">
                        <img id="currentImage" runat="server" class="img-thumbnail" src="#" alt="Current Image" style="display: none; max-width: 225px; max-height: 225px;" />
                        <asp:HiddenField ID="imagePreviewBase64" runat="server" />
                        <div id="cropperContainer" class="cropper-container mt-3" style="display: none;">
                            <img id="cropperImage" src="#" alt="Image for cropping" />
                        </div>
                        <asp:Button ID="btnCrop" runat="server" Text="Crop" CssClass="btn btn-primary mt-2" OnClientClick="return cropImage();" Style="display: none;" />
                    </div>
                </div>

                <!-- Save/Update Button -->
                <div class="col-12 text-end">
                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger ms-2" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        $(document).ready(function () {
            var cropper;
            var fileUpload = $("#<%= fileUpload.ClientID %>");
            var currentImage = $("#currentImage");
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
                        currentImage.attr("src", e.target.result).show();
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
                    };
                    reader.readAsDataURL(file);
                } else {
                    fileTypeError.show();
                    currentImage.hide();
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
                currentImage.attr("src", base64String).show();
                cropperContainer.hide();
                btnCrop.hide();
                if (cropper) {
                    cropper.destroy();
                }
                return false;
            };
        });
    </script>
</asp:Content>
