using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customer"); // Specify the table name if different

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.FirstName).Column("FirstName").Length(55).Not.Nullable();
            Map(x => x.LastName).Column("LastName").Length(55).Not.Nullable();
            Map(x => x.Address).Column("Address").Length(255);
            Map(x => x.Email).Column("Email").Length(100).Not.Nullable();
            Map(x => x.Phone).Column("Phone").Length(100);
            Map(x => x.Status).Column("Status").Not.Nullable();
            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreationDate).Column("CreationDate").Not.Nullable();
            Map(x => x.ModifiedBy).Column("ModifiedBy");
            Map(x => x.ModificationDate).Column("ModificationDate");
            Map(x => x.Rank).Column("Rank").Not.Nullable();
            Map(x => x.BusinessId).Column("BusinessId").Length(256);
            Map(x => x.Version).Column("Version").Not.Nullable();
            Map(x => x.CustomerTypeId).Column("CustomerTypeId").Not.Nullable();

            References(x => x.CustomerType)
                .Column("CustomerTypeId")
                .Not.Nullable()
                .Not.Insert()
                .Not.Update();

            HasMany(x => x.SalesOrders)
                .KeyColumn("CustomerId")
                .Inverse()
                .LazyLoad();
        }
    }
}
