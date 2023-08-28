using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.Bill;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class BillService : BaseSecondaryService<Bill>
    {
        private readonly IBaseDao<Bill> _baseDao = new BaseDao<Bill>();
        private readonly IBaseDao<PurchaseOrder> _purchaseOrderDao = new BaseDao<PurchaseOrder>();

        public async Task<List<BillDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities =await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new BillDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(BillFormDto billFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var bill = new Bill();
                    var mappedBill = MapToEntity(billFormDto, bill);
                    mappedBill.Rank = GetNextRank(session);
                    mappedBill.CreatedBy = userId;
                    mappedBill.CreationDate = DateTime.Now;
                    var po = await _purchaseOrderDao.GetById(mappedBill.PurchaseOrderId, session);
                    mappedBill.PurchaseOrder = po;
                    await _baseDao.Create(mappedBill, session);
                    po.BillId = mappedBill.Id;
                    await _purchaseOrderDao.Update(po, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(BillDto billDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var bill = new Bill();
                    var mappedBill = MapToEntity(billDto, bill);
                    mappedBill.ModificationDate = DateTime.Now;
                    mappedBill.ModifiedBy = modifiedById;

                    await _baseDao.Update(mappedBill, sess);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<DropDownDto>> GetDropDownList(ISession session)
        {
            try
            {
                var entities =await _baseDao.GetAll(session);
                return (from t in entities let dto = new DropDownDto() select MapToDropDownDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DropDownDto MapToDropDownDto(Bill entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#BL{entity.Id}PO{entity.PurchaseOrderId}";
            dto.DueAmount = entity.PurchaseOrder.DueAmount;
            return dto;
        }

        private BillDto MapToDto(Bill entity, BillDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#BL{entity.Id}PO{entity.PurchaseOrderId}";
            dto.BillDate = entity.BillDate;
            dto.BillTypeId = entity.BillTypeId;
            dto.BillTypeName = entity.BillType.Name;
            dto.BillDueDate = entity.BillDueDate;
            dto.PurchaseOrderId = entity.PurchaseOrderId;
            dto.PurchaseOrderSerialNumber = $"#PO{entity.PurchaseOrder.Id}V{entity.PurchaseOrder.VendorId}";
            dto.Status = entity.Status;
            dto.Rank = entity.Rank;
            dto.CreatedBy = entity.CreatedBy;
            dto.CreationDate = entity.CreationDate;
            dto.ModifiedBy = entity.ModifiedBy;
            dto.ModificationDate = entity.ModificationDate;
            dto.Version = entity.Version;
            dto.BusinessId = entity.BusinessId;

            return dto;
        }

        //for create operation
        private Bill MapToEntity(BillFormDto dto, Bill bill)
        {
            bill.BillTypeId = dto.BillTypeId;
            bill.BillDate = dto.BillDate;
            bill.BillDueDate = dto.BillDueDate;
            bill.PurchaseOrderId = dto.PurchaseOrderId;
            bill.Version = 1;
            bill.BusinessId = "IMS-1";
            bill.Status = 1;

            return bill;
        }

        //for update action
        private Bill MapToEntity(BillDto dto, Bill bill)
        {
            bill.Id = dto.Id;
            bill.CreatedBy = dto.CreatedBy;
            bill.CreationDate = dto.CreationDate;
            bill.Rank = dto.Rank;
            bill.BillTypeId = dto.BillTypeId;
            bill.BillDate = dto.BillDate;
            bill.BillDueDate = dto.BillDueDate;
            bill.PurchaseOrderId = dto.PurchaseOrderId;
            bill.Version = 1;
            bill.BusinessId = "IMS-1";
            bill.Status = dto.Status;

            return bill;
        }
    }
}