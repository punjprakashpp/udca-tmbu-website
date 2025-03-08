<%@ Page Title="Video Gallery" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="VideoGallery.aspx.cs" Inherits="pages_VideoGallery" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-4">
            <!-- Gallery Header -->
            <h1 class="text-center text-primary mb-4">Video Gallery</h1>
            
            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="d-block alert text-center"></asp:Label>

            <!-- Video Gallery -->
            <div class="row g-4">
                <asp:Repeater ID="linksRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-12 col-md-6 col-lg-4">
                            <div class="card shadow border-0">
                                <!-- Video -->
                                <div class="ratio ratio-16x9">
                                    <iframe src='<%# Eval("LinkURL") %>' title="YouTube video player" frameborder="0" 
                                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
                                            referrerpolicy="strict-origin-when-cross-origin" allowfullscreen>
                                    </iframe>
                                </div>
                                <!-- Video Label -->
                                <div class="card-body">
                                    <h5 class="card-title text-center text-primary"><%# Eval("LinkText") %></h5>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
</asp:Content>
