<%@ Page Title="News Gallery" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="NewsGallery.aspx.cs" Inherits="NewsGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        // JavaScript for displaying the lightbox modal
        function displayImage(FilePath) {
            const lightboxImage = document.getElementById('lightboxImage');
            lightboxImage.src = FilePath;

            // Trigger the Bootstrap modal
            const lightboxModal = new bootstrap.Modal(document.getElementById('lightboxModal'));
            lightboxModal.show();
        }
    </script>
    <style>
    .modal-dialog {
        max-width: 50%;
    }
    @media (max-width: 1200px) {
        .modal-dialog {
            max-width: 75%;
        }
    }
    @media (max-width: 840px) {
        .modal-dialog {
            max-width: 100%;
        }
    }
</style>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-4">
            <!-- Gallery Header -->
            <h1 class="text-center text-primary mb-4">News Gallery</h1>

            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="d-block alert text-center"></asp:Label>

            <!-- News Gallery -->
            <div class="row g-4">
                <asp:Repeater ID="NewsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-12 col-md-6 col-lg-4">
                            <div class="card shadow border-0">
                                <!-- News Image -->
                                <a href="javascript:void(0);" onclick="displayImage('<%# Eval("FilePath") %>')">
                                    <img class="card-img-top img-fluid" src='<%# Eval("FilePath") %>' alt='<%# Eval("Title") %>'>
                                </a>
                                <!-- News Title -->
                                <div class="card-footer bg-light">
                                    <h5 class="text-center text-primary mb-0"><%# Eval("Title") %></h5>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Lightbox Modal -->
    <div class="modal fade" id="lightboxModal" tabindex="-1" aria-labelledby="lightboxModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="lightboxModalLabel">Image Preview</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <img id="lightboxImage" src="" class="img-fluid rounded" alt="Image Preview">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
