using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity.OrderDetails
{
    public class SalesOrderLine
    {
        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual decimal Total { get; set; }

        // Navigation properties for foreign keys
        public virtual long SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        public virtual long ProductId { get; set; }
        public virtual Product Product { get; set; }

        #region Overriden Methods
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (SalesOrderLine)obj;
            return SalesOrderId == other.SalesOrderId && ProductId == other.ProductId;
        }

        public override int GetHashCode()
        {
            // Combine hash codes of the individual properties to create a unique hash code for the composite key
            return (SalesOrderId.GetHashCode() * 397) ^ ProductId.GetHashCode();
        }

        #endregion
    }

}
