using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity
{
    public class Invoice : BaseEntity
    {
        public virtual DateTime InvoiceDate { get; set; }
        public virtual DateTime InvoiceDueDate { get; set; }

        // Navigation properties for foreign keys
        public virtual long InvoiceTypeId { get; set; }
        public virtual InvoiceType InvoiceType { get; set; }
        public virtual long SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

        public virtual IList<PaymentReceived> PaymentReceivedList { get; set; }
    }
}
