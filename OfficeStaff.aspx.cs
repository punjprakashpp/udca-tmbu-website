using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class pages_OfficeStaff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFacultyDetails();
            LoadSupportDetails();
        }
    }

    private void LoadFacultyDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Qualification, Position, Phone, Email, FilePath FROM Member WHERE Type = 'Office'";
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
    private void LoadSupportDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Qualification, Position, Phone, Email, FilePath FROM Member WHERE Type = 'Support'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    SupportRepeater.DataSource = dt;
                    SupportRepeater.DataBind();
                }
            }
        }
    }
}
