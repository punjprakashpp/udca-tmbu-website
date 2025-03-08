using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            BindSlider();
            BindLinks();
            BindButton();
            BindNotices();
            BindEvent();
            BindTender();
            LoadPersons();
        }
    }

    private void BindSlider()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Title, FilePath FROM Image WHERE Type ='Slider' ORDER BY ImageID DESC";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            SliderRepeater.DataSource = dt;
            SliderRepeater.DataBind();
        }
    }

    private void BindLinks()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT LinkText, LinkURL FROM Links WHERE Type = 'Link' ORDER BY LinkID DESC";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            linksRepeater.DataSource = dt;
            linksRepeater.DataBind();
        }
    }

    private void BindButton()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT LinkText, LinkURL FROM Links WHERE Type = 'Button'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ButtonRepeater.DataSource = dt;
            ButtonRepeater.DataBind();
        }
    }

    private void BindNotices()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Title, Date, FilePath FROM Docs WHERE Type = 'Notice' ORDER BY Date DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    StringBuilder marqueeContent = new StringBuilder();

                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        DateTime noticeDate = Convert.ToDateTime(reader["Date"]);
                        string filePath = reader["FilePath"].ToString();

                        marqueeContent.Append("<div class='notice'>");
                        marqueeContent.AppendFormat("<strong>{0}:</strong> ", noticeDate.ToString("dd-MM-yyyy"));
                        marqueeContent.AppendFormat("<a href='/pdfjs/web/viewer.html?file=/{0}' target='_Blank'>{1}</a>", filePath, title);
                        marqueeContent.Append("</div>");
                    }

                    LiteralNotices.Text = marqueeContent.ToString();
                }
            }
        }
    }

    private void BindEvent()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Title, Date, FilePath FROM Docs WHERE Type = 'Event' ORDER BY Date DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    StringBuilder marqueeContent = new StringBuilder();

                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        DateTime newsDate = Convert.ToDateTime(reader["Date"]);
                        string filePath = reader["FilePath"].ToString();

                        marqueeContent.Append("<div class='notice'>");
                        marqueeContent.AppendFormat("<strong>{0}:</strong> ", newsDate.ToString("dd-MM-yyyy"));
                        marqueeContent.AppendFormat("<a href='/pdfjs/web/viewer.html?file=/{0}' target='_Blank'>{1}</a>", filePath, title);
                        marqueeContent.Append("</div>");
                    }

                    LiteralEvent.Text = marqueeContent.ToString();
                }
            }
        }
    }

    private void BindTender()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT Title, Date, FilePath FROM Docs WHERE Type = 'Tender' ORDER BY Date DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    StringBuilder marqueeContent = new StringBuilder();

                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        DateTime newsDate = Convert.ToDateTime(reader["Date"]);
                        string filePath = reader["FilePath"].ToString();

                        marqueeContent.Append("<div class='notice'>");
                        marqueeContent.AppendFormat("<strong>{0}:</strong> ", newsDate.ToString("dd-MM-yyyy"));
                        marqueeContent.AppendFormat("<a href='/pdfjs/web/viewer.html?file=/{0}' target='_Blank'>{1}</a>", filePath, title);
                        marqueeContent.Append("</div>");
                    }

                    LiteralTender.Text = marqueeContent.ToString();
                }
            }
        }
    }

    private void LoadPersons()
    {
        PersonDAL dal = new PersonDAL();
        List<Person> persons = dal.GetPersons();

        foreach (Person person in persons)
        {
            switch (person.Position)
            {
                case "Chancellor":
                    lblNameChancellor.Text = person.Name;
                    ImageChancellor.ImageUrl = person.FilePath;
                    break;
                case "Vice Chancellor":
                    lblNameViceChancellor.Text = person.Name;
                    ImageViceChancellor.ImageUrl = person.FilePath;
                    break;
                case "Director":
                    lblNameDirector.Text = person.Name;
                    NameDirector.Text = person.Name;
                    ImageDirector.ImageUrl = person.FilePath;
                    ImgDirector.ImageUrl = person.FilePath;
                    break;
            }
        }
    }
}

public class PersonDAL
{
    private string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

    public List<Person> GetPersons()
    {
        List<Person> persons = new List<Person>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Member WHERE type = 'Admin'", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Person person = new Person();
                person.PersonID = Convert.ToInt32(rdr["MemberId"]);
                person.Position = rdr["Position"].ToString();
                person.Name = rdr["Name"].ToString();
                person.FilePath = rdr["FilePath"].ToString();
                persons.Add(person);
            }
        }

        return persons;
    }
}

public class Person
{
    public int PersonID { get; set; }
    public string Position { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
}