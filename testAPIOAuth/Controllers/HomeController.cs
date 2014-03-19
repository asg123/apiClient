using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using testAPIOAuth.Classes;

namespace testAPIOAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.ErrorText = string.Empty;

            if (Request.IsAuthenticated)
            {

                try
                {
                    var id = (FormsIdentity) User.Identity;
                    var token = id.Ticket.UserData;
                    // vamos a probar
                    var api = ApiClient.GetConsumer();
                    //var ret = ApiClient.Call<ChatList>(ApiClient.GetChatsEndpoint, api, token);
                    var ret = ApiClient.Call<ChatList>(ApiClient.GetChatsEndpoint);
                    ViewBag.Devices = ret.chats.ToArray();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorText = ex.Message;
                }
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
