<%@ Page Title="Upload Modal Question" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="UploadModalQuestion.aspx.cs" Inherits="UploadModalQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validateForm() {
            var ddlSemester = document.getElementById('<%= ddlSemester.ClientID %>');
            var ddlSession = document.getElementById('<%= ddlSession.ClientID %>');
            var isValid = true;

            if (!ddlSession.disabled && ddlSession.selectedIndex === 0) {
                alert("Please select a session.");
                isValid = false;
            }
            if (!ddlSemester.disabled && ddlSemester.selectedIndex === 0) {
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
            <h2 class="text-center text-primary rounded-top p-4 mb-4">Upload Modal Question Image/PDF</h2>
            <div class="mb-4">
                <div class="row">
                    <div class="col-md-3 text-center mb-2">
                        <label for="ddlSession" class="form-label">Select Session :</label>
                        <asp:DropDownList ID="ddlSession" runat="server" class="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" InitialValue="--- Select Session ---" ErrorMessage="Please select a session." ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-md-3 text-center mb-2">
                        <label for="ddlSemester" class="form-label">Select Semester :</label>
                        <asp:DropDownList ID="ddlSemester" runat="server" class="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" InitialValue="--- Select Semester ---" ErrorMessage="Please select a semester." ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-md-3 text-center mb-2">
                        <label for="fileUpload" class="form-label">Select Image/PDF/ZIP File :</label>
                        <asp:FileUpload ID="fileUpload" ToolTip="Select File" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-3 text-center mb-2">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary mt-3" runat="server" Text="Uplload File" OnClick="btnSubmit_Click" OnClientClick="return validateForm();" />
                    </div>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
