<%@ Page Title="CMS Login" Language="C#" EnableEventValidation="false" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
    <link rel="stylesheet" href="Content/flatpickr.min.css">
    <script type="text/javascript" src="Scripts/flatpickr.js"></script>
    <script type="text/javascript" src="Scripts/jquery-3.7.1.min.js"></script>
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
        <div class="container bg-white rounded shadow-sm p-5" style="max-width: 480px">
            <div class="d-flex align-items-center gap-3 mb-3">
                <label for="rblLoginType" class="form-label mb-0 mx-auto text-primary">Select Portal:</label>
                <asp:RadioButtonList ID="rblLoginType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblLoginType_SelectedIndexChanged" RepeatLayout="Flow" CssClass="d-flex gap-3 mx-auto">
                    <asp:ListItem Text="Alumni Portal" Value="Alumni"></asp:ListItem>
                    <asp:ListItem Text="CMS Portal" Value="CMS"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Panel ID="AlumniPanel" runat="server" Enabled="false" Visible="false">
                <h2 class="text-center text-primary mb-4">
                    <i class="fas fa-user-lock"></i>&nbsp;Alumni Portal Login
                </h2>
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="card shadow border-0">
                            <div class="card-body">
                                <asp:Label ID="Label1" runat="server" Text="Login to Alumni / Student Portal" CssClass="form-text text-muted text-center mb-4"></asp:Label>
                                <div class="mb-3">
                                    <label for="ddlSession" class="form-label">Select Session:</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        InitialValue="" ErrorMessage="Session is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="mb-3">
                                    <label for="txtRollNo" class="form-label">Roll No:</label>
                                    <asp:TextBox ID="txtRollNo" placeholder="e.g. 2201" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRollNo" runat="server" ControlToValidate="txtRollNo"
                                        ErrorMessage="Roll No is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revRollNo" runat="server" ControlToValidate="txtRollNo"
                                        ErrorMessage="Roll No should be only numerals." ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="mb-3">
                                    <label for="txtRegistrationNo" class="form-label">Registration No:</label>
                                    <asp:TextBox ID="txtRegistrationNo" placeholder="e.g. 165630043" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                        ErrorMessage="Registration No is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                        ErrorMessage="Registration No should be only numerals." ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="mb-3">
                                    <label for="txtRegistrationYear" class="form-label">Registration Year:</label>
                                    <asp:TextBox ID="txtRegistrationYear" placeholder="e.g. 2016" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRegistrationYear" runat="server" ControlToValidate="txtRegistrationYear"
                                        ErrorMessage="Registration Year is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revRegistrationYear" runat="server" ControlToValidate="txtRegistrationYear"
                                        ErrorMessage="Invalid year format." ForeColor="Red" ValidationExpression="^\d{4}$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="mb-3">
                                    <label for="txtDOB" class="form-label">Date of Birth:</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDOB" placeholder="e.g. 01-01-1999" runat="server"
                                            CssClass="form-control flatpickr-input"></asp:TextBox>
                                        <span class="input-group-text" id="calendar-icon">
                                            <i class="fa-solid fa-calendar"></i>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB"
                                        ErrorMessage="Date of Birth is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                    <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-primary me-md-2" Text="Verify" OnClick="btnVerify_Click"></asp:Button>
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnClear_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="CMSPanel" runat="server" Enabled="false" Visible="false">
                <h2 class="text-center text-primary mb-4">
                    <i class="fas fa-user-lock"></i>&nbsp;CMS Portal Login
                </h2>
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="card shadow border-0">
                            <div class="card-body">
                                <asp:Label ID="lblMessage" runat="server" Text="Login to Website's Content Management System" CssClass="form-text text-muted text-center mb-4"></asp:Label>
                                <div class="mb-3">
                                    <label for="txtUserName" class="form-label"><i class="fas fa-user"></i>Username:</label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Placeholder="Enter your username"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtPassword" class="form-label"><i class="fas fa-lock"></i>Password:</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
                                </div>
                                <div class="form-check mb-3">
                                    <asp:CheckBox ID="chkboxRem" runat="server" CssClass="form-check-input" />
                                    <label class="form-check-label" for="chkboxRem">Remember me</label>
                                </div>
                                <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary me-md-2" Text="Login" OnClick="btnLogin_Click"></asp:Button>
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </section>
</asp:Content>
