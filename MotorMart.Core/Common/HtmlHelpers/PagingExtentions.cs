using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;
using System.Web.Routing;

namespace MotorMart.Core.HtmlHelpers
{
    public static class PagingExtensions
    {
        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, PagedList<AdminVehicleSearchResult> ResultsList)
        {
            return Paging(htmlHelper, ResultsList.TotalCount, ResultsList.PageSize, ResultsList.PageIndex, ResultsList.TotalPages, ResultsList.IsPreviousPage, ResultsList.IsNextPage, "vacancysectorsearch");
        }

        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, PagedList<dealer> ResultsList)
        {
            return Paging(htmlHelper, ResultsList.TotalCount, ResultsList.PageSize, ResultsList.PageIndex, ResultsList.TotalPages, ResultsList.IsPreviousPage, ResultsList.IsNextPage, null);
        }

        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, PagedList<make> ResultsList)
        {
            return Paging(htmlHelper, ResultsList.TotalCount, ResultsList.PageSize, ResultsList.PageIndex, ResultsList.TotalPages, ResultsList.IsPreviousPage, ResultsList.IsNextPage, null);
        }

        public static MvcHtmlString Paging(this HtmlHelper htmlHelper, int TotalCount, int PageSize, int PageIndex, int TotalPages, bool IsPreviousPage, bool IsNextPage, string SearchPrefix)
        {
            /*            
            <div class="paging">
             * <span>Page 1 of 10</span>
               <a href="" class="arrow" title="first">‹‹</a>
             * <a href="" class="arrow" title="previous">‹</a>
             * <a href="" title="1">1</a>
             * <a href="" class="selected" title="2">2</a>
             * <a href="" title="3">3</a>
             * <a href="" title="4">4</a>
             * <a href="" title="5">5</a>
             * <a href="" class="arrow" title="next">›</a>
             * <a href="" class="arrow" title="last">››</a>
            </div>
             */

            PageIndex += 1;
            if (PageIndex < 1) PageIndex = 1;

            int ResultsFromCount = (PageIndex * PageSize) - PageSize + 1;
            int ResultsToCount = ((PageIndex * PageSize) > TotalCount) ? TotalCount : (PageIndex * PageSize);

            TagBuilder tb = new TagBuilder("div");
            tb.AddCssClass("paging");
            if (TotalCount > 0)
            {
                tb.InnerHtml += String.Format("<span class=\"result-info\"><strong>Showing {0} to {1} of {2} results...</strong></span> ", ResultsFromCount, ResultsToCount, TotalCount);
                tb.InnerHtml += String.Format("<span class=\"page-info\"><strong>Page {0} of {1}</strong>: ", PageIndex, TotalPages);
            }
            else
            {
                tb.InnerHtml += String.Format("<span class=\"result-info\"><strong>0 results...</strong></span>");
                tb.InnerHtml += String.Format("<span class=\"page-info\"><strong>Page 1 of 1</strong>");
            }

            if (IsPreviousPage)
            {
                tb.InnerHtml += htmlHelper.ActionQueryLink("‹‹", "index", PageQueryParam(SearchPrefix, 1));
                tb.InnerHtml += htmlHelper.ActionQueryLink("‹", "index", PageQueryParam(SearchPrefix, PageIndex));
            }

            for (int i = 1; i <= TotalPages; i++)
            {
                string css = String.Empty;

                if (i == PageIndex) css = "selected";

                tb.InnerHtml += htmlHelper.RouteQueryLink(i.ToString(), "index", PageQueryParam(SearchPrefix, i), new { @class = css });
            }

            if (IsNextPage)
            {
                tb.InnerHtml += htmlHelper.ActionQueryLink("›", "index", PageQueryParam(SearchPrefix, PageIndex + 1));
                tb.InnerHtml += htmlHelper.ActionQueryLink("››", "index", PageQueryParam(SearchPrefix, TotalPages));
            }

            tb.InnerHtml += "</span>";

            return MvcHtmlString.Create(tb.ToString());
        }

        private static RouteValueDictionary PageQueryParam(string Prefix, int Value)
        {
            string Key = Prefix != null ? String.Format("{0}.page", Prefix) : "page";

            RouteValueDictionary values = new RouteValueDictionary();
            values.Add(Key, Value);

            return values;
        }
    }
}