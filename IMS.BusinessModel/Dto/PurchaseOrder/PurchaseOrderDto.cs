using System.Collections.Generic;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Entity.OrderDetails;

namespace IMS.BusinessModel.Dto.PurchaseOrder
{
    public sealed class PurchaseOrderDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public int PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }
        public bool IsArchived { get; set; }

        // Navigation property for the foreign key
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public long? BillId { get; set; }
        public string BillSerialNumber { get; set; }

        public List<PurchaseOrderLine> PurchaseOrderLines { get; set;}
    }

    public sealed class PurchaseOrderFormDto
    {
        public long VendorId { get; set; }
        public decimal TotalAmount { get; set; }
        
        public List<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}