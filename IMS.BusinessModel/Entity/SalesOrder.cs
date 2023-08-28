using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity
{
    public class SalesOrder : BaseEntity
    {
        public virtual decimal TotalAmount { get; set; }
        public virtual decimal DueAmount { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual int ShipmentStatus { get; set; }
        public virtual int PaymentStatus { get; set; }

        // Navigation property for the foreign key
        public virtual long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual long? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public virtual IList<SalesOrderLine> SalesOrderLines { get; set; }
        public virtual IList<Shipment> Shipments { get; set; }
    }
}
