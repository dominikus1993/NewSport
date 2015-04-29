using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewSport.Domain.Filters
{
    public class DefaultAuthorize:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                 base.OnAuthorization(filterContext);
            }
          
        }
    }
}
