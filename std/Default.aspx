<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/std/std.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="std_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/Styles/std.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container dashboard-container">
            <!-- Profile Header -->
            <div class="profile-header">
                <h2 class="mb-0"><i class="fa-solid fa-user-graduate"></i> Student Dashboard</h2>
            </div>

            <!-- Student Details -->
            <asp:Panel ID="pnlStudent" runat="server">
                <div class="text-center">
                    <asp:Image ID="imgProfile" runat="server" CssClass="profile-img mt-2" />
                </div>
                
                <div class="p-4">
                    <h4 class="text-center text-primary">
                        <i class="fa-solid fa-user"></i> <asp:Label ID="lblName" runat="server"></asp:Label>
                    </h4>

                    <div class="info-card">
                        <p><i class="fa-solid fa-calendar"></i> <b>Session:</b> <asp:Label ID="lblSession" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-venus-mars"></i> <b>Gender:</b> <asp:Label ID="lblGender" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-cake-candles"></i> <b>DOB:</b> <asp:Label ID="lblDOB" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-briefcase"></i> <b>Occupation:</b> <asp:Label ID="lblOccupation" runat="server"></asp:Label> at <asp:Label ID="lblCompany" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-award"></i> <b>Achievements:</b> <asp:Label ID="lblAchievement" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-phone"></i> <b>Phone:</b> <asp:Label ID="lblPhone" runat="server"></asp:Label></p>
                        <p><i class="fa-solid fa-envelope"></i> <b>Email:</b> <asp:Label ID="lblEmail" runat="server"></asp:Label></p>
                    </div>

                    <!-- Social Media Links -->
                    <div class="text-center social-icons">
                        <asp:HyperLink ID="lnkFacebook" runat="server" CssClass="btn btn-sm btn-primary me-2" Target="_blank"><i class="fa-brands fa-facebook"></i></asp:HyperLink>
                        <asp:HyperLink ID="lnkInstagram" runat="server" CssClass="btn btn-sm btn-danger me-2" Target="_blank"><i class="fa-brands fa-instagram"></i></asp:HyperLink>
                        <asp:HyperLink ID="lnkTwitter" runat="server" CssClass="btn btn-sm btn-info me-2" Target="_blank"><i class="fa-brands fa-twitter"></i></asp:HyperLink>
                        <asp:HyperLink ID="lnkGitHub" runat="server" CssClass="btn btn-sm btn-dark me-2" Target="_blank"><i class="fa-brands fa-github"></i></asp:HyperLink>
                        <asp:HyperLink ID="lnkLinkedIn" runat="server" CssClass="btn btn-sm btn-primary" Target="_blank"><i class="fa-brands fa-linkedin"></i></asp:HyperLink>
                    </div>
                </div>
            </asp:Panel>

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger text-center d-block py-2"></asp:Label>
        </div>
    </section>
</asp:Content>
