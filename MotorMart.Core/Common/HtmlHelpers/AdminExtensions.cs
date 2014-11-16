using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MotorMart.Core.Models;
using MotorMart.Core.Routing;

namespace MotorMart.Core.HtmlHelpers
{
    public static class AdminExtensions
    {
        public static MvcHtmlString AdminSubmitButton(this HtmlHelper htmlHelper, string ButtonText)
        {
            TagBuilder tb = new TagBuilder("input");
            tb.MergeAttribute("type", "submit", true);
            tb.MergeAttribute("value", ButtonText, true);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString AdminButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action)
        {
            return AdminButton(htmlHelper, ButtonText, Controller, Action, null, null, null);
        }

        public static MvcHtmlString AdminButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues)
        {
            return AdminButton(htmlHelper, ButtonText, Controller, Action, routeValues, null, null);
        }

        public static MvcHtmlString AdminButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), null);
        }

        public static MvcHtmlString AdminButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes, string iconCssClass)
        {
            return AdminButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), iconCssClass);
        }

        public static MvcHtmlString AdminButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes, string iconCssClass)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");            
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all");
            tbAnchor.InnerHtml = String.Format("<span class=\"ui-icon {0}\"></span>{1}", iconCssClass, ButtonText);

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #region Add Button

        public static MvcHtmlString AdminAddButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action)
        {
            return AdminAddButton(htmlHelper, ButtonText, Controller, Action, null, null);            
        }

        public static MvcHtmlString AdminAddButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues)
        {
            return AdminAddButton(htmlHelper, ButtonText, Controller, Action, routeValues, null);
        }

        public static MvcHtmlString AdminAddButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminAddButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));            
        }

        public static MvcHtmlString AdminAddButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-plusthick\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #endregion

        #region View Button

        public static MvcHtmlString AdminViewButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action)
        {
            return AdminViewButton(htmlHelper, ButtonText, Controller, Action, null, null);
        }

        public static MvcHtmlString AdminViewButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues)
        {
            return AdminViewButton(htmlHelper, ButtonText, Controller, Action, routeValues, null);
        }

        public static MvcHtmlString AdminViewButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminViewButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString AdminViewButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-carat-1-e\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #endregion

        #region Enable Button

        public static MvcHtmlString AdminEnableButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminEnableButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString AdminEnableButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all toggle-enable");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));            
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-check\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #endregion

        #region Disable Button

        public static MvcHtmlString AdminDisableButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminDisableButton(htmlHelper, ButtonText, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString AdminDisableButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all toggle-disable");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));            
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-close\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #endregion

        #region Move Button
        public static MvcHtmlString AdminMoveUpButton(this HtmlHelper htmlHelper, string Action, string Controller, object routeValues)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(String.Format("<span class=\"\"><a href=\"{0}\" class=\"fg-button ui-state-default fg-button-icon-left ui-corner-all\"><span class=\"ui-icon ui-icon-triangle-1-n\"></span>{1}</a></span>", url.Action(Action, Controller, routeValues), "Move Up"));
        }
        public static MvcHtmlString AdminMoveUpButton(this HtmlHelper htmlHelper, string ButtonText, string Action, string Controller, object routeValues, object htmlAttributes)
        {
            return AdminMoveUpButton(htmlHelper, ButtonText, Action, Controller, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString AdminMoveUpButton(this HtmlHelper htmlHelper, string ButtonText, string Action, string Controller, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-triangle-1-n\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }
        
        public static MvcHtmlString AdminMoveDownButton(this HtmlHelper htmlHelper, string Action, string Controller, object routeValues)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(String.Format("<span class=\"\"><a href=\"{0}\" class=\"fg-button ui-state-default fg-button-icon-left ui-corner-all\"><span class=\"ui-icon ui-icon-triangle-1-s\"></span>{1}</a></span>", url.Action(Action, Controller, routeValues), "Move Down"));
        }
        public static MvcHtmlString AdminMoveDownButton(this HtmlHelper htmlHelper, string ButtonText, string Action, string Controller, object routeValues, object htmlAttributes)
        {
            return AdminMoveDownButton(htmlHelper, ButtonText, Action, Controller, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }
        public static MvcHtmlString AdminMoveDownButton(this HtmlHelper htmlHelper, string ButtonText, string Action, string Controller, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-left ui-corner-all");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-triangle-1-s\"></span>" + ButtonText;

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }
        
        #endregion

        #region Edit Button

        public static MvcHtmlString AdminEditButton(this HtmlHelper htmlHelper, string Action, string Controller, object routeValues)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(String.Format("<span class=\"\"><a href=\"{0}\" class=\"fg-button ui-state-default fg-button-icon-left ui-corner-all\"><span class=\"ui-icon ui-icon-triangle-1-e\"></span>{1}</a></span>", url.Action(Action, Controller, routeValues), "edit"));
        }

        #endregion

        #region Delete Button

        public static MvcHtmlString AdminDeleteButton(this HtmlHelper htmlHelper, string Controller, string Action, object routeValues)
        {
            return AdminDeleteButton(htmlHelper, "delete", Controller, Action, routeValues);
        }

        public static MvcHtmlString AdminDeleteButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues)
        {
            return AdminDeleteButton(htmlHelper, "delete", Controller, Action, routeValues, "", "");
        }

        public static MvcHtmlString AdminDeleteButton(this HtmlHelper htmlHelper, string ButtonText, string Controller, string Action, object routeValues, string Id, string rel)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(String.Format("<span class=\"\"><a href=\"{0}\" id=\"{1}\" rel=\"{2}\" class=\"fg-button ui-state-default fg-button-icon-left ui-corner-all delete-btn\"><span class=\"ui-icon ui-icon-closethick\"></span>{3}</a></span>", url.Action(Action, Controller, routeValues), Id, rel, ButtonText));
        }

        public static MvcHtmlString AdminDeleteButtonIcon(this HtmlHelper htmlHelper, string Controller, string Action, object routeValues)
        {
            return AdminDeleteButtonIcon(htmlHelper, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary());
        }

        public static MvcHtmlString AdminDeleteButtonIcon(this HtmlHelper htmlHelper, string Controller, string Action, object routeValues, object htmlAttributes)
        {
            return AdminDeleteButtonIcon(htmlHelper, Controller, Action, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString AdminDeleteButtonIcon(this HtmlHelper htmlHelper, string Controller, string Action, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder tbContainer = new TagBuilder("span");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.MergeAttributes(htmlAttributes, true);
            tbAnchor.AddCssClass("fg-button ui-state-default fg-button-icon-solo ui-corner-all delete-btn");
            tbAnchor.MergeAttribute("href", url.Action(Action, Controller, routeValues));
            tbAnchor.InnerHtml = "<span class=\"ui-icon ui-icon-closethick\"></span>delete";

            tbContainer.InnerHtml = tbAnchor.ToString();

            return MvcHtmlString.Create(tbContainer.ToString());
        }

        #endregion

        #region UI Alert

        public static MvcHtmlString AdminUIAlert(this HtmlHelper htmlHelper, string Text)
        {
            return AdminUIAlert(htmlHelper, "Alert", Text);
        }

        public static MvcHtmlString AdminUIAlert(this HtmlHelper htmlHelper, string Title, string Text)
        {
            /*
            <div class="ui-widget">
				<div style="padding: 0pt 0.7em;" class="ui-state-error ui-corner-all"> 
					<p><span style="float: left; margin-right: 0.3em;" class="ui-icon ui-icon-alert"></span> 
					<strong>Alert:</strong> Sample ui-state-error style.</p>
				</div>
			</div>
             */

            TagBuilder tbWidget = new TagBuilder("div");
            tbWidget.AddCssClass("ui-widget");

            TagBuilder tbInner = new TagBuilder("div");
            tbInner.AddCssClass("ui-state-error ui-corner-all");
            tbInner.MergeAttribute("style", "padding: 0pt 0.7em;");
            tbInner.InnerHtml = String.Format("<p><span style=\"float: left; margin-right: 0.3em;\" class=\"ui-icon ui-icon-alert\"></span><strong>{0}:</strong> {1}</p>", Title, Text);

            tbWidget.InnerHtml = tbInner.ToString();

            return MvcHtmlString.Create(tbWidget.ToString());
        }

        #endregion

        #region Generic Buttons

        public static MvcHtmlString SubmitButton(this HtmlHelper helper)
        {
            /*<input type="submit" class="submit" value="Submit &rsaquo;" />*/
            TagBuilder tb = new TagBuilder("input");
            tb.MergeAttribute("type", "submit", true);
            tb.MergeAttribute("value", "Submit ›", true);
            tb.MergeAttribute("class", "submit", true);
            return MvcHtmlString.Create(tb.ToString());
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string Text)
        {
            return GenerateSubmitButton(helper, Text, new RouteValueDictionary());
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string Text, object htmlAttributes)
        {
            return GenerateSubmitButton(helper, Text, new RouteValueDictionary(htmlAttributes));
        }

        private static MvcHtmlString GenerateSubmitButton(this HtmlHelper htmlHelper, string Text, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes.ContainsKey("id"))
            {
                string id = htmlAttributes["id"] as string;
            }

            /*<a href="">Submit</a>*/
            StringBuilder sb = new StringBuilder();
            TagBuilder tb = new TagBuilder("a");
            tb.MergeAttribute("class", "submit-btn btn forinput", true);
            tb.MergeAttributes(htmlAttributes, true);
            tb.InnerHtml += Text;
            sb.Append(tb.ToString());

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion
    }
}