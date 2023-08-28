using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMS.BusinessModel.Dto.Bill;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.Services.Helpers;
using IMS.Services.SecondaryServices;
using log4net;
using Microsoft.AspNet.Identity;

namespace IMS.WEB.Controllers.IMS
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly BillService _billService = new BillService();
        private readonly ILog _logger = LogManager.GetLogger("IMS.WEB");

        // GET: Bill
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
                    var result = new DataResult<BillDto>
                    {
                        count = await _billService.GetTotalCount(session),
                        result = await _billService.GetAll(session, request)
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
        public async Task<ActionResult> DropDownList()
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    var result = await _billService.GetDropDownList(session);

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
        public async Task<ActionResult> Insert(CRUDRequest<BillFormDto> billCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _billService.Create(billCreateReq.value, User.Identity.GetUserId<long>(), session);

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
        public async Task<ActionResult> Update(CRUDRequest<BillDto> billCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _billService.Update(billCreateReq.value, User.Identity.GetUserId<long>(), session);

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
                    await _billService.UpdateRank(changeRankDto, session);
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
        public async Task<ActionResult> Delete(DeleteRequest billCreateReq)
        {
            try
            {
                using (var session = NHibernateConfig.OpenSession())
                {
                    await _billService.Delete(billCreateReq.Key, session);

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