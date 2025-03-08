<%@ Page Title="Director's Message" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Director.aspx.cs" Inherits="About_Director" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container bg-white rounded shadow-sm py-5 px-4">
            <!-- Header Section -->
            <h1 class="text-center text-primary mb-5">Director of University Department of Computer Applications</h1>

            <div class="row align-items-center">
                <!-- Director Image and Information -->
                <div class="col-md-4 text-center">
                    <asp:Image ID="imgPerson" runat="server" Width="175" CssClass="img-fluid rounded-circle shadow mb-3" />
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
                        <br />
                        <a href="/pdfjs/web/viewer.html?file=/Uploads/Profile/DirectorProfile.pdf" target="_blank" class="btn btn-primary mt-3">
                            <i class="fas fa-info-circle"></i>Learn More
                        </a>
                    </p>
                </div>

                <!-- Director's Message -->
                <div class="col-md-8">
                    <h2>Director's Message</h2>
                    <p align="justify" class="lead">Welcome to the University Department of Computer Applications (UDCA), a vibrant and dynamic institution renowned for its global vision, academic innovation, and commitment to excellence. We are driven by the mission to shape the future of technology through high-quality education and research</p>
                    <p align="justify" class="lead">At UDCA, we empower students with the knowledge and skills needed to thrive in today's fast-evolving technological landscape. Our curriculum is thoughtfully crafted to align with industry demands while nurturing a broader sense of social responsibility. Supported by state-of-the-art facilities and a dedicated faculty, we prepare students to become not only skilled professionals but also compassionate leaders ready to drive meaningful change.</p>
                </div>
            </div>
            <div class="row align-items-center">
                <p align="justify" class="lead">Learning at UDCA is a collaborative journey. We emphasize a supportive, inclusive environment that promotes intellectual growth, personal development, and the power of partnerships among students, faculty, and the community.</p>
                <p align="justify" class="lead">I invite you to explore UDCA - a place where knowledge opens doors, and aspirations take flight. Join us in shaping a brighter, more innovative future.</p>
            </div>
        </div>
    </section>
</asp:Content>
