using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.SalesOrder;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class SalesOrderService : BaseSecondaryService<SalesOrder>
    {
        private readonly IBaseDao<SalesOrder> _baseDao;
        public SalesOrderService()
        {
            _baseDao = new BaseDao<SalesOrder>();
        }

        public async Task<List<SalesOrderDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities =await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new SalesOrderDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(SalesOrderFormDto salesOrderFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var productDao = new BaseDao<Product>();
                    var salesOrder = new SalesOrder();
                    var mappedSalesOrder = MapToEntity(salesOrderFormDto, salesOrder);
                    mappedSalesOrder.Rank = GetNextRank(session);
                    mappedSalesOrder.CreatedBy = userId;
                    mappedSalesOrder.CreationDate = DateTime.Now;
                    foreach (var salesOrderLine in mappedSalesOrder.SalesOrderLines)
                    {
                        salesOrderLine.SalesOrder = mappedSalesOrder;
                        salesOrderLine.Product = await productDao.GetById(salesOrderLine.ProductId, session);
                        salesOrderLine.Product .StockQuantity -= salesOrderLine.Quantity;
                        await productDao.Update(salesOrderLine.Product , session);
                    }
                    
                    await _baseDao.Create(mappedSalesOrder, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(SalesOrderDto salesOrderDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var salesOrder = new SalesOrder();
                    var mappedSalesOrder = MapToEntity(salesOrderDto, salesOrder);
                    mappedSalesOrder.ModificationDate = DateTime.Now;
                    mappedSalesOrder.ModifiedBy = modifiedById;
                    
                    await _baseDao.Update(mappedSalesOrder, sess);
                    
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        
        public async Task<List<(int Month, decimal Total)>> GetMonthlyTotalSales(ISession session)
        {
            try
            {
                var listOfMonthlySales = await _baseDao.GetMonthlyTotalSales(session);
                return listOfMonthlySales;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
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
        
        private DropDownDto MapToDropDownDto(SalesOrder entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#SO{entity.Id}C{entity.CustomerId}";

            return dto;
        }
        
        private SalesOrderDto MapToDto(SalesOrder entity, SalesOrderDto dto)
        {
            dto.SerialNumber =  $"#SO{entity.Id}C{entity.CustomerId}";
            dto.Id = entity.Id;
            dto.ShipmentStatus = entity.ShipmentStatus;
            dto.PaymentStatus = entity.PaymentStatus;
            dto.IsArchived = entity.IsArchived;
            dto.TotalAmount = entity.TotalAmount;
            dto.DueAmount = entity.DueAmount;
            dto.CustomerId = entity.CustomerId;
            dto.CustomerName = entity.Customer.FirstName + " " + entity.Customer.LastName;
            dto.InvoiceId = entity.InvoiceId;
            dto.InvoiceSerialNumber =entity.Invoice!=null? $"#IN{entity.InvoiceId}SO{entity.Id}":"NOT INVOICED";
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
        private SalesOrder MapToEntity(SalesOrderFormDto dto, SalesOrder salesOrder)
        {
            salesOrder.TotalAmount = dto.TotalAmount;
            salesOrder.DueAmount = dto.TotalAmount;
            salesOrder.CustomerId = dto.CustomerId;
            salesOrder.SalesOrderLines = dto.SalesOrderLines;
            salesOrder.Version = 1;
            salesOrder.BusinessId = "IMS-1";
            salesOrder.Status = 1;

            return salesOrder;
        }

        //for update action
        private SalesOrder MapToEntity(SalesOrderDto dto, SalesOrder salesOrder)
        {
            salesOrder.Id = dto.Id;
            salesOrder.CreatedBy = dto.CreatedBy;
            salesOrder.CreationDate = dto.CreationDate;
            salesOrder.Rank = dto.Rank;
            salesOrder.CustomerId = dto.CustomerId;
            salesOrder.ShipmentStatus = dto.ShipmentStatus;
            salesOrder.PaymentStatus = dto.PaymentStatus;
            salesOrder.IsArchived = dto.IsArchived;
            salesOrder.InvoiceId = dto.InvoiceId;
            salesOrder.TotalAmount = dto.TotalAmount;
            salesOrder.DueAmount = dto.DueAmount;
            salesOrder.SalesOrderLines = dto.SalesOrderLines;
            salesOrder.Version = 1;
            salesOrder.BusinessId = "IMS-1";
            salesOrder.Status = dto.Status;

            return salesOrder;
        }
    }
}