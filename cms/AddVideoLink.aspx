<%@ Page Title="Add Video Links" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="AddVideoLink.aspx.cs" Inherits="Admin_pages_UploadMarqueeVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:720px" class="container bg-white rounded shadow-sm p-5">
            <h1 class="text-center text-primary mb-4">Add Video Link for Video Gallery</h1>
            <div class="row justify-content-center">
                <div class="col-12 col-md-6">
                    <!-- Video Label -->
                    <div class="mb-3">
                        <label for="txtVideoText" class="form-label"><i class="fas fa-tag"></i>Video Label:</label>
                        <asp:TextBox ID="txtVideoText" runat="server" CssClass="form-control" Placeholder="Enter a label for the video"></asp:TextBox>
                    </div>
                    <!-- Video URL -->
                    <div class="mb-3">
                        <label for="txtVideoURL" class="form-label"><i class="fas fa-link"></i>Video Link URL:</label>
                        <asp:TextBox ID="txtVideoURL" runat="server" CssClass="form-control" Placeholder="Enter the video URL"></asp:TextBox>
                    </div>
                    <!-- Buttons -->
                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary me-md-2" OnClick="btnSubmit_Click" />
                        <asp:Button ID="ButtonVideo" runat="server" Text="Manage Links" CssClass="btn btn-secondary" OnClick="btnSubmit_Edit" />
                    </div>
                    <!-- Message Label -->
                    <div class="mt-3">
                        <asp:Label ID="lblMessage" runat="server" CssClass="form-text text-muted"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
