using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FinAPI
{
    public class Cheque
    {
        public String FImageName { get; set; }
        public String BImageName { get; set; }

        public String BImage { get; set; }

        public String FImage { get; set; }
        public String DateIssued { get; set; }
        public Double Amount { get; set; }
        public String Status { get; set; }
        public String response { get; set; }
        public String ocrResults { get; set; }
        public String ChequeName { get; set; }
        public String micr { get; set; }
        public String scannedMicr { get; set; }
        public String Currency { get; set; }
        public String DateSubmitted { get; set; }
        public int Accountno { get; set; }
        public String GS_Front_Image { get; set; }
        public String GS_Back_Image { get; set; }

        public String BW_Front_Image { get; set; }

        public String BW_Back_Image { get; set; }
        
        public Cheque() { }
        public Cheque(Double amount, String sresponse, String Chequename, String crResults, String dateIssued)
        {
            this.response = sresponse;
            this.ChequeName = Chequename;
            this.ocrResults = crResults;
            this.Amount = amount;
            this.DateIssued = dateIssued;
        }
        public Cheque(String FImageName, String BImageName, String DateIssued, Double Amount)
        {
            this.Amount = Amount;
            this.BImageName = BImageName;
            this.FImageName = FImageName;
            this.DateIssued = DateIssued;
        }

        public Cheque(String FImageName, String BImageName, String DateIssued, Double Amount, String ChequeName, String micr, String scannedMicr)
        {
            this.Amount = Amount;
            this.BImageName = BImageName;
            this.FImageName = FImageName;
            this.DateIssued = DateIssued;
            this.ChequeName = ChequeName;
            this.micr = micr;
            this.scannedMicr = scannedMicr;
        }

        public Cheque(String FImageName, String BImageName, String DateIssued, Double Amount, String ChequeName, String micr, String scannedMicr, String Currency)
        {
            this.Amount = Amount;
            this.BImageName = BImageName;
            this.FImageName = FImageName;
            this.DateIssued = DateIssued;
            this.ChequeName = ChequeName;
            this.micr = micr;
            this.scannedMicr = scannedMicr;
            this.Currency = Currency;
        }

        public Cheque(String FImageName, String BImageName, String DateIssued, Double Amount, String ChequeName, String micr, String scannedMicr, String Currency, int Accountno)
        {
            this.Amount = Amount;
            this.BImageName = BImageName;
            this.FImageName = FImageName;
            this.DateIssued = DateIssued;
            this.ChequeName = ChequeName;
            this.micr = micr;
            this.scannedMicr = scannedMicr;
            this.Currency = Currency;
            this.Accountno = Accountno;
        }

        public Cheque(String FImageName, String BImageName, String DateIssued, Double Amount, String ChequeName, String micr, String scannedMicr, String Currency, int Accountno,String GS_Front_Image, String GS_Back_Image, String BW_Front_Image, String BW_Back_Image)
        {
            this.Amount = Amount;
            this.BImageName = BImageName;
            this.FImageName = FImageName;
            this.DateIssued = DateIssued;
            this.ChequeName = ChequeName;
            this.micr = micr;
            this.scannedMicr = scannedMicr;
            this.Currency = Currency;
            this.Accountno = Accountno;
            this.GS_Front_Image = GS_Front_Image;
            this.GS_Back_Image = GS_Back_Image;
            this.BW_Front_Image = BW_Front_Image;
            this.BW_Back_Image = BW_Back_Image;
        }

        public Cheque(String Status)
        {
            this.Status = Status;
        }

        public int SaveCheque(Cheque cheque)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "AddNewCheque";
                com.Parameters.Add("@fname", SqlDbType.VarChar).Value =
                    cheque.FImageName;
                com.Parameters.Add("@bname", SqlDbType.VarChar).Value =
                    cheque.BImageName;
                com.Parameters.Add("@date_issued", SqlDbType.VarChar).Value = cheque.DateIssued/*DateTime.ParseExact(cheque.DateIssued, "yyyy-MM-dd", null)*/;
                /*Convert.ToDateTime(cheque.DateIssued)*/
                ;
                com.Parameters.Add("@amount", SqlDbType.VarChar).Value =
                    cheque.Amount;
                com.Parameters.Add("@cheque_name", SqlDbType.VarChar).Value =
                    cheque.ChequeName;
                com.Parameters.Add("@date_submitted", SqlDbType.VarChar).Value =
                    /*"2018-12-13 11:20:00"*/time.ToString("yyyy-MM-dd HH:mm:ss");
                com.Parameters.Add("@micr", SqlDbType.VarChar).Value =
                    cheque.micr;
                com.Parameters.Add("@scannedMicr", SqlDbType.VarChar).Value =
                    cheque.scannedMicr;
                com.Parameters.Add("@currency", SqlDbType.VarChar).Value =
                    cheque.Currency;
                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    cheque.Accountno;
                com.Parameters.Add("@GS_Front_Image", SqlDbType.VarChar).Value =
                    cheque.GS_Front_Image/*"-"*/;
                com.Parameters.Add("@GS_Back_Image", SqlDbType.VarChar).Value =
                    /*"-"*/cheque.GS_Back_Image;
                com.Parameters.Add("@BW_Front_Image", SqlDbType.VarChar).Value =
                    cheque.BW_Front_Image/*"-"*/;
                com.Parameters.Add("@BW_Back_Image", SqlDbType.VarChar).Value =
                    /*"-"*/cheque.BW_Back_Image;

                int i = com.ExecuteNonQuery();

                return i;
            }
        }

        public Cheque getChequeDetails(String chequeName)
        {
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCheque";
                com.Parameters.Add("@ChequeName", SqlDbType.VarChar).Value =
                    chequeName;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Currency = reader["Currency"].ToString();
                    c.Accountno = Int32.Parse(reader["AccountNo"].ToString());
                    c.Status = reader["chequestatus"].ToString();
                    c.GS_Front_Image = reader["GS_Front_Image"].ToString();
                    c.GS_Back_Image = reader["GS_Back_Image"].ToString();
                    c.BW_Front_Image = reader["BW_Front_Image"].ToString();
                    c.BW_Back_Image = reader["BW_Back_Image"].ToString();
                }
            }
            return c;
        }

        public Cheque getRejectedChequeDetails(String chequeName)
        {
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCheque";
                com.Parameters.Add("@ChequeName", SqlDbType.VarChar).Value =
                    chequeName;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Currency = reader["Currency"].ToString();
                    c.Accountno = Int32.Parse(reader["AccountNo"].ToString());
                    c.Status = reader["chequestatus"].ToString();
                }
            }
            return c;
        }

        public List<Cheque> GetCheques()
        {
            Cheque c = new Cheque();
            //List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCheques";
                SqlDataReader reader = com.ExecuteReader();

                List<Cheque> chequeList = new List<Cheque>();

                while (reader.Read())
                {
                    j++;
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.Currency = reader["Currency"].ToString();

                    chequeList.Add(new Cheque(
                                    reader["fname"].ToString(),
                                    reader["bname"].ToString(),
                                    reader["date_issued"].ToString(),
                                    Double.Parse(reader["amount"].ToString()),
                                    reader["ChequeName"].ToString(),
                                    reader["micr"].ToString(),
                                    reader["scannedMicr"].ToString(),
                                    reader["Currency"].ToString()
                                    )
                    );
                }

                return chequeList;
            }
        }

        public List<Cheque> Query(int accountno)
        {
            List<Cheque> chequeList = new List<Cheque>();
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "CHEQUEQUERY";
                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.Currency = reader["Currency"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Status = reader["chequestatus"].ToString();
                    chequeList.Add(c);
                    /*chequeList.Add(new Cheque(
                                    reader["fname"].ToString(),
                                    reader["bname"].ToString(),
                                    reader["date_issued"].ToString(),
                                    Double.Parse(reader["amount"].ToString()),
                                    reader["ChequeName"].ToString(),
                                    reader["micr"].ToString(),
                                    reader["scannedMicr"].ToString(),
                                    reader["Currency"].ToString()
                                    )
                    );*/
                }
                return chequeList;
            }
        }

        public Cheque GetMicrCheque(String micr)
        {
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetMicrCheque";

                com.Parameters.Add("micr", SqlDbType.VarChar).Value =
                    micr;
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Status = reader["chequestatus"].ToString();
                    c.Accountno = Int32.Parse(reader["AccountNo"].ToString());
                    c.Currency = reader["Currency"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                }

                return c;
            }
        }

        public Cheque GetChequeStatus(String Chequename)
        {
            Cheque c = new Cheque();
            //List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetChequeStatus";

                com.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    Chequename;

                SqlDataReader reader = com.ExecuteReader();

                List<Cheque> chequeList = new List<Cheque>();

                while (reader.Read())
                {
                    j++;
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Status = reader["chequestatus"].ToString();
                    c.Accountno= Int32.Parse(reader["AccountNo"].ToString());
                    c.Currency= reader["Currency"].ToString();

                    //chequeList.Insert(0, c);
                    chequeList.Add(c);
                }

                return c;
            }
        }

        public int GetChequesCount()
        {
            Cheque c = new Cheque();
            //List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCheques";
                SqlDataReader reader = com.ExecuteReader();

                List<Cheque> chequeList = new List<Cheque>();

                while (reader.Read())
                {
                    j++;
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();

                    //chequeList.Insert(0, c);
                    chequeList.Add(c);
                }
                /*com.Parameters.Add("@fname", SqlDbType.VarChar).Value =
                    cheque.FImageName;*/

                //int i = com.ExecuteNonQuery();

                return j;
            }
        }

        public List<Cheque> GetCheque(String key)
        {
            List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCheque";

                com.Parameters.Add("@fname", SqlDbType.VarChar).Value =
                    key;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chequeList.Add(new Cheque(reader.GetString(1), reader.GetString(2), reader.GetString(4), reader.GetDouble(5)));
                    }
                }
                return chequeList;
            }
        }

        public int RejectCheque(RejectedCheque rC)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            int j = 0;
            int i = 0;
            String email = "";

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "AddRejectedCheque";

                com.Parameters.Add("@cheque_name", SqlDbType.VarChar).Value =
                    rC.ChequeName;
                com.Parameters.Add("@scannedMicr", SqlDbType.VarChar).Value =
                    rC.ScannedMicr;
                com.Parameters.Add("@micr", SqlDbType.VarChar).Value =
                    rC.Micr;
                com.Parameters.Add("@amount", SqlDbType.VarChar).Value =
                    rC.Amount;
                com.Parameters.Add("@currency", SqlDbType.VarChar).Value =
                    rC.Currency;
                com.Parameters.Add("@reasons", SqlDbType.Text).Value =
                    rC.Reasons;
                com.Parameters.Add("@accountno", SqlDbType.Text).Value =
                    rC.Accountno;

                i = com.ExecuteNonQuery();
            }
            using (SqlCommand com2 = conn.CreateCommand())
            {
                com2.CommandType = System.Data.CommandType.StoredProcedure;
                if (i == 1)
                {
                    com2.CommandText = "UpdateChequeStatus";

                    com2.Parameters.Add("@cheque_name", SqlDbType.VarChar).Value =
                        rC.ChequeName;
                    com2.Parameters.Add("@Micr", SqlDbType.VarChar).Value =
                        rC.Micr;
                    com2.Parameters.Add("@cheque_status", SqlDbType.VarChar).Value =
                        "Rejected";

                    j = com2.ExecuteNonQuery();
                }
            }

            using (SqlCommand com3 = conn.CreateCommand())
            {
                com3.CommandType = System.Data.CommandType.StoredProcedure;
                com3.CommandText = "PickEmail";

                com3.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    rC.ChequeName;
                using (SqlDataReader reader3 = com3.ExecuteReader())
                {
                    while (reader3.Read())
                    {
                        email = reader3["email"].ToString();
                    }
                    reader3.Close();
                }
            }

            SendMail("oduorderick@gmail.com", "Cheque rejected!");

            return j;
        }

        public List<Cheque> AdminSearchCheque(String key)
        {
            Cheque c = new Cheque();
            List<Cheque> chequeList = new List<Cheque>();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "AdminSearchCheque";

                com.Parameters.Add("@key", SqlDbType.VarChar).Value =
                    key;
                SqlDataReader reader = com.ExecuteReader();
                Boolean found = false;
                while (reader.Read())
                {
                    c.micr = reader["micr"].ToString();
                    c.scannedMicr = reader["scannedMicr"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    c.ChequeName = reader["ChequeName"].ToString();
                    c.Amount = Double.Parse(reader["amount"].ToString());
                    c.DateIssued = reader["date_issued"].ToString();
                    c.DateSubmitted = reader["date_submitted"].ToString();
                    c.Status = reader["chequestatus"].ToString();
                    c.Accountno = Int32.Parse(reader["AccountNo"].ToString());
                    c.Currency = reader["Currency"].ToString();
                    c.FImageName = reader["fname"].ToString();
                    c.BImageName = reader["bname"].ToString();
                    if (chequeList.Count > 0)
                    {
                        foreach(Cheque ch in chequeList){
                            if(ch.micr== reader["micr"].ToString())
                            {
                                found = true;
                                break;
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    if (found == false)
                    {
                        chequeList.Add(c);
                    }
                }

                return chequeList;
            }
        }

        public int AcceptCheque(String chequename)
        {
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            int j = 0;
            int i = 0;
            String email = "";
            int accountno = 0;
            String deviceToken = "";

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "AcceptCheque";

                com.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    chequename;

                i = com.ExecuteNonQuery();
            }

            using (SqlCommand com2 = conn.CreateCommand())
            {
                com2.CommandType = System.Data.CommandType.StoredProcedure;
                com2.CommandText = "PickEmail";

                com2.Parameters.Add("@chequename", SqlDbType.VarChar).Value =
                    chequename;
                using (SqlDataReader reader2 = com2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        email = reader2["email"].ToString();
                        accountno = Int32.Parse(reader2["AccountNo"].ToString());
                    }
                    reader2.Close();
                }
            }

            SendMail(email, "Cheque accepted!");
            /*using (SqlCommand com2 = conn.CreateCommand())
            {
                com2.CommandType = System.Data.CommandType.StoredProcedure;
                if (i == 1)
                {
                    com2.CommandText = "UpdateChequeStatus";

                    com2.Parameters.Add("@cheque_name", SqlDbType.VarChar).Value =
                        rC.ChequeName;
                    com2.Parameters.Add("@Micr", SqlDbType.VarChar).Value =
                        rC.Micr;
                    com2.Parameters.Add("@cheque_status", SqlDbType.VarChar).Value =
                        "Rejected";

                    j = com2.ExecuteNonQuery();
                }
            }*/


            return i;
        }

        public List<RejectedCheque> GetRejectedCheques(int accountno)
        {
            List<RejectedCheque> chequeList = new List<RejectedCheque>();
            int j = 0;
            String a = null;
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();
            DateTime time = DateTime.Now;
            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "GetCustomerRejectedCheques";

                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chequeList.Add(
                            new RejectedCheque(
                                reader["cheque_name"].ToString(),
                                reader["scannedMicr"].ToString(),
                                reader["micr"].ToString(),
                                Double.Parse(reader["amount"].ToString()),
                                reader["reasons"].ToString(),
                                reader["currency"].ToString()
                             )
                         );
                    }
                }
                return chequeList;
            }
        }

        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;

            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                //bitmap.Save(stream, bitmap.RawFormat);
                bitmap.Save(stream, ImageFormat.Jpeg);
                result = stream.ToArray();
            }

            return result;
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public Bitmap CropGrayScaleImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            //g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);



            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            //get a graphics object from the new image
            //Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               }
            );

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(source, section /*new Rectangle(0, 0, source.Width, source.Height)*/,
               0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            return bmp;
        }

        public Bitmap MakeGrayscale(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            newBitmap.SetResolution(100, 100);
            Graphics g = Graphics.FromImage(newBitmap);
            ColorMatrix colorMatrix = new ColorMatrix(
                                           new float[][]
                                           {
                                             new float[] {.3f, .3f, .3f, 0, 0},
                                             new float[] {.59f, .59f, .59f, 0, 0},
                                             new float[] {.11f, .11f, .11f, 0, 0},
                                             new float[] {0, 0, 0, 1, 0},
                                             new float[] {0, 0, 0, 0, 1}
                                           }
                                      );

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
            //g.DrawImage(original, new Rectangle(0, 0, 1920, 1080),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap/*new Bitmap(newBitmap, new Size(1920, 1080))*/;
        }

        public Bitmap BlackAndWhite(Bitmap image, Rectangle rectangle)
        {
            Bitmap blackAndWhite = new System.Drawing.Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = System.Drawing.Graphics.FromImage(blackAndWhite))
                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // for every pixel in the rectangle region
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy++)
                {
                    // average the red, green and blue of the pixel to get a gray value
                    Color pixel = blackAndWhite.GetPixel(xx, yy);
                    Int32 avg = (pixel.R + pixel.G + pixel.B) / 3;

                    blackAndWhite.SetPixel(xx, yy, Color.FromArgb(0, avg, avg, avg));
                }
            }
            //blackAndWhite.SetResolution(200, 200);

            return blackAndWhite/* new Bitmap(blackAndWhite, new Size(1920, 1080))*/;
        }

        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            //destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public Bitmap MakeBlackAndWhite(Bitmap image)
        {
            Bitmap originalbmp = image; // Load the  image

            Bitmap newbmp = image; // New image

            for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            {
                for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
                {
                    var colorValue = originalbmp.GetPixel(row, column); // Get the color pixel
                    var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
                    newbmp.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue)); // Set the value to new pixel
                }
            }
            return newbmp/* new Bitmap(newbmp, new Size(1920, 1080))*/;
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public void SendMail(String email,String message) {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("appsderick@gmail.com");
            mail.To.Add(email);
            mail.Subject = "FinCapture";

            mail.IsBodyHtml = true;
            string htmlBody;

            htmlBody = "<p>"+message+"</p>"/* +
            //"<a href='" + HttpContext.Current.Request.Url.Authority + "/Account/ActivateAccount?acc_code="+this.RandomPassword()+"'>Complete registration</a>";
            //"<a href='" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("Registration.aspx", "Login.aspx" ) + "'>Complete registration</a>";
            "<a href='" + HttpContext.Current.Request.Url.Authority + /*":"+ HttpContext.Current.Request.Url.Port +*//* "/Account/ActivateAccount" + "'>Complete registration</a>"*/;

            //"<a href='localhost:5038/Account/ActivateAccount'>Complete registration</a>";
            mail.Body = htmlBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("appsderick@gmail.com", "Confirmation1");
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
                //return "";
            }
            catch (Exception ex)
            {
                //ex.Message;
                //return status;
            }
        }

        public List<Cheque> SearchChequeDate(int accountno,String startdate,String enddate)
        {
            List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "SearchChequeDate";

                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;
                com.Parameters.Add("@startdate", SqlDbType.VarChar).Value =
                    startdate;
                com.Parameters.Add("@enddate", SqlDbType.VarChar).Value =
                    enddate;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c.micr = reader["micr"].ToString();
                        c.scannedMicr = reader["scannedMicr"].ToString();
                        c.FImageName = reader["fname"].ToString();
                        c.BImageName = reader["bname"].ToString();
                        c.ChequeName = reader["ChequeName"].ToString();
                        c.Amount = Double.Parse(reader["amount"].ToString());
                        c.DateIssued = reader["date_issued"].ToString();
                        c.Currency = reader["Currency"].ToString();
                        c.DateSubmitted = reader["date_submitted"].ToString();
                        c.Status = reader["chequestatus"].ToString();
                        chequeList.Add(c);
                    }
                }
                return chequeList;
            }
        }

        public List<Cheque> SearchChequeName(int accountno, String micr)
        {
            List<Cheque> chequeList = new List<Cheque>();
            int j = 0;
            String a = null;
            Cheque c = new Cheque();
            DBConnect dbConnect = new DBConnect();
            SqlConnection conn = dbConnect.OpenDBConn();

            using (SqlCommand com = conn.CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = "SearchChequeName";

                com.Parameters.Add("@accountno", SqlDbType.VarChar).Value =
                    accountno;
                com.Parameters.Add("@micr", SqlDbType.VarChar).Value =
                    micr;
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c.micr = reader["micr"].ToString();
                        c.scannedMicr = reader["scannedMicr"].ToString();
                        c.FImageName = reader["fname"].ToString();
                        c.BImageName = reader["bname"].ToString();
                        c.ChequeName = reader["ChequeName"].ToString();
                        c.Amount = Double.Parse(reader["amount"].ToString());
                        c.DateIssued = reader["date_issued"].ToString();
                        c.Currency = reader["Currency"].ToString();
                        c.DateSubmitted = reader["date_submitted"].ToString();
                        c.Status = reader["chequestatus"].ToString();
                        chequeList.Add(c);
                    }
                }
                return chequeList;
            }
        }
    }
}