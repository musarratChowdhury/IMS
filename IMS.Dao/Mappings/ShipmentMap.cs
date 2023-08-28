using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class ShipmentMap : ClassMap<Shipment>
    {
        public ShipmentMap()
        {
            Table("Shipment");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.ShipmentDate).Column("ShipmentDate").Not.Nullable();
            Map(x => x.ShippingAddress).Column("ShippingAddress").Length(100).Not.Nullable();
            Map(x => x.SalesOrderId).Column("SalesOrderId").Not.Nullable();
            Map(x => x.ShipmentTypeId).Column("ShipmentTypeId").Not.Nullable();
            Map(x => x.Status).Column("Status").Not.Nullable();

            References(x => x.SalesOrder) // Many-to-one relationship with SalesOrder
                .Column("SalesOrderId")
                .Not.Insert()
                .Not.Update();

            References(x => x.ShipmentType) // Many-to-one relationship with ShipmentType
                .Column("ShipmentTypeId")
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
