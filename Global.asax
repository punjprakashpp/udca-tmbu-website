<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["NoOfVisitors"] = 0;
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        IncrementVisitorCount();
    }

    private void IncrementVisitorCount()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Visit SET Count = Count + 1 WHERE ID = 1", connection);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log it)
            LogError(ex);
        }
    }

    private void LogError(Exception ex)
    {
        try
        {
            // Log errors to a file or database as needed
            string logFilePath = Server.MapPath("~/log/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("Date: " + DateTime.Now.ToString());
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("StackTrace: " + ex.StackTrace);
                writer.WriteLine("-----------------------------------------------------");
            }
        }
        catch
        {
            // Handle any errors that occurred during logging
        }
    }
</script>