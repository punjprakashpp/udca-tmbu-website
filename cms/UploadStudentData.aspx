<%@ Page Title="Upload Student Data" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="UploadStudentData.aspx.cs" Inherits="UploadStudentData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-6 col-md-8">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h2 class="card-title text-center mb-4">Upload Student Data</h2>
                        <div class="mb-3">
                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                        </div>
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-primary" />
                        </div>
                        <asp:Label ID="lblmsg" runat="server" CssClass="form-text text-center text-danger"></asp:Label>
                        <div class="text-center mt-3">
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/cms/ManageStudents.aspx" runat="server" CssClass="text-decoration-none">Click here to view Student Report</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
