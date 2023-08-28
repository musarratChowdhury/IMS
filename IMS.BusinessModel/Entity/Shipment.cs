using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity
{
    public class Shipment : BaseEntity
    {
        public virtual DateTime ShipmentDate { get; set; }
        public virtual string ShippingAddress { get; set; }

        // Navigation properties for foreign keys
        public virtual long SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        public virtual long ShipmentTypeId { get; set; }
        public virtual ShipmentType ShipmentType { get; set; }
    }
}
