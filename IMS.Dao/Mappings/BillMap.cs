using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using IMS.Dao.Mappings;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class BillMap : ClassMap<Bill>
    {
        public BillMap()
        {
            Table("Bill");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.BillDate).Column("BillDate").Not.Nullable();
            Map(x => x.BillDueDate).Column("BillDueDate").Not.Nullable();
            Map(x => x.BillTypeId).Column("BillTypeId").Not.Nullable();
            Map(x => x.PurchaseOrderId).Column("PurchaseOrderId").Not.Nullable();

            References(x => x.BillType)
                .Column("BillTypeId")
                .Not.Insert()
                .Not.Update();

            References(x => x.PurchaseOrder)
                .Column("PurchaseOrderId")
                .Not.Insert()
                .Not.Update();

            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreationDate).Column("CreationDate").Not.Nullable();
            Map(x => x.ModifiedBy).Column("ModifiedBy");
            Map(x => x.ModificationDate).Column("ModificationDate");
            Map(x => x.Rank).Column("Rank").Not.Nullable();
            Map(x => x.BusinessId).Column("BusinessId").Length(256);
            Map(x => x.Version).Column("Version").Not.Nullable();
            Map(x => x.Status).Column("Status").Not.Nullable();

            HasMany(x => x.PaymentVoucherList)
                .KeyColumn("BillId")
                .Cascade.Delete()
                .Inverse()
                .LazyLoad();
        }
    }

}
