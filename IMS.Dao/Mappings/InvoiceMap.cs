using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class InvoiceMap : ClassMap<Invoice>
    {
        public InvoiceMap()
        {
            Table("Invoice"); 

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.InvoiceDate).Column("InvoiceDate").Not.Nullable();
            Map(x => x.InvoiceDueDate).Column("InvoiceDueDate").Not.Nullable();
            Map(x => x.InvoiceTypeId).Column("InvoiceTypeId").Not.Nullable();
            Map(x => x.SalesOrderId).Column("SalesOrderId").Not.Nullable();

            References(x => x.InvoiceType) 
                .Column("InvoiceTypeId")
                .Not.Insert()
                .Not.Update();

            References(x => x.SalesOrder)
                .Column("SalesOrderId")
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
            Map(x=>x.Status).Column("Status").Not.Nullable();

            HasMany(x => x.PaymentReceivedList)
                .KeyColumn("InvoiceId")
                .Cascade.All()
                .LazyLoad()
                .Inverse();
        }
    }
}
