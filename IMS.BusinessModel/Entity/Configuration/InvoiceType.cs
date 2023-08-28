using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class InvoiceType : ConfigurationEntity
    {
        public virtual IList<Invoice> Invoices { get; set;}
    }
}
