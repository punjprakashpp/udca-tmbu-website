using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Configuration;

public partial class cms_UploadSliderImage : System.Web.UI.Page
{
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string descText = txtDescription.Text.Trim();
        DateTime uploadDate = DateTime.Now;
        string filePath = null;

        if (fileUpload.HasFile)
        {
            // Check if the file is an image
            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string folderPath = Server.MapPath("~/Uploads/slider/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fullPath = folderPath + fileName;

                // Save the cropped and compressed image
                string base64String = imagePreviewBase64.Value;
                base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "");
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid); 
                        Encoder qualityEncoder = Encoder.Quality;
                        EncoderParameters encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(qualityEncoder, 75L); // Adjust quality here (0-100)

                        bmp.Save(fullPath, jpgEncoder, encoderParams);
                    }
                }
                filePath = "uploads/slider/" + fileName;
            }
            else
            {
                lblMessage.Text = "Invalid file type. Only .jpg, .jpeg, .png files are allowed.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "INSERT INTO Image (Type, Title, FilePath, UploadDate) VALUES (@Type, @Desc, @FilePath, @UploadDate)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Type", "Slider");
                cmd.Parameters.AddWithValue("@Desc", descText);
                cmd.Parameters.AddWithValue("@FilePath", filePath);
                cmd.Parameters.AddWithValue("@UploadDate", uploadDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Slider Image Uploaded successfully!";
                lblMessage.ForeColor = Color.Green;

                txtDescription.Text = string.Empty;
                fileUpload.Attributes.Clear();
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteSliderImages.aspx");
    }
}