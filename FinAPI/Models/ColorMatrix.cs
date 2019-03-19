using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace FinAPI.Models
{
    public class Colormatrix
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Colormatrix()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Matrix containing the values of the ColorMatrix
        /// </summary>
        public float[][] Matrix { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// Applies the color matrix
        /// </summary>
        /// <param name="OriginalImage">Image sent in</param>
        /// <returns>An image with the color matrix applied</returns>
        public Bitmap Apply(Bitmap OriginalImage)
        {
            Bitmap NewBitmap = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            using (Graphics NewGraphics = Graphics.FromImage(NewBitmap))
            {
                System.Drawing.Imaging.ColorMatrix NewColorMatrix = new System.Drawing.Imaging.ColorMatrix(Matrix);
                using (ImageAttributes Attributes = new ImageAttributes())
                {
                    Attributes.SetColorMatrix(NewColorMatrix);
                    NewGraphics.DrawImage(OriginalImage,
                        new System.Drawing.Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                       0, 0, OriginalImage.Width, OriginalImage.Height,
                         GraphicsUnit.Pixel,
                        Attributes);
                }
            }
            return NewBitmap;
        }

        #endregion
    }
}