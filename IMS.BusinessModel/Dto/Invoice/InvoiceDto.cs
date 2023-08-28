using System;
using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.Invoice
{
    public sealed class InvoiceDto : BaseDto
    {
        public string SerialNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public long InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public long SalesOrderId { get; set; }
        public string SalesOrderSerialNumber { get; set; }
    }

    public sealed class InvoiceFormDto
    {
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public long InvoiceTypeId { get; set; }
        public long SalesOrderId { get; set; }
    }
}