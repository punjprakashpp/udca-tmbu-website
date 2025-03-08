using System;
using System.Web.Security;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;

public partial class UploadStudentData : System.Web.UI.Page
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            try
            {
                // Save uploaded file
                string filePath = Server.MapPath("~/Uploads/docs/") + Path.GetFileName(fileUpload.PostedFile.FileName);
                fileUpload.SaveAs(filePath);

                // Define DataTable columns matching the database table
                DataTable dtExcelData = new DataTable();
                dtExcelData.Columns.AddRange(new DataColumn[12] {
                    new DataColumn("Session", typeof(string)),
                    new DataColumn("RollNo", typeof(string)),    
                    new DataColumn("RegNo", typeof(string)),
                    new DataColumn("RegYear", typeof(string)),
                    new DataColumn("FirstName", typeof(string)),
                    new DataColumn("MidName", typeof(string)),
                    new DataColumn("LastName", typeof(string)),
                    new DataColumn("Gender", typeof(string)),
                    new DataColumn("DOB", typeof(DateTime)),
                    new DataColumn("Alumnus", typeof(string)),
                    new DataColumn("Achiever", typeof(string)),
                    new DataColumn("EntryDate", typeof(DateTime))
                });

                // Read data from Excel
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1); // Assuming first sheet
                    bool firstRow = true;

                    foreach (var row in worksheet.Rows())
                    {
                        if (firstRow)
                        {
                            firstRow = false;
                            continue; // Skip header row
                        }

                        // Read data from Excel row
                        string session = row.Cell(1).Value.ToString();
                        string rollNo = row.Cell(2).Value.ToString();
                        string regNo = row.Cell(3).Value.ToString();
                        string regYear = row.Cell(4).Value.ToString();
                        string firstName = row.Cell(5).Value.ToString();
                        string midName = row.Cell(6).Value.ToString();
                        string lastName = row.Cell(7).Value.ToString();
                        string gender = row.Cell(8).Value.ToString();
                        string dobString = row.Cell(9).Value.ToString();
                        string alumnus = "no";
                        string achiever = "no";
                        string entrydate = DateTime.Now.ToString();

                        DateTime dob;
                        // Parse DOB and EntryDate with default values
                        if (!DateTime.TryParse(dobString, out dob))
                        {
                            dob = DateTime.MinValue; // Default if invalid date
                        }

                        // Add the row to DataTable
                        dtExcelData.Rows.Add(session, rollNo, regNo, regYear, firstName, midName, lastName, gender, dob, alumnus, achiever, entrydate);
                    }
                }

                // SQL connection and bulk copy operation
                string consString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.Student";

                        // Column mappings to ensure DataTable columns match the database columns
                        sqlBulkCopy.ColumnMappings.Add("Session", "Session");
                        sqlBulkCopy.ColumnMappings.Add("RollNo", "RollNo");      
                        sqlBulkCopy.ColumnMappings.Add("RegNo", "RegNo");
                        sqlBulkCopy.ColumnMappings.Add("RegYear", "RegYear");
                        sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                        sqlBulkCopy.ColumnMappings.Add("MidName", "MidName");
                        sqlBulkCopy.ColumnMappings.Add("LastName", "LastName");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("DOB", "DOB");
                        sqlBulkCopy.ColumnMappings.Add("Alumnus", "Alumnus");
                        sqlBulkCopy.ColumnMappings.Add("Achiever", "Achiever");
                        sqlBulkCopy.ColumnMappings.Add("EntryDate", "EntryDate");

                        // Execute bulk copy
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        lblmsg.Text = "Student Data Added Successfully!";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Error: " + ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblmsg.Text = "Please upload a file!";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }
}
