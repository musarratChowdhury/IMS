using IMS.BusinessModel.Entity.Configuration;
using IMS.Services.Helpers;
using IMS.WEB.Areas.Admin.Controllers.BaseControllers;
using System;
using System.Web.Mvc;

namespace IMS.WEB.Areas.Admin.Controllers
{
    [Authorize]
    public class ShipmentTypeController : BaseConfigurationController<ShipmentType>
    {
        public ActionResult Index()
        {
            try
            {
                using (NHibernateConfig.OpenSession())
                {

                    var result = GetAll();
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View();
        }
    }
}