﻿using System.Web;
using System.Web.Mvc;

namespace WebApplication_MVC_EF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
