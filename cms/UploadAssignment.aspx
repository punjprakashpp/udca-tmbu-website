<%@ Page Title="Upload Assignment" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="UploadAssignment.aspx.cs" Inherits="UploadAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function validateForm() {
            var ddlSemester = document.getElementById('<%= ddlSemester.ClientID %>');
            var isValid = true;
            if (ddlSemester.selectedIndex === 0) {
                alert("Please select a semester.");
                isValid = false;
            }
            return isValid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container contain bg-white rounded shadow-sm py-2 px-4">
            <h2 class="text-center text-primary rounded-top p-4 mb-4">Upload Assignment Image/PDF</h2>
            <div class="mb-4">
                <div class="row">
                    <div class="col-md-4 text-center mb-2">
                        <label for="ddlSemester" class="form-label">Select Semester :</label>
                        <asp:DropDownList ID="ddlSemester" runat="server" class="form-select">
                            <asp:ListItem Text="--- Select Semester ---" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Semester-1" Value="Semester-1"></asp:ListItem>
                            <asp:ListItem Text="Semester-2" Value="Semester-2"></asp:ListItem>
                            <asp:ListItem Text="Semester-3" Value="Semester-3"></asp:ListItem>
                            <asp:ListItem Text="Semester-4" Value="Semester-4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" InitialValue="--- Select Semester ---" ErrorMessage="Please select a semester." ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-md-4 text-center mb-2">
                        <label for="fileUpload" class="form-label">Select Image/PDF/ZIP File :</label>
                        <asp:FileUpload ID="fileUpload" ToolTip="Select File" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-4 text-center mb-2">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary mt-3" runat="server" Text="Uplload File" OnClick="btnSubmit_Click" OnClientClick="return validateForm();" />
                    </div>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
