using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Administration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadChancellor();
            LoadAdministrativeDetails();
        }
    }

    private void LoadChancellor()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Name, Position, FilePath FROM Member WHERE Position = 'Chancellor'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblName.Text = reader["Name"].ToString();
                        Image.ImageUrl = reader["FilePath"].ToString();
                        lblType.Text = reader["Position"].ToString();
                    }
                }
            }
        }
    }

    private void LoadAdministrativeDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Align, Position, Name, Type, Phone, Email, FilePath FROM Member WHERE Type = 'Admin' AND Position <> 'Chancellor' ORDER BY Align";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    AdministrativeRepeater.DataSource = dt;
                    AdministrativeRepeater.DataBind();
                }
            }
        }
    }
}
