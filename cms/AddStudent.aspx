<%@ Page Title="Add New Student" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/Content/flatpickr.min.css">
    <script type="text/javascript" src="/Scripts/flatpickr.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-3.7.1.min.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Initialize Flatpickr on the TextBox
            const fp = flatpickr('.flatpickr-input', {
                enableTime: false,       // Disable time
                dateFormat: "d-m-Y",     // Date format
                allowInput: true,        // Allow manual input
                clickOpens: false,       // Calendar opens on click
                disableMobile: true,     // Force Flatpickr on mobile
                maxDate: "today"         // today is maximum date
            });

            // Open the calendar when clicking on the calendar icon
            document.getElementById('calendar-icon').addEventListener('click', function () {
                fp.open();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-4" style="max-width:840px">
            <h2 class="text-center text-primary mb-4">Add New Student</h2>
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" CssClass="form-text text-danger"></asp:Label>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="UploadStudentData.aspx" CssClass="d-block mb-3">Click here to upload student data using an Excel file</asp:HyperLink>
            <div class="row g-3">
                <!-- Session -->
                <div class="col-md-6">
                    <label for="ddlSession" class="form-label">Session</label>
                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-select"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" ErrorMessage="Please select a session" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Roll No. -->
                <div class="col-md-6">
                    <label for="txtRollNo" class="form-label">Roll No.</label>
                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revRollNo" runat="server" ControlToValidate="txtRollNo" ErrorMessage="Roll number must be numeric" ForeColor="Red" CssClass="form-text text-danger" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvRollNo" runat="server" ControlToValidate="txtRollNo" ErrorMessage="Roll number is required" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Registration No. -->
                <div class="col-md-6">
                    <label for="txtRegNo" class="form-label">Registration No.</label>
                    <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revRegNo" runat="server" ControlToValidate="txtRegNo" ErrorMessage="Registration No. must be numeric" ForeColor="Red" CssClass="form-text text-danger" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvRegNo" runat="server" ControlToValidate="txtRegNo" ErrorMessage="Registration No. is required" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Registration Year -->
                <div class="col-md-6">
                    <label for="txtRegYear" class="form-label">Registration Year</label>
                    <asp:TextBox ID="txtRegYear" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revRegYear" runat="server" ControlToValidate="txtRegYear" ErrorMessage="Year must be a 4-digit number" ForeColor="Red" CssClass="form-text text-danger" ValidationExpression="^\d{4}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvRegYear" runat="server" ControlToValidate="txtRegYear" ErrorMessage="Registration Year is required" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- First Name -->
                <div class="col-md-4">
                    <label for="txtFirstName" class="form-label">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is required" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Middle Name -->
                <div class="col-md-4">
                    <label for="txtMidName" class="form-label">Middle Name</label>
                    <asp:TextBox ID="txtMidName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <!-- Last Name -->
                <div class="col-md-4">
                    <label for="txtLastName" class="form-label">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required" ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Gender -->
                <div class="col-md-4">
                    <label for="Gender" class="form-label">Gender</label>
                    <div class="form-control">
                        <asp:RadioButton ID="rdoMale" runat="server" Text="Male" GroupName="Gender" CssClass="form-check-input me-2 border-0" Checked="true" />
                        <asp:RadioButton ID="rdoFemale" runat="server" Text="Female" GroupName="Gender" CssClass="form-check-input border-0" />
                    </div>
                </div>
                <!-- Date of Birth -->
                <div class="col-md-5">
                    <label for="txtDOB" class="form-label">Date of Birth</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtDOB" placeholder="e.g. 08-07-2000" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                        <span class="input-group-text" id="calendar-icon">
                            <i class="fa-solid fa-calendar"></i>
                        </span>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB" ErrorMessage="Date of Birth is required." ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
                <!-- Add Student Button -->
                <div class="col-md-3 mt-4 pt-4">
                    <asp:Button ID="btnAddStudent" runat="server" CssClass="btn btn-primary ms-5" OnClick="btnAddStudent_Click" Text="Add Student" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
