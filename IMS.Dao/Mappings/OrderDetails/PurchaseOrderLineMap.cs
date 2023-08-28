using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity.OrderDetails;

namespace IMS.Dao.Mappings.OrderDetails
{
    public class PurchaseOrderLineMap : ClassMap<PurchaseOrderLine>
    {
        public PurchaseOrderLineMap()
        {
            CompositeId()
            .KeyReference(x => x.PurchaseOrder, "PurchaseOrderId")
            .KeyReference(x => x.Product, "ProductId");

            Map(x => x.Quantity).Column("Quantity").Not.Nullable();
            Map(x => x.UnitPrice).Column("UnitPrice").Not.Nullable();
            Map(x => x.Amount).Column("Amount").Not.Nullable();
            Map(x => x.Total).Column("Total").Not.Nullable();
        }
    }
}
