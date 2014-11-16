using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using System.ServiceModel.Syndication;

namespace MotorMart.Web.Models
{
    public class CmsViewModel : MasterViewModel
    {
        public List<vehicle> AvailableVehicles { get; set; }
        public List<vehicle> RequestedVehicles { get; set; }

        public int[] AvailableSelected { get; set; }
        public int[] RequestedSelected { get; set; }
        public string SavedRequested { get; set; }

        //public YouTubeFeedModel youtubefeed { get; set; }

        /// <summary>
        /// YouTube playlist feed
        /// </summary>
        public SyndicationFeed videoFeed { get; set; }


        /// <summary>
        /// FBCMB Legal news feed
        /// </summary>
        public SyndicationFeed newsFeed { get; set; }

        /// <summary>
        /// FBCMB Legal news feed items
        /// </summary>
        public IEnumerable<FeedItem> newsFeedItems { get; set; }

        public GeneratePdfModel generatepdf { get; set; }        
    }

    public class FeedItem
    {
        public string newsContent { get; set; }

        public SyndicationItem feedItem { get; set; }
    }

    public class GeneratePdfModel
    {
        public string sourceurl { get; set; }
    }
}