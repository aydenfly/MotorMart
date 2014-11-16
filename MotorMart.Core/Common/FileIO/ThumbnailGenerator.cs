using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace MotorMart.Core.Common.FileIO
{
    public class ThumbnailGenerator
    {
        public ArrayList Messages { get; set; }

        public ThumbnailGenerator()
        {
            Messages = new ArrayList();
        }

        public bool GenerateThumbnail(string SourceImage, string TargetFile, int width, int height)
        {
            string sExtension = SourceImage.Substring(SourceImage.LastIndexOf(".") + 1); 
            
            if (sExtension.ToLower() == "jpg" || sExtension.ToLower() == "gif" || sExtension.ToLower() == "bmp" || sExtension.ToLower() == "jpeg")
            {
                try
                {
                    Bitmap bmp = new Bitmap(SourceImage);

                    Bitmap thumb = new Bitmap(width, height);

                    Graphics g = Graphics.FromImage(thumb);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                    Rectangle Resized = GetScaledRectangle(bmp, new Rectangle(0, 0, width, height));

                    g.DrawImage(bmp, Resized);


                    //Set Image codec of JPEG type, the index of JPEG codec is "1"
                    System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];

                    //Set the parameters for defining the quality of the thumbnail... here it is set to 100%
                    System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(1);
                    eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);

                    thumb.Save(TargetFile, codec, eParams);

                    Messages.Add("Thumbnail created successfully");
                    return true;
                        
                    
                }
                catch (Exception ex)
                {
                    Messages.Add(ex.Message);
                }
            }
            else
            {
                Messages.Add("File is not an image file");
            }
            
            return false;
        }

        public static Rectangle GetScaledRectangle(Image img, Rectangle thumbRect)
        {
            if (img.Width < thumbRect.Width && img.Height < thumbRect.Height)
                return new Rectangle(thumbRect.X + ((thumbRect.Width - img.Width) / 2), thumbRect.Y + ((thumbRect.Height - img.Height) / 2), img.Width, img.Height);

            int sourceWidth = img.Width;
            int sourceHeight = img.Height;

            int targetWidth = thumbRect.Width;
            int targetHeight = thumbRect.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)thumbRect.Width / (float)sourceWidth);
            nPercentH = ((float)thumbRect.Height / (float)sourceHeight);

            // This bit needs fixing! Ensure % does not push the 
            // width or height below the detination width or height
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            if (destWidth.Equals(0))
                destWidth = 1;
            if (destHeight.Equals(0))
                destHeight = 1;

            Rectangle retRect = new Rectangle(thumbRect.X, thumbRect.Y, destWidth, destHeight);


            int differenceH = 0;
            int differenceW = 0;

            // Scale the image to fill the minimum size of the thumbnail
            if (retRect.Height < thumbRect.Height - 1)
            {   
                differenceH = thumbRect.Height - retRect.Height;
                nPercent = ((float)(retRect.Height + differenceH) / (float)retRect.Height);
                retRect.Height += differenceH;
                retRect.Width += (int)Math.Round(differenceH * nPercent);
            }
            if (retRect.Width < thumbRect.Width)
            {   
                differenceW = thumbRect.Width - retRect.Width;
                nPercent = ((float)(retRect.Width + differenceW) / (float)retRect.Width);
                retRect.Width += differenceW;
                retRect.Height += (int)Math.Round(differenceW * nPercent);
            }

            // Centre the Rectangle over the thumbnail
            if (retRect.Width > thumbRect.Width)
            {
                differenceW = retRect.Width - thumbRect.Width;
                retRect.X -= (int)Math.Round(Convert.ToDouble(differenceW / 2));
            }

            if (retRect.Height > thumbRect.Height)
            {
                differenceH = retRect.Height - thumbRect.Height;
                retRect.Y -= (int)Math.Round(Convert.ToDouble(differenceH / 2));
            }

            return retRect;
        }
    }
}