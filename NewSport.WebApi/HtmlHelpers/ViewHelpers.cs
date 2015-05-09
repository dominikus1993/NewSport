using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Entity;

namespace NewSport.WebApi.HtmlHelpers
{
    public static class ViewHelpers
    {
        public static bool IsPostHaveImage(this HtmlHelper helper,Post post )
        {
            return post.ImageData != null;
        }
    }
}
