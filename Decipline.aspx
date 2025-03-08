<%@ Page Title="Discipline & Rules" MasterPageFile="Site.master" AutoEventWireup="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/pages.css">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container contain bg-white rounded shadow-sm p-4">
            <h1 class="text-center text-primary">Student Discipline Rules</h1>
            <p class="mb-4 text-justify">
                Students enrolled in the University must recognize their responsibilities towards the faculty, office staff, and fellow students. Failure to maintain appropriate standards of conduct will attract disciplinary action.
            </p>
            <p class="mb-4 text-justify">
                The University believes that duty, decorum, and discipline are the hallmarks of a good student. The following acts would be deemed to be acts of indiscipline:
            </p>
            <ul class="list-unstyled ps-3">
                <li class="list-item mb-3">Any overt or covert act leading to ragging and eve-teasing.</li>
                <li class="list-item mb-3">Disruption of classroom activities or disturbing the studies of other students.</li>
                <li class="list-item mb-3">Marring the operations of the University or its educational activities.</li>
                <li class="list-item mb-3">Harming the health or safety of staff or students.</li>
                <li class="list-item mb-3">Damage to property or possession of any intoxicants, illicit drugs, or weapons on campus.</li>
                <li class="list-item mb-3">Misconduct during examinations.</li>
                <li class="list-item mb-3">Production of false information or documents for admission purposes.</li>
                <li class="list-item mb-3">Failure to return loaned materials or settle debts with the University.</li>
            </ul>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">Prohibition of Ragging</h2>
            <p class="mb-4 text-justify">
                Ragging in any form is strictly prohibited. If any incident of ragging comes to notice, the authority would expel the student from the University.
            </p>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">Specific Rules for Student Discipline</h2>
            <ol class="ps-3">
                <li class="mb-3">Students are expected to use only courteous and polite language and behave with decorum.</li>
                <li class="mb-3">Insulting, inciting, threatening, or explicit language and violence are prohibited.</li>
                <li class="mb-3">Students shall not act in a way that brings disrepute to the University.</li>
                <li class="mb-3">Minimum 75% attendance is mandatory for appearing in examinations.</li>
            </ol>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">Attendance Rules</h2>
            <ol class="ps-3">
                <li class="mb-3">Students must attend all classes regularly and punctually.</li>
                <li class="mb-3">Late arrivals by more than five minutes will not be marked as present.</li>
                <li class="mb-3">Students must maintain over 90% attendance for scholarships.</li>
                <li class="mb-3">Absence without prior permission will be marked as unexcused.</li>
                <li class="mb-3">Consecutive absences for three days without leave will result in removal from rolls.</li>
            </ol>

            <h2 class="text-center text-secondary border-bottom pb-2 mb-4">Library Rules</h2>
            <ol class="ps-3">
                <li class="mb-3">The library is open to all students, faculty, and staff of the University.</li>
                <li class="mb-3">Personal belongings are not allowed inside the library.</li>
                <li class="mb-3">Damaged or lost books must be replaced or compensated four times their value.</li>
                <li class="mb-3">A fine of Rs. 10 per day will be charged for overdue books.</li>
                <li class="mb-3">Reference books are not to be issued and must remain in the library.</li>
            </ol>
        </div>
    </section>
</asp:Content>
