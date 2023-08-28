using System;
using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.Bill
{
    public sealed class BillDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillDueDate { get; set; }
        public long BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public long PurchaseOrderId {get; set;}
        public string PurchaseOrderSerialNumber {get;set;}
    }

    public sealed class BillFormDto
    {
        public DateTime BillDate { get; set; }
        public DateTime BillDueDate { get; set; }
        public long BillTypeId { get; set; }
        public long PurchaseOrderId {get; set;}
    }
}