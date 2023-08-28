using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;

namespace IMS.BusinessModel.Entity
{
    public class Vendor : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }

        // Navigation properties for foreign keys
        public virtual long VendorTypeId { get; set; }
        public virtual VendorType VendorType { get; set; }
        public virtual IList<PurchaseOrder> PurchaseOrders { get; set;}
    }
}
