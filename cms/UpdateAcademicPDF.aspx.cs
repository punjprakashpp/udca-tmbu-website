using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateAcademicPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = Page.User.Identity as FormsIdentity;
            if (identity != null)
            {
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userRole = ticket.UserData;

                if (string.IsNullOrEmpty(userRole))
                {
                    // Redirect to Login if user role is missing
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Allow access only if the user is an Admin
                if (userRole == "Admin")
                {
                    // User is an Admin; proceed with page logic
                }
                else
                {
                    // Show unauthorized access notification and redirect
                    NotificationHelper.ShowNotification(this, "You are not authorized to access this page!", "warning", "warning");
                    Response.Redirect("~/cms/", false); // Redirect to an Dashboard page
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                // Redirect to Login if identity is null
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        else
        {
            // Redirect to Login if the user is not authenticated
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }

    protected void btnCalender_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadCalender, "AcademicCalender.pdf");
    }

    protected void btnProspectus_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadProspectus, "Prospectus.pdf");
    }

    protected void btnTimeTable_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadTimeTable, "TimeTable.pdf");
    }

    protected void btnHolidayList_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadHolidayList, "HolidayList.pdf");
    }

    protected void btnFeeStructure_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadFeeStructure, "FeeStructure.pdf");
    }

    protected void btnCourseStructure_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadCourseStructure, "CourseStructure.pdf");
    }

    protected void btnCourseSyllabus_Click(object sender, EventArgs e)
    {
        UploadPDF(fileUploadCourseSyllabus, "CourseSyllabus.pdf");
    }

    private void UploadPDF(FileUpload fileUpload, string fileName)
    {
        if (fileUpload.HasFile)
        {
            if (fileUpload.PostedFile.ContentType == "application/pdf")
            {
                try
                {
                    string savePath = Server.MapPath("~/Uploads/docs/") + fileName;
                    fileUpload.SaveAs(savePath);
                    lblMessage.Text = "File uploaded successfully: " + fileName;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "File could not be uploaded. Error: " + ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Only PDF files are allowed.";
            }
        }
        else
        {
            lblMessage.Text = "Please select a file to upload.";
        }
    }
}
