using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Configuration;

public partial class cms_UploadGalleryImage : System.Web.UI.Page
{
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string occasion = txtOccasion.Text.Trim();
        string uploadFolder = Server.MapPath("~/Uploads/gallery/");
        string FilePath = "Uploads/gallery/";

        // Ensure the upload directory exists
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        if (fileUpload.HasFiles)
        {
            foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
            {
                string fileName = Path.GetFileName(uploadedFile.FileName);
                string fullFilePath = Path.Combine(uploadFolder, fileName);
                string relativeFilePath = Path.Combine(FilePath, fileName);

                // Compress and save the image
                using (Bitmap bitmap = new Bitmap(uploadedFile.InputStream))
                {
                    SaveCompressedImage(bitmap, fullFilePath);
                }

                // Save the file path and details to the database
                SaveImageDetailsToDatabase(occasion, relativeFilePath);
            }

            lblMessage.Text = "Images uploaded successfully.";
            lblMessage.ForeColor = Color.Green;
            txtOccasion.Text = string.Empty;
            fileUpload.Attributes.Clear();
        }
        else
        {
            lblMessage.Text = "Please select at least one image to upload.";
            lblMessage.ForeColor = Color.Red;
        }
    }

    private void SaveImageDetailsToDatabase(string occasion, string FilePath)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        DateTime uploadDate = DateTime.Now;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Image (Type, Title, FilePath, UploadDate) VALUES (@Type, @Desc, @FilePath, @UploadDate)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Type", "Gallery");
                command.Parameters.AddWithValue("@Desc", occasion);  // Updated parameter name
                command.Parameters.AddWithValue("@FilePath", FilePath);
                command.Parameters.AddWithValue("@UploadDate", uploadDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    private void SaveCompressedImage(Bitmap image, string filePath)
    {
        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
        Encoder qualityEncoder = Encoder.Quality;
        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = new EncoderParameter(qualityEncoder, 75L); // Adjust quality (0-100)

        image.Save(filePath, jpgEncoder, encoderParams); // Save with compression
    }

    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteGalleryImages.aspx");
    }
}