using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.PaymentVoucher;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class PaymentVoucherService : BaseSecondaryService<PaymentVoucher>
    {
        private readonly IBaseDao<PaymentVoucher> _baseDao = new BaseDao<PaymentVoucher>();
        private readonly IBaseDao<Bill> _billDao = new BaseDao<Bill>();
        private readonly IBaseDao<PurchaseOrder> _purchaseOrderDao = new BaseDao<PurchaseOrder>();

        public async Task<List<PaymentVoucherDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities = await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new PaymentVoucherDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(PaymentVoucherFormDto paymentVoucherFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var paymentVoucher = new PaymentVoucher();
                    var mappedPaymentVoucher = MapToEntity(paymentVoucherFormDto, paymentVoucher);
                    mappedPaymentVoucher.Rank = GetNextRank(session);
                    mappedPaymentVoucher.CreatedBy = userId;
                    mappedPaymentVoucher.CreationDate = DateTime.Now;
                    mappedPaymentVoucher.Bill = await _billDao.GetById(mappedPaymentVoucher.BillId, session);
                    await _baseDao.Create(mappedPaymentVoucher, session);
                    var relatedSalesOrder =
                        await _purchaseOrderDao.GetById(mappedPaymentVoucher.Bill.PurchaseOrderId, session);
                    relatedSalesOrder.DueAmount -= mappedPaymentVoucher.PaymentAmount;
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

                    await _purchaseOrderDao.Update(relatedSalesOrder, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Update(PaymentVoucherDto paymentVoucherDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var paymentVoucher = new PaymentVoucher();
                    var mappedPaymentVoucher = MapToEntity(paymentVoucherDto, paymentVoucher);
                    mappedPaymentVoucher.ModificationDate = DateTime.Now;
                    mappedPaymentVoucher.ModifiedBy = modifiedById;

                    await _baseDao.Update(mappedPaymentVoucher, sess);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private PaymentVoucherDto MapToDto(PaymentVoucher entity, PaymentVoucherDto dto)
        {
            dto.Id = entity.Id;
            dto.SerialNumber = $"#PV{entity.Id}BL{entity.BillId}";
            dto.PaymentDate = entity.PaymentDate;
            dto.PaymentAmount = entity.PaymentAmount;
            dto.DueAmount = entity.Bill.PurchaseOrder.DueAmount;
            dto.BillId = entity.BillId;
            dto.BillSerialNumber = $"#BL{entity.Bill.Id}PO{entity.Bill.PurchaseOrderId}";
            dto.PaymentTypeId = entity.PaymentTypeId;
            dto.PaymentTypeName = entity.PaymentType.Name;
            dto.CashBankId = entity.CashBankId;
            dto.CashBankName = entity.CashBank!=null? entity.CashBank.Name : "Cash Payment";
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
        private PaymentVoucher MapToEntity(PaymentVoucherFormDto dto, PaymentVoucher paymentVoucher)
        {
            paymentVoucher.PaymentDate = dto.PaymentDate;
            paymentVoucher.PaymentAmount = dto.PaymentAmount;
            paymentVoucher.BillId = dto.BillId;
            paymentVoucher.PaymentTypeId = dto.PaymentTypeId;
            paymentVoucher.CashBankId = dto.CashBankId;
            paymentVoucher.Version = 1;
            paymentVoucher.BusinessId = "IMS-1";
            paymentVoucher.Status = 1;

            return paymentVoucher;
        }

        //for update action
        private PaymentVoucher MapToEntity(PaymentVoucherDto dto, PaymentVoucher paymentVoucher)
        {
            paymentVoucher.Id = dto.Id;
            paymentVoucher.CreatedBy = dto.CreatedBy;
            paymentVoucher.CreationDate = dto.CreationDate;
            paymentVoucher.Rank = dto.Rank;
            paymentVoucher.PaymentDate = dto.PaymentDate;
            paymentVoucher.PaymentAmount = dto.PaymentAmount;
            paymentVoucher.BillId = dto.BillId;
            paymentVoucher.CashBankId = dto.CashBankId;
            paymentVoucher.PaymentTypeId = dto.PaymentTypeId;
            paymentVoucher.Version = 1;
            paymentVoucher.BusinessId = "IMS-1";
            paymentVoucher.Status = dto.Status;

            return paymentVoucher;
        }
    }
}