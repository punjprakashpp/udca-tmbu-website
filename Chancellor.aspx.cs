using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class pages_Chancellor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPerson();
        }
    }

    private void LoadPerson()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT * FROM Member WHERE Position = 'Chancellor'";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string name = rdr["Name"].ToString();
                    string FilePath = rdr["FilePath"].ToString();

                    lblName.Text = name;
                    imgPerson.ImageUrl = "~/" + FilePath;
                }
                else
                {
                    lblName.Text = "No person found.";
                    imgPerson.ImageUrl = "";
                }
                con.Close();
            }
        }
    }
}