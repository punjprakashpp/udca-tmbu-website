<%@ Page Title="University Hostel" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container contain bg-white rounded shadow-sm p-4">
            <!-- Title Section -->
            <div class="text-center text-primary p-4 rounded">
                <h1>University Hostel</h1>
            </div>

            <!-- Hostel Image Section -->
            <div class="text-center mt-4 mb-4">
                <img class="img-fluid rounded shadow-sm" src="Image/dept/Boys Hostel.jpg" alt="Boys Hostel">
            </div>

            <!-- Hostel Description Section -->
            <p>
                T.M. Bhagalpur University offers segregated hostel accommodations for both male and female students. Availability of seats is limited, with allocations made based on university regulations. The hostel charges amount to &#8377;3800 annually for a double bedroom. These accommodations serve as a conducive living environment for students, fostering a sense of community and providing essential amenities. With a focus on student welfare, the university ensures fair allocation procedures and maintains reasonable pricing, facilitating an enriching academic experience for residents.
            </p>

            <!-- Note Section -->
            <div class="mb-4">
                <h3 class="text-secondary">Note:</h3>
                <ol>
                    <li>The allocation of hostel seats is done by the University upon submission of a prescribed application form available at the University Central Office.</li>
                    <li>Mess facilities are available in the hostels for a subsidized charge.</li>
                </ol>
            </div>

            <!-- Hostel Facilities & Policies Section -->
            <div>
                <h3 class="text-secondary">Hostel Facilities & Policies</h3>
                <hr>
                <ul class="list-unstyled">
                    <li class="list-item"><i class="fas fa-smoking-ban"></i>Non-smoking environment</li>
                    <li class="list-item"><i class="fas fa-users"></i>Common room</li>
                    <li class="list-item"><i class="fas fa-praying-hands"></i>Prayer room</li>
                    <li class="list-item"><i class="fas fa-dumbbell"></i>Workout room</li>
                    <li class="list-item"><i class="fas fa-wifi"></i>Free Wi-Fi</li>
                    <li class="list-item"><i class="fas fa-bicycle"></i>Bicycle & bike parking</li>
                    <li class="list-item"><i class="fas fa-luggage-cart"></i>Luggage storage</li>
                    <li class="list-item"><i class="fas fa-water"></i>Pure (RO) and refrigerated water</li>
                    <li class="list-item"><i class="fas fa-chess"></i>Indoor games (e.g., chess, table tennis)</li>
                    <li class="list-item"><i class="fas fa-futbol"></i>Outdoor games (e.g., cricket, volleyball, football, badminton)</li>
                </ul>
            </div>
        </div>
    </section>
</asp:Content>
