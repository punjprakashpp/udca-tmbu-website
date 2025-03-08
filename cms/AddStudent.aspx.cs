using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class AddStudent : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            PopulateSessionDropdown();
        }
    }

    private void PopulateSessionDropdown()
    {
        ddlSession.Items.Add(new ListItem("--- Select Session ---", string.Empty));
        int currentYear = DateTime.Now.Year;

        for (int year = 2002; year <= currentYear; year++)
        {
            // Determine session length based on the year
            int sessionLength = year <= 2019 ? 3 : 2;
            string sessionDisplay = string.Format("{0} - {1}", year, year + sessionLength);

            // Add session with display text and value as start year
            ddlSession.Items.Add(new ListItem(sessionDisplay, sessionDisplay));
        }
    }

    protected void btnAddStudent_Click(object sender, EventArgs e)
    {
        // Validate all controls before processing
        if (Page.IsValid)
        {
            string session = ddlSession.SelectedValue;
            string rollNo = txtRollNo.Text.Trim();
            string regNo = txtRegNo.Text.Trim();
            string regYear = txtRegYear.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string midName = txtMidName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string gender = rdoMale.Checked ? "Male" : "Female";
            string alumnus = "no";
            string achiever = "no";
            DateTime dob;

            // Specify the expected date format
            string dateFormat = "dd-MM-yyyy";

            // Try parsing the input date using the specified format and culture
            if (!DateTime.TryParseExact(txtDOB.Text, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dob))
            {
                lblmsg.Text = "Invalid Date of Birth format. Please use 'dd-MM-yyyy'.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }


            // Define the connection string and insert command
            string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            string query = "INSERT INTO Student (Session, RollNo, RegNo, RegYear, FirstName, MidName, LastName, Gender, DOB, Alumnus, Achiever, EntryDate) " +
                           "VALUES (@Session, @RollNo, @RegNo, @RegYear, @FirstName, @MidName, @LastName, @Gender, @DOB, @Alumnus, @Achiever, @EntryDate)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Define parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@Session", session);
                    cmd.Parameters.AddWithValue("@RollNo", rollNo);
                    cmd.Parameters.AddWithValue("@RegNo", regNo);
                    cmd.Parameters.AddWithValue("@RegYear", regYear);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@MidName", string.IsNullOrEmpty(midName) ? (object)DBNull.Value : midName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@Alumnus", alumnus);
                    cmd.Parameters.AddWithValue("@Achiever", achiever);
                    cmd.Parameters.AddWithValue("@EntryDate", DateTime.Now);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Student added successfully!";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        lblmsg.Text = "Error: " + ex.Message;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }

    private void ClearForm()
    {
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtRegYear.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtMidName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        rdoMale.Checked = false;
        rdoFemale.Checked = false;
        txtDOB.Text = string.Empty;
    }
}
