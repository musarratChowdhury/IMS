using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PaymentReceive;
using IMS.Services.Helpers;
using IMS.Services.SecondaryServices;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class PaymentReceiveController : Controller
    {
        private readonly PaymentReceiveService _paymentReceiveService = new PaymentReceiveService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");
        // GET: PaymentReceive
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
                    var result = new DataResult<PaymentReceiveDto>
                    {
                        count = await _paymentReceiveService.GetTotalCount(session),
                        result = await _paymentReceiveService.GetAll(session, request)
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
        public async Task<ActionResult> Insert(CRUDRequest<PaymentReceiveFormDto> paymentReceiveCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentReceiveService.Create(paymentReceiveCreateReq.value, User.Identity.GetUserId<long>(),
                        session);

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
        public async Task<ActionResult> Create(PaymentReceiveFormDto paymentReceiveCreateDto)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentReceiveService.Create(paymentReceiveCreateDto, User.Identity.GetUserId<long>(),
                        session);

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
        public async Task<ActionResult> Update(CRUDRequest<PaymentReceiveDto> paymentReceiveUpdateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentReceiveService.Update(paymentReceiveUpdateReq.value, User.Identity.GetUserId<long>(),
                        session);

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
                    await _paymentReceiveService.UpdateRank(changeRankDto, session);
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
        public async Task<ActionResult> Delete(DeleteRequest paymentReceiveDeleteReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _paymentReceiveService.Delete(paymentReceiveDeleteReq.Key, session);

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