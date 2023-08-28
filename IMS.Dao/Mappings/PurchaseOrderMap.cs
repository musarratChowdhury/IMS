using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class PurchaseOrderMap : ClassMap<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            Table("PurchaseOrder");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity(); 
            Map(x => x.VendorId).Column("VendorId").Not.Nullable();
            Map(x => x.BillId).Column("BillId");
            Map(x => x.PaymentStatus).Column("PaymentStatus").Not.Nullable();
            Map(x => x.TotalAmount).Column("TotalAmount").Not.Nullable();
            Map(x => x.DueAmount).Column("DueAmount").Not.Nullable();
            Map(x => x.IsArchived).Column("IsArchived").Not.Nullable();

            References(x => x.Vendor) 
                .Column("VendorId")
                .Not.Insert()
                .Not.Update();

            References(x => x.Bill)
                .Column("BillId")
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

            HasMany(x => x.PurchaseOrderLines)
                .KeyColumn("PurchaseOrderId")
                .Cascade.All()
                .LazyLoad()
                .Inverse();
        }
    }
}
