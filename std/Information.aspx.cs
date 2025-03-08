using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class std_Information : System.Web.UI.Page
{
    private string StudentID = string.Empty;
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

                if (string.IsNullOrEmpty(userRole) || userRole == "Admin" || userRole == "User")
                {
                    // Redirect to Login if user role is missing
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    StudentID = userRole;
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
            LoadStudent();
        }
    }

    private void LoadStudent()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT StudentID, Qualification, Occupation, Company, Achievement, Alumnus, Achiever FROM Student WHERE StudentID = @StudentID";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", StudentID);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        hfPersonID.Value = rdr["StudentID"].ToString();
                        txtQualification.Text = rdr["Qualification"].ToString();
                        txtOccupation.Text = rdr["Occupation"].ToString();
                        txtCompany.Text = rdr["Company"].ToString();
                        txtAchievement.Text = rdr["Achievement"].ToString();
                        rblAlumnus.SelectedValue = rdr["Alumnus"].ToString();
                        rblAchiever.SelectedValue = rdr["Achiever"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error loading Details: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error loading Details: " + ex.Message;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtQualification.Enabled = true;
        txtOccupation.Enabled = true;
        txtCompany.Enabled = true;
        txtAchievement.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        rblAlumnus.Enabled = true;
        rblAchiever.Enabled = true;
        btnEdit.Enabled = false;
        txtQualification.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Student SET Qualification = @Qualification, Occupation = @Occupation, Company = @Company, Achievement = @Achievement, Achiever = @Achiever, Alumnus = @Alumnus WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@Qualification", SqlDbType.NVarChar).Value = txtQualification.Text.Trim();
                cmd.Parameters.Add("@Occupation", SqlDbType.NVarChar).Value = txtOccupation.Text.Trim();
                cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = txtCompany.Text.Trim();
                cmd.Parameters.Add("@Achievement", SqlDbType.NVarChar).Value = txtAchievement.Text.Trim();
                cmd.Parameters.Add("@Alumnus", SqlDbType.NVarChar).Value = rblAlumnus.SelectedValue.ToString();
                cmd.Parameters.Add("@Achiever", SqlDbType.NVarChar).Value = ((txtAchievement.Text.Trim()==string.Empty)|| rblAchiever.SelectedValue=="no")?"no":"yes";

                if (!string.IsNullOrEmpty(hfPersonID.Value))
                {
                    cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = int.Parse(hfPersonID.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                ClearForm();
                NotificationHelper.ShowNotification(this, "Details saved successfully!", "success", "Success");
            }
        }
    }

    private void ClearForm()
    {
        txtQualification.Enabled = false;
        txtOccupation.Enabled = false;
        txtCompany.Enabled = false;
        txtAchievement.Enabled = false;
        rblAlumnus.Enabled = false;
        rblAchiever.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        btnEdit.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
}