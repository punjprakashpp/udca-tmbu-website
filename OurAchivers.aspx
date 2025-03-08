<%@ Page Title="Our Achievers" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="OurAchivers.aspx.cs" Inherits="OurAchivers" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container content bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary rounded-top p-4">Meet Our Achievers</h1>
            
            <!-- Message Section -->
            <asp:Label ID="lblMessage" runat="server" Text="" class="d-block mb-4"></asp:Label>

            <!-- Achievers List -->
            <div class="row">
                <asp:Repeater ID="AchiverRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6">
                            <div class="card shadow-sm mb-2">
                                <div class="row g-0 align-items-center">
                                    <!-- Achiever Image -->
                                    <div class="col-md-5 text-center p-3">
                                        <img class="img-fluid rounded-circle" src='<%# Eval("FilePath") %>' alt='<%# Eval("FirstName") %>' style="max-width: 120px;">
                                        <h4 class="text-primary mt-2">
                                            <%# Eval("FirstName") %> <%# Eval("MidName") %> <%# Eval("LastName") %>
                                        </h4>
                                        <p><strong>Session:</strong> <%# Eval("Session") %></p>
                                    </div>

                                    <!-- Achiever Details -->
                                    <div class="col-md-7 p-2">
                                        <p><strong>Achievement:</strong> <%# Eval("Achievement") %></p>
                                        <p><strong>Qualification:</strong> <%# Eval("Qualification") %></p>
                                        <p><strong>Occupation / Job:</strong> <%# Eval("Occupation") %></p>
                                        <p><strong>Work Location / Company:</strong> <%# Eval("Company") %></p>
                                        <p><i class="fa fa-phone"></i><strong>Phone:</strong> <a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                                        <p><i class="fa fa-envelope"></i><strong>Email:</strong> <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
</asp:Content>
