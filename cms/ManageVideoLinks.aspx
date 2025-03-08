<%@ Page Title="Manage Video Links" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ManageVideoLinks.aspx.cs" Inherits="Admin_pages_EditDeleteMarqueeLinks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Heading -->
        <h1 class="text-center text-primary mb-4">Manage Video Links</h1>

        <!-- Search Section -->
        <div class="card shadow-sm mb-4">
            <div class="card-body m-auto">
                <div class="row g-3 align-items-center">
                    <!-- Video Label Search -->
                    <div class="col-md-5">
                        <label for="txtSearchVideoLabel" class="form-label fw-bold">Search Video:</label>
                        <asp:TextBox ID="txtSearchVideoLabel" CssClass="form-control" runat="server" Placeholder="Enter video label"></asp:TextBox>
                    </div>

                    <!-- Records Per Page -->
                    <div class="col-md-4">
                        <label for="ddlRecordsPerPage" class="form-label fw-bold">Records per page:</label>
                        <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged">
                            <asp:ListItem Value="10" Text="10" />
                            <asp:ListItem Value="20" Text="20" />
                            <asp:ListItem Value="50" Text="50" />
                        </asp:DropDownList>
                    </div>

                    <!-- Search Button -->
                    <div class="col-md-3 d-flex justify-content-end align-items-end">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Pagination Controls -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-secondary" OnClick="btnPrevious_Click" />
            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-secondary" OnClick="btnNext_Click" />
        </div>

        <!-- Video Links Grid -->
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LinkID"
                CssClass="table table-striped table-hover table-bordered align-middle"
                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LinkID" HeaderText="Video ID" ReadOnly="True" SortExpression="LinkID" Visible="False" />
                    <asp:TemplateField HeaderText="Video Label">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVideoText" runat="server" Text='<%# Bind("LinkText") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVideoText" runat="server" Text='<%# Eval("LinkText") %>' CssClass="text-truncate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Video URL">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVideoURL" runat="server" Text='<%# Bind("LinkURL") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="linkVideoURL" runat="server" NavigateUrl='<%# Eval("LinkURL") %>' Text="View Video" Target="_blank" CssClass="btn btn-link"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Actions" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
