using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity.OrderDetails;

namespace IMS.Dao.Mappings.OrderDetails
{
    public class SalesOrderLineMap : ClassMap<SalesOrderLine>
    {
        public SalesOrderLineMap()
        {
            CompositeId()
                .KeyReference(x => x.SalesOrder, "SalesOrderId") // Composite key with SalesOrder reference
                .KeyReference(x => x.Product, "ProductId");

            Map(x => x.Quantity).Column("Quantity").Not.Nullable();
            Map(x => x.UnitPrice).Column("UnitPrice").Not.Nullable();
            Map(x => x.Discount).Column("Discount");
            Map(x => x.Amount).Column("Amount").Not.Nullable();
            Map(x => x.Total).Column("Total").Not.Nullable();
        }
    }
}
