using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FacultyMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFacultyDetails();
            LoadGuestDetails();
            LoadFormerDetails();
        }
    }

    private void LoadFacultyDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Qualification, Position, Phone, Email, FilePath FROM Member WHERE Type = 'Faculty' AND Status ='Current'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    facultyRepeater.DataSource = dt;
                    facultyRepeater.DataBind();
                }
            }
        }
    }

    private void LoadGuestDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Qualification, Position, Phone, Email, FilePath FROM Member WHERE Type = 'Guest' AND Status ='Current'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    GuestRepeater.DataSource = dt;
                    GuestRepeater.DataBind();
                }
            }
        }
    }

    private void LoadFormerDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Qualification, Position, Phone, Email, FilePath FROM Member WHERE Status = 'Former'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    FormerRepeater.DataSource = dt;
                    FormerRepeater.DataBind();
                }
            }
        }
    }
}
