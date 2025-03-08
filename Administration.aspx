<%@ Page Title="Administrative Persons" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Administration.aspx.cs" Inherits="Administration" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css"/>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container content bg-white rounded shadow-sm p-4">            
            <div class="text-center text-primary rounded-top mb-2">
                <h1>Administration</h1>
            </div>
            <div class="mx-auto my-3">
                <p align="justify" class="lead">The University Department of Computer Applications (UDCA) at Tilka Manjhi Bhagalpur University (TMBU) is managed by a dedicated administration that ensures the efficient delivery of academic programs and services. The department is part of the larger TMBU administration, which oversees academic affairs, research, and student activities. For more detailed administrative information about UDCA, you can visit the official website of <a href="https://www.tmbuniv.ac.in/" target="_blank">TMBU</a>.</p>
            </div>            
            <div class="text-center text-primary rounded-top p-2 mb-2">
                <h1>Administrative Persons</h1>
            </div>
            <div class="mt-4">
                <div class="row row-cols-1 row-cols-lg-2">
                    <div class="col">
                        <div class="card shadow-sm mb-4">
                            <div class="row g-0 align-items-center">
                                <div class="col-md-4 text-center">
                                    <asp:Image ID="Image" CssClass="img-fluid rounded-circle p-1" runat="server" />
                                </div>
                                <div class="col-md-8 p-3">
                                    <h2 class="h5 text-primary mb-2">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </h2>
                                    <p><strong>Hon'ble</strong>&nbsp;Governor of Bihar</p>
                                    <p><strong>Position:</strong>&nbsp;<asp:Label ID="lblType" runat="server" Text=""></asp:Label></p>
                                    <p><strong>To know more:</strong>&nbsp;<a href="https://governor.bih.nic.in/" target="_blank">Click here</a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Repeater ID="AdministrativeRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card shadow-sm mb-4">
                                    <div class="row g-0 align-items-center">
                                        <div class="col-md-4 text-center">
                                            <img src='<%# Eval("FilePath") %>' alt='<%# Eval("Name") %>' class="img-fluid rounded-circle p-1" />
                                        </div>
                                        <div class="col-md-8 p-3">
                                            <h2 class="h5 text-primary mb-2"><%# Eval("Name") %></h2>
                                            <p><strong>Position:</strong>&nbsp;<%# Eval("Position") %></p>
                                            <p class="mb-1"><i class="fa fa-phone"></i>&nbsp;<strong>Phone:</strong>&nbsp;<a href='tel:<%# Eval("Phone") %>'><%# Eval("Phone") %></a></p>
                                            <p><i class="fa fa-envelope"></i>&nbsp;<strong>Email:</strong>&nbsp;<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
