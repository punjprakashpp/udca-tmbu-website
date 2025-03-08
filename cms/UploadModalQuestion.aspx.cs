using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI.WebControls;

public partial class UploadModalQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSessionDropDown();
            populateSemesterDropdown();
        }
    }

    private void populateSemesterDropdown()
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("--- Select-Semester ---", string.Empty));
        ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("Semester 1", "Semester-1"));
        ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("Semester 2", "Semester-2"));
        ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("Semester 3", "Semester-3"));
        ddlSemester.Items.Add(new System.Web.UI.WebControls.ListItem("Semester 4", "Semester-4"));
    }

    private void PopulateSessionDropDown()
    {
        ddlSession.Items.Add(new System.Web.UI.WebControls.ListItem("--- Select-Session ---", string.Empty));
        int currentYear = DateTime.Now.Year;
        for (int year = 2020; year <= currentYear; year++)
        {
            ddlSession.Items.Add(new System.Web.UI.WebControls.ListItem(year.ToString() + " - " + (year + 2).ToString(), year.ToString() + "-" + (year + 2).ToString()));
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string filePath = null;

        try
            {                
                if (fileUpload.HasFile)
                {
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                    
                    string fileName = "Modal_Question_" + ddlSession.SelectedValue + "_" + ddlSemester.SelectedValue + (fileExtension == ".zip" ? ".zip" : ".pdf");

                    string uploadFolder = Server.MapPath("~/uploads/Modal/");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    filePath = Path.Combine(uploadFolder, fileName);

                    if (fileExtension == ".pdf" || fileExtension == ".zip")
                    {
                        fileUpload.SaveAs(filePath);
                    }
                    else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                    {
                        using (var ms = new MemoryStream(fileUpload.FileBytes))
                        using (var img = System.Drawing.Image.FromStream(ms))
                        {
                            var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                            using (var stream = new FileStream(filePath, FileMode.Create))
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

                    string relativeFilePath = "uploads/Modal/" + fileName;
                    string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "INSERT INTO Docs (Type, Title, Semester, Session, FilePath, UploadDate) VALUES (@Type, @Title, @Semester, @Session, @FilePath, @UploadDate)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Type", "Modal");
                            cmd.Parameters.AddWithValue("@Title", "Modal Question " + ddlSession.SelectedValue + " " + ddlSemester.SelectedValue);
                        cmd.Parameters.AddWithValue("@Semester", ddlSemester.SelectedValue);
                        cmd.Parameters.AddWithValue("@Session", ddlSession.SelectedValue);
                            cmd.Parameters.AddWithValue("@FilePath", relativeFilePath);
                            cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            lblMessage.Text = "File uploaded successfully!";
                            lblMessage.ForeColor = Color.Green;

                        ddlSemester.SelectedIndex = 0;
                            fileUpload.Attributes.Clear();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please upload a PDF or image file.";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageModalQuestions.aspx");
    }
}