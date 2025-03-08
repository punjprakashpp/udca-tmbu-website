<%@ Page Title="Manage Student Details" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="ManageStudents.aspx.cs" Inherits="ManageStudents" %>

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
    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to delete this student ?");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container mt-4">

        <asp:Label ID="lblMessage" runat="server" CssClass="text-center d-block text-danger mb-4"></asp:Label>

        <div class="text-center mb-4">
            <h2 class="text-primary">Manage Student Detail</h2>
        </div>
        <div class="row g-3 align-items-center">
            <div class="col-md-3">
                <label for="ddlSession" class="form-label">Select Session:</label>
                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                    InitialValue="" ErrorMessage="Session is required." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-3">
                <label for="SearchCriteria" class="form-label">Search for Student via:</label>
                <div class="form-control">
                    <asp:RadioButton ID="rdRoll" runat="server" Text="Roll No." GroupName="SearchCriteria" AutoPostBack="True" class="form-check-input me-2 border-0" />
                    <asp:RadioButton ID="rdName" runat="server" Text="First Name" GroupName="SearchCriteria" AutoPostBack="True" class="form-check-input border-0" />
                </div>
            </div>
            <div class="col-md-3">
                <label for="txtsearch" class="form-label">Enter Search Text:</label>
                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Enter search term"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <div class="d-flex justify-content-around">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-secondary ms-2" />
                </div>
            </div>
        </div>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <asp:Label ID="lbl" CssClass="text-center d-block text-danger mb-4" runat="server"></asp:Label>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped text-center" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Session" HeaderText="Session" />
                            <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True" SortExpression="FirstName" />
                            <asp:BoundField DataField="MidName" HeaderText="Middle Name" ReadOnly="True" SortExpression="MidName" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" SortExpression="LastName" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkview" runat="server" CommandArgument='<%# Eval("StudentID") %>' CommandName="View" CssClass="btn btn-link" Text="View"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkRem" runat="server" CommandName="Remove" CommandArgument='<%# Eval("StudentID") %>' CssClass="btn btn-link text-danger" Text="Remove" OnClientClick="return confirmDelete();"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:View>

            <asp:View ID="View2" runat="server">
                <div class="text-center mb-4">
                    <label class="form-label">Student's Detail</label>
                </div>
                <asp:HiddenField ID="hfPersonID" runat="server" />
                <asp:HiddenField ID="hfCurrentFilePath" runat="server" />
                <!-- Form -->
                <div class="row g-3">
                    <!-- Name -->
                    <div class="col-md-4">
                        <label for="txtFirstName" class="form-label">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label for="txtMidName" class="form-label">Middle Name</label>
                        <asp:TextBox ID="txtMidName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label for="txtLastName" class="form-label">Last Name</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <!-- Phone -->
                    <div class="col-md-6">
                        <label for="txtPhone" class="form-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                    </div>

                    <!-- Email -->
                    <div class="col-md-6">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" Enabled="false"></asp:TextBox>
                    </div>

                    <!-- Image Upload -->
                    <div class="col-md-6">
                        <label for="fileUpload" class="form-label">Image</label>
                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:Label ID="lblFileTypeError" runat="server" CssClass="form-text text-danger" Visible="false">Invalid file type. Only .jpg, .jpeg, .png files are allowed.</asp:Label>
                    </div>

                    <!-- Current Image Preview -->
                    <div class="col-md-6">
                        <img id="currentImage" runat="server" class="img-thumbnail" src="#" alt="Current Image" style="display: none; max-width: 225px; max-height: 225px;" />
                        <asp:HiddenField ID="imagePreviewBase64" runat="server" />
                        <div id="cropperContainer" class="cropper-container mt-3" style="display: none;">
                            <img id="cropperImage" src="#" alt="Image for cropping" />
                        </div>
                        <asp:Button ID="btnCrop" runat="server" Text="Crop" CssClass="btn btn-primary mt-2" OnClientClick="return cropImage();" Style="display: none;" />
                    </div>

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
                    <div class="col-12 text-center">
                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary me-2" Text="Edit" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnUpdate" CssClass="btn btn-success me-2" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" OnClick="btnBack_Click" Text="Back" />
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
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
