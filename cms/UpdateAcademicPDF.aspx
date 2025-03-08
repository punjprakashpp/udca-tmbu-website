<%@ Page Title="Update Academic PDF" Language="C#" MasterPageFile="cms.master" AutoEventWireup="true" CodeFile="UpdateAcademicPDF.aspx.cs" Inherits="UpdateAcademicPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="container bg-white rounded shadow-sm p-5">
        <h2 class="text-center text-primary mb-4">Update Academic Related PDF</h2>
        <div class="table-responsive">
            <table class="table table-bordered">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMessage" runat="server" Text="" class="text-center d-block"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProspectus" runat="server" Text="Upload Prospectus PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadProspectus" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnProspectus" runat="server" Text="Update" OnClick="btnProspectus_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTimeTable" runat="server" Text="Upload Time Table PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadTimeTable" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnTimeTable" runat="server" Text="Update" OnClick="btnTimeTable_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblHolidayList" runat="server" Text="Upload Holiday List PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadHolidayList" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnHolidayList" runat="server" Text="Update" OnClick="btnHolidayList_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFeeStructure" runat="server" Text="Upload Fee Structure PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadFeeStructure" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnFeeStructure" runat="server" Text="Update" OnClick="btnFeeStructure_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCourseStructure" runat="server" Text="Upload Course Structure PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadCourseStructure" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnCourseStructure" runat="server" Text="Update" OnClick="btnCourseStructure_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCourseSyllabus" runat="server" Text="Upload Course Syllabus PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadCourseSyllabus" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnCourseSyllabus" runat="server" Text="Update" OnClick="btnCourseSyllabus_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCalender" runat="server" Text="Upload Academic Calendar PDF:" class="form-label"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="fileUploadCalender" runat="server" class="form-control" />
                    </td>
                    <td>
                        <asp:Button ID="btnCalender" runat="server" Text="Update" OnClick="btnCalender_Click" class="btn btn-success w-100" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
