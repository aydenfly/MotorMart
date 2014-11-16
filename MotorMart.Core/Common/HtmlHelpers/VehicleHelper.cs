using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMart.Core.Common
{
    public static class VehicleHelper
    {
        public static string ImageFileTargetDirectory()
        {
            return String.Format("{0}{1}", GlobalSettings.ClientSiteBaseDir, GlobalSettings.VehicleImageDirectory);
        }

        public static string ImageWebTargetDirectory()
        {
            return String.Format("{0}", GlobalSettings.VehicleImageDirectory);
        }

        public static string ImageFileThumbDirectory(string[] Dimension)
        {
            return FileHelper.ImageThumbDirectory(ImageFileTargetDirectory(), Dimension);
        }

        public static string ImageWebThumbDirectory(string[] Dimension)
        {
            return FileHelper.ImageThumbDirectory(ImageWebTargetDirectory(), Dimension);
        }

        public static string[] ImageDimensions()
        {
            return FileHelper.ImageDimensions(GlobalSettings.VehicleImageDimensions);
        }
    }
}