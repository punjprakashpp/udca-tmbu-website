<%@ Page Title="Dress Code" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/pages.css">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container contain bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary">Dress Code Policy</h1>
            <p class="mb-4 text-justify">
                In order to maintain a professional and respectful environment, all students are required to adhere to the following dress code:
            </p>

            <div class="text-center mt-5 mb-4">
                <h3 class="text-secondary mb-4">Examples of Acceptable Dress Code:</h3>
                <img src="Image/other/dress.jpg" alt="Acceptable Dress Code" class="img-fluid rounded shadow">
            </div>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">General Guidelines</h2>
            <ul class="list-unstyled ps-3">
                <li class="list-item mb-3">Clothing should be clean, neat, and in good condition.</li>
                <li class="list-item mb-3">For boys: Clothing should consist of <span class="grey-pants">grey pants</span>, <span class="white-shirt">white shirt</span>, and <span class="black-shoes">black shoes</span>.</li>
                <li class="list-item mb-3">For girls: Clothing should consist of <span class="grey-salwar">grey salwar</span>, <span class="white-pajama">white pajama</span>, and <span class="black-shoes">black shoes</span>.</li>
                <li class="list-item mb-3">Offensive slogans or images on clothing are not permitted.</li>
            </ul>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">Specific Requirements</h2>
            <p class="mb-4 text-justify">
                Depending on the context, specific dress code requirements may apply:
            </p>
            <ul class="list-unstyled ps-3">
                <li class="list-item mb-3"><strong>Business Attire:</strong> Required for presentations, meetings, and formal events.</li>
                <li class="list-item mb-3"><strong>Casual Attire:</strong> Acceptable for day-to-day activities, but should still be presentable.</li>
                <li class="list-item mb-3"><strong>Sports Attire:</strong> Allowed for sports activities only, not suitable for other occasions.</li>
            </ul>

            <p class="mt-4 text-justify">
                Please ensure that you are aware of any specific dress code requirements for your classes, events, or activities.
            </p>
        </div>
    </section>
</asp:Content>
