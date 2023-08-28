using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Data;
using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity.Configuration;
using IMS.BusinessModel.Entity;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class PaymentVoucherMap : ClassMap<PaymentVoucher>
    {
        public PaymentVoucherMap()
        {
            Table("PaymentVoucher");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.PaymentDate).Column("PaymentDate").Not.Nullable();
            Map(x => x.PaymentAmount).Column("PaymentAmount").Not.Nullable();
            Map(x => x.Status).Column("Status").Not.Nullable();
            Map(x => x.BillId).Column("BillId").Not.Nullable();
            Map(x => x.PaymentTypeId).Column("PaymentTypeId").Not.Nullable();
            Map(x => x.CashBankId).Column("CashBankId");

            References(x => x.Bill) // Many-to-one relationship with Bill
                .Column("BillId")
                .Not.Insert()
                .Not.Update();

            References(x => x.PaymentType) // Many-to-one relationship with PaymentType
                .Column("PaymentTypeId")
                .Not.Insert()
                .Not.Update();

            References(x => x.CashBank) // Many-to-one relationship with CashBank
                .Column("CashBankId")
                .Not.Insert()
                .Not.Update();

            // BaseEntity properties
            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreationDate).Column("CreationDate").Not.Nullable();
            Map(x => x.ModifiedBy).Column("ModifiedBy");
            Map(x => x.ModificationDate).Column("ModificationDate");
            Map(x => x.Rank).Column("Rank").Not.Nullable();
            Map(x => x.BusinessId).Column("BusinessId").Length(256);
            Map(x => x.Version).Column("Version").Not.Nullable();
        }
    }
 

}
