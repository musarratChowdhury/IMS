using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity.OrderDetails
{
 

    public class PurchaseOrderLine
    {
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual decimal Total { get; set; }

        // Navigation properties for foreign keys
        public virtual long PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual long ProductId { get; set; }
        public virtual Product Product { get; set; }

        #region Overriden Methods
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PurchaseOrderLine)obj;
            return PurchaseOrderId == other.PurchaseOrderId && ProductId == other.ProductId;
        }

        public override int GetHashCode()
        {
            // Combine hash codes of the individual properties to create a unique hash code for the composite key
            return (PurchaseOrderId.GetHashCode() * 397) ^ ProductId.GetHashCode();
        }
        #endregion
    }
}
