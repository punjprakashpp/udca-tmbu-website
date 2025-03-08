<%@ Page Title="Syllabus for MCA Entrance Test" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div style="max-width:800px" class="container bg-white rounded shadow-sm p-4">
            <!-- Page Title -->
            <h1 class="text-primary text-center mb-4">Syllabus for MCA Entrance Test</h1>

            <!-- Introduction -->
            <p class="fst-italic text-muted">
                The selection process for admission to the MCA course is conducted in three phases:
            </p>

            <!-- Phase I: Written Test -->
            <h2 class="text-secondary mb-3">PHASE I: Written Test</h2>
            <p>The written test evaluates candidates on four key subjects. The distribution of marks and topics is as follows:</p>
            <div class="table-responsive">
                <table class="table table-bordered text-center">
                    <thead class="table-dark">
                        <tr>
                            <th>Sl. No.</th>
                            <th>Subjects / Test</th>
                            <th>Marks</th>
                            <th>Weightage (%)<span class="fst-italic">*</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="bg-light fw-bold">
                            <td colspan="4">Part A: Core Subjects</td>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>Mathematics<br><small><b>Level:</b> 10+2</small></td>
                            <td>36 marks</td>
                            <td>36%</td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>Reasoning<br><small><b>Level:</b> High</small></td>
                            <td>18 marks</td>
                            <td>18%</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>Computer Fundamentals</td>
                            <td>18 marks</td>
                            <td>18%</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>English</td>
                            <td>18 marks</td>
                            <td>18%</td>
                        </tr>
                        <tr class="bg-light fw-bold">
                            <td colspan="4">Part B: Group Discussion</td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>Group Discussion</td>
                            <td>10 marks</td>
                            <td>10%</td>
                        </tr>
                        <tr class="table-info fw-bold">
                            <td></td>
                            <td>Total:</td>
                            <td>100 marks</td>
                            <td>100%</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Examination Duration:</td>
                            <td colspan="2">2 hours (Composite Time)</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <p class="fst-italic text-muted">*The percentage represents the weightage of each subject or test section out of the total examination questions.</p>

            <!-- Phase II: Group Discussion -->
            <h2 class="text-secondary mt-5 mb-3">PHASE II: Group Discussion</h2>
            <p>In the Group Discussion (GD) phase, candidates are assessed based on their:</p>
            <ul>
                <li><b>Communication skills:</b> Ability to articulate ideas clearly and effectively.</li>
                <li><b>Analytical ability:</b> Critical thinking and problem-solving in a group setting.</li>
                <li><b>Leadership qualities:</b> Teamwork, initiative, and the ability to lead discussions.</li>
            </ul>

            <!-- Phase III: Interview -->
            <h2 class="text-secondary mt-5 mb-3">PHASE III: Interview</h2>
            <p>
                The interview will assess the overall suitability of the candidates for the MCA program. Key areas evaluated include:
            </p>
            <ul>
                <li><b>Academic background:</b> Understanding the candidate's qualifications and preparedness for the MCA course.</li>
                <li><b>Personal achievements:</b> Insights into extracurricular activities, achievements, and personal qualities.</li>
                <li><b>Career goals:</b> Alignment of the candidate's aspirations with the objectives of the MCA program.</li>
            </ul>
        </div>
    </section>
</asp:Content>
