using System;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Linq;

public partial class cms_ManageTenders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = Page.User.Identity as FormsIdentity;
            if (identity != null)
            {
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userRole = ticket.UserData;

                if (string.IsNullOrEmpty(userRole))
                {
                    // Redirect to Login if user role is missing
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Allow access only if the user is an Admin
                if (userRole == "Admin")
                {
                    // User is an Admin; proceed with page logic
                }
                else
                {
                    // Show unauthorized access notification and redirect
                    NotificationHelper.ShowNotification(this, "You are not authorized to access this page!", "warning", "warning");
                    Response.Redirect("~/cms/", false); // Redirect to an Dashboard page
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                // Redirect to Login if identity is null
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        else
        {
            // Redirect to Login if the user is not authenticated
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void gvNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNotice.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void BindGridView()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
                SELECT 
                    DocsID,
                    No, 
                    Title, 
                    Date, 
                    FilePath,
                    ROW_NUMBER() OVER (ORDER BY Date) AS RowNum
                FROM 
                    Docs
                WHERE
                    Type = 'Tender'";

            if (rdNo.Checked || rdTitle.Checked)
            {
                query += " AND (@SearchNo IS NULL OR No LIKE '%' + @SearchNo + '%') " +
                         "AND (@SearchTitle IS NULL OR Title LIKE '%' + @SearchTitle + '%')";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                string searchNo = rdNo.Checked ? txtSearch.Text : string.Empty;
                string searchTitle = rdTitle.Checked ? txtSearch.Text : string.Empty;

                cmd.Parameters.AddWithValue("@SearchNo", string.IsNullOrEmpty(searchNo) ? (object)DBNull.Value : searchNo);
                cmd.Parameters.AddWithValue("@SearchTitle", string.IsNullOrEmpty(searchTitle) ? (object)DBNull.Value : searchTitle);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        sda.Fill(dt);

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

                        gvNotice.DataSource = dt;
                        gvNotice.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred while retrieving data: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
        }
    }

    protected void gvNotice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvNotice.EditIndex = e.NewEditIndex;
        BindGridView();
    }

    protected void gvNotice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        GridViewRow row = gvNotice.Rows[e.RowIndex];
        int noticeID = Convert.ToInt32(gvNotice.DataKeys[e.RowIndex].Values[0]);
        string no = (row.FindControl("txtNo") as TextBox).Text.Trim();
        string title = (row.FindControl("txtTitle") as TextBox).Text.Trim();
        string noticeDateText = (row.FindControl("txtNoticeDate") as TextBox).Text;
        DateTime updateDate = DateTime.Now;
        DateTime noticeDate;

        // Improved date parsing
        if (!DateTime.TryParse(noticeDateText, out noticeDate))
        {
            lblMessage.Text = "Invalid date format. Please ensure the date is in a correct format.";
            lblMessage.ForeColor = Color.Red;
            return;
        }

        FileUpload fileUpload = (row.FindControl("fileUpload") as FileUpload);
        string existingFilePath = (row.FindControl("hiddenFilePath") as HiddenField).Value;
        string newFilePath = existingFilePath;

        // File handling
        try
        {
            if (fileUpload.HasFile)
            {
                if (File.Exists(Server.MapPath("~/" + newFilePath)))
                {
                    File.Delete(Server.MapPath("~/" + newFilePath));
                }
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                StringBuilder safeTitleBuilder = new StringBuilder();
                foreach (char c in title)
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        safeTitleBuilder.Append(c);
                    }
                    else
                    {
                        safeTitleBuilder.Append('_');
                    }
                }
                string safeTitle = safeTitleBuilder.ToString();
                StringBuilder safeNoBuilder = new StringBuilder();
                foreach (char c in no)
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        safeNoBuilder.Append(c);
                    }
                    else
                    {
                        safeNoBuilder.Append('_');
                    }
                }
                string safeNo = safeNoBuilder.ToString();

                string filename = "Notice_" + safeNo + "_" + noticeDate.ToString("dd-MM-yyyy") + "_" + safeTitle + ".pdf";
                string uploadFolder = Server.MapPath("~/uploads/tender/");
                newFilePath = Path.Combine(uploadFolder, filename);

                // Check file type
                if (fileExtension == ".pdf")
                {
                    fileUpload.SaveAs(newFilePath);
                }
                else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    using (var ms = new MemoryStream(fileUpload.FileBytes))
                    using (var img = System.Drawing.Image.FromStream(ms))
                    {
                        var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                        using (var stream = new FileStream(newFilePath, FileMode.Create))
                        {
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();

                            // Compress image by converting to JPEG format with 50% quality
                            using (var compressedMs = new MemoryStream())
                            {
                                var jpegEncoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                                                   .FirstOrDefault(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                                if (jpegEncoder != null)
                                {
                                    var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);

                                    img.Save(compressedMs, jpegEncoder, encoderParams);
                                    compressedMs.Seek(0, SeekOrigin.Begin);

                                    var pdfImg = iTextSharp.text.Image.GetInstance(compressedMs);
                                    pdfImg.Alignment = Element.ALIGN_CENTER;
                                    pdfImg.ScaleToFit(doc.PageSize.Width - 100, doc.PageSize.Height - 100);

                                    doc.Add(pdfImg);
                                }
                                else
                                {
                                    lblMessage.Text = "Error: JPEG encoder not found.";
                                    lblMessage.ForeColor = Color.Red;
                                    return;
                                }
                            }

                            doc.Close();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Only PDF or image files are allowed.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                newFilePath = "uploads/tender/" + filename; // Fixed path concatenation
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string updateQuery = @"
                UPDATE Docs 
                SET 
                    No = @No, 
                    Title = @Title, 
                    Date = @Date,  
                    FilePath = @FilePath,
                    UploadDate = @UpdateDate
                WHERE DocsID = @DocsID";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@No", no);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Date", noticeDate);
                    cmd.Parameters.AddWithValue("@FilePath", newFilePath);
                    cmd.Parameters.AddWithValue("@DocsID", noticeID);
                    cmd.Parameters.AddWithValue("@UpdateDate", updateDate);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Tender updated successfully.";
                        lblMessage.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred while updating the Tender: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An error occurred during file processing: " + ex.Message;
            lblMessage.ForeColor = Color.Red;
        }

        gvNotice.EditIndex = -1;
        BindGridView();
    }

    protected void gvNotice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvNotice.EditIndex = -1;
        BindGridView();
    }

    protected void gvNotice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int noticeID = Convert.ToInt32(gvNotice.DataKeys[e.RowIndex].Values[0]);
        string query;
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            query = "SELECT FilePath FROM Docs WHERE DocsID=@NoticeID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NoticeID", noticeID);
                conn.Open();
                string filePath = cmd.ExecuteScalar() as string;

                if (filePath != null && File.Exists(Server.MapPath("~/" + filePath)))
                {
                    try
                    {
                        File.Delete(Server.MapPath("~/" + filePath));
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error deleting file: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }

            query = "DELETE FROM Docs WHERE DocsID = @DocsID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DocsID", noticeID);
                cmd.ExecuteNonQuery();
            }
        }

        BindGridView();
        lblMessage.Text = "Tender deleted successfully.";
        lblMessage.ForeColor = Color.Green;
    }
}