using System;
using System.Collections.Generic;
using System.Text;
using IMS.BusinessModel.Entity.Common;

namespace IMS.BusinessModel.Entity.Configuration
{
    public class PaymentType : ConfigurationEntity
    {
        public virtual IList<PaymentReceived> PaymentReceivedList { get; set; }
        public virtual IList<PaymentVoucher> PaymentVoucherList { get; set; }

    }
}
