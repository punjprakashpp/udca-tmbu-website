<%@ Page Title="Download Assignments" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="DownloadAssignments.aspx.cs" Inherits="DownloadAssignments" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css" />
    <script type="text/javascript" src="Scripts/jquery-3.7.1.min.js"></script>
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

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container contain bg-white rounded shadow-sm py-2 px-4">
            <h1 class="text-center text-primary rounded-top p-4 mb-4">Download Assignments</h1>

            <!-- Download Type Selection Section -->
            <div class="mb-4">
                <div class="row">
                    <div class="col-sm-6 text-center mb-2">
                        <asp:DropDownList ID="ddlSemester" runat="server" class="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" InitialValue="--- Select Semester ---" ErrorMessage="Please select a semester." ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-sm-6 text-center mb-2">
                        <asp:Button ID="btnShowFiles" runat="server" Text="Show Files" OnClick="btnShowFiles_Click" OnClientClick="return validateForm();" CssClass="btn btn-primary" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-secondary ms-2" />
                    </div>
                </div>
            </div>

            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" class="d-block mb-4"></asp:Label>

            <!-- Files Grid Section -->
            <div class="table-responsive">
                <asp:GridView ID="GridViewFiles" runat="server" AutoGenerateColumns="False" DataKeyNames="DocsID" OnRowCommand="GridViewFiles_RowCommand" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="File Name" />
                        <asp:TemplateField HeaderText="File">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("DocsID") %>' class="btn btn-link">Download</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>
