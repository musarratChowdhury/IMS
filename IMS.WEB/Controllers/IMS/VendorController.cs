using IMS.BusinessModel.Dto.GridData;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.Vendor;
using IMS.Services.SecondaryServices;
using IMS.Services.Helpers;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly VendorService _vendorService = new VendorService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");

        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DataSource(DataRequest request)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    var result = new DataResult<VendorDto>
                    {
                        count = await _vendorService.GetTotalCount(session),
                        result = await _vendorService.GetAll(session, request)
                    };
                    
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.Error(ex.Message, ex);
                throw;
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> DropDownList()
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    var result = await _vendorService.GetDropDownList(session);

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.Error(ex.Message, ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CRUDRequest<VendorFormDto> vendorCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _vendorService.Create(vendorCreateReq.value, User.Identity.GetUserId<long>(), session);

                    return Json(new { success = true, message = "Added successfully." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Json(new { success = false, message = "Error occurred while Adding.", ex.Message });
            }
        } 
        
        [HttpPost]
        public async Task<ActionResult> Update(CRUDRequest<VendorDto> vendorCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    
                    await _vendorService.Update(vendorCreateReq.value, User.Identity.GetUserId<long>(), session);

                    return Json(new { success = true, message = "Updated successfully." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Json(new { success = false, message = "Error occurred while Updating.", ex.Message });
            }
        } 
        
        [HttpPost]
        public async Task<ActionResult> UpdateRank(ChangeRankDto changeRankDto)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _vendorService.UpdateRank(changeRankDto, session);
                    var response = new { message = "Rank updated successfully." };
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Json(new { success = false, message = "Error occurred while Updating.", ex.Message });
            }
            
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteRequest vendorCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _vendorService.Delete(vendorCreateReq.Key, session);

                    return Json(new { success = true, message = "Deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Json(new { success = false, message = "Error occurred while deleting.", ex.Message });
            }
        }
    }
}