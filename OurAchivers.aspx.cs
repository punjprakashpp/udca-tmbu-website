using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class OurAchivers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAchiversDetails();
        }
    }

    private void LoadAchiversDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Session, FirstName, MidName, LastName, Achievement, Qualification, Occupation, Company, Phone, Email, FilePath FROM Student WHERE Achiever = 'yes' ORDER BY Session";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(reader);

                if (dt.Rows.Count == 0)
                {
                    lblMessage.Text = "No records found.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = ""; // Hide the message if records are found
                }

                AchiverRepeater.DataSource = dt;
                AchiverRepeater.DataBind();
            }
        }
    }
}