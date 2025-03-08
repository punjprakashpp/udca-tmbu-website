<%@ Page Title="Holiday List" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/iframe.css" />
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:1024px" class="container bg-white rounded shadow-sm p-4">
            <!-- Heading with download link -->
            <h1 class="text-center text-primary mb-4">
                TMBU Holiday List:
                <a class="dnld" href="Uploads/docs/HolidayList.pdf" download="HolidayList.pdf">Download PDF</a>
            </h1>

            <!-- PDF Viewer -->
            <iframe src="/pdfjs/web/viewer.html?file=/Uploads/docs/HolidayList.pdf" allowfullscreen webkitallowfullscreen></iframe>
        </div>
    </section>
</asp:Content>
