using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStats();
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
    }

    private void LoadStats()
    {
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();

            // Get total students
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Student", connection);
            int totalStudents = Convert.ToInt32(command.ExecuteScalar());
            lbltotalStudents.Text = totalStudents.ToString();

            // Get total achievers
            command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Achiever = 'yes'", connection);
            int totalAchievers = Convert.ToInt32(command.ExecuteScalar());
            lbltotalAchievers.Text = totalAchievers.ToString();

            // Get total alumni
            command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Alumnus = 'yes'", connection);
            int totalAlumni = Convert.ToInt32(command.ExecuteScalar());
            lbltotalAlumni.Text = totalAlumni.ToString();

            // Get total faculty 
            command = new SqlCommand("SELECT COUNT(*) FROM Member WHERE Type = 'Faculty' OR Type = 'Guest' ", connection);
            int totalFaculty = Convert.ToInt32(command.ExecuteScalar());
            lbltotalFaculty.Text = totalFaculty.ToString();

            // Get total staffs
            command = new SqlCommand("SELECT COUNT(*) FROM Member WHERE Type = 'Office' OR Type = 'Support'", connection);
            int totalStaffs = Convert.ToInt32(command.ExecuteScalar());
            lbltotalStaffs.Text = totalStaffs.ToString();

            // Get total notice
            command = new SqlCommand("SELECT COUNT(*) FROM Docs WHERE TYPE = 'Notice'", connection);
            int totalNotice = Convert.ToInt32(command.ExecuteScalar());
            lbltotalNotice.Text = totalNotice.ToString();

            // Get total docs
            command = new SqlCommand("SELECT COUNT(*) FROM [User]", connection);
            int totalDocs = Convert.ToInt32(command.ExecuteScalar());
            lbltotalDocs.Text = totalDocs.ToString();

            // Get total files
            command = new SqlCommand("SELECT COUNT(*) FROM Docs WHERE TYPE = 'Report'", connection);
            int totalFiles = Convert.ToInt32(command.ExecuteScalar());
            lbltotalFiles.Text = totalFiles.ToString();
        }
    }
}