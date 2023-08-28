using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class CashBankMap : ClassMap<CashBank>
    {
        public CashBankMap()
        {
            Table("CashBank");

            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.Name).Column("Name").Length(55).Not.Nullable();
            Map(x => x.Description).Column("Description").Length(256);
            Map(x => x.Status).Column("Status").Not.Nullable();
            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreationDate).Column("CreationDate").Not.Nullable();
            Map(x => x.ModifiedBy).Column("ModifiedBy");
            Map(x => x.ModificationDate).Column("ModificationDate");
            Map(x => x.Rank).Column("Rank").Not.Nullable();
            Map(x => x.BusinessId).Column("BusinessId").Length(256);
            Map(x => x.Version).Column("Version").Not.Nullable();

            HasMany(x => x.PaymentVoucherList)
                .KeyColumn("CashBankId")
                .Inverse()
                .LazyLoad();
        }
    }
}
