using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity
{
    public class PaymentVoucher : BaseEntity
    {
        public virtual DateTime PaymentDate { get; set; }
        public virtual decimal PaymentAmount { get; set; }

        // Navigation properties for foreign keys
        public virtual long BillId { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual long PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual long? CashBankId { get; set; }
        public virtual CashBank CashBank { get; set; }
    }
}
