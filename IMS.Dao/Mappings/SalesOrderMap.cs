using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;

namespace IMS.Dao.Mappings
{
    public class SalesOrderMap : ClassMap<SalesOrder>
    {
        public SalesOrderMap()
        {
            Table("SalesOrder");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.CustomerId).Column("CustomerId").Not.Nullable();
            Map(x => x.InvoiceId).Column("InvoiceId");
            Map(x => x.TotalAmount).Column("TotalAmount").Not.Nullable();
            Map(x => x.DueAmount).Column("DueAmount").Not.Nullable();
            Map(x => x.IsArchived).Column("IsArchived").Not.Nullable();
            Map(x => x.ShipmentStatus).Column("ShipmentStatus").Not.Nullable();
            Map(x => x.PaymentStatus).Column("PaymentStatus").Not.Nullable();

            References(x => x.Customer) // Many-to-one relationship with Customer
                .Column("CustomerId")
                .Not.Insert()
                .Not.Update()
                .LazyLoad();

            References(x => x.Invoice)
                .Column("InvoiceId")
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
            Map(x => x.Status).Column("Status").Not.Nullable();

            HasMany(x => x.SalesOrderLines)
                .KeyColumn("SalesOrderId")
                .Cascade.All()
                .LazyLoad()
                .Inverse();

            HasMany(x => x.Shipments)
                .KeyColumn("SalesOrderId")
                .Cascade.All()
                .LazyLoad()
                .Inverse();
        }
    }
}