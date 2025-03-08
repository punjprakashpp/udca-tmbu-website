<%@ Page Title="Our Alumni" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="OurAlumni.aspx.cs" Inherits="OurAlumni" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container content bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary rounded-top p-4">Meet Our Alumni</h1>

            <!-- Search and Navigation Section -->
            <div class="row mb-4">
                <!-- Session Dropdown -->
                <div class="col-md-3 text-center my-2">
                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <!-- Search Button -->
                <div class="col-md-2 text-center my-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                </div>
                <!-- Records Per Page -->
                <div class="col-md-2 text-center my-2">
                    <label class="form-label py-2">Records per page:</label>
                </div>
                <div class="col-md-2 text-center my-2">
                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged">
                        <asp:ListItem Value="10" Text="10" />
                        <asp:ListItem Value="20" Text="20" />
                        <asp:ListItem Value="50" Text="50" />
                    </asp:DropDownList>
                </div>
            <!-- Pagination Buttons -->
            <div class="col-md-3 text-center">
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" CssClass="btn btn-secondary" />
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="btn btn-secondary ms-2" />
            </div>
        </div>

            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" class="d-block mb-4"></asp:Label>
            <asp:Label ID="lblmsg" runat="server" Text="" class="d-block mb-4"></asp:Label>

        <!-- Alumni Display Section -->
        <asp:Repeater ID="facultyRepeater" runat="server">
            <ItemTemplate>
                <div class="card mb-2">
                    <div class="row m-4 align-items-center">
                        <!-- Image and Name -->
                        <div class="col-md-4 text-center">
                            <img class="img-fluid rounded-circle mb-3" src='<%# Eval("FilePath") %>' alt='<%# Eval("FirstName") %>' style="max-width: 150px;">
                            <h3 class="text-primary">
                                <%# Eval("FirstName") %> <%# Eval("MidName") %> <%# Eval("LastName") %>
                            </h3>
                        </div>

                        <!-- Details -->
                        <div class="col-md-4">
                            <p><strong>Session:</strong> <%# Eval("Session") %></p>
                            <p><strong>Qualification:</strong> <%# Eval("Qualification") %></p>
                            <p><strong>Achievement:</strong> <%# Eval("Achievement") %></p>
                            <p><strong>Occupation / Job:</strong> <%# Eval("Occupation") %></p>
                            <p><strong>Work Location / Company:</strong> <%# Eval("Company") %></p>
                        </div>

                        <!-- Contact Details -->
                        <div class="col-md-4">
                            <p><i class="fa fa-phone"></i>&nbsp;<strong>Phone No.:</strong> <a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                            <p><i class="fa fa-envelope"></i>&nbsp;<strong>Email:</strong> <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                            <p><i class="fab fa-linkedin"></i>&nbsp;<strong>LinkedIn:</strong> <a href='https://www.linkedin.com/in/<%# Eval("LinkedIn") %>' target="_blank">@<%# Eval("LinkedIn") %></a></p>
                            <p><i class="fab fa-facebook"></i>&nbsp;<strong>Facebook:</strong> <a href='https://www.facebook.com/<%# Eval("Facebook") %>' target="_blank">@<%# Eval("Facebook") %></a></p>
                            <p><i class="fab fa-instagram"></i>&nbsp;<strong>Instagram:</strong> <a href='https://www.instagram.com/<%# Eval("Instagram") %>' target="_blank">@<%# Eval("Instagram") %></a></p>
                            <p><i class="fab fa-twitter"></i>&nbsp;<strong>Twitter:</strong> <a href='https://twitter.com/<%# Eval("Twitter") %>' target="_blank">@<%# Eval("Twitter") %></a></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        </div>
    </section>
</asp:Content>
