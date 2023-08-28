using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.Invoice;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class InvoiceService : BaseSecondaryService<Invoice>
    {
        private readonly IBaseDao<Invoice> _baseDao = new BaseDao<Invoice>();
        private readonly IBaseDao<SalesOrder> _salesOrderDao = new BaseDao<SalesOrder>();

        public async Task<List<InvoiceDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities = await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new InvoiceDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(InvoiceFormDto invoiceFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var invoice = new Invoice();
                    var mappedInvoice = MapToEntity(invoiceFormDto, invoice);
                    mappedInvoice.Rank = GetNextRank(session);
                    mappedInvoice.CreatedBy = userId;
                    mappedInvoice.CreationDate = DateTime.Now;
                    var salesOrder = await _salesOrderDao.GetById(mappedInvoice.SalesOrderId, session);
                    mappedInvoice.SalesOrder = salesOrder;
                    await _baseDao.Create(mappedInvoice, session);
                    salesOrder.InvoiceId = mappedInvoice.Id;
                    await _salesOrderDao.Update(salesOrder, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(InvoiceDto invoiceDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var invoice = new Invoice();
                    var mappedInvoice = MapToEntity(invoiceDto, invoice);
                    mappedInvoice.ModificationDate = DateTime.Now;
                    mappedInvoice.ModifiedBy = modifiedById;

                    await _baseDao.Update(mappedInvoice, sess);

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
                var entities = await _baseDao.GetAll(session);
                return (from t in entities let dto = new DropDownDto() select MapToDropDownDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DropDownDto MapToDropDownDto(Invoice entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#IN{entity.Id}SO{entity.SalesOrderId}";
            dto.DueAmount = entity.SalesOrder.DueAmount;
            return dto;
        }

        private InvoiceDto MapToDto(Invoice entity, InvoiceDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#IN{entity.Id}SO{entity.SalesOrderId}";
            dto.InvoiceDueDate = entity.InvoiceDueDate;
            dto.InvoiceTypeId = entity.InvoiceTypeId;
            dto.InvoiceTypeName = entity.InvoiceType.Name;
            dto.InvoiceDate = entity.InvoiceDate;
            dto.SalesOrderId = entity.SalesOrderId;
            dto.SalesOrderSerialNumber = $"#SO{entity.SalesOrder.Id}C{entity.SalesOrder.CustomerId}";
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
        private Invoice MapToEntity(InvoiceFormDto dto, Invoice invoice)
        {
            invoice.InvoiceTypeId = dto.InvoiceTypeId;
            invoice.InvoiceDate = dto.InvoiceDate;
            invoice.InvoiceDueDate = dto.InvoiceDueDate;
            invoice.SalesOrderId = dto.SalesOrderId;
            invoice.Version = 1;
            invoice.BusinessId = "IMS-1";
            invoice.Status = 1;

            return invoice;
        }

        //for update action
        private Invoice MapToEntity(InvoiceDto dto, Invoice invoice)
        {
            invoice.Id = dto.Id;
            invoice.CreatedBy = dto.CreatedBy;
            invoice.CreationDate = dto.CreationDate;
            invoice.Rank = dto.Rank;
            invoice.InvoiceTypeId = dto.InvoiceTypeId;
            invoice.InvoiceDate = dto.InvoiceDate;
            invoice.InvoiceDueDate = dto.InvoiceDueDate;
            invoice.SalesOrderId = dto.SalesOrderId;
            invoice.Version = 1;
            invoice.BusinessId = "IMS-1";
            invoice.Status = dto.Status;

            return invoice;
        }
    }
}