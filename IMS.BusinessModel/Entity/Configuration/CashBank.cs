using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class CashBank : ConfigurationEntity
    {
        public virtual IList<PaymentVoucher> PaymentVoucherList { get; set;}
    }
}
