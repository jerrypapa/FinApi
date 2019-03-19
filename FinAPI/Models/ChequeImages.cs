using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class ChequeImages
    {
        [Display(Name = "ChequeImage")]
        public String ChequeImage { get; set; }

        [Display(Name = "BackChequeImage")]
        public String BackChequeImage { get; set; }

        [Display(Name = "FrontChequeImage")]
        public String FrontChequeImage { get; set; }

        [Display(Name = "GrayScale")]
        public String GrayScale { get; set; }

        [Display(Name = "BackGrayScale")]
        public String BackGrayScale { get; set; }

        [Display(Name = "FrontGrayScale")]
        public String FrontGrayScale { get; set; }

        [Display(Name = "BlackWhite")]
        public String BlackWhite { get; set; }

        [Display(Name = "BackBlackWhite")]
        public String BackBlackWhite { get; set; }

        [Display(Name = "FrontBlackWhite")]
        public String FrontBlackWhite { get; set; }

        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }

        public static Image BinaryToImage(System.Data.Linq.Binary binaryData)
        {
            if (binaryData == null) return null;

            byte[] buffer = binaryData.ToArray();
            MemoryStream memStream = new MemoryStream();
            memStream.Write(buffer, 0, buffer.Length);
            return Image.FromStream(memStream);
        }
    }
}