<%@ Page Title="Vice-Chancellor" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="ViceChancellor.aspx.cs" Inherits="ViceChancellor" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container bg-white rounded shadow-sm py-5 px-4">
            <!-- Header Section -->
            <h1 class="text-center text-primary mb-5">Vice-Chancellor of Tilka Manjhi Bhagalpur University</h1>

            <div class="row align-items-center">
                <!-- Vice-Chancellor Image and Information -->
                <div class="col-md-4 text-center">
                    <asp:Image ID="imgPerson" runat="server" CssClass="img-fluid rounded-circle shadow mb-3" AlternateText="Vice-Chancellor's Image" />
                    <h2 class="text-primary mt-2">
                        <asp:Label ID="lblName" runat="server" />
                    </h2>
                    <p>
                        <b>Phone No.:</b>
                        <asp:HyperLink ID="phoneLink" runat="server" class="text-decoration-none">
                            <asp:Label ID="lblPhone" runat="server" />
                        </asp:HyperLink>
                        <br />
                        <b>Email:</b>
                        <asp:HyperLink ID="emailLink" runat="server" class="text-decoration-none">
                            <asp:Label ID="lblEmail" runat="server" />
                        </asp:HyperLink>
                    </p>
                </div>

                <!-- Vice-Chancellor's Message -->
                <div class="col-md-8">
                    <h2>Vice-Chancellor's Message</h2>
                    <p align="justify" class="lead">
                        Welcome to Tilka Manjhi Bhagalpur University (TMBU), a hub of learning and innovation. As Vice-Chancellor, I am honored to lead this institution dedicated to academic excellence, research, and holistic development. At TMBU, we strive to empower our students with the skills, knowledge, and values needed to succeed in a rapidly changing world.
                    </p>
                    <p align="justify" class="lead">
                        Our diverse programs and dynamic learning environment encourage critical thinking, ethical behavior, and meaningful societal contributions. We are proud of our accomplished faculty who mentor and guide students, fostering a culture of inquiry, innovation, and community engagement.
                    </p>
                    <p align="justify" class="lead">
                        We are committed to growth, research, and global collaboration, ensuring that our graduates are not only well-prepared professionals but also socially responsible citizens. I invite you to join us on this journey of excellence, innovation, and impact.
                    </p>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
