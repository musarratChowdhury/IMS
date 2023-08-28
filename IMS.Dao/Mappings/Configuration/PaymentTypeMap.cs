using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity.Configuration;

namespace IMS.Dao.Mappings
{
    public class PaymentTypeMap : ClassMap<PaymentType>
    {
        public PaymentTypeMap()
        {
            Table("PaymentType");

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

            HasMany(x => x.PaymentReceivedList)
                .KeyColumn("PaymentTypeId")
                .Inverse()
                .LazyLoad();

            HasMany(x => x.PaymentVoucherList)
                .KeyColumn("PaymentTypeId")
                .Inverse()
                .LazyLoad();
        }
    }
}








