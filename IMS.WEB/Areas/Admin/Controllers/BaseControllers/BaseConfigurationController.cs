using IMS.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.Services.BaseServices;
using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Dto.GridData;

namespace IMS.WEB.Areas.Admin.Controllers.BaseControllers
{
    public class BaseConfigurationController<TEntity> : Controller where TEntity : class, IConfigurationEntity
    {
        private readonly BaseConfigurationService<ConfigurationDto, ConfigurationFormData, TEntity> _baseConfigurationService;

        public BaseConfigurationController()
        {
            _baseConfigurationService = new BaseConfigurationService<ConfigurationDto, ConfigurationFormData, TEntity>();
        }

        [HttpPost]
        public async Task<ActionResult> DataSource()
        {
            using (var session = NHibernateConfig.OpenSession())
            {
                var result = new DataResult<ConfigurationDto>();
                var resultEnumerable =await _baseConfigurationService.GetAll(session);
                result.result = resultEnumerable.ToList();
                result.count = result.result.Count;

                return Json(result, JsonRequestBehavior.AllowGet);  
            }
        }

        [HttpPost]
        public async Task<ActionResult> DropDownList()
        {
            using (var session = NHibernateConfig.OpenSession())
            {
                var result = await _baseConfigurationService.GetDropDownList(session);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {

                    var customerTypes =await _baseConfigurationService.GetAll(session);
                    
                    if (customerTypes == null || !customerTypes.Any())
                    {
                        return Json(new List<ConfigurationDto>(), JsonRequestBehavior.AllowGet);
                    }

                    return Json(customerTypes, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return Json(new { error = "An error occurred while fetching data." });

        }

        // POST: Admin/Type/Create
        [HttpPost]
        public async Task<ActionResult> Create(ConfigurationFormData customerTypeFormData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var session = NHibernateConfig.OpenSession())
                    {
                       await _baseConfigurationService.Create(customerTypeFormData, session);


                        return Json(new { success = true, message = "Added successfully." });

                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return Json(new { success = false, message = "Validation failed.", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while Adding.", ex.Message });
            }

        }
        
        // POST: Admin/Type/Edit/
        [HttpPost]
        public async Task<ActionResult> Edit(ConfigurationFormData customerTypeFormData)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (var session = NHibernateConfig.OpenSession())
                    {
                        await _baseConfigurationService.Update(customerTypeFormData, session);

                        return Json(new { success = true, message = "Updated successfully." });

                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return Json(new { success = false, message = "Validation failed.", errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while updating", ex.Message });
            }
        }
        
        // POST: Admin/Type/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _baseConfigurationService.Delete(id, session);

                    return Json(new { success = true, message = "Deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting" + ex.Message });
            }
        }
    }
}
