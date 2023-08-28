using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class ProductCategory : ConfigurationEntity
    {
        public virtual IList<Product> Products { get; set; }
    }
}
