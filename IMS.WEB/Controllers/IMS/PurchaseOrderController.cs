using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PurchaseOrder;
using IMS.Services.Helpers;
using IMS.Services.SecondaryServices;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class PurchaseOrderController : Controller
    {
        private readonly PurchaseOrderService _purchaseOrderService = new PurchaseOrderService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");

        // GET: PurchaseOrder
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
                    var result = new DataResult<PurchaseOrderDto>
                    {
                        count = await _purchaseOrderService.GetTotalCount(session),
                        result = await _purchaseOrderService.GetAll(session, request)
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Error(e.Message,e);
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
                    var result = await _purchaseOrderService.GetDropDownList(session);

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.Error(e.Message,e);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CRUDRequest<PurchaseOrderFormDto> purchaseOrderCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _purchaseOrderService.Create(purchaseOrderCreateReq.value, User.Identity.GetUserId<long>(),
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
        public async Task<ActionResult> Create(PurchaseOrderFormDto purchaseOrderCreateDto)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _purchaseOrderService.Create(purchaseOrderCreateDto, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Update(CRUDRequest<PurchaseOrderDto> purchaseOrderCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _purchaseOrderService.Update(purchaseOrderCreateReq.value, User.Identity.GetUserId<long>(),
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
                    await _purchaseOrderService.UpdateRank(changeRankDto, session);
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
        public async Task<ActionResult> Archive(DeleteRequest purchaseOrderArchiveReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _purchaseOrderService.ArchiveRecord(purchaseOrderArchiveReq.Key, session);

                    return Json(new { success = true, message = "Archived successfully." });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Json(new { success = false, message = "Error occurred while archiving.", ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(DeleteRequest purchaseOrderCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _purchaseOrderService.Delete(purchaseOrderCreateReq.Key, session);

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