using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;

namespace MotorMart.Core.HtmlHelpers
{
    public static class SelectListHelper
    {
        #region SelectLists

        #region Vehicle select list

        public static SelectList GetDealerSelect(IList<dealer> DealerList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Please select...", Value = "" });
            foreach (var item in DealerList)
            {
                Items.Add(new SelectListItem { Text = item.name, Value = item.dealerid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetDistanceSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Distance (select)", Value = "" });
            Items.Add(new SelectListItem { Text = "Within 1 mile", Value = "1" });
            Items.Add(new SelectListItem { Text = "Within 10 miles", Value = "10" });
            Items.Add(new SelectListItem { Text = "Within 20 miles", Value = "20" });
            Items.Add(new SelectListItem { Text = "Within 30 miles", Value = "30" });
            Items.Add(new SelectListItem { Text = "Within 40 miles", Value = "40" });
            Items.Add(new SelectListItem { Text = "Within 50 miles", Value = "50" });
            Items.Add(new SelectListItem { Text = "Within 60 miles", Value = "60" });
            Items.Add(new SelectListItem { Text = "Within 100 miles", Value = "100" });
            Items.Add(new SelectListItem { Text = "Within 200 miles", Value = "200" });
            Items.Add(new SelectListItem { Text = "National", Value = "-1" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetAdminVehicleMakeSelect(IList<make> VehicleMakeList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Please select...", Value = "" });
            foreach (var item in VehicleMakeList)
            {
                Items.Add(new SelectListItem { Text = item.name.ToUpper(), Value = item.makeid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetVehicleMakeSelect(IList<make> VehicleMakeList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Make (All)", Value = "" });
            foreach (var item in VehicleMakeList)
            {
                Items.Add(new SelectListItem { Text = item.name.ToUpper(), Value = item.makeid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetVehicleModelSelect(IList<model> VehicleModelList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Model (Any)", Value = "" });
            foreach (var item in VehicleModelList.Distinct())
            {
                Items.Add(new SelectListItem { Text = item.name, Value = item.modelid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetFuelTypeSelect(IList<fueltype> FuelTypeList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Fuel type (Any)", Value = "" });
            foreach (var item in FuelTypeList.Distinct())
            {
                Items.Add(new SelectListItem { Text = item.type, Value = item.fueltypeid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetAgeSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Age (Any)", Value = "" });
            Items.Add(new SelectListItem { Text = "Up to 1 year old", Value = "1" });
            Items.Add(new SelectListItem { Text = "Up to 2 years old", Value = "2" });
            Items.Add(new SelectListItem { Text = "Up to 3 years old", Value = "3" });
            Items.Add(new SelectListItem { Text = "Up to 4 years old", Value = "4" });
            Items.Add(new SelectListItem { Text = "Up to 5 years old", Value = "5" });
            Items.Add(new SelectListItem { Text = "Up to 6 years old", Value = "6" });
            Items.Add(new SelectListItem { Text = "Up to 7 years old", Value = "7" });
            Items.Add(new SelectListItem { Text = "Up to 8 years old", Value = "8" });
            Items.Add(new SelectListItem { Text = "Over 8 years old", Value = "9" });
            Items.Add(new SelectListItem { Text = "Unlisted", Value = "10" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetMinPriceSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Min price", Value = "" });
            Items.Add(new SelectListItem { Text = "£150", Value = "150" });
            Items.Add(new SelectListItem { Text = "£200", Value = "200" });
            Items.Add(new SelectListItem { Text = "£500", Value = "500" });
            Items.Add(new SelectListItem { Text = "£800", Value = "800" });
            Items.Add(new SelectListItem { Text = "£1000", Value = "1000" });
            Items.Add(new SelectListItem { Text = "£1500", Value = "1500" });
            Items.Add(new SelectListItem { Text = "£3000", Value = "3000" });
            Items.Add(new SelectListItem { Text = "£5000", Value = "5000" });
            Items.Add(new SelectListItem { Text = "£10000", Value = "10000" });
            Items.Add(new SelectListItem { Text = "£50000", Value = "50000" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetMaxPriceSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Max price", Value = "" });
            Items.Add(new SelectListItem { Text = "£1500", Value = "1500" });
            Items.Add(new SelectListItem { Text = "£2000", Value = "2000" });
            Items.Add(new SelectListItem { Text = "£5000", Value = "5000" });
            Items.Add(new SelectListItem { Text = "£8000", Value = "8000" });
            Items.Add(new SelectListItem { Text = "£10000", Value = "10000" });
            Items.Add(new SelectListItem { Text = "£15000", Value = "15000" });
            Items.Add(new SelectListItem { Text = "£30000", Value = "30000" });
            Items.Add(new SelectListItem { Text = "£50000", Value = "50000" });
            Items.Add(new SelectListItem { Text = "£100000", Value = "100000" });
            Items.Add(new SelectListItem { Text = "£500000", Value = "500000" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetBodyTypeSelect(IList<bodytype> BodyTypeList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Body type (All)", Value = "" });
            foreach (var item in BodyTypeList)
            {
                Items.Add(new SelectListItem { Text = item.type, Value = item.bodytypeid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetMileageSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Mileage (Any)", Value = "" });
            Items.Add(new SelectListItem { Text = "Up to 5000 miles", Value = "5000" });
            Items.Add(new SelectListItem { Text = "Up to 10,000 miles", Value = "10000" });
            Items.Add(new SelectListItem { Text = "Up to 20,000 miles", Value = "20000" });
            Items.Add(new SelectListItem { Text = "Up to 40,000 miles", Value = "40000" });
            Items.Add(new SelectListItem { Text = "Up to 60,000 miles", Value = "60000" });
            Items.Add(new SelectListItem { Text = "Up to 80,000 miles", Value = "80000" });
            Items.Add(new SelectListItem { Text = "Up to 100,000 miles", Value = "100000" });
            Items.Add(new SelectListItem { Text = "Up to 200,000 miles", Value = "200000" });
            Items.Add(new SelectListItem { Text = "Over 200,000 miles", Value = "300000" });
            Items.Add(new SelectListItem { Text = "Unlisted", Value = "unlisted" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetTransmissionSelect(IList<transmission> TransmissionList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Transmission (Any)", Value = "" });
            foreach (var item in TransmissionList)
            {
                Items.Add(new SelectListItem { Text = item.name, Value = item.transmissionid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetEngineSizeSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Engine size (Any)", Value = "" });
            Items.Add(new SelectListItem { Text = "Less than 1L", Value = "1" });
            Items.Add(new SelectListItem { Text = "1L - 1.3L", Value = "2" });
            Items.Add(new SelectListItem { Text = "1.4L - 1.6L", Value = "3" });
            Items.Add(new SelectListItem { Text = "1.7L - 1.9L", Value = "4" });
            Items.Add(new SelectListItem { Text = "2L - 2.5L", Value = "5" });
            Items.Add(new SelectListItem { Text = "2.6L - 2.9L", Value = "6" });
            Items.Add(new SelectListItem { Text = "3L - 3.9L", Value = "7" });
            Items.Add(new SelectListItem { Text = "4L - 4.9L", Value = "8" });
            Items.Add(new SelectListItem { Text = "Over 5L", Value = "9" });
            Items.Add(new SelectListItem { Text = "Unlisted", Value = "10" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetVehicleColorSelect(IList<color> ColorList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Color (Any)", Value = "" });
            foreach (var item in ColorList)
            {
                Items.Add(new SelectListItem { Text = item.name, Value = item.colorid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetCountrySelect(IList<country> CountryList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Select a country...", Value = "" });
            foreach (var item in CountryList)
            {
                Items.Add(new SelectListItem { Text = item.name, Value = item.countryid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetNumberOfDoorsSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "No. of doors", Value = "" });
            Items.Add(new SelectListItem { Text = "2 doors", Value = "2" });
            Items.Add(new SelectListItem { Text = "3 doors", Value = "3" });
            Items.Add(new SelectListItem { Text = "4 doors", Value = "4" });
            Items.Add(new SelectListItem { Text = "5 doors", Value = "5" });
            Items.Add(new SelectListItem { Text = "Unlisted", Value = "unlisted" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList GetSortBySelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Price (Lowest)", Value = "1" });
            Items.Add(new SelectListItem { Text = "Price (Highest)", Value = "2" });
            Items.Add(new SelectListItem { Text = "Distance (Furthest)", Value = "3" });
            Items.Add(new SelectListItem { Text = "Distance (Nearest)", Value = "4" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList HourSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "00", Value = "00" });
            Items.Add(new SelectListItem { Text = "01", Value = "01" });
            Items.Add(new SelectListItem { Text = "02", Value = "02" });
            Items.Add(new SelectListItem { Text = "03", Value = "03" });
            Items.Add(new SelectListItem { Text = "04", Value = "04" });
            Items.Add(new SelectListItem { Text = "05", Value = "05" });
            Items.Add(new SelectListItem { Text = "06", Value = "06" });
            Items.Add(new SelectListItem { Text = "07", Value = "07" });
            Items.Add(new SelectListItem { Text = "08", Value = "08" });
            Items.Add(new SelectListItem { Text = "09", Value = "09" });
            Items.Add(new SelectListItem { Text = "10", Value = "10" });
            Items.Add(new SelectListItem { Text = "11", Value = "11" });
            Items.Add(new SelectListItem { Text = "12", Value = "12" });
            Items.Add(new SelectListItem { Text = "13", Value = "13" });
            Items.Add(new SelectListItem { Text = "14", Value = "14" });
            Items.Add(new SelectListItem { Text = "15", Value = "15" });
            Items.Add(new SelectListItem { Text = "16", Value = "16" });
            Items.Add(new SelectListItem { Text = "17", Value = "17" });
            Items.Add(new SelectListItem { Text = "18", Value = "18" });
            Items.Add(new SelectListItem { Text = "19", Value = "19" });
            Items.Add(new SelectListItem { Text = "20", Value = "20" });
            Items.Add(new SelectListItem { Text = "21", Value = "21" });
            Items.Add(new SelectListItem { Text = "22", Value = "22" });
            Items.Add(new SelectListItem { Text = "23", Value = "23" });
            return new SelectList(Items, "Value", "Text", Selected);
        }

        public static SelectList MinutesSelect(int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "00", Value = "00" });
            Items.Add(new SelectListItem { Text = "01", Value = "01" });
            Items.Add(new SelectListItem { Text = "02", Value = "02" });
            Items.Add(new SelectListItem { Text = "03", Value = "03" });
            Items.Add(new SelectListItem { Text = "04", Value = "04" });
            Items.Add(new SelectListItem { Text = "05", Value = "05" });
            Items.Add(new SelectListItem { Text = "06", Value = "06" });
            Items.Add(new SelectListItem { Text = "07", Value = "07" });
            Items.Add(new SelectListItem { Text = "08", Value = "08" });
            Items.Add(new SelectListItem { Text = "09", Value = "09" });
            Items.Add(new SelectListItem { Text = "10", Value = "10" });
            Items.Add(new SelectListItem { Text = "11", Value = "11" });
            Items.Add(new SelectListItem { Text = "12", Value = "12" });
            Items.Add(new SelectListItem { Text = "13", Value = "13" });
            Items.Add(new SelectListItem { Text = "14", Value = "14" });
            Items.Add(new SelectListItem { Text = "15", Value = "15" });
            Items.Add(new SelectListItem { Text = "16", Value = "16" });
            Items.Add(new SelectListItem { Text = "17", Value = "17" });
            Items.Add(new SelectListItem { Text = "18", Value = "18" });
            Items.Add(new SelectListItem { Text = "19", Value = "19" });
            Items.Add(new SelectListItem { Text = "20", Value = "20" });
            Items.Add(new SelectListItem { Text = "21", Value = "21" });
            Items.Add(new SelectListItem { Text = "22", Value = "22" });
            Items.Add(new SelectListItem { Text = "23", Value = "23" });
            Items.Add(new SelectListItem { Text = "24", Value = "24" });
            Items.Add(new SelectListItem { Text = "25", Value = "25" });
            Items.Add(new SelectListItem { Text = "26", Value = "26" });
            Items.Add(new SelectListItem { Text = "27", Value = "27" });
            Items.Add(new SelectListItem { Text = "28", Value = "28" });
            Items.Add(new SelectListItem { Text = "29", Value = "29" });

            Items.Add(new SelectListItem { Text = "30", Value = "30" });
            Items.Add(new SelectListItem { Text = "31", Value = "31" });
            Items.Add(new SelectListItem { Text = "32", Value = "32" });
            Items.Add(new SelectListItem { Text = "33", Value = "33" });
            Items.Add(new SelectListItem { Text = "34", Value = "34" });
            Items.Add(new SelectListItem { Text = "35", Value = "35" });
            Items.Add(new SelectListItem { Text = "36", Value = "36" });
            Items.Add(new SelectListItem { Text = "37", Value = "37" });
            Items.Add(new SelectListItem { Text = "38", Value = "38" });
            Items.Add(new SelectListItem { Text = "39", Value = "39" });

            Items.Add(new SelectListItem { Text = "40", Value = "40" });
            Items.Add(new SelectListItem { Text = "41", Value = "41" });
            Items.Add(new SelectListItem { Text = "42", Value = "42" });
            Items.Add(new SelectListItem { Text = "43", Value = "43" });
            Items.Add(new SelectListItem { Text = "44", Value = "44" });
            Items.Add(new SelectListItem { Text = "45", Value = "45" });
            Items.Add(new SelectListItem { Text = "46", Value = "46" });
            Items.Add(new SelectListItem { Text = "47", Value = "47" });
            Items.Add(new SelectListItem { Text = "48", Value = "48" });
            Items.Add(new SelectListItem { Text = "49", Value = "49" });

            Items.Add(new SelectListItem { Text = "50", Value = "50" });
            Items.Add(new SelectListItem { Text = "51", Value = "51" });
            Items.Add(new SelectListItem { Text = "52", Value = "52" });
            Items.Add(new SelectListItem { Text = "53", Value = "53" });
            Items.Add(new SelectListItem { Text = "54", Value = "54" });
            Items.Add(new SelectListItem { Text = "55", Value = "55" });
            Items.Add(new SelectListItem { Text = "56", Value = "56" });
            Items.Add(new SelectListItem { Text = "57", Value = "57" });
            Items.Add(new SelectListItem { Text = "58", Value = "58" });
            Items.Add(new SelectListItem { Text = "59", Value = "59" });

            return new SelectList(Items, "Value", "Text", Selected);
        }

        #endregion

        #region Sitemap Select list
        public static SelectList SitemapSelectList(sitemap CurrentSitemap, IList<sitemap> SitemapList, int? SelectedId)
        {
            List<sitemap> l = new List<sitemap>();

            if (CurrentSitemap == null) CurrentSitemap = new sitemap();

            foreach (var s in SitemapList.Where(s => s.sitemapid != CurrentSitemap.sitemapid))
            {
                l.Add(new sitemap { sitemapid = s.sitemapid, title = MotorMart.Core.Common.SiteMapHelper.SitemapPath(s, SitemapList).ToString() });
            }

            l = l.OrderBy(s => s.title).ToList();

            return new SelectList(l.ToSelectList(s => s.title, s => s.sitemapid.ToString(), (s => s.sitemapid == SelectedId), null), "value", "text");
        }

        #endregion

        #endregion

        #region SMS carrier Select list

        public static SelectList GetSMSCarrierSelect(IList<smscarrier> SMSCarriersList, int Selected)
        {
            List<SelectListItem> Items = new List<SelectListItem>();
            Items.Add(new SelectListItem { Text = "Select a carrier...", Value = "" });
            foreach (var item in SMSCarriersList)
            {
                Items.Add(new SelectListItem { Text = item.carrier, Value = item.smscarrierid.ToString() });
            }
            return new SelectList(Items, "Value", "Text", Selected);
        }

        #endregion



    }
}