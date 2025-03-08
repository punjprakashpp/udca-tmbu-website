<%@ Page Title="Chancellor" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Chancellor.aspx.cs" Inherits="pages_Chancellor" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-5 bg-light">
        <div class="container bg-white rounded shadow-sm py-5 px-4">
            <!-- Header Section -->
            <h1 class="text-center text-primary mb-5">Chancellor of Tilka Manjhi Bhagalpur University</h1>

            <div class="row align-items-center">
                <!-- Chancellor Image and Information -->
                <div class="col-md-4 text-center">
                    <asp:Image ID="imgPerson" runat="server" CssClass="img-fluid rounded-circle shadow mb-3" AlternateText="Chancellor's Image" />
                    <h2 class="text-primary mt-2">
                        <asp:Label ID="lblName" runat="server" />
                    </h2>
                    <p class="text-muted">
                        <b>Position:</b> Governor of Bihar
                    </p>
                </div>

                <!-- Chancellor's Description -->
                <div class="col-md-8 mt-3">
                    <h2>About the Chancellor</h2>
                    <p align="justify" class="lead">
                        The Governor of Bihar is the Constitutional Head of the State of Bihar and exercises powers as defined in the Constitution of India. The Governor is also the ex-officio Chancellor of universities in the State.
                    </p>
                    <p align="justify" class="lead">
                        The Governor’s Secretariat provides secretarial assistance to the Governor in discharging his constitutional, ceremonial, and other State responsibilities, along with his duties as the Chancellor of universities in the State.
                    </p>
                    <a href="https://governor.bih.nic.in/" target="_blank" class="btn btn-primary mt-3">
                        <i class="fas fa-info-circle"></i> Learn More
                    </a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
