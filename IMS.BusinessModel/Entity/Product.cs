using IMS.BusinessModel.Entity.Common;
using IMS.BusinessModel.Entity.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
namespace IMS.BusinessModel.Entity
{
    public class Product : BaseEntity
    {
        public virtual string ProductName { get; set; }
        public virtual string ProductImageUrl { get; set; }
        public virtual decimal? BuyingUnitPrice { get; set; }
        public virtual decimal? SellingUnitPrice { get; set; }
        public virtual int StockQuantity { get; set; }
        public virtual int SKU { get; set; }
        public virtual DateTime? ManufacturingDate { get; set; }

        // Navigation properties for the foreign keys
        public virtual long UnitOfMeasurementId { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public virtual long ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
