using IMS.BusinessModel.Dto.GridData;
using System;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.Customer;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.Services.SecondaryServices;
using IMS.Services.Helpers;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService = new CustomerService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");
        
        // GET: Customer
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
                    var result = new DataResult<CustomerDto>
                    {
                        count = await _customerService.GetTotalCount(session),
                        result = await _customerService.GetAll(session, request)
                    };
                    
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
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
                    var result = await _customerService.GetDropDownList(session);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CRUDRequest<CustomerFormDto> customerCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _customerService.Create(customerCreateReq.value, User.Identity.GetUserId<long>(), session);
                    return Json(new { success = true, message = "Added successfully." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while Adding.", ex.Message });
            }
        } 
        
        [HttpPost]
        public async Task<ActionResult> Update(CRUDRequest<CustomerDto> customerCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _customerService.Update(customerCreateReq.value, User.Identity.GetUserId<long>(), session);
                    return Json(new { success = true, message = "Updated successfully." });
                }
            }
            catch (Exception ex)
            {
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
                    await _customerService.UpdateRank(changeRankDto, session);
                    var response = new { message = "Rank updated successfully." };
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while Updating.", ex.Message });
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteRequest customerCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _customerService.Delete(customerCreateReq.Key, session);
                    return Json(new { success = true, message = "Deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while deleting.", ex.Message });
            }
        }
    }
}