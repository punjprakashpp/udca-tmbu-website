using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class ViceChancellor : System.Web.UI.Page
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
        string query = "SELECT Name, Phone, Email, FilePath FROM Member WHERE Position = 'Vice Chancellor'";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string name = rdr["Name"].ToString();
                    string phone = rdr["Phone"].ToString();
                    string email = rdr["Email"].ToString();
                    string FilePath = rdr["FilePath"].ToString();

                    lblName.Text = name;
                    lblPhone.Text = phone;
                    lblEmail.Text = email;
                    phoneLink.NavigateUrl = "mailto:" + phone;
                    emailLink.NavigateUrl = "mailto:" + email;
                    imgPerson.ImageUrl = "~/" + FilePath;
                }
                else
                {
                    lblName.Text = "No person found.";
                    lblPhone.Text = "";
                    lblEmail.Text = "";
                    emailLink.NavigateUrl = "#";
                    imgPerson.ImageUrl = "";
                }
                con.Close();
            }
        }
    }
}
