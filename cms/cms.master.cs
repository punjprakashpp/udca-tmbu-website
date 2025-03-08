using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cms_master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = Page.User.Identity as FormsIdentity;
            if (identity != null && Page.User.Identity == identity)
            {
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userRole = ticket.UserData;

                if (string.IsNullOrEmpty(userRole))
                {
                    // Redirect to Login and display message after setting it
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Allow access only if the user is an Admin
                if (userRole == "Admin")
                {
                    AdminPanel.Visible = true;
                    AdminPanel1.Visible = true;
                    AdminPanel2.Visible = true;
                    AdminPanel3.Visible = true;
                    AdminPanel4.Visible = true;
                    AdminPanel5.Visible = true;
                }
                else if (userRole == "User")
                {
                    AdminPanel.Visible = false;
                    AdminPanel1.Visible = false;
                    AdminPanel2.Visible = false;
                    AdminPanel3.Visible = false;
                    AdminPanel4.Visible = false;
                    AdminPanel5.Visible = false;
                }
                else
                {
                    // Redirect to Login and display message after setting it
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
        }
        else
        {
            // Redirect to login if user is not authenticated
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }


    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        // Sign out the user
        FormsAuthentication.SignOut();

        // Clear the authentication cookie
        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        authCookie.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(authCookie);
    }
}
