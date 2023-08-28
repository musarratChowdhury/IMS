using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class BillType : ConfigurationEntity
    {
        public virtual IList<Bill> Bills { get; set; }
    }
}
