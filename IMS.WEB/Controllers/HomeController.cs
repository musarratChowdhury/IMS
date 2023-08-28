using IMS.BusinessModel.Entity.Configuration;
using IMS.Services.Helpers;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace IMS.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}