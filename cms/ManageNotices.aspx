<%@ Page Title="Manage Notice" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ManageNotices.aspx.cs" Inherits="cms_ManageNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../Content/flatpickr.min.css">
    <script type="text/javascript" src="../Scripts/flatpickr.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Initialize Flatpickr on the TextBox
            const fp = flatpickr('.flatpickr-input', {
                enableTime: false,       // Disable time
                dateFormat: "d-m-Y",     // Date format
                allowInput: true,        // Allow manual input
                clickOpens: true,       // Calendar opens on click
                disableMobile: true,     // Force Flatpickr on mobile
                maxDate: "today"         // today is maximum date
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Heading -->
        <h2 class="text-center text-primary mb-4">Manage Notice</h2>

        <!-- Search Section -->
        <div class="card shadow-sm mb-4">
            <div class="card-body">
                <div class="row g-3 align-items-center justify-content-center">
                    <div class="col-auto">
                        <label for="txtSearch" class="form-label fw-bold">Search using:</label>
                    </div>
                    <div class="col-auto">
                        <asp:RadioButton ID="rdNo" runat="server" Text="No." GroupName="SearchCriteria" CssClass="form-check-input me-2 border-0" />
                    </div>
                    <div class="col-auto">
                        <asp:RadioButton ID="rdTitle" runat="server" Text="Notice" GroupName="SearchCriteria" CssClass="form-check-input me-2 border-0" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="Enter search text"></asp:TextBox>
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Message Display -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>

        <!-- Notices Table -->
        <div class="table-responsive">
            <asp:GridView
                ID="gvNotice"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="DocsID"
                CssClass="table table-hover table-bordered table-striped table-sm align-middle"
                AllowPaging="True"
                PageSize="10"
                OnRowEditing="gvNotice_RowEditing"
                OnRowUpdating="gvNotice_RowUpdating"
                OnRowDeleting="gvNotice_RowDeleting"
                OnRowCancelingEdit="gvNotice_RowCancelingEdit"
                OnPageIndexChanging="gvNotice_PageIndexChanging">

                <Columns>
                    <asp:BoundField DataField="DocsID" HeaderText="Notice ID" ReadOnly="True" Visible="False" />
                    <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNo" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("No") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" Text='<%# Eval("No") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notice" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is required." ForeColor="Red" />
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Title") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNoticeDate" runat="server" CssClass="form-control form-control-sm flatpickr-input" Text='<%# Bind("Date") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNoticeDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Path" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <EditItemTemplate>
                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control form-control-sm" />
                            <asp:HiddenField ID="hiddenFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink
                                ID="linkFilePath"
                                runat="server"
                                NavigateUrl='<%# string.Format("../pdfjs/web/viewer.html?file=/{0}", Eval("FilePath")) %>'
                                Text="View File"
                                Target="_blank"
                                CssClass="btn btn-link btn-sm"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField
                        HeaderText="Action"
                        ShowEditButton="True"
                        ShowDeleteButton="True"
                        HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
