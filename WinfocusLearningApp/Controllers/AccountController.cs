using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WinfocusLearningApp.Authentication;
using WinfocusLearningApp.Models;

namespace WinfocusLearningApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel loginView, string ReturnUrl = "")
        {
            if (Membership.ValidateUser(loginView.Username, loginView.Password))
            {
                var user = (CustomMembershipUser)Membership.GetUser(loginView.Username, false);
                if (user.IsActive == 0)
                {
                    ViewBag.Error = "Your account is Inactive";
                    return View(loginView);
                }
                else
                {
                    if (user != null)
                    {


                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList()
                        };


                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginView.Username, DateTime.Now, DateTime.Now.AddDays(1), false, userData);

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);

                    }
                    if (user.Roles.FirstOrDefault().RoleName.Equals("Admin"))
                    {
                        ReturnUrl = "";
                    }
                        if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(loginView);
              
        }
    }
}