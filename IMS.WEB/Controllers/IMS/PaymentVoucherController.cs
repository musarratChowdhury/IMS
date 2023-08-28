using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PaymentVoucher;
using IMS.Services.Helpers;
using IMS.Services.SecondaryServices;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class PaymentVoucherController : Controller
    {
        private readonly PaymentVoucherService _paymentVoucherService = new PaymentVoucherService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");
        
        // GET: PaymentVoucher
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
                    var result = new DataResult<PaymentVoucherDto>
                    {
                        count = await _paymentVoucherService.GetTotalCount(session),
                        result = await _paymentVoucherService.GetAll(session, request)
                    };
                    
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
        public async Task<ActionResult> Insert(CRUDRequest<PaymentVoucherFormDto> paymentVoucherCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentVoucherService.Create(paymentVoucherCreateReq.value, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Create(PaymentVoucherFormDto paymentVoucherCreateDto)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentVoucherService.Create(paymentVoucherCreateDto, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Update(CRUDRequest<PaymentVoucherDto> paymentVoucherCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    
                    await _paymentVoucherService.Update(paymentVoucherCreateReq.value, User.Identity.GetUserId<long>(), session);

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
                    await _paymentVoucherService.UpdateRank(changeRankDto, session);
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
        public async Task<ActionResult> Delete(DeleteRequest paymentVoucherDeleteReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentVoucherService.Delete(paymentVoucherDeleteReq.Key, session);

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