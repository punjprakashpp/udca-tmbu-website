<%@ Page Title="Download Files" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="DownloadOtherFiles.aspx.cs" Inherits="Downloads" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <style>
        .contain {
            max-width:840px;
        }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container contain bg-white rounded shadow-sm py-5 px-4">
            <h1 class="text-center text-primary rounded-top p-4 mb-4">Download Other Uploaded Files</h1>
                        
            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" class="d-block mb-4"></asp:Label>

            <!-- Files Grid Section -->
            <div class="table-responsive">
                <asp:GridView ID="GridViewFiles" runat="server" AutoGenerateColumns="False" DataKeyNames="DocsID" PageSize="10" AllowPaging="true" OnRowCommand="GridViewFiles_RowCommand" CssClass="table table-bordered table-striped">
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
