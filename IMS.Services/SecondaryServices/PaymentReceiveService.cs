using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PaymentReceive;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class PaymentReceiveService : BaseSecondaryService<PaymentReceived>
    {
        private readonly IBaseDao<PaymentReceived> _baseDao = new BaseDao<PaymentReceived>();
        private readonly IBaseDao<SalesOrder> _salesOrderDao = new BaseDao<SalesOrder>();
        private readonly IBaseDao<Invoice> _invoiceDao = new BaseDao<Invoice>();

        public async Task<List<PaymentReceiveDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities = await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new PaymentReceiveDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(PaymentReceiveFormDto paymentReceiveFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var paymentReceive = new PaymentReceived();
                    var mappedPaymentReceive = MapToEntity(paymentReceiveFormDto, paymentReceive);
                    mappedPaymentReceive.Rank = GetNextRank(session);
                    mappedPaymentReceive.CreatedBy = userId;
                    mappedPaymentReceive.CreationDate = DateTime.Now;
                    var relatedInvoice = await _invoiceDao.GetById(mappedPaymentReceive.InvoiceId, session);
                    mappedPaymentReceive.Invoice = relatedInvoice;
                    await _baseDao.Create(mappedPaymentReceive, session);
                    var relatedSalesOrder =
                        await _salesOrderDao.GetById(mappedPaymentReceive.Invoice.SalesOrderId, session);
                    relatedSalesOrder.DueAmount -= mappedPaymentReceive.PaymentAmount;
                    if (relatedSalesOrder.DueAmount > 1)
                    {
                        relatedSalesOrder.PaymentStatus = 2;
                    }else if (relatedSalesOrder.DueAmount == 0)
                    {
                        relatedSalesOrder.PaymentStatus = 1;
                    }
                    else
                    {
                        relatedSalesOrder.PaymentStatus = 0;
                    }

                    await _salesOrderDao.Update(relatedSalesOrder, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(PaymentReceiveDto paymentReceiveDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var paymentReceive = new PaymentReceived();
                    var mappedPaymentReceive = MapToEntity(paymentReceiveDto, paymentReceive);
                    mappedPaymentReceive.ModificationDate = DateTime.Now;
                    mappedPaymentReceive.ModifiedBy = modifiedById;

                    await _baseDao.Update(mappedPaymentReceive, sess);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private PaymentReceiveDto MapToDto(PaymentReceived entity, PaymentReceiveDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#PR{entity.Id}IN{entity.InvoiceId}";
            dto.PaymentDate = entity.PaymentDate;
            dto.PaymentAmount = entity.PaymentAmount;
            dto.DueAmount = entity.Invoice.SalesOrder.DueAmount;
            dto.InvoiceId = entity.InvoiceId;
            dto.InvoiceSerialNumber = $"#IN{entity.Invoice.Id}SO{entity.Invoice.SalesOrderId}";
            dto.PaymentTypeId = entity.PaymentTypeId;
            dto.PaymentTypeName = entity.PaymentType.Name;
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
        private PaymentReceived MapToEntity(PaymentReceiveFormDto dto, PaymentReceived paymentReceive)
        {
            paymentReceive.PaymentDate = dto.PaymentDate;
            paymentReceive.PaymentAmount = dto.PaymentAmount;
            paymentReceive.InvoiceId = dto.InvoiceId;
            paymentReceive.PaymentTypeId = dto.PaymentTypeId;
            paymentReceive.Version = 1;
            paymentReceive.BusinessId = "IMS-1";
            paymentReceive.Status = 1;

            return paymentReceive;
        }

        //for update action
        private PaymentReceived MapToEntity(PaymentReceiveDto dto, PaymentReceived paymentReceive)
        {
            paymentReceive.Id = dto.Id;
            paymentReceive.CreatedBy = dto.CreatedBy;
            paymentReceive.CreationDate = dto.CreationDate;
            paymentReceive.Rank = dto.Rank;
            paymentReceive.PaymentDate = dto.PaymentDate;
            paymentReceive.PaymentAmount = dto.PaymentAmount;
            paymentReceive.InvoiceId = dto.InvoiceId;
            paymentReceive.PaymentTypeId = dto.PaymentTypeId;
            paymentReceive.Version = 1;
            paymentReceive.BusinessId = "IMS-1";
            paymentReceive.Status = dto.Status;

            return paymentReceive;
        }
    }
}