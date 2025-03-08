<%@ Page Title="Health Centre" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Styles/pages.css">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container contain bg-white rounded shadow-sm p-4">
            <!-- Title Section -->
            <div class="text-center text-primary p-4 rounded">
                <h1>Health Centre</h1>
            </div>

            <!-- Health Center Image Section -->
            <div class="text-center mt-4 mb-4">
                <img class="img-fluid rounded shadow-sm" src="Image/dept/Health Centre.jpg" alt="Health Center Image">
            </div>

            <!-- Health Center Information Table -->
            <table class="table table-bordered mb-4">
                <thead>
                    <tr>
                        <th colspan="2" class="text-center">Health Centre Information</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Doctor</td>
                        <td>---</td>
                    </tr>
                    <tr>
                        <td>Working Hours</td>
                        <td>10:30 AM - 5:00 PM</td>
                    </tr>
                    <tr>
                        <td>Location</td>
                        <td>Opposite of University Department of Computer Applications</td>
                    </tr>
                </tbody>
            </table>

            <!-- Health Center Description -->
            <p>The University Health Center at Tilka Manjhi Bhagalpur University is primarily funded by the institution itself and caters exclusively to currently enrolled students. We do not issue excuses for missed classes due to illness or injury.</p>
            <p>However, students facing serious illness or significant disabilities can request medical documentation to be placed in their records, accessible to Academic Advising. Withdrawal from classes or the university itself must be requested following university guidelines.</p>
            <p>Encouraging self-management of minor illnesses and injuries is a key aspect of our approach. Many common ailments can be managed without medical intervention, and learning to handle these situations independently fosters valuable coping skills for both academic and professional life.</p>
            <p>It's important to recognize that each individual's experience with illness varies, and our staff cannot predict recovery times or assess the impact of illness on academic performance. Conversations between students and faculty are crucial for finding ways to navigate academic challenges during illness.</p>
            <p>While we provide health tips and resources for a healthier lifestyle, it's essential to understand that the information offered here is general. It cannot replace personalized healthcare or human services. For medical diagnosis or treatment, it's always best to consult with a healthcare professional at the Health Center.</p>

            <!-- Contact Information Section -->
            <p>
                For Information / Emergency services, please contact the Health Centre Reception Desk at 
            <a href="tel:+919934873798" class="text-danger">telephone No. 9934873798</a> or 
            <a href="tel:+918292754529" class="text-danger">telephone No. 8292754529</a>.
            </p>
        </div>
    </section>
</asp:Content>
