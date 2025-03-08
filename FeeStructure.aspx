<%@ Page Title="Fee Structure" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/iframe.css" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width: 1024px" class="container bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary mb-4">Fee Structure for MCA Course : <a class="dnld" href="Uploads/docs/FeeStructure.pdf" download="FeeStructure.pdf">Download PDF</a></h1>
            <p class="text-center">Below is the semester-wise breakdown of fees for the MCA course:</p>
            <!-- PDF Viewer -->
            <iframe src="/pdfjs/web/viewer.html?file=/Uploads/docs/FeeStructure.pdf" allowfullscreen webkitallowfullscreen></iframe>
        </div>
    </section>
</asp:Content>
