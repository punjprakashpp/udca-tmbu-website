<%@ Page Title="About Developers" MasterPageFile="Site.master" AutoEventWireup="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media(min-width:1024px) {
            .w-md-25 {
                width: 25% !important;
            }
        }

        .card-text a {
            text-decoration: none; /* Remove underline */
            transition: all 0.3s ease; /* Smooth hover effect */
        }

            .card-text a:hover {
                color: #007bff; /* Bootstrap's primary color on hover */
                text-decoration: underline; /* underline on hover */
            }
    </style>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="Content" runat="Server">
    <section class="py-4 bg-light">
        <div class="container">
            <div class="text-center mb-5">
                <h1 class="text-uppercase text-primary fw-bold">Website Developing Team</h1>
            </div>
            <div class="row mb-4">
                <!-- Team Leader -->
                <div class=" col-12 col-md-6 col-lg-4">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/punj.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Punj Prakash" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Punj Prakash</h5>
                            <p class="card-text">
                                <strong>Session:</strong> 2022-2024<br>
                                <strong>Work:</strong> Full Stack Development<br>
                                <strong>Phone / WhatsApp:</strong> <a href="tel:+919060311534">+91 9060311534</a><br>
                                <strong>Email:</strong> <a href="mailto:punjprakashpp@gmail.com">punjprakashpp@gmail.com</a>
                            </p>
                            <div class="d-flex justify-content-center gap-3">
                                <a href="https://www.facebook.com/punjprakashop" target="_blank" class="text-primary"><i class="fab fa-facebook"></i></a>
                                <a href="https://www.instagram.com/punjprakashop" target="_blank" class="text-danger"><i class="fab fa-instagram"></i></a>
                                <a href="https://x.com/punjprakashpp" target="_blank" class="text-info"><i class="fab fa-twitter"></i></a>
                                <a href="https://github.com/punjprakashpp" target="_blank" class="text-dark"><i class="fab fa-github"></i></a>
                                <a href="https://www.linkedin.com/in/punjprakashpp" target="_blank" class="text-primary"><i class="fab fa-linkedin"></i></a>
                                <a href="https://punjprakashpp.github.io/portfolio/" target="_blank" class="text-decoration-none text-primary"><i class="fa fa-globe me-2"></i>Know more</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="d-none col-md-6 d-md-block col-lg-8">
                    <div class="card shadow-sm px-4 h-100">
                        <h2 class="text-primary my-4">About Punj Prakash</h2>
                        <p align="justify" class="lead"><b>Punj Prakash</b> is a skilled full-stack developer who completed his <b>BCA in 2022</b> from <b>T.N.B. College, Bhagalpur</b>, and pursued his <b>MCA</b> at the <b>University Department of Computer Applications (UDCA), T.M. Bhagalpur University (TMBU)</b>. An expert in <b>.NET technologies</b> like <b>ASP.NET</b>, he combines his front-end and back-end development skills to create robust applications. Notably, he successfully designed and developed the <b>official website for UDCA</b>.</p>
                        <a href="https://punjprakashpp.github.io/portfolio/" target="_blank" class="btn btn-primary my-2 w-50 w-md-25"><i class="fa fa-globe me-2"></i>Know more</a>
                    </div>
                </div>
            </div>
            <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
                <!-- Team Member 1 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/anmol.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Anmol Ratan" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Anmol Ratan</h5>
                            <p class="card-text">
                                <strong>Session:</strong> 2022-2024<br>
                                <strong>Work:</strong> Front-End and Back-End<br>
                                <strong>Phone:</strong> <a href="tel:+918434032071">+91 8434032071</a><br>
                                <strong>Email:</strong> <a href="mailto:yadavnmol@gmail.com">yadavnmol@gmail.com</a>
                            </p>
                            <div class="d-flex justify-content-center gap-3">
                                <a href="https://www.facebook.com/Yadavnmol" target="_blank" class="text-primary"><i class="fab fa-facebook"></i></a>
                                <a href="https://www.instagram.com/anmol_king" target="_blank" class="text-danger"><i class="fab fa-instagram"></i></a>
                                <a href="https://x.com/AnmolKing3" target="_blank" class="text-info"><i class="fab fa-twitter"></i></a>
                                <a href="https://github.com/AnmolKing03" target="_blank" class="text-dark"><i class="fab fa-github"></i></a>
                                <a href="https://www.linkedin.com/in/anmol-ratan-0a0281a9/" target="_blank" class="text-primary"><i class="fab fa-linkedin"></i></a>
                                <a href="https://anmolratan.netlify.app/" target="_blank" class="text-decoration-none text-primary"><i class="fa fa-globe me-2"></i>Know more</a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Team Member 2 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/anand.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Anand Kumar" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Anand Prakash</h5>
                            <p class="card-text">
                                <strong>Session:</strong> 2022-2024<br>
                                <strong>Work:</strong> Front-End and Back-End<br>
                                <strong>Phone:</strong> <a href="tel:+919801441325">+91 9801441325</a><br>
                                <strong>Email:</strong> <a href="mailto:anandprakash4568@gmail.com">anandprakash4568@gmail.com</a>
                            </p>
                            <div class="d-flex justify-content-center gap-3">
                                <a href="https://www.facebook.com/" target="_blank" class="text-primary"><i class="fab fa-facebook"></i></a>
                                <a href="https://www.instagram.com/" target="_blank" class="text-danger"><i class="fab fa-instagram"></i></a>
                                <a href="https://x.com/" target="_blank" class="text-info"><i class="fab fa-twitter"></i></a>
                                <a href="https://github.com/" target="_blank" class="text-dark"><i class="fab fa-github"></i></a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Team Member 3 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/anurag.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Anurag Kumar" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Anurag Bhardwaj</h5>
                            <p class="card-text">
                                <strong>Session:</strong> 2022-2024<br>
                                <strong>Work:</strong> Front-End and Back-End<br>
                                <strong>Phone:</strong> <a href="tel:+919110019419">+91 9110019419</a><br>
                                <strong>Email:</strong> <a href="mailto:anuragbhardwajab@gmail.com">anuragbhardwajab@gmail.com</a>
                            </p>
                            <div class="d-flex justify-content-center gap-3">
                                <a href="https://www.facebook.com/" target="_blank" class="text-primary"><i class="fab fa-facebook"></i></a>
                                <a href="https://www.instagram.com/" target="_blank" class="text-danger"><i class="fab fa-instagram"></i></a>
                                <a href="https://x.com/" target="_blank" class="text-info"><i class="fab fa-twitter"></i></a>
                                <a href="https://github.com/" target="_blank" class="text-dark"><i class="fab fa-github"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="text-center my-5">
                <h1 class="text-uppercase text-primary fw-bold">Website Management Team</h1>
            </div>

            <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
                <!-- Guide 1 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/sheo.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Dr. Sheo Shankar Prasad" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Dr. Sheo Shankar Prasad</h5>
                            <p class="card-text">
                                <strong>Faculty:</strong> UDCA, TMBU<br>
                                <strong>Phone:</strong> <a href="tel:+919162380697">+91 9162380697</a><br>
                                <strong>Email:</strong> <a href="mailto:sheobgp@gmail.com">sheobgp@gmail.com</a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Guide 2 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/rak.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Dr. Rakesh Kumar Pandey" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Dr. Rakesh Kumar Pandey</h5>
                            <p class="card-text">
                                <strong>Faculty:</strong> UDCA, TMBU<br>
                                <strong>Phone:</strong> <a href="tel:+919546040085">+91 9546040085</a><br>
                                <strong>Email:</strong> <a href="mailto:rakeshkrpandey1978@gmail.com">rakeshkrpandey1978@gmail.com</a>
                            </p>
                        </div>
                    </div>
                </div>

                <!-- Guide 3 -->
                <div class="col">
                    <div class="card shadow-sm text-center h-100">
                        <img src="Image/dev/kamesh.jpg" class="card-img-top rounded-circle mx-auto mt-3" alt="Kamesh Kumar Gupta" style="width: 130px; height: 130px; object-fit: cover;">
                        <div class="card-body">
                            <h5 class="card-title">Kamesh Kumar Gupta</h5>
                            <p class="card-text">
                                <strong>Lab-In-Charge:</strong> UDCA, TMBU<br>
                                <strong>Phone:</strong> <a href="tel:+919431403011">+91 9431403011</a><br>
                                <strong>Email:</strong> <a href="mailto:kameshsony@gmail.com">kameshsony@gmail.com</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
