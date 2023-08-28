using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class ShipmentType : ConfigurationEntity
    {
        public virtual IList<Shipment> Shipments { get; set;}
    }
}
