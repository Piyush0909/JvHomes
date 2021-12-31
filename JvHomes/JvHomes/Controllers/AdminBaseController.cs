using JvHomes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing; 

namespace JvHomes.Controllers
{
    public class AdminBaseController : Controller
    {  


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            // If the browser session or authentication session has expired...
            if (session.IsNewSession || Session["Pk_adminId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                     new { action = "Login", Controller = "Home" }));
            }



            else
            {
                 


                
            }
            base.OnActionExecuting(filterContext); 
        }

    }
}
