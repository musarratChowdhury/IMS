using System;
using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.Shipment
{
    public sealed class ShipmentDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShippingAddress { get; set; }
        public long SalesOrderId { get; set; }
        public string SalesOrderSerialNumber { get; set; }
        public long ShipmentTypeId { get; set; }
        public string ShipmentTypeName { get; set; }
    }

    public sealed class ShipmentFormDto
    {
        public DateTime ShipmentDate { get; set; }
        public string ShippingAddress { get; set; }
        public long SalesOrderId { get; set; }
        public long ShipmentTypeId { get; set; }
    }
}