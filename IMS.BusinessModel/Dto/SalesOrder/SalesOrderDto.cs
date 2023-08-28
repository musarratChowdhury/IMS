using System.Collections.Generic;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Entity.OrderDetails;

namespace IMS.BusinessModel.Dto.SalesOrder
{
    public sealed class SalesOrderDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }
        public bool IsArchived { get; set; } = false;
        public int ShipmentStatus { get; set; } = 0;
        public int PaymentStatus { get; set; } = 0;
        
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? InvoiceId { get; set; }
        public string InvoiceSerialNumber { get; set; } = string.Empty;
        
        public List<SalesOrderLine> SalesOrderLines { get; set; }
    }

    public sealed class SalesOrderFormDto
    {
        public long CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        
        public List<SalesOrderLine> SalesOrderLines { get; set; }
    }
}