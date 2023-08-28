using System;
using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.PaymentReceive
{
    public sealed class PaymentReceiveDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DueAmount { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceSerialNumber { get; set; }
        public long PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }

    public sealed class PaymentReceiveFormDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public long InvoiceId { get; set; }
        public long PaymentTypeId { get; set; }
    }
}