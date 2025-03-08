﻿<%@ Page Title="Delete Gallery Images" Language="C#" MasterPageFile="~/cms/cms.master" AutoEventWireup="true" CodeFile="DeleteGalleryImages.aspx.cs" Inherits="cms_DeleteGalleryImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5">
    <div class="container">
        <div class="row mb-4">
            <div class="col-md-6 mx-auto">
                <h2 class="text-center">Delete Gallery Images</h2>
                <div class="search-bar mt-3">
                    <div class="d-flex flex-column gap-3">
                        <!-- Search Input -->
                        <div class="d-flex align-items-center">
                            <label for="txtSearch" class="me-3 fw-bold">Search Image:</label>
                            <asp:TextBox ID="txtSearch" CssClass="form-control flex-grow-1" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" CssClass="btn btn-primary ms-3" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <!-- Message Label -->
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-8 mx-auto">
                <!-- Gallery Grid -->
                <asp:GridView ID="gvGallery" runat="server" AutoGenerateColumns="False" DataKeyNames="ImageID" 
                    CssClass="table table-bordered table-striped text-center" AllowPaging="True" PageSize="10" 
                    OnRowDeleting="gvGallery_RowDeleting" OnPageIndexChanging="gvGallery_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ImageID" HeaderText="ImageID" ReadOnly="True" Visible="False" />
                        <asp:BoundField DataField="Title" HeaderText="Occasion" HeaderStyle-CssClass="text-center fw-bold" ItemStyle-CssClass="text-center" />
                        <asp:ImageField DataImageUrlField="FilePath" HeaderText="Image" HeaderStyle-CssClass="text-center fw-bold" 
                            ItemStyle-CssClass="text-center">
                            <ControlStyle CssClass="img-thumbnail" Height="100px" Width="150px" />
                        </asp:ImageField>
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" ButtonType="Button" 
                            HeaderText="Action" HeaderStyle-CssClass="text-center fw-bold" 
                            ItemStyle-CssClass="text-center" ControlStyle-CssClass="btn btn-danger" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</section>

</asp:Content>
