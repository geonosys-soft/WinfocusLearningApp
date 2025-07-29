using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.Models;

namespace WinfocusLearningApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected FormsAuthenticationTicket GetAuthTicket()
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];


            if (authCookie == null) return null;
            try
            {
                return FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception exception)
            {
                throw new Exception("Can't decrypt cookie! {0}", exception);
            }
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = GetAuthTicket();
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = authCookie;

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    FirstName = serializeModel.FirstName,
                    LastName = serializeModel.LastName,
                    Roles = serializeModel.RoleName.ToArray<string>()
                };


                HttpContext.Current.User = principal;



            }

        }
    }
}
