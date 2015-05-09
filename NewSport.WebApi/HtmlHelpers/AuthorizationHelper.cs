using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewSport.WebApi.Models;

namespace NewSport.WebApi.HtmlHelpers
{
    public static class AuthorizationHelper
    {
        public static bool IsLogged(this HtmlHelper helper)
        {
            return HttpContext.Current.Session["user"] != null;
        }

        public static LoggedUserViewModel CurrentUser(this HtmlHelper helper)
        {
            LoggedUserViewModel loggedUser = new LoggedUserViewModel()
            {
                Username = HttpContext.Current.Session["user"].ToString()
               
            };
            return loggedUser;
        }
    }
}
