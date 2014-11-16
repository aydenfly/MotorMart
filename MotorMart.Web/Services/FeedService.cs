using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Web.Models;
using System.Net;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Caching;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Text;
using HtmlAgilityPack;
//using PdfSharp.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using PdfSharp.Drawing;
using MotorMart.Core.Common.Helpers;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

namespace MotorMart.Web.Services
{
    public class FeedService
    {
        private const string _atomfeedUrl = "https://gdata.youtube.com/feeds/api/playlists/PLDY5sFpUEM11Rwqfd3HDlDiWeZ2Q18J8s?v=2&showinfo=1";
        private const string _rssfeedUrl = "https://gdata.youtube.com/feeds/api/playlists/DADC4893C08FA745?v=2";
        private const string _jsonfeedUrl = "https://gdata.youtube.com/feeds/api/playlists/DADC4893C08FA745?v=2";

        private const string _fbcRssFeedUrl = "http://feeds.bhpinfosolutions.co.uk/news/category/legalnews/feed/ ";
        private const string _fbcRssFeedItemRootUrl = "http://feeds.bhpinfosolutions.co.uk";

        private string _itemContentUrl = "";

        public YouTubeFeedModel GetVideoFeed()
        {
            // Check if we have a feed in the cache
            const string CACHE_KEY = "MotorMart.Controllers.CmsController.Video";
            var cachedFeed = HttpContext.Current.Cache.Get(CACHE_KEY) as YouTubeFeedModel;
            if (cachedFeed != null)
                return cachedFeed;

            // Make webrequest            
            
            var request = (HttpWebRequest)WebRequest.Create(_jsonfeedUrl);
            request.Method = WebRequestMethods.Http.Get;
            request.AutomaticDecompression = DecompressionMethods.GZip; // Remember to unzip the feed

            // Download the json data
            string feedData;
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                feedData = new StreamReader(responseStream).ReadToEnd();
            }

            // Deserialize the json
            var feed = new JavaScriptSerializer().Deserialize<YouTubeFeedModel>(feedData);

            // Add to cache
            HttpContext.Current.Cache.Add(CACHE_KEY, feed,
            dependencies: null,
            absoluteExpiration: Cache.NoAbsoluteExpiration,
            slidingExpiration: new TimeSpan(hours: 0, minutes: 1, seconds: 0),
            priority: CacheItemPriority.Normal,
            onRemoveCallback: null);

            // Return the feed
            return feed;
        }

        public SyndicationFeed GetYouTubeFeed()
        {
            // Check if we have a feed in the cache
            const string CACHE_KEY = "MotorMart.Controllers.CmsController.Video";
            var cachedFeed = HttpContext.Current.Cache.Get(CACHE_KEY) as SyndicationFeed;
            if (cachedFeed != null)
                return cachedFeed;

            if (!string.IsNullOrEmpty(_atomfeedUrl))
            {
                
                // for Rss you can use Rss20FeedFormatter()
                //var _rssFeedFormatter = new Rss20FeedFormatter(); 
                
                // for Atom you can use Atom10FeedFormatter()
                var _atomFeedFormatter = new Atom10FeedFormatter();

                var xmlReader = XmlReader.Create(_atomfeedUrl);
                _atomFeedFormatter.ReadFrom(xmlReader);

                //Add feed to Cache
                HttpContext.Current.Cache.Add(CACHE_KEY, _atomFeedFormatter.Feed,
                dependencies: null,
                absoluteExpiration: Cache.NoAbsoluteExpiration,
                slidingExpiration: new TimeSpan(hours: 0, minutes: 1, seconds: 0),
                priority: CacheItemPriority.Normal,
                onRemoveCallback: null);

                //Return the feed
                return _atomFeedFormatter.Feed;
            }
            return null;
        }

        public SyndicationFeed GetNewsFeed()
        {
            // Check if we have a feed in the cache
            const string CACHE_KEY = "MotorMart.Controllers.CmsController.NewsFeed";
            var cachedFeed = HttpContext.Current.Cache.Get(CACHE_KEY) as SyndicationFeed;
            if (cachedFeed != null)
                return cachedFeed;

            if (!string.IsNullOrEmpty(_fbcRssFeedUrl))
            {
                var _rssFeedFormatter = new Rss20FeedFormatter();

                var _xmlReader = XmlReader.Create(_fbcRssFeedUrl);
                _rssFeedFormatter.ReadFrom(_xmlReader);

                //Add feed to Cache
                HttpContext.Current.Cache.Add(CACHE_KEY, _rssFeedFormatter.Feed,
                dependencies: null,
                absoluteExpiration: Cache.NoAbsoluteExpiration,
                slidingExpiration: new TimeSpan(hours: 0, minutes: 1, seconds: 0),
                priority: CacheItemPriority.Normal,
                onRemoveCallback: null);

                //Return the feed
                return _rssFeedFormatter.Feed;
            }

            return null;
        }

        public IEnumerable<FeedItem> GetNewsFeedItems()
        {
            // Check if we have a feed in the cache
            const string CACHE_KEY = "MotorMart.Controllers.CmsController.NewsFeedItems";
            var cachedFeedItems = HttpContext.Current.Cache.Get(CACHE_KEY) as IList<FeedItem>;
            if (cachedFeedItems != null)
                return cachedFeedItems;

            //If not, get the feed
            var _itemsList = new List<FeedItem>();

            var _items = GetNewsFeed().Items;

            if (_items.Any())
            {
                foreach (var x in _items)
                {
                    _itemContentUrl = String.Format("{0}{1}", _fbcRssFeedItemRootUrl, x.Links.First().Uri.PathAndQuery);
                    _itemsList.Add(new FeedItem
                        {
                            feedItem = x,
                            newsContent = ItemContent
                        });
                }
            }

            //Add feed to Cache
            HttpContext.Current.Cache.Add(CACHE_KEY, _itemsList,
            dependencies: null,
            absoluteExpiration: Cache.NoAbsoluteExpiration,
            slidingExpiration: new TimeSpan(hours: 0, minutes: 1, seconds: 0),
            priority: CacheItemPriority.Normal,
            onRemoveCallback: null);

            //Spit out its items
            return _itemsList;
        }

        public string ItemContent
        {
            get
            {
                string _result = "";

                try
                {                    
                    HtmlWeb _webDocument = new HtmlWeb();
                    HtmlDocument _htmlDocument = _webDocument.Load(_itemContentUrl);

                    if (_htmlDocument.DocumentNode != null)
                    {
                        HtmlNode _contentEntry = _htmlDocument.DocumentNode.SelectSingleNode("//div[@class='entry']");
                        if (_contentEntry != null)
                        {
                            //_result = _contentEntry.InnerText;
                            _result = _contentEntry.InnerHtml;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                return _result;
            }
            
        }

        #region PDF generator

        public string GeneratePdf(GeneratePdfModel model, out string pdfVirtualPath)
        {
            pdfVirtualPath = String.Empty;
            string _generatedFileName;
            string _samplefilePath = HttpContext.Current.Server.MapPath("/Resources/files/htmls/sanitized_sample_html.html");
            string _samplefileUrl; //= String.Format("http://{0}/Resources/files/htmls/sample_html.html", HttpContext.Current.Request.Url.Host.ToString());
            _samplefileUrl = model.sourceurl;
            //return GeneratePdfFromHtml(_samplefilePath);
            //return GeneratePdfFromHtmlAbc(_samplefileUrl);

            //string _generatedFilePath = GeneratePdfFromHtmlWK(_samplefileUrl);

            //iTextSharp
            //string _generatedFileName = Path.GetFileName(_generatedFilePath);
            _generatedFileName = Path.GetFileName(GeneratePdfFromHtml(_samplefileUrl));

            //Abc
            //_generatedFileName = Path.GetFileName(GeneratePdfFromHtmlAbc(_samplefileUrl));

            //Custom
            //_generatedFileName = ConvertHTMLToPDF("", true, _samplefileUrl);

            pdfVirtualPath = String.Format("http://{0}/Resources/files/pdfs/{1}", HttpContext.Current.Request.Url.Host.ToString(), _generatedFileName);

            return _generatedFileName;
        }

        //iTextSharp
        internal string GeneratePdfFromHtml(string htmlFileUrl)
        {
            var document = new Document(PageSize.A4, 30, 30, 30, 30);
            HttpContext _context = HttpContext.Current;

            string _outputDir = _context.Server.MapPath("/Resources/files/pdfs/");
            if (!Directory.Exists(_outputDir))
                Directory.CreateDirectory(_outputDir);

            string _pdfFile = String.Format("{0}HTMLToPDF_{1}.pdf", _outputDir, Guid.NewGuid().ToString());
            try
            {
                PdfWriter.GetInstance(document, new FileStream(_pdfFile, FileMode.Create));
                document.Open();
                WebClient _webClient = new WebClient();
                _webClient.Proxy = null;
                string htmlText = _webClient.DownloadString(htmlFileUrl);

                //Replace all relative Urls to absolute Urls
                var fileUri = new Uri(htmlFileUrl);
                var baseUrl = String.Format("{0}://{1}", fileUri.Scheme, fileUri.Host);

                var stringWriter = new StringWriter();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlText);

                //Replace all image paths
                foreach (var img in htmlDoc.DocumentNode.Descendants("img"))
                {
                    img.Attributes["src"].Value = String.Format("{0}/{1}", baseUrl, img.Attributes["src"].Value);
                }
                
                //Replace all URL paths
                foreach (var anc in htmlDoc.DocumentNode.Descendants("a"))
                {
                    anc.Attributes["href"].Value = String.Format("{0}/{1}", baseUrl, anc.Attributes["href"].Value);
                }

                htmlDoc.Save(stringWriter);
                var newHtml = stringWriter.ToString();
                

                List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StringReader(newHtml), null);
                for (int k = 0; k < htmlarraylist.Count; k++)
                {
                    document.Add((IElement)htmlarraylist[k]);
                }

                document.Close();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                //document.Dispose();
            }

            return _pdfFile;

        }

        //PDF Sharp
        internal string GeneratePdfFromHtmlSharp(string htmlFileUrl)
        {
            // Create a new PDF document
            PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfSharp.Pdf.PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);

            // Save the document...
            const string filename = "HelloWorld.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);

            return string.Empty;
        }

       //WKhtmlToPDF Sharp
        

        #region WKHtmlToPdf
        internal string GeneratePdfFromHtmlWK(string htmlFileUrl)
        {


            string[] htmlFileUrls = new string[] { htmlFileUrl };

            //Create PDF from one or multiple URLs
            var pdfUrl = PdfGenerator.HtmlToPdf(pdfOutputLocation: "~/Resources/files/pdfs/", outputFilenamePrefix: "GeneratedPDF", urls: htmlFileUrls);

            if (!String.IsNullOrEmpty(pdfUrl))
            {
                return pdfUrl;
            }
            return String.Empty;
        }

        #endregion

        public string ConvertHTMLToPDF(string htmlString, bool convertFromFile, string htmlFilePath, string htmlFileUrl, bool convertFromUrl = false)
        {
            string _pdfFile = String.Empty;

            HttpContext _context = HttpContext.Current;

            TextReader _txtReader = new StringReader(htmlString);

            string _tempDir = _context.Server.MapPath("/temp/");
            if (!Directory.Exists(_tempDir))
                Directory.CreateDirectory(_tempDir);

            string _outputDir = _context.Server.MapPath("/Resources/pdfs/");
            if (!Directory.Exists(_outputDir))
                Directory.CreateDirectory(_outputDir);

            string _tempHtmlFile = String.Format("{0}{1}", _tempDir, "HTMLToCovert.html");

            _pdfFile = String.Format("{0}HTMLToPDF_{1}.pdf", _outputDir, DateTime.Now);

            if (File.Exists(_pdfFile))
            {
                File.Delete(_pdfFile);
            }

            var _pdfDoc = new Document(PageSize.A4);
            var _parser = new HTMLWorker(_pdfDoc);

            try
            {
                if (convertFromFile)
                {
                    //html file to pdf file
                    var reader = new StreamReader(htmlFilePath);
                    htmlString = reader.ReadToEnd();
                    reader.Dispose();
                    reader.Close();

                    _txtReader = new StringReader(htmlString);

                    using (var _fileStream = new FileStream(_pdfFile, FileMode.Create))
                    {
                        using (var _sWriter = new StreamWriter(_fileStream, Encoding.UTF8))
                        {
                            _sWriter.Write(htmlString);
                        }

                        PdfWriter.GetInstance(_pdfDoc, _fileStream);
                        _pdfDoc.Open();
                        _parser.StartDocument();
                        _parser.Parse(_txtReader);
                    }

                }
                else if (convertFromUrl)
                {

                    var _webClient = new WebClient();
                    string htmlText = _webClient.DownloadString(htmlFileUrl);

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlString);

                    _txtReader = new StringReader(htmlDoc.DocumentNode.OuterHtml);
                    
                }
                else
                {
                    //html string to pdf file
                    PdfWriter.GetInstance(_pdfDoc, new FileStream(_pdfFile, FileMode.Create));
                    _pdfDoc.Open();

                    /********************************************************************************/
                    var _interfaceProps = new Dictionary<string, object>();
                    var _imageHandler = new ImageHander() { BaseUri = _context.Request.Url.ToString() };

                    _interfaceProps.Add(HTMLWorker.IMG_PROVIDER, _imageHandler);

                    foreach (IElement element in HTMLWorker.ParseToList(_txtReader, null))
                    {
                        _pdfDoc.Add(element);
                    }
                    _pdfDoc.Close();

                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                _parser.EndDocument();
                _parser.Close();
                _pdfDoc.Close();
            }

            return _pdfFile;
        }

        //handle Image relative and absolute URL's for ITextSharp
        public class ImageHander : IImageProvider
        {
            public string BaseUri;
            public iTextSharp.text.Image GetImage(string src, IDictionary<string, string> h, ChainedProperties cprops, IDocListener doc)
            {
                string imgPath = string.Empty;

                if (!src.ToLower().Contains("http://"))
                {
                    imgPath = HttpContext.Current.Request.Url.Scheme + "://" +

                    HttpContext.Current.Request.Url.Authority + src;
                }
                else
                {
                    imgPath = src;
                }

                return iTextSharp.text.Image.GetInstance(imgPath);
            }
        }

        #endregion

    }
}