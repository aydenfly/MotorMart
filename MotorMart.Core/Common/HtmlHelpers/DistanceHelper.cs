using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Web.Script.Serialization;
using HtmlAgilityPack;

namespace MotorMart.Core.Common
{
    public class DistanceHelper
    {
        #region Scrap distance from WebPage
        public static string StartFragment = "<li id=\"altroute_0";
        public static string EndFragment = "<div class=\"dir-altroute-clear\"></div></div></li>";
        public static string MatchContainerStart = "<div class=\"altroute-rcol altroute-info\">";
        public static string MatchContainerEnd = "</div><div>";
        
        private static string MiddleFragment;
        private static string DistanceFragment;

        public static string visitorPostCodePartA;
        public static string visitorPostCodePartB;
        public static string dealerPostCodePartA;
        public static string dealerPostCodePartB;

        private static void PreparePostCodes(string visitorPostcode, string dealerPostcode)
        {
            string[] vparts = visitorPostcode.Split(' ');
            if (vparts.Length > 1)
            {
                visitorPostCodePartA = vparts[0];
                visitorPostCodePartB = vparts[1];
            }

            string[] dparts = dealerPostcode.Split(' ');
            if (dparts.Length > 1)
            {
                dealerPostCodePartA = dparts[0];
                dealerPostCodePartB = dparts[1];
            }
        }

        //"http://maps.google.co.uk/maps?f=q&hl=en&geocode=&q=from:+DE1+3GU+to:+DE4+2BA";
        private static string GoogleUrl;

        
        private static string GetGoogleHtmlPage(string strURL)
        {
            String strResult;
            WebResponse objResponse;
            WebRequest objRequest = HttpWebRequest.Create(strURL);
            objResponse = objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

        private static string ProcessHtmlResponse(string PageContent)
        {
            MiddleFragment = PageContent.Substring(PageContent.IndexOf(StartFragment), PageContent.Length - (PageContent.IndexOf(StartFragment) + (PageContent.Length - PageContent.IndexOf(EndFragment))));
            DistanceFragment = MiddleFragment.Substring(MiddleFragment.IndexOf(MatchContainerStart) + 43, MiddleFragment.Length - (MiddleFragment.IndexOf(MatchContainerStart) + (MiddleFragment.Length - MiddleFragment.IndexOf(MatchContainerEnd)))).Replace(MatchContainerStart, String.Empty).Replace(MatchContainerEnd, String.Empty);
            string [] parts = DistanceFragment.Split(',');
            string [] value = parts[0].ToString().Split(' ');
            return value[0].ToString();
 

             
        }

        public static string ClearEmptyTag()
        {
            string MyString = "<p style=\"font-family:Arial\">{SectionHeader}</p>";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(MyString);
            foreach (HtmlNode par in doc.DocumentNode.SelectNodes("//p"))
            {
                if (1 == 1)
                {
                    par.Remove();
                }
            }
            
            return doc.DocumentNode.InnerHtml;
        }

        public static string GetDealerDistance(string visitorpostcode, string dealerpostcode)
        {
            PreparePostCodes(visitorpostcode, dealerpostcode);

            GoogleUrl = String.Concat("http://maps.google.co.uk/maps?f=q&hl=en&geocode=&q=from:+", visitorPostCodePartA, "+", visitorPostCodePartB, "+to:+", dealerPostCodePartA, "+", dealerPostCodePartB);
            string HtmlResponse = GetGoogleHtmlPage(GoogleUrl);

            return ProcessHtmlResponse(HtmlResponse);
        }

        #endregion

        #region Distance Calculator
        public enum DistanceUnits
        {
            Miles,
            Kilometers,
            NauticalMiles
        }

        public static double GetDistance(double lat1, double lon1, double lat2, double lon2, string units)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(ConvertDegToRad(lat1)) * Math.Sin(ConvertDegToRad(lat2)) + Math.Cos(ConvertDegToRad(lat1)) * Math.Cos(ConvertDegToRad(lat2)) * Math.Cos(ConvertDegToRad(theta));
            dist = Math.Acos(dist);
            dist = ConvertRadToDeg(dist);
            dist = dist * 60 * 1.1515;
            if (units == DistanceUnits.Kilometers.ToString())
            {
                dist = dist * 1.609344;
            }
            else if (units == DistanceUnits.NauticalMiles.ToString())
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
                
        //::  This function converts decimal degrees to radians
        private static double ConvertDegToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        
        //::  This function converts radians to decimal degrees        
        private static double ConvertRadToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);

        }

        public class CoordianteLookUp
        {
            public List<LookUpResult> postalcodes { get; set; }
        }

        public class LookUpResult
        {
            public string lat { get; set; }
            public string lng { get; set; }
            public string countryCode { get; set; }
        }

        public static string LookUpCoordinates(string PostalCode)
        {
            string Coordinates = String.Empty;
            string Latitude = String.Empty;
            string Longitude = String.Empty;
            string CountryCode = String.Empty;

            string LookUpUrl = String.Concat("http://api.geonames.org/postalCodeLookupJSON?postalcode=", PostalCode, "&username=motormart");

            WebRequest webRequest = WebRequest.Create(LookUpUrl);
            WebResponse webResponse = webRequest.GetResponse();

            if (webResponse != null)
            {
                using (StreamReader sReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string json = sReader.ReadToEnd().Trim();

                    if (!String.IsNullOrEmpty(json))
                    {
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        CoordianteLookUp dic = jsSerializer.Deserialize<CoordianteLookUp>(json);

                        foreach (var item in dic.postalcodes)
                        {
                            //Extract latitude from the Latitude Value
                            Latitude = item.lat;

                            //Extract the longitude from the Longitude Value
                            Longitude = item.lng;

                            //Extract countrycode from CountyCode value
                            CountryCode = item.countryCode;
                        }

                        Coordinates = String.Format("{0},{1},{2}", Latitude, Longitude, CountryCode);
                    }
                }
            }
            return Coordinates;
        }

        public static string LookUpCoordinates(string PostalCode, string CountryCode)
        {
            string Coordinates = String.Empty;
            string Latitude = String.Empty;
            string Longitude = String.Empty;
            
            string LookUpUrl = String.Concat("http://ws.geonames.org/postalCodeSearch?postalcode=", PostalCode, "&maxRows=10&country=", CountryCode, "&username=motormart");
            
            WebRequest webRequest = HttpWebRequest.Create(LookUpUrl);

            WebResponse webResponse = webRequest.GetResponse();

            if (webResponse != null)
            {
                using(StreamReader sReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string strResult = sReader.ReadToEnd().Trim();
                    if (!String.IsNullOrEmpty(strResult))
                    {
                        //Load the response into an XML doc
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(strResult);

                        //Extract the latitude from the Latitude Node
                        XmlNodeList node = xDoc.GetElementsByTagName("lat");
                        if (node.Count > 0)
                        {
                            Latitude = node[0].InnerText;
                        }

                        //Extract the longitude from the Longitude Node
                        node = xDoc.GetElementsByTagName("lng");
                        if (node.Count > 0)
                        {
                            Longitude = node[0].InnerText;
                        }

                        Coordinates = String.Format("{0},{1}", Latitude, Longitude);
                    }
                }
            }
            return Coordinates;
        }

        #endregion
    }
}