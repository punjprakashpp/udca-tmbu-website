<%@ Page Title="Campus Map" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/iframe.css" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:1024px" class="container bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary">Campus Map</h1>
            <p class="text-center mb-4">Below is the interactive campus map. You can zoom in and navigate to view various campus buildings and facilities.</p>
            <iframe src="/pdfjs/web/viewer.html?file=/Uploads/docs/CampusMap.pdf" allowfullscreen webkitallowfullscreen></iframe>
        </div>
    </section>
</asp:Content>
