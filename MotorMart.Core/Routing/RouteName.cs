using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorMart.Core.Routing
{
    public class RouteName
    {
        public RouteName()
        {
        }

        public static RouteName Instance
        {
            get
            {
                return new RouteName();
            }
        }

        public string Sitemap { get { return "Sitemap"; } }

        public string SearchDefault { get { return "SearchDefault"; } }
    }
}
