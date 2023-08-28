using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class CustomerType : ConfigurationEntity
    {
        public virtual IList<Customer> Customers { get; set; }
    }
}
