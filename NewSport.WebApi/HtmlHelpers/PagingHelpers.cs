using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.WebApi.Models;

namespace NewSport.WebApi.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PostLinks(this HtmlHelper helper,PagingInfo pagingInfo,Func<int,string> pageurl )
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < pagingInfo.TotalPages(); i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href",pageurl(i));
                tagBuilder.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                        tagBuilder.AddCssClass("selected");
                }
                builder.Append(tagBuilder.ToString());
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
