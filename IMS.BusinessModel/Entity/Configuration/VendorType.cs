using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class VendorType : ConfigurationEntity
    {
        public virtual IList<Vendor> Vendors { get; set; }
    }
}
