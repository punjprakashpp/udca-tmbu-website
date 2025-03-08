<%@ Page Title="Delete Other Files" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="DeleteOtherFiles.aspx.cs" Inherits="DeleteOtherFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container py-4">
        <!-- Page Heading -->
        <h2 class="text-center mb-4">Delete Other Files</h2>

        <!-- Message Display -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>

        <!-- Notices Table -->
        <div class="table-responsive">
            <asp:GridView ID="gvNotice" runat="server" AutoGenerateColumns="False" DataKeyNames="DocsID"
                CssClass="table table-hover table-bordered table-striped table-sm align-middle" 
                AllowPaging="True" PageSize="10" OnRowDeleting="gvNotice_RowDeleting" OnPageIndexChanging="gvNotice_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="DocsID" HeaderText="Notice ID" ReadOnly="True" Visible="False" />
                    <asp:TemplateField HeaderText="File Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Path" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">                        
                        <ItemTemplate>
                            <asp:HyperLink ID="linkFilePath" runat="server" NavigateUrl='<%# string.Format("../pdfjs/web/viewer.html?file=/{0}", Eval("FilePath")) %>' Text="View File" Target="_blank" CssClass="btn btn-link btn-sm"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Action" ShowDeleteButton="True"
                        HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center btn-danger" ControlStyle-CssClass="btn btn-danger" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
