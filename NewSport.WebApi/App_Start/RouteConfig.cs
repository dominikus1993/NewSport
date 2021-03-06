﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewSport.Domain.Entities;

namespace NewSport.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "",
                url:"",
                defaults: new { controller = "Post", action = "Index",page = 1,username = (string)null }
            );

            routes.MapRoute(
                name: "",
                url: "Page/{page}",
                defaults: new { controller = "Post", action = "Index", username=(string)null}
                );

            routes.MapRoute(
               name:"",
               url:"{username}",
               defaults: new { controller = "Post", action = "Index",page=1}               
                );

            routes.MapRoute(
                name: "",
                url:"{username}/Page/{page}",
                defaults:new {controller="Post",action = "Index"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
