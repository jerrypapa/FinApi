using FinAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using Tesseract;
//using tessnet2;

namespace FinAPI
{
    public class NewChequeController : ApiController
    {
        //Cheque c = null;

        private static List<Car> _datas = new List<Car>();
        Cheque cheque = null;
        Cheque[] c = null;
        Users user = new Users();
        MobileUserBranch mBranch= new MobileUserBranch();

        public class myres
        {
            public string desc { get; set; }
            public bool status { get; set; }
        }


        [HttpPost]
        public Cheque UploadFiles()
        {
            Cheque ch=null,cheque_db=null;
            Cheque ourcheque = new Cheque();
            var fimage = HttpContext.Current.Request.Params["fimage"];
            var ftitle = HttpContext.Current.Request.Params["ftitle"];
            var bimage = HttpContext.Current.Request.Params["bimage"];
            var btitle = HttpContext.Current.Request.Params["btitle"];
            var amount = HttpContext.Current.Request.Params["amount"];
            var date = HttpContext.Current.Request.Params["date"];
            var cheque_name = HttpContext.Current.Request.Params["cheque"];
            var micr = HttpContext.Current.Request.Params["micr"];
            var accountno = HttpContext.Current.Request.Params["accountno"];
            var currency = HttpContext.Current.Request.Params["currency"];

            //Grayscale & Black and white
            String GS_Front_Image = "-", GS_Back_Image = "-", BW_Front_Image = "-", BW_Back_Image = "-";

            //

            String return_statement = "";

            //if (!fimage.Equals("") && !ftitle.Equals("") && !bimage.Equals("") && !btitle.Equals("") && !cheque_name.Equals("") && !amount.Equals("") && !date.Equals("") && !micr.Equals(""))
            //{
                String fpath = HttpContext.Current.Server.MapPath("~/Content/UploadedImages/");
                if (!System.IO.Directory.Exists(fpath))
                {
                    System.IO.Directory.CreateDirectory(fpath); //Create directory if it doesn't exist
                }
                
                // Cheque bank code, deposit limit
                Users customerInfoAcc = user.LoginMobileUser(Int32.Parse(accountno));
                MobileUserBranch mBranchDetails = mBranch.GetUserBranch(Int32.Parse(customerInfoAcc.Branchid),Int32.Parse(customerInfoAcc.Bankcode));

                //int postedChequeMicr = Int32.Parse(micr);
                int chqNoFromMicr = Int32.Parse(micr.Substring(0, 6));
                int branchCodeFromMicr = Int32.Parse(micr.Substring(6,5));
                int bankCodeFromMicr = Int32.Parse(micr.Substring(6,2));
                int chequeType = Int32.Parse(micr.Substring(12,2));
            int totalMicrCount = micr.Length;

            if (bankCodeFromMicr != mBranchDetails.Bankcode)
            {
                if (Double.Parse(amount) > 1000000)
                {
                    ch = new Cheque(
                        Convert.ToDouble(amount),
                         "Amount Above Limit: kes. 1,000,000",
                        "Amount Above Limit: kes. 1,000,000! \n",
                        "",
                        ""
                    );
                }else
                {
                    string fimageName = ftitle + ".jpg";
                    string fimgPath = Path.Combine(fpath, fimageName);
                    byte[] imageBytes = null;
                    //string fimgPath = path+"/"+ fimageName;
                    imageBytes = Convert.FromBase64String(fimage);
                    File.WriteAllBytes(fimgPath, imageBytes);

                    //test
                    /*using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(fimage)))
                    {
                        using (Bitmap bm2 = new Bitmap(ms))
                        {
                            bm2.Save(fimgPath);
                        }
                    }*/

                    //end test

                    string bimageName = btitle + ".jpg";
                    string bimgPath = Path.Combine(fpath, bimageName);
                    imageBytes = Convert.FromBase64String(bimage);
                    File.WriteAllBytes(bimgPath, imageBytes);

                    /*Save Grayscale image*/
                    imageBytes = Convert.FromBase64String(fimage);
                    Image tI = Image.FromFile(fimgPath);
                    Bitmap tBM = ourcheque.MakeGrayscale(new Bitmap(tI));
                    //tBM.SetResolution(100, 100);
                    tBM.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + ftitle + ".tiff"), ImageFormat.Tiff);
                    GS_Front_Image= "GS_"+ftitle +".tiff";

                    //*Save Blak and white/
                    //tBM = ourcheque.BlackAndWhite(new Bitmap(tI),new Rectangle(0,0,1920,1080));
                    tBM = ourcheque.MakeBlackAndWhite(new Bitmap(tI));
                    tBM.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/BW_" + ftitle + ".tiff"), ImageFormat.Tiff);
                    BW_Front_Image = "BW_" + ftitle + ".tiff";
                    /*/*/
                    /*using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(fimage)))
                    {
                        using (Bitmap bm1 = new Bitmap(ms))
                        {
                            //bm1.Save(fimgPath);
                            Image i = Image.FromFile(fimgPath);
                            Bitmap bm = new Bitmap(i);
                            Bitmap result = ourcheque.MakeGrayscale(bm);
                            result.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + ftitle + ".tiff"), ImageFormat.Tiff);
                        }
                    }*/
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(bimage)))
                    {
                        using (Bitmap bm1 = new Bitmap(ms))
                        {
                            //bm1.Save(fimgPath);
                            Image i = Image.FromFile(bimgPath);
                            Bitmap bm = new Bitmap(i);
                            Bitmap result = ourcheque.MakeGrayscale(bm);
                            result.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + btitle + ".tiff"), ImageFormat.Tiff);
                            GS_Back_Image = "GS_" + btitle + ".tiff";
                        }
                    }
                    /*End Save Grayscale image*/

                    var imageMicr = fimgPath;
                    Image im = Image.FromFile(fimgPath);
                    string frintImageName = "";
                    Bitmap b = new Bitmap(im);

                    List<String> micrResults = new List<String>();
                    String micrString = "";
                    fimageName = HttpContext.Current.Server.MapPath("~/Content/UploadedImages/" + ftitle + ".jpg");

                    /*
                     * 
                     * Scann MICR from cheque
                     * 
                     * Image imgXY = Image.FromFile(fimageName);
                    int  imgX = imgXY.Width;
                    int imgY = imgXY.Height;

                    Rectangle rect = new Rectangle(0, imgX/4, imgX, imgY/4);
                    Bitmap croppedImage = ourcheque.CropImage(b, rect);

                    if (croppedImage == null)
                    {
                        Console.WriteLine("not set");
                    }
                    else
                    {
                        imageBytes = ourcheque.ConvertBitMapToByteArray(croppedImage);
                        fimageName = HttpContext.Current.Server.MapPath("~/Content/MicrImages/"+fimageName);
                        File.WriteAllBytes(fimageName, imageBytes);
                    }

                    using (var ocrEngine = new TesseractEngine(HttpContext.Current.Server.MapPath("~/Content/Tessdata/"), "mcr", EngineMode.Default))
                    {
                        using (var imageWithText = Pix.LoadFromFile(fimageName))
                        //using (var imageWithText = Pix.LoadFromFile(blogPostImage))
                        {
                            using (var page = ocrEngine.Process(imageWithText))
                            {
                                using (var iter = page.GetIterator())
                                {
                                    iter.Begin();
                                    do
                                    {
                                        micrResults.Add(iter.GetText(PageIteratorLevel.TextLine));
                                        //Console.WriteLine("TextLine 1: " + iter.GetText(PageIteratorLevel.TextLine));

                                    } while (iter.Next(PageIteratorLevel.TextLine));
                                }

                                int i = 0;
                                foreach (String s in micrResults)
                                {
                                    i++;
                                    if (s.Length > 26)
                                        micrString = s;
                                        //Console.WriteLine("Line " + i + ": " + s);
                                }

                            }
                        }
                    }*/
                    GS_Front_Image = "GS_" + ftitle + ".tiff";
                    cheque = new Cheque(ftitle + ".jpg", btitle + ".jpg", date, Double.Parse(amount), cheque_name, micr, micr/*micrString*/, currency, Int32.Parse(accountno),GS_Front_Image,GS_Back_Image,BW_Front_Image,BW_Back_Image);

                    int cheque_saved = cheque.SaveCheque(cheque);
                    cheque_db = ourcheque.getChequeDetails(cheque_name);

                    ch = new Cheque(
                            Convert.ToDouble(amount),
                            "Image received successfully! \nCheque Unique ID: " + cheque_db.ChequeName
                            + "\nAmount: " + cheque_db.Amount +
                            "\nDate Issued: " + cheque_db.DateIssued
                            + "\nCheque MICR: " + cheque_db.micr
                            + "\n",
                            cheque_name.ToString(),
                            "Result.ToString()",
                            date.ToString()
                        );
                }
                return ch;
            }
            else 
            {
                string fimageName = ftitle + ".jpg";
                string fimgPath = Path.Combine(fpath, fimageName);
                byte[] imageBytes = null;
                //string fimgPath = path+"/"+ fimageName;
                /*imageBytes = Convert.FromBase64String(fimage);
                File.WriteAllBytes(fimgPath, imageBytes);*/

                //test
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(fimage)))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save(fimgPath);
                    }
                }

                //end test

                string bimageName = btitle + ".jpg";
                string bimgPath = Path.Combine(fpath, bimageName);
                imageBytes = Convert.FromBase64String(bimage);
                File.WriteAllBytes(bimgPath, imageBytes);

                /*Save Grayscale image*/
                imageBytes = Convert.FromBase64String(fimage);
                Image tI = Image.FromFile(fimgPath);
                Bitmap tBM = ourcheque.MakeGrayscale(new Bitmap(tI));
                tBM.SetResolution(100, 100);
                tBM.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + ftitle + ".tiff"), ImageFormat.Tiff);
                GS_Front_Image = "GS_" + ftitle + ".tiff";

                //*Save Blak and white/
                //tBM = ourcheque.BlackAndWhite(new Bitmap(tI),new Rectangle(0,0,1920,1080));
                tBM = ourcheque.MakeBlackAndWhite(new Bitmap(tI));
                tBM.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/BW_" + ftitle + ".tiff"), ImageFormat.Tiff);
                BW_Front_Image = "BW_" + ftitle + ".tiff";

                /*/*/
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(fimage)))
                {
                    using (Bitmap bm1 = new Bitmap(ms))
                    {
                        //bm1.Save(fimgPath);
                        Image i = Image.FromFile(fimgPath);
                        Bitmap bm = new Bitmap(i);
                        Bitmap result = ourcheque.MakeGrayscale(bm);
                        result.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + ftitle + ".tiff"), ImageFormat.Tiff);
                    }
                }
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(bimage)))
                {
                    using (Bitmap bm1 = new Bitmap(ms))
                    {
                        //bm1.Save(fimgPath);
                        Image i = Image.FromFile(bimgPath);
                        Bitmap bm = new Bitmap(i);
                        Bitmap result = ourcheque.MakeGrayscale(bm);
                        result.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/GS_" + btitle + ".tiff"), ImageFormat.Tiff);
                        GS_Back_Image = "GS_" + btitle + ".tiff";
                    }
                }
                /*End Save Grayscale image*/

                var imageMicr = fimgPath;
                Image im = Image.FromFile(fimgPath);
                string frintImageName = "";
                Bitmap b = new Bitmap(im);

                List<String> micrResults = new List<String>();
                String micrString = "";
                fimageName = HttpContext.Current.Server.MapPath("~/Content/UploadedImages/" + ftitle + ".jpg");

                /*
                 * 
                 * Scann MICR from cheque
                 * 
                 * Image imgXY = Image.FromFile(fimageName);
                int  imgX = imgXY.Width;
                int imgY = imgXY.Height;

                Rectangle rect = new Rectangle(0, imgX/4, imgX, imgY/4);
                Bitmap croppedImage = ourcheque.CropImage(b, rect);

                if (croppedImage == null)
                {
                    Console.WriteLine("not set");
                }
                else
                {
                    imageBytes = ourcheque.ConvertBitMapToByteArray(croppedImage);
                    fimageName = HttpContext.Current.Server.MapPath("~/Content/MicrImages/"+fimageName);
                    File.WriteAllBytes(fimageName, imageBytes);
                }

                using (var ocrEngine = new TesseractEngine(HttpContext.Current.Server.MapPath("~/Content/Tessdata/"), "mcr", EngineMode.Default))
                {
                    using (var imageWithText = Pix.LoadFromFile(fimageName))
                    //using (var imageWithText = Pix.LoadFromFile(blogPostImage))
                    {
                        using (var page = ocrEngine.Process(imageWithText))
                        {
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();
                                do
                                {
                                    micrResults.Add(iter.GetText(PageIteratorLevel.TextLine));
                                    //Console.WriteLine("TextLine 1: " + iter.GetText(PageIteratorLevel.TextLine));

                                } while (iter.Next(PageIteratorLevel.TextLine));
                            }

                            int i = 0;
                            foreach (String s in micrResults)
                            {
                                i++;
                                if (s.Length > 26)
                                    micrString = s;
                                    //Console.WriteLine("Line " + i + ": " + s);
                            }

                        }
                    }
                }*/

                //cheque = new Cheque(ftitle + ".jpg", btitle + ".jpg", date, Double.Parse(amount), cheque_name, micr, micr/*micrString*/, currency, Int32.Parse(accountno));
                cheque = new Cheque(ftitle + ".jpg", btitle + ".jpg", date, Double.Parse(amount), cheque_name, micr, micr/*micrString*/, currency, Int32.Parse(accountno), GS_Front_Image, GS_Back_Image, BW_Front_Image, BW_Back_Image);
                int cheque_saved = cheque.SaveCheque(cheque);
                cheque_db = ourcheque.getChequeDetails(cheque_name);

                ch = new Cheque(
                        Convert.ToDouble(amount),
                        "Image received successfully! \nCheque Unique ID: " + cheque_db.ChequeName
                        + "\nAmount: " + cheque_db.Amount +
                        "\nDate Issued: " + cheque_db.DateIssued
                        + "\nCheque MICR: " + cheque_db.micr
                        + "\n" /*postedChequeMicr+
                            "\n" + chqNoFromMicr +
                            "\n" + branchCodeFromMicr +
                            "\n" + bankCodeFromMicr +
                            "\n" + chequeType +
                            "\n" + totalMicrCount +
                            "\n" + drawersChequeAccNo*/,
                        cheque_name.ToString(),
                        "Result.ToString()",
                        date.ToString()
                    );
                //}

                return ch;
            }
            
        }

        /*[HttpPost]
        public String Upload()
        {
            var image = HttpContext.Current.Request.Params["image"];
            var title = HttpContext.Current.Request.Params["title"];

            String return_statement = "";
            String path = HttpContext.Current.Server.MapPath("~/Content/Uploads/"); //Path
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = title + ".jpg";
            string imgPath = Path.Combine(path, imageName);
            byte[] imageBytes = Convert.FromBase64String(image);
            File.WriteAllBytes(imgPath, imageBytes);

            return_statement = "Cheque received";

            return return_statement;
        }*/

        /*[HttpPost]
        public String Details()
        {
            var image = HttpContext.Current.Request.Params["image"];
            var title = HttpContext.Current.Request.Params["title"];

            return "Received "+image+" "+title;
        }*/

        /*public String ReceiveCheque([FromBody]JToken a) {
            c = new Cheque();
            JObject o = JObject.Parse(@"" + a);
            String image = a.Value<String>("Image");
            String title = a.Value<String>("Title");
            double amount= a.Value<double>("amount");
            String date = a.Value<String>("date");*/

        /*if (!image.Equals("") && !title.Equals(""))
        {
            string filePath = "~Content/Uploads/"+title+".jpg";
            File.WriteAllBytes(filePath, Convert.FromBase64String(image));
        }
        else if (amount!=0 && !date.Equals(""))
        {

        }*/

        /*return o.ToString();
    }*/

        /*[System.Web.Http.HttpPost]
        public String ReceiveCheque([FromBody]JToken a)
        {
            List<Car> carList = new List<Car>();
            JObject o = JObject.Parse(@"" + a);
            carList.Add(new Car(a.Value<String>("Color"), a.Value<String>("Make")));

            carList.Add(new Car(a.Value<String>("Color"), a.Value<String>("Make")));
            cheque = new Cheque(a.Value<String>("FImageName"), a.Value<String>("BImageName"), a.Value<String>("DateIssued"), a.Value<Double>("Amount"));

            return a.ToString();
        }*/
































        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        /*
         * // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/
    }
}