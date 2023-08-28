using System;
using System.Web.Mvc;
using IMS.WEB.Areas.Admin.Controllers.BaseControllers;
using IMS.BusinessModel.Entity.Configuration;

namespace IMS.WEB.Areas.Admin.Controllers
{
    [Authorize]
    public class PaymentTypeController : BaseConfigurationController<PaymentType>
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