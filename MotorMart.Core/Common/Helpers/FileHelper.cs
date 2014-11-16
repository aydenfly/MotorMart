using System.Text.RegularExpressions;
using System;
using System.Text;

namespace MotorMart.Core.Common
{
    public class FileHelper
    {
        public static string[] ImageDimensions(string ImageSizes)
        {            
            string[] Dimensions = ImageSizes.Split(Convert.ToChar(";"));
            for (int a = 0; a < Dimensions.Length; a++)
            {
                Dimensions[a] = Dimensions[a].Trim().ToLower();
            }

            return Dimensions;
        }

        public static string[] ImageDimension(string DimensionsPart)
        {
            string[] Dimension = DimensionsPart.Split(Convert.ToChar("x"));
            return Dimension;
        }

        public static int ImageDimensionWidth(string[] Dimension)
        {
            int Width = Convert.ToInt32(Dimension[0]);
            return Width;
        }

        public static int ImageDimensionHeight(string[] Dimension)
        {
            int Height = Convert.ToInt32(Dimension[1]);
            return Height;
        }

        public static string ImageThumbDirectory(string RootDirectory, string[] Dimension)
        {
            string TargetThumbDirectory = String.Format("{0}/{1}x{2}/", RootDirectory, FileHelper.ImageDimensionWidth(Dimension), FileHelper.ImageDimensionHeight(Dimension));
            return TargetThumbDirectory;
        }        

        public static string ImageThumbDirectory(string RootDirectory, string Dimension)
        {
            string Directory = String.Format("{0}{1}", RootDirectory, Dimension);
            if (!Directory.EndsWith("/")) Directory += "/";
            Directory = Directory.Replace("//", "/");
            return Directory;
        }
    }
}
