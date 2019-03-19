using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class ChequeImage
    {
        public int ChequImageId { get; set; }
        public String ProcNo { get; set; }
        public int ImageTypeId { get; set; }
        public Image Image { get; set; }
        /*For receiving Images*/
        public String ImageFace { get; set; }
        public byte[] ImageBytes_ { get; set; }
        public String ImageString { get; set; }
        /*End receiving image*/

        public Bitmap ConvertToGrayScale(Bitmap bmp)
        {
            Bitmap newBmp = bmp;
            int width = bmp.Width;
            int height = bmp.Height;
            Color p;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    p = bmp.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //find average
                    int avg = (r + g + b) / 3;

                    //set new pixel value
                    newBmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            return newBmp;
        }

        public Bitmap ConvertBlackAndWhite(Bitmap Image)
        {
            Colormatrix TempMatrix = new Colormatrix();
            TempMatrix.Matrix = new float[][]{
                     new float[] {.3f, .3f, .3f, 0, 0},
                      new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                      new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                 };
            return TempMatrix.Apply(Image);
        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;

            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                result = stream.ToArray();
            }

            return result;
        }

        public byte[] ConvertBitMapToBnWByteArray(Bitmap bitmap)
        {
            byte[] result = null;

            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Tiff);
                result = stream.ToArray();
            }

            return result;
        }

        public byte[] ImageStringToBinary(string imageString)
        {
            return Convert.FromBase64String(imageString);
            /*FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;*/
        }

        public static Image BinaryToImage(System.Data.Linq.Binary binaryData)
        {
            if (binaryData == null) return null;

            byte[] buffer = binaryData.ToArray();
            MemoryStream memStream = new MemoryStream();
            memStream.Write(buffer, 0, buffer.Length);
            return Image.FromStream(memStream);
        }

        public String InsertChequeImage(ChequeImage chequeImage)
        {
            String inserted = "";
            int count = 0;
            DBConnect dbConnect;
            SqlConnection conn = null;
            try
            {
                dbConnect = new DBConnect();
                conn = dbConnect.OpenDBConn();
            }
            catch (Exception e)
            {
                inserted = "sqlexception-" + e.Message;
            }

            try
            {
                string existQuery = "SELECT * FROM CHEQUEIMAGE WHERE PROCNO='" +chequeImage.ProcNo + "'";
                SqlCommand existQueryCmd = new SqlCommand(existQuery, conn);
                SqlDataReader reader = existQueryCmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                inserted = e.Message + "\n" + e.StackTrace;
                //conn.Close();
            }
            /*if (count == 0|| count != 0)
            {*/
                /*
                 [PROCNO]
      ,[IMAGE]
      ,[IMAGETYPEID]*/
                inserted = "chequeimagenotexists";
                string insertQuery = @"INSERT INTO CHEQUEIMAGE (PROCNO,IMAGE,IMAGETYPEID) VALUES (@PROCNO,@IMAGE,@IMAGETYPEID)";
                /*string query =
               "INSERT INTO CHEQUEIMAGE (PROCNO,IMAGE,IMAGETYPEID)" +
               "VALUES" +
               "('" + chequeImage.ProcNo + "','" + chequeImage.ImageBytes_ + "','" + chequeImage.ImageTypeId + "')";*/
                String sSql = "INSERT INTO CHEQUEIMAGE(PROCNO, IMAGE, IMAGETYPEID) VALUES(@PROCNO, @IMAGE, @IMAGETYPEID)";
                string query =
                "INSERT INTO CHEQUEIMAGE (PROCNO,IMAGE,IMAGETYPEID)" +
                "VALUES" +
                "('" + chequeImage.ProcNo + "','CONVERT(VARBINARY(MAX),"  +chequeImage.ImageBytes_+")','" + chequeImage.ImageTypeId + "')";
                SqlCommand cmd = new SqlCommand(sSql, conn);
                try
                {
                    //CONVERT(VARBINARY(MAX),'IMAGE')
                    /*SqlParameter sqlParam = cmd.Parameters.AddWithValue("@PROCNO", chequeImage.ProcNo);
                    sqlParam.DbType = DbType.String;
                    SqlParameter sqlParam2 = cmd.Parameters.AddWithValue("@IMAGE", chequeImage.ImageBytes_);
                    sqlParam2.DbType = DbType.Binary;
                    SqlParameter sqlParam3 = cmd.Parameters.AddWithValue("@IMAGETYPEID", chequeImage.ProcNo);
                    sqlParam3.DbType = DbType.Int32;*/
                    cmd.Parameters.Add("@PROCNO", SqlDbType.VarChar).Value = chequeImage.ProcNo;
                    cmd.Parameters.Add("@IMAGETYPEID", SqlDbType.VarChar).Value = chequeImage.ImageTypeId;
                    cmd.Parameters.Add("@IMAGE", SqlDbType.VarBinary).Value = chequeImage.ImageBytes_;
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        inserted = "chequeimageok";
                    }
                    else
                    {
                        inserted = "chequeimagebad";
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    inserted = e.Message + "\n" + e.StackTrace;
                    //conn.Close();
                }
                finally
                {
                    conn.Close();
                    //Console.ReadKey();
                }
            /*}
            else
            {
                inserted = "chequeimageexists:" + chequeImage.ProcNo;
                conn.Close();
            }*/
            return inserted;
        }
    }
}