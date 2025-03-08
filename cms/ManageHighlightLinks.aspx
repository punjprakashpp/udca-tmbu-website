<%@ Page Title="Manage Highlight Links" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="ManageHighlightLinks.aspx.cs" Inherits="cms_ManageHighlightLinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <h2 class="text-center text-primary mb-4">Manage Highlight Links</h2>

                <!-- Search Bar -->
                <div class="mb-4">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" Placeholder="Search Highlight Link"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>

                <!-- Message Label -->
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="d-block text-center"></asp:Label>
            </div>
        </div>

        <!-- Links Grid -->
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView
                        ID="gvLinks"
                        runat="server"
                        AutoGenerateColumns="False"
                        DataKeyNames="LinkID"
                        CssClass="table table-striped table-bordered align-middle"
                        AllowPaging="True"
                        PageSize="10"
                        OnPageIndexChanging="gvLinks_PageIndexChanging"
                        OnRowEditing="gvLinks_RowEditing"
                        OnRowUpdating="gvLinks_RowUpdating"
                        OnRowDataBound="gvLinks_RowDataBound"
                        OnRowDeleting="gvLinks_RowDeleting"
                        OnRowCancelingEdit="gvLinks_RowCancelingEdit">

                        <Columns>
                            <asp:BoundField DataField="LinkID" HeaderText="Link ID" ReadOnly="True" Visible="False" />
                            <asp:TemplateField HeaderText="Link Text">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLinkText" runat="server" CssClass="form-control" Text='<%# Bind("LinkText") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLinkText" runat="server" Text='<%# Eval("LinkText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Link URL">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                                <EditItemTemplate>
                                    <div class="form-check mb-2">
                                        <asp:CheckBox ID="chkboxExt" runat="server" AutoPostBack="true" CssClass="form-check-input" OnCheckedChanged="chkboxExt_CheckedChanged" />
                                        <label for="chkboxExt" class="form-check-label">External Link</label>
                                    </div>
                                    <asp:TextBox ID="txtLinkURL" runat="server" CssClass="form-control mb-2" Text='<%# Bind("LinkURL") %>' Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlLinkPicker" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLinkPicker_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLinkURL" runat="server" Text='<%# Eval("LinkURL") %>' CssClass="d-block text-truncate"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Action">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


