<%@ Page Title="Faculty Members" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="FacultyMember.aspx.cs" Inherits="FacultyMember" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container content bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary rounded-top p-4">Our Faculty Members</h1>
            <div class="row">
                <asp:Repeater ID="facultyRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 mb-3">
                            <div class="card shadow-sm">
                                <div class="row g-0 align-items-center">
                                    <div class="col-md-4 text-center">
                                        <img src='<%# Eval("FilePath") %>' alt='<%# Eval("Name") %>' class="img-fluid rounded-circle p-1" />
                                    </div>
                                    <div class="col-md-8 p-2">
                                        <div class="mx-2">
                                            <h2 class="h5 text-primary my-2"><%# Eval("Name") %></h2>
                                            <p><strong>Qualification:</strong>&nbsp;<%# Eval("Qualification") %></p>
                                            <p><strong>Position:</strong>&nbsp;<%# Eval("Position") %></p>
                                            <p><i class="fa fa-phone"></i>&nbsp;<strong>Phone:</strong>&nbsp;<a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                                            <p><i class="fa fa-envelope"></i>&nbsp;<strong>Email:</strong>&nbsp;<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <h1 class="text-center text-primary rounded-top p-4">Our Guest Faculty Members</h1>
                <asp:Repeater ID="GuestRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 mb-3">
                            <div class="card shadow-sm">
                                <div class="row g-0 align-items-center">
                                    <div class="col-md-4 text-center">
                                        <img src='<%# Eval("FilePath") %>' alt='<%# Eval("Name") %>' class="img-fluid rounded-circle p-1" />
                                    </div>
                                    <div class="col-md-8 p-2">
                                        <div class="mx-2">
                                            <h2 class="h5 text-primary my-2"><%# Eval("Name") %></h2>
                                            <p><strong>Qualification:</strong>&nbsp;<%# Eval("Qualification") %></p>
                                            <p><strong>Position:</strong>&nbsp;<%# Eval("Position") %></p>
                                            <p><i class="fa fa-phone"></i>&nbsp;<strong>Phone:</strong>&nbsp;<a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                                            <p><i class="fa fa-envelope"></i>&nbsp;<strong>Email:</strong>&nbsp;<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <h1 class="text-center text-primary rounded-top p-4">Our Former Faculty Members</h1>
                <asp:Repeater ID="FormerRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 mb-3">
                            <div class="card shadow-sm">
                                <div class="row g-0 align-items-center">
                                    <div class="col-md-4 text-center">
                                        <img src='<%# Eval("FilePath") %>' alt='<%# Eval("Name") %>' class="img-fluid rounded-circle p-1" />
                                    </div>
                                    <div class="col-md-8 p-2">
                                        <div class="mx-2">
                                            <h2 class="h5 text-primary my-2"><%# Eval("Name") %></h2>
                                            <p><strong>Qualification:</strong>&nbsp;<%# Eval("Qualification") %></p>
                                            <p><strong>Position:</strong>&nbsp;<%# Eval("Position") %></p>
                                            <p><i class="fa fa-phone"></i>&nbsp;<strong>Phone:</strong>&nbsp;<a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                                            <p><i class="fa fa-envelope"></i>&nbsp;<strong>Email:</strong>&nbsp;<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                                        </div>
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
