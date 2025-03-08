<%@ Page Title="Alumni Registration" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .instruction-box {
            background: #f8f9fa;
            border-left: 5px solid #007bff;
            padding: 20px;
            border-radius: 8px;
        }

        .step-icon {
            font-size: 1.5rem;
            color: #007bff;
        }

        .card {
            border-radius: 10px;
        }

        .btn-custom {
            min-width: 150px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <section class="py-4 bg-light">
        <div class="container bg-white rounded shadow-sm p-4" style="max-width: 800px;">
            <h2 class="text-center text-primary mb-4">
                <i class="fa-solid fa-user-graduate"></i>Alumni Registration Instructions
            </h2>

            <div class="instruction-box mb-3">
                <h5 class="text-dark"><i class="fa-solid fa-info-circle step-icon"></i>How to Register as an Alumni?</h5>
                <p>Follow the steps below to verify your details and gain access to the Alumni Portal.</p>
            </div>

            <!-- Step 1 -->
            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title text-primary"><i class="fa-solid fa-check-circle step-icon"></i>Step 1: Verify your Academic Details</h5>
                    <p class="card-text">Ensure that your details such as **Roll Number, Registration Number, and Date of Birth** match the university records.</p>
                </div>
            </div>

            <div class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title text-primary"><i class="fa-solid fa-user-edit step-icon"></i>Step 2: Update Your Details</h5>
                    <p class="card-text">Log in to the **Alumni Portal** and update your details, including occupation, achievements, and social links.</p>
                </div>
            </div>

            <!-- Buttons -->
            <div class="text-center mb-3">
                <a href="/" class="btn btn-primary btn-custom">
                    <i class="fa-solid fa-arrow-left"></i>Back to Home Page
                </a>
                <a href="Login.aspx" class="btn btn-success btn-custom ms-2">
                    <i class="fa-solid fa-right-to-bracket"></i>Go to Verification
                </a>
            </div>

            <!-- Final Note -->
            <div class="alert alert-info text-center">
                <i class="fa-solid fa-envelope"></i>Need help? Contact the **UDCA TMBU Website Management Team**
            </div>
        </div>
    </section>
</asp:Content>
