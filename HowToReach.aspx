<%@ Page Title="How to Reach Here" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media(max-width:1024px) {
            iframe {
                width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:1024px" class="container bg-white rounded shadow-sm p-4">

            <!-- Travel Directions -->
            <div class="mb-4">
                <h1 class="text-center text-primary mb-4">How to reach UDCA?</h1>

                <!-- From Bhagalpur Railway Station -->
                <h2 class="text-secondary">1) From Bhagalpur Railway Station (Junction):</h2>
                <h3 class="text-muted">ROUTE 1:</h3>
                <img src="Image/reach/route 1.jpg" alt="Route 1" class="img-fluid mb-3 d-block mx-auto">
                <p align="justify" class="lead">
                    Bhagalpur Railway Station is the nearest railway station to the University Department of Computer Applications (UDCA). The UDCA is 2.2 km from Bhagalpur Railway Station (junction) and can be reached by auto rickshaw, easy cabs, or cycle rickshaw. Get down at Hindustan Press and travel another 550 meters by foot to reach the UDCA. Landmarks between Hindustan Press and UDCA include the M.Com Department, MBA Department, Rural Economics Department, Dinkar Bhawan, and Multi-Purpose Hall (just 50 meters back). The destination will be on your right.
                </p>

                <h3 class="text-muted">ROUTE 2:</h3>
                <img src="Image/reach/route 2.jpg" alt="Route 2" class="img-fluid mb-3 d-block mx-auto">
                <p align="justify" class="lead">
                    The University Department of Computer Applications (UDCA) is 2.2 km from Bhagalpur Railway Station (junction) and can be reached by auto rickshaw, easy cabs, or cycle rickshaw. Get down at Hindustan Press and travel another 550 meters by foot to reach the UDCA. Landmarks between Hindustan Press and UDCA include the M.Com Department, MBA Department, Rural Economics Department, Dinkar Bhawan, and Multi-Purpose Hall (about 50 meters back). The destination will be on your right.
                </p>

                <!-- From Bhagalpur Bus Depot -->
                <h2 class="text-secondary">2) From Bhagalpur Bus Depot (Stand):</h2>
                <img src="Image/reach/bus stop.jpg" alt="Bus Stop" class="img-fluid mb-3 d-block mx-auto">
                <p align="justify" class="lead">
                    The University Department of Computer Applications is 5.3 km from the B.S.R.T.C. Bhagalpur Bus Depot and can be reached by auto rickshaw, easy cabs, or cycle rickshaw. Get down at SARAI Chowk and change to another auto rickshaw going to Champanagar. Get down at UNIVERSITY OP (Police Station) and travel another 400 meters by foot to reach the UDCA. Landmarks between TilkaManjhi Chowk and UDCA include JLN College and Hospital, Manali Restaurant Chowk, Adampur Chowk, Deep Prabha Cinema Hall, Lajpath Children Park, Shankar Talkies Chowk, Budhanath Chowk, Nayabazar, Golaghat, SARAI Chowk, TM Bhagalpur University, UNIVERSITY OP (Police Station), Marwari College, Marwari College Hostel, and Dinkar Bhawan (about 50 meters back). The destination will be on your left.
                </p>
            </div>
            
            <!-- Location Map -->
            <div class="text-center">
                <h1 class="title text-primary">Location Map of UDCA</h1>
                <h3>We are at:</h3>
                <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d451.0993222352922!2d86.955931!3d25.243843!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x39f04a743aaaaaab%3A0xde5c7df4de4859e1!2sUniversity%20Department%20Of%20Computer%20Application!5e0!3m2!1sen!2sin!4v1716199756791!5m2!1sen!2sin" width="1000" height="450" style="border: 0;" allowfullscreen="" loading="fast" referrerpolicy="no-referrer-when-downgrade"></iframe>
            </div>
        </div>
    </section>
</asp:Content>
