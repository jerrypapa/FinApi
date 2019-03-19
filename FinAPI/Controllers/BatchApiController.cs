using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace FinAPI.Controllers
{
    public class BatchApiController : ApiController
    {
        Batch batch = new Batch();
        Batches batches = new Batches();
        Slip slip = new Slip();
        OutCheques outcheques = new OutCheques();
        ChequeImage chequeImage = new ChequeImage();
        // GET: api/BatchApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BatchApi/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        public String S()
        {
            return "Res";
        }

        // POST: api/BatchApi
        [ResponseType(typeof(Batch))]
        public /*Batch*/String ReceiveBatch(/*[FromBody]string value*/Batches b)
        {
            String filepath = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("log something");
                 filepath= HttpContext.Current.Server.MapPath("~/Content/Logs/");
                // flush every 20 seconds as you do it
                File.AppendAllText(filepath + "log_" + b.BatchNo + ".txt", new JavaScriptSerializer().Serialize(b));
                File.AppendAllText(filepath + "BatchLogs.txt", "\n" + new DateTime().ToString()+"\n"+"Batch JSON received");

                sb.Clear();
            }
            catch(Exception e)
            {

            }

            String batch_insered = "";
            String slip_inserted = "";
            String outcheque_inserted = "";
            String chequeimage_inserted = "";
            String batch_response = "";
            int BatchId = 0;
            int SlipId = 0;
            int a = 0;
            OutCheques oc = new OutCheques();

            List<Slip> slipList=b.SlipList;
            List<OutCheques> outchequesList = b.OutChequeList;
            List<ChequeImage> ChequeImagesList = b.ChequeImagesList;
            byte[] ImageBtyes = null;
            Bitmap bmp;
            
            try
            {
                batch_insered = batches.InsertBatch(b);
                if (batch_insered.Equals("batchok"))
                {
                    try
                    {
                        File.AppendAllText(filepath + "BatchLogs.txt", "\n" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + "\n" + "Batch Inserted "+batch_insered);
                    }
                    catch(Exception e)
                    {

                    }
                    Batches insertedBatch = batches.GetBatch(b.BatchNo);
                    if (insertedBatch != null)
                    {
                        BatchId = insertedBatch.BatchId;
                        batch_response = batch_insered;
                        foreach (Slip s in slipList)
                        {
                            try
                            {
                                File.AppendAllText(filepath + "BatchLogs.txt","\n"+ new DateTime().ToString() + "\n" + "Slip :"+s.SlipNo);
                            }
                            catch (Exception e)
                            {

                            }
                            s.BatchId = BatchId;
                            slip_inserted = slip.InsertSlip(s);
                            if (!slip_inserted.Equals("slipok"))
                            {
                                try
                                {
                                    File.AppendAllText(filepath + "BatchLogs.txt", "\n" + new DateTime().ToString() + "\n" + "slipnotok :" + s.SlipNo);
                                }
                                catch (Exception e)
                                {

                                }
                                batch_response = slip_inserted;
                                break;
                            }else if(slip_inserted.Equals("slipok"))
                            {
                                try
                                {
                                    File.AppendAllText(filepath + "BatchLogs.txt", "\n" + new DateTime().ToString() + "\n" + "slipok :" + s.SlipNo);
                                }
                                catch (Exception e)
                                {

                                }
                                Slip insertedSlip = slip.GetSlip(s.SlipNo);
                                if (insertedSlip != null)
                                {
                                    SlipId = insertedSlip.SlipId;
                                    foreach (OutCheques o in outchequesList)
                                    {
                                        try
                                        {
                                            File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + " \n" + "Outcheque :" + o.ProcNo);
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                        if (o.SlipNo.Equals(s.SlipNo))
                                        {
                                            o.SlipId = SlipId;
                                            outcheque_inserted = outcheques.InsertOutCheque(o);
                                            if (!outcheque_inserted.Equals("outchequeok"))
                                            {
                                                try
                                                {
                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + " \n" + "outchequenotok :" + o.ProcNo+" "+outcheque_inserted);
                                                }
                                                catch (Exception e)
                                                {

                                                }
                                                batch_response = slip_inserted;
                                                break;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + " \n" + "outchequeok :" + o.ProcNo);
                                                }
                                                catch (Exception e)
                                                {

                                                }
                                                if (ChequeImagesList.Count > 0)
                                                {
                                                    try
                                                    {
                                                        File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "ChequeImagesList.Count :" + ChequeImagesList.Count);
                                                    }
                                                    catch (Exception e)
                                                    {

                                                    }
                                                    foreach (ChequeImage c in ChequeImagesList)
                                                    {
                                                        Bitmap frontBnW = null;
                                                        try
                                                        {
                                                            File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "ChequeImage :" + c.ProcNo+" Face"+c.ImageFace);
                                                        }
                                                        catch (Exception e)
                                                        {

                                                        }
                                                        ImageBtyes = chequeImage.ImageStringToBinary(c.ImageString);
                                                        using (var ms = new MemoryStream(ImageBtyes))
                                                        {
                                                            bmp = new Bitmap(ms);
                                                        }
                                                        Bitmap saveBmp = chequeImage.ConvertToGrayScale(bmp);

                                                        if (c.ImageFace.Equals("front"))
                                                        {
                                                            
                                                            c.ImageTypeId = 1;
                                                            c.Image = (Image)saveBmp;
                                                            c.ImageBytes_ = chequeImage.ConvertBitMapToByteArray(saveBmp);
                                                            try
                                                            {
                                                                File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "ChequeImage ImageFace :" + c.ImageFace);
                                                            }
                                                            catch (Exception e)
                                                            {

                                                            }
                                                            try
                                                            {
                                                                //c.ImageTypeId = 1;
                                                                saveBmp.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/Grayscale_front" + c.ProcNo + ".tiff"), ImageFormat.Tiff);
                                                            }catch(Exception e)
                                                            {

                                                            }
                                                        }
                                                        else if (c.ImageFace.Equals("back"))
                                                        {
                                                            c.ImageTypeId = 2;
                                                            c.Image = (Image)saveBmp;
                                                            c.ImageBytes_ = chequeImage.ConvertBitMapToByteArray(saveBmp);
                                                            try
                                                            {
                                                                File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "ChequeImage ImageFace :" + c.ImageFace);
                                                            }
                                                            catch (Exception e)
                                                            {

                                                            }
                                                            //c.ImageTypeId = 2;
                                                            saveBmp.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/Grayscale_back" + c.ProcNo + ".tiff"), ImageFormat.Tiff);
                                                        }
                                                        if (c.ProcNo.Equals(o.ProcNo))
                                                        {
                                                            chequeimage_inserted = chequeImage.InsertChequeImage(c);
                                                            if (chequeimage_inserted.Equals("chequeimageok"))
                                                            {
                                                                try
                                                                {
                                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "chequeimage_inserted :" + c.ProcNo + " " +chequeimage_inserted);
                                                                }
                                                                catch (Exception e)
                                                                {

                                                                }
                                                                batch_response = chequeimage_inserted;
                                                            }
                                                            else
                                                            {
                                                                try
                                                                {
                                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "chequeimage_not_inserted :" + c.ProcNo+" Reason: "+ chequeimage_inserted);
                                                                }
                                                                catch (Exception e)
                                                                {

                                                                }
                                                                batch_response = chequeimage_inserted;
                                                                break;
                                                            }
                                                            if (c.ImageFace.Equals("front"))
                                                            {
                                                                try
                                                                {
                                                                    frontBnW = chequeImage.ConvertBlackAndWhite(bmp);
                                                                    
                                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "Front BnW:" + c.ProcNo);
                                                                    c.ImageTypeId = 3;
                                                                    c.Image = (Image)frontBnW;
                                                                    c.ImageBytes_ = chequeImage.ConvertBitMapToBnWByteArray(frontBnW);
                                                                    String frontBnW_inserted = chequeImage.InsertChequeImage(c);
                                                                    if (frontBnW_inserted.Equals("chequeimageok"))
                                                                    {
                                                                        File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "Front BnW ChequeImage  :" + c.ProcNo + " " + frontBnW_inserted);
                                                                    }
                                                                    else
                                                                    {
                                                                        File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "Front BnW ChequeImage  :" + c.ProcNo + " " + frontBnW_inserted);
                                                                    }
                                                                    frontBnW.Save(HttpContext.Current.Server.MapPath("~/Content/UploadedImages/BnW_front" + c.ProcNo + ".tiff"), ImageFormat.Tiff);
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    File.AppendAllText(filepath + "BatchLogs.txt", " \n" + new DateTime().ToString() + "\n" + "Front BnW:" + c.ProcNo+" "+e.Message + "\n" +e.StackTrace);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    batch_response = "chequeimagesempty";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string.Concat(batch_response, "insertedSlip null");
                                }
                            }
                            
                        }
                    }else
                    {

                    }
                }
                else
                {
                    try
                    {
                        File.AppendAllText(filepath + "BatchLogs.txt", "\n" + new DateTime().ToString() + "\n" + "batch_not_inserted :" + b.BatchNo);
                    }
                    catch (Exception e)
                    {

                    }
                    batch_response = batch_insered;
                }
                return batch_response+ " OutChequeList: "+outchequesList.Count.ToString();
            }
            catch(Exception e)
            {
                return e.Message + "\n" + e.StackTrace;
            }
        }

        // PUT: api/BatchApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BatchApi/5
        public void Delete(int id)
        {
        }
    }
}
