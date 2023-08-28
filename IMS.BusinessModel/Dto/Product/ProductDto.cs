using System;
using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.Product
{
    public sealed class ProductDto:BaseDto
    {
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal? BuyingUnitPrice { get; set; }
        public decimal? SellingUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int SKU { get; set; }
        public DateTime? ManufacturingDate { get; set; }

        public long UnitOfMeasurementId { get; set; }
        public string UnitOfMeasurementName { get; set; }
        public long ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }

    public sealed class ProductFormDto
    {
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal? BuyingUnitPrice { get; set; }
        public decimal? SellingUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int SKU { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public long UnitOfMeasurementId { get; set; }
        public long ProductCategoryId { get; set; }
    }
}