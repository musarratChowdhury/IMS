using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PurchaseOrder;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class PurchaseOrderService : BaseSecondaryService<PurchaseOrder>
    {
        private readonly IBaseDao<PurchaseOrder> _baseDao;
        public PurchaseOrderService()
        {
            _baseDao = new BaseDao<PurchaseOrder>();
        }

        public async Task<List<PurchaseOrderDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities =await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new PurchaseOrderDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(PurchaseOrderFormDto purchaseOrderFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var productDao = new BaseDao<Product>();
                    var purchaseOrder = new PurchaseOrder();
                    var mappedPurchaseOrder = MapToEntity(purchaseOrderFormDto, purchaseOrder);
                    mappedPurchaseOrder.Rank = GetNextRank(session);
                    mappedPurchaseOrder.CreatedBy = userId;
                    mappedPurchaseOrder.CreationDate = DateTime.Now;
                    foreach (var purchaseOrderLine in mappedPurchaseOrder.PurchaseOrderLines)
                    {
                        purchaseOrderLine.PurchaseOrder = mappedPurchaseOrder;
                        purchaseOrderLine.Product =  await productDao.GetById(purchaseOrderLine.ProductId, session);
                        purchaseOrderLine.Product .StockQuantity += purchaseOrderLine.Quantity;
                        await productDao.Update(purchaseOrderLine.Product , session);
                    }
                    
                    await _baseDao.Create(mappedPurchaseOrder, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(PurchaseOrderDto purchaseOrderDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var purchaseOrder = new PurchaseOrder();
                    var mappedPurchaseOrder = MapToEntity(purchaseOrderDto, purchaseOrder);
                    mappedPurchaseOrder.ModificationDate = DateTime.Now;
                    mappedPurchaseOrder.ModifiedBy = modifiedById;
                    
                    await _baseDao.Update(mappedPurchaseOrder, sess);
                    
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        
        public async Task ArchiveRecord(long entityId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var entity =await _baseDao.GetById(entityId, session);
                    if (entity != null)
                    {
                        entity.IsArchived = true;
                        entity.Status = 404;
                        await _baseDao.Update(entity, session);
                        await transaction.CommitAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
        
        private DropDownDto MapToDropDownDto(PurchaseOrder entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#PO{entity.Id}V{entity.VendorId}";

            return dto;
        }
        
        private PurchaseOrderDto MapToDto(PurchaseOrder entity, PurchaseOrderDto dto)
        {
            dto.SerialNumber =  $"#PO{entity.Id}V{entity.VendorId}";
            dto.Id = entity.Id;
            dto.PaymentStatus = entity.PaymentStatus;
            dto.IsArchived = entity.IsArchived;
            dto.TotalAmount = entity.TotalAmount;
            dto.DueAmount = entity.DueAmount;
            dto.VendorId = entity.VendorId;
            dto.VendorName = entity.Vendor.FirstName + " " + entity.Vendor.LastName;
            dto.BillId = entity.BillId;
            dto.BillSerialNumber =entity.Bill!=null? $"#BL{entity.BillId}PO{entity.Id}":"NOT BILLED";
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
        private PurchaseOrder MapToEntity(PurchaseOrderFormDto dto, PurchaseOrder purchaseOrder)
        {
            purchaseOrder.TotalAmount = dto.TotalAmount;
            purchaseOrder.DueAmount = dto.TotalAmount;
            purchaseOrder.VendorId = dto.VendorId;
            purchaseOrder.PurchaseOrderLines = dto.PurchaseOrderLines;
            purchaseOrder.Version = 1;
            purchaseOrder.BusinessId = "IMS-1";
            purchaseOrder.Status = 1;

            return purchaseOrder;
        }

        //for update action
        private PurchaseOrder MapToEntity(PurchaseOrderDto dto, PurchaseOrder purchaseOrder)
        {
            purchaseOrder.Id = dto.Id;
            purchaseOrder.CreatedBy = dto.CreatedBy;
            purchaseOrder.CreationDate = dto.CreationDate;
            purchaseOrder.Rank = dto.Rank;
            purchaseOrder.VendorId = dto.VendorId;
            purchaseOrder.PaymentStatus = dto.PaymentStatus;
            purchaseOrder.IsArchived = dto.IsArchived;
            purchaseOrder.BillId = dto.BillId;
            purchaseOrder.TotalAmount = dto.TotalAmount;
            purchaseOrder.DueAmount = dto.DueAmount;
            purchaseOrder.PurchaseOrderLines = dto.PurchaseOrderLines;
            purchaseOrder.Version = 1;
            purchaseOrder.BusinessId = "IMS-1";
            purchaseOrder.Status = dto.Status;

            return purchaseOrder;
        }
    }
}