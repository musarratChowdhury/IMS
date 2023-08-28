using System;
using System.Collections.Generic;
using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;

namespace IMS.BusinessModel.Entity
{
    public class Bill : BaseEntity
    {
        public virtual DateTime BillDate { get; set; }
        public virtual DateTime BillDueDate { get; set; }

        // Navigation properties for foreign keys
        public virtual long BillTypeId { get; set; }
        public virtual BillType BillType { get; set; }
        public virtual PurchaseOrder PurchaseOrder {get;set;}
        public virtual long PurchaseOrderId {get; set;}
        public virtual IList<PaymentVoucher> PaymentVoucherList { get; set; } = null;
    }
}
