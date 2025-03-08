<%@ Page Title="University Department of Computer Applications" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/slider.css">
    <link rel="stylesheet" type="text/css" href="Styles/section.css">
    <script defer type="text/javascript" src="Scripts/slider.js"></script>
    <script defer type="text/javascript" src="Scripts/section.js"></script>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-md-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <div class="row">
                <div class="col-lg-8 col-md-7 col-12">
                    <div class="slider-section">
                        <div class="slider-container">
                            <div class="slides">
                                <asp:Repeater ID="SliderRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="slide">
                                            <img class="image" src="<%# Eval("FilePath") %>" alt="<%# Eval("Title") %>" />
                                            <div class="caption"><%# Eval("Title") %></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <a class="prev text-decoration-none" onclick="changeSlide(-1)">&#10094;</a>
                                <a class="next text-decoration-none" onclick="changeSlide(1)">&#10095;</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-5 col-12">
                    <div class="notice-section card shadow-sm border-0">
                        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                            <h2 class="h5 mb-0 text-center">Latest News and Notices</h2>
                            <a href="LatestNotice.aspx" class="btn btn-sm btn-light">View All</a>
                        </div>
                        <div class="card-body p-3">
                            <div class="notice-board">
                                <div class="notice-links">
                                    <marquee class="marquee text-muted" id="marq" direction="up" scrollamount="2" onmouseover="this.stop();" onmouseout="this.start();">
                                        <asp:Literal ID="LiteralNotices" runat="server"></asp:Literal>
                                    </marquee>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row py-3">
                <div class="col-12">
                    <div class="links-section">
                        <p class="high">Highlights: </p>
                        <marquee behavior="scroll" onmouseover="this.stop();" onmouseout="this.start();">
                            <asp:Repeater ID="linksRepeater" runat="server">
                                <ItemTemplate>
                                    <a href='<%# Eval("LinkURL") %>' target="_blank"><b><font size="3"><%# Eval("LinkText") %></font></b></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </marquee>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary">WELCOME TO<br />
                UNIVERSITY DEPARTMENT OF COMPUTER APPLICATIONS</h1>
            <p align="justify" class="lead text-justify">The University Department of Computer Applications (UDCA), also known as the Computer Centre, is a prominent unit of Tilka Manjhi Bhagalpur University (TMBU), Bihar. Renowned for its academic excellence, UDCA offer a Master of Computer Applications (MCA) program, providing in-depth knowledge in computer applications. Recognized by the Government of Bihar and approved by AICTE, New Delhi, UDCA sets a high standard for computer science education in the region.</p>

            <h2>Our Mission:</h2>
            <p align="justify" class="lead text-justify">UDCA is committed to equipping students with the necessary knowledge and skills to thrive in the ever-evolving field of Information Technology. The department creates an environment that fosters critical thinking, creativity, and social responsibility. It is dedicated to nurturing students to become not only successful IT professionals but also compassionate citizens who make positive contributions to society.</p>

            <h2>Our Vision:</h2>
            <p align="justify" class="lead text-justify">UDCA envisions becoming a center of excellence in computer applications, providing world-class education with a global perspective. The department aims to develop innovative and ethical leaders who will shape the future of technology and contribute to the betterment of society.</p>
        </div>
    </section>
    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <div class="col">
                    <a href="Chancellor.aspx" class="card text-center text-decoration-none">
                        <div class="card-body box">
                            <h3 class="card-title text-secondary">
                                <asp:Label ID="lblChancellor" runat="server" Text="Chancellor of T.M.B.U"></asp:Label>
                            </h3>
                            <asp:Image ID="ImageChancellor" class="img-fluid rounded-circle mb-3" runat="server" />
                            <h4 class="card-subtitle text-primary">
                                <asp:Label ID="lblNameChancellor" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                    </a>
                </div>
                <div class="col">
                    <a href="ViceChancellor.aspx" class="card text-center text-decoration-none">
                        <div class="card-body box">
                            <h3 class="card-title text-secondary">
                                <asp:Label ID="lblViceChancellor" runat="server" Text="Vice-Chancellor of T.M.B.U"></asp:Label>
                            </h3>
                            <asp:Image ID="ImageViceChancellor" class="img-fluid rounded-circle mb-3" runat="server" />
                            <h4 class="card-subtitle text-primary">
                                <asp:Label ID="lblNameViceChancellor" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                    </a>
                </div>
                <div class="col">
                    <a href="Director.aspx" class="card text-center text-decoration-none">
                        <div class="card-body box">
                            <h3 class="card-title text-secondary">
                                <asp:Label ID="lblDirector" runat="server" Text="Director of U.D.C.A."></asp:Label>
                            </h3>
                            <asp:Image ID="ImageDirector" class="img-fluid rounded-circle mb-3" runat="server" />
                            <h4 class="card-subtitle text-primary">
                                <asp:Label ID="lblNameDirector" runat="server" Text=""></asp:Label>
                            </h4>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </section>
    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <div class="row">
                <div class="bg-white rounded shadow-sm px-4 col-12 col-md-6">
                    <h2 class="text-center">Features of UDCA:</h2>
                    <p align="justify"><b>Constituent Unit of TMBU: </b>UDCA is an integral part of Tilka Manjhi Bhagalpur University, Bhagalpur.</p>
                    <p align="justify"><b>Pioneer in MCA Program: </b>TMBU is the first university in Bihar to offer a 3-year, 6-semester full-time MCA course.</p>
                    <p align="justify"><b>Recognition and Accreditation: </b>UDCA's MCA course recognized by the Govt. of Bihar and approved by AICTE.</p>
                    <p align="justify"><b>Expansive Campus: </b>The UDCA campus spans 2.5 acres, providing a spacious and conducive environment.</p>
                    <p align="justify"><b>Strategic Location: </b>Conveniently located between the University Administrative Building and NH 80.</p>
                    <p align="justify"><b>Wi-Fi Enabled Campus: </b>Fully Wi-Fi-enabled campus, ensuring seamless internet access for students.</p>
                </div>
                <div class="bg-white rounded shadow-sm px-4 col-12 col-md-6">
                    <h2 class="text-center">Facilities at UDCA:</h2>
                    <div class="row row-cols-2 row-cols-lg-3 g-4">
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/computer-lab.png" class="card-img-top w-75 m-auto" alt="Computer Lab">
                                    <p class="card-text">Computer Lab</p>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/library.png" class="card-img-top w-75 m-auto" alt="Library">
                                    <p class="card-text">Library</p>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/faculty.png" class="card-img-top w-75 m-auto" alt="Faculty">
                                    <p class="card-text">Qualified Faculty</p>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/hostel.png" class="card-img-top w-75 m-auto" alt="Hostel">
                                    <p class="card-text">University Hostel</p>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/hospital.png" class="card-img-top w-75 m-auto" alt="Health Centre">
                                    <p class="card-text">Health Centre</p>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <div class="card-body">
                                    <img src="Image/icon/free-wifi.png" class="card-img-top w-75 m-auto" alt="Free Wi-Fi">
                                    <p class="card-text">Wi-Fi Enabled</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <div class="row">
                <div class="bg-white box rounded shadow-sm px-4 col-md-4 text-center">
                    <asp:Image ID="ImgDirector" class="img-fluid rounded-circle mb-3" Width="150" runat="server" />
                    <h2>
                        <asp:Label ID="NameDirector" runat="server" Text="" CssClass="text-primary" />
                    </h2>
                    <h3>Director of UDCA</h3>
                </div>
                <div class="bg-white rounded shadow-sm px-4 col-md-8">
                    <h2 class="text-primary">Director's Message</h2>
                    <p align="justify" class="lead">Welcome to the University Department of Computer Applications (UDCA), a vibrant and dynamic institution renowned for its global vision, academic innovation, and commitment to excellence. As the Director, it is my privilege to guide you through our department, where we are driven by the mission to shape the future of technology and empower our students with the skills and knowledge essential for success in today's fast-evolving world.</p>
                    <a href="Director.aspx" class="btn btn-primary mb-2">Read more</a>
                </div>
            </div>
        </div>
    </section>
    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <h2>Important Links:</h2>
            <div class="row">
                <asp:Repeater ID="ButtonRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-sm-6 col-md-4 col-lg-3 col-xl-2 mb-4">
                            <a href='<%# Eval("LinkURL") %>' target="_blank" class="card text-center text-decoration-none">
                                <div class="card-body">
                                    <img src="Image/icon/udca.png" class="card-img-top w-75 m-auto" alt="logo">
                                    <p class="card-text"><%# Eval("LinkText") %></p>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <section class="py-4">
        <div class="container bg-white rounded shadow-sm p-4">
            <div class="row">
                <div class="bg-white rounded shadow-sm px-4 col-12 col-md-6">
                    <div class="row row-cols-1 row-cols-lg-2 g-4">
                        <div class="col">
                            <div class="card">
                                <div class="board">
                                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                                        <h2 class="h5 mb-0 text-center">Latest Events</h2>
                                        <a href="LatestEvents.aspx" class="btn btn-sm btn-light">View All</a>
                                    </div>
                                    <div class="notice-links">
                                        <marquee class="marquee" id="marq1" direction="up" scrollamount="2" onmouseover="this.stop();" onmouseout="this.start();">
                                            <asp:Literal ID="LiteralEvent" runat="server"></asp:Literal>
                                        </marquee>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card">
                                <div class="board">
                                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                                        <h2 class="h5 mb-0 text-center">Latest Tenders</h2>
                                        <a href="ViewTender.aspx" class="btn btn-sm btn-light">View All</a>
                                    </div>
                                    <div class="notice-links">
                                        <marquee class="marquee" id="marq2" direction="up" scrollamount="2" onmouseover="this.stop();" onmouseout="this.start();">
                                            <asp:Literal ID="LiteralTender" runat="server"></asp:Literal>
                                        </marquee>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bg-white rounded shadow-sm px-4 col-12 col-md-6">
                    <h2>Quick Links:</h2>
                    <div class="row row-cols-2 row-cols-lg-3 g-4">
                        <div class="col">
                            <div class="card text-center">
                                <a href="AboutDevelopers.aspx" class="text-decoration-none">
                                    <div class="card-body text-center">
                                        <img src="Image/icon/developer.png" class="card-img-top w-75 m-auto" alt="About Developers"/>
                                        <div class="card-text">About Developers</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <a href="ImageGallery.aspx" class="text-decoration-none">
                                    <div class="card-body">
                                        <img src="Image/icon/gallery.png" class="card-img-top w-75 m-auto" alt="Image Gallery"/>
                                        <div class="card-text">Image Gallery</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <a href="HolidayList.aspx" class="text-decoration-none">
                                    <div class="card-body">
                                        <img src="Image/icon/holiday-list.png" class="card-img-top w-75 m-auto" alt="Holiday List"/>
                                        <div class="card-text">Holiday List</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <a href="Prospectus.aspx" class="text-decoration-none">
                                    <div class="card-body">
                                        <img src="Image/icon/prospectus.png" class="card-img-top w-75 m-auto" alt="Prospectus"/>
                                        <div class="card-text">Prospectus</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <a href="TimeTable.aspx" class="text-decoration-none">
                                    <div class="card-body">
                                        <img src="Image/icon/time-table.png" class="card-img-top w-75 m-auto" alt="Time Table"/>
                                        <div class="card-text">Time Table</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-center">
                                <a href="CourseSyllabus.aspx" class="text-decoration-none">
                                    <div class="card-body">
                                        <img src="Image/icon/syllabus.png" class="card-img-top w-75 m-auto" alt="Syllabus"/>
                                        <div class="card-text">Syllabus</div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>