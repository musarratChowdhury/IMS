using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.SalesOrder;
using IMS.Services.Helpers;
using IMS.Services.SecondaryServices;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class SalesOrderController : Controller
    {
         private readonly SalesOrderService _salesOrderService = new SalesOrderService();
         private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");

         // GET: SalesOrder
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
                    var result = new DataResult<SalesOrderDto>
                    {
                        count = await _salesOrderService.GetTotalCount(session),
                        result = await _salesOrderService.GetAll(session, request)
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
                    var result = await _salesOrderService.GetDropDownList(session);

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
        public async Task<ActionResult> GetMonthlyTotalSales()
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    var result = await _salesOrderService.GetMonthlyTotalSales(session);

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
        public async Task<ActionResult> Insert(CRUDRequest<SalesOrderFormDto> salesOrderCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _salesOrderService.Create(salesOrderCreateReq.value, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Create(SalesOrderFormDto salesOrderCreateDto)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _salesOrderService.Create(salesOrderCreateDto, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Update(CRUDRequest<SalesOrderDto> salesOrderCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    
                    await _salesOrderService.Update(salesOrderCreateReq.value, User.Identity.GetUserId<long>(), session);

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
                    await _salesOrderService.UpdateRank(changeRankDto, session);
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
        public async Task<ActionResult> Archive(DeleteRequest salesOrderDeleteReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _salesOrderService.ArchiveRecord(salesOrderDeleteReq.Key, session);

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
        public async Task<ActionResult> Delete(DeleteRequest salesOrderDeleteReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _salesOrderService.Delete(salesOrderDeleteReq.Key, session);

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