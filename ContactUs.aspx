<%@ Page Title="Contacts" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <style>
        p a {
            text-decoration: none; /* Remove underline */
            transition: all 0.3s ease; /* Smooth hover effect */
        }

            p a:hover {
                color: #007bff; /* Bootstrap's primary color on hover */
                text-decoration: underline; /* underline on hover */
            }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:1024px" class="container bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary mb-4">Contact Us</h1>

            <div class="row">
                <!-- Left Column -->
                <div class="col-md-5 rounded shadow-sm text-center pb-4 h-100">
                    <h2>Mailing Address:</h2>
                    <p>
                        <a href="https://maps.app.goo.gl/Zv7E8aFZ3hYQ1ZMf6" target="_blank">University Department of Computer Applications,<br />
                            Computer Centre,<br />
                            Tilka Manjhi Bhagalpur University,<br />
                            Bhagalpur, Bihar, 812007
                        </a>
                    </p>

                    <h2>Phone No.:</h2>
                    <p><a href="tel:0641-2500040">0641-2500040</a></p>

                    <h2>E-Mail Id.:</h2>
                    <p><a href="mailto:udcatmbu.bgp@gmail.com">udcatmbu.bgp@gmail.com</a></p>

                    <h2>Website:</h2>
                    <p><a href="http://udcatmbu.org">http://udcatmbu.org</a></p>

                    <h2>Social Links:</h2>
                    <div>
                        <a href="https://www.facebook.com/udcatmbu" target="_blank" class="me-3"><i class="fab fa-facebook"></i></a>
                        <a href="https://www.instagram.com/udcatmbu" target="_blank" class="me-3"><i class="fab fa-instagram"></i></a>
                        <a href="https://x.com/udcatmbu" target="_blank" class="me-3"><i class="fab fa-twitter"></i></a>
                        <a href="https://www.linkedin.com/in/udcatmbu" target="_blank" class="me-3"><i class="fab fa-linkedin"></i></a>
                    </div>
                </div>

                <!-- Right Column (Map) -->
                <div class="col-md-7 mb-4">
                    <h2 class="mb-2">Location of UDCA on Google Maps:</h2>
                    <iframe class="rounded shadow-sm p-4 w-100 h-100" src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d451.0993222352922!2d86.955931!3d25.243843!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x39f04a743aaaaaab%3A0xde5c7df4de4859e1!2sUniversity%20Department%20Of%20Computer%20Application!5e0!3m2!1sen!2sin!4v1716199756791!5m2!1sen!2sin" allowfullscreen="true" loading="fast" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
