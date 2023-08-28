using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class PaymentReceivedMap : ClassMap<PaymentReceived>
    {
        public PaymentReceivedMap()
        {
            Table("PaymentReceived");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.PaymentDate).Column("PaymentDate").Not.Nullable();
            Map(x => x.PaymentAmount).Column("PaymentAmount").Not.Nullable();
            Map(x => x.Status).Column("Status").Not.Nullable();
            Map(x => x.InvoiceId).Column("InvoiceId").Not.Nullable();
            Map(x => x.PaymentTypeId).Column("PaymentTypeId").Not.Nullable();

            References(x => x.Invoice) // Many-to-one relationship with Invoice
                .Column("InvoiceId")
                .Not.Insert()
                .Not.Update();

            References(x => x.PaymentType) // Many-to-one relationship with PaymentType
                .Column("PaymentTypeId")
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
