using IMS.BusinessModel.Entity.Configuration;
using IMS.WEB.Areas.Admin.Controllers.BaseControllers;
using System;
using System.Web.Mvc;

namespace IMS.WEB.Areas.Admin.Controllers
{
    [Authorize]
    public class UOMController : BaseConfigurationController<UnitOfMeasurement>
    {
        public ActionResult Index()
        {
            try
            {
                var result = GetAll();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View();
        }
    }
}