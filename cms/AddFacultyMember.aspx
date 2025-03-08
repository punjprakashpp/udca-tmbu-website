<%@ Page Title="Add Faculty Members" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="AddFacultyMember.aspx.cs" Inherits="Admin_pages_AddFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../Content/Cropper.min.css">
    <script type="text/javascript" src="../Scripts/cropper.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-3.7.1.min.js"></script>
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
            <h2 class="text-center text-primary mb-4">Add Faculty Members</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="d-block mb-3"></asp:Label>
            <div class="row g-3">
            <div class="col-md-6">
                <label for="ddlType" class="form-label">Faculty Type:</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Faculty Member" Value="Faculty"></asp:ListItem>
                    <asp:ListItem Text="Guest Faculty" Value="Guest"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
                <label for="ddlStatus" class="form-label">Faculty Status:</label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                    <asp:ListItem Text="Former" Value="Former"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
                <label for="txtName" class="form-label">Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="txtQualification" class="form-label">Qualification:</label>
                <asp:TextBox ID="txtQualification" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="txtPosition" class="form-label">Position:</label>
                <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="txtPhone" class="form-label">Phone:</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                    <label for="fileUpload" class="form-label">Image</label>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                    <asp:Label ID="lblFileTypeError" runat="server" ForeColor="Red" Visible="false" CssClass="form-text text-danger">Invalid file type. Only .jpg, .jpeg, .png files are allowed.</asp:Label>
                    <div class="mt-3">
                        <asp:HiddenField ID="imagePreviewBase64" runat="server" />
                        <div id="cropperContainer" class="cropper-container" style="display: none;">
                            <img id="cropperImage" src="#" alt="Image for cropping" class="img-fluid" />
                        </div>
                        <asp:Button ID="btnCrop" runat="server" Text="Crop" OnClientClick="return cropImage();" CssClass="btn btn-primary mt-2" Style="display: none;" />
                    </div>
                </div>
            <div class="d-flex justify-content-around">
                <asp:Button ID="btnEdit" runat="server" Text="Manage" OnClick="btnEdit_Click" CssClass="btn btn-secondary" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            </div>
        </div>
            </div>
    </section>

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
