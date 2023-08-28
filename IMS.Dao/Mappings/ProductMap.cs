using FluentNHibernate.Mapping;
using IMS.BusinessModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Dao.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Product");

            Id(x =>  x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.ProductName).Column("ProductName").Not.Nullable().Length(55);
            Map(x => x.ProductImageUrl).Column("ProductImageUrl");
            Map(x => x.UnitOfMeasurementId).Column("UnitOfMeasurementId").Not.Nullable();
            Map(x => x.ProductCategoryId).Column("ProductCategoryId").Not.Nullable();
            Map(x => x.BuyingUnitPrice).Column("BuyingUnitPrice");
            Map(x => x.SellingUnitPrice).Column("SellingUnitPrice");
            Map(x => x.StockQuantity).Column("StockQuantity").Not.Nullable();
            Map(x => x.SKU).Column("SKU").Not.Nullable();
            Map(x => x.ManufacturingDate).Column("ManufacturingDate");
            Map(x => x.Status).Column("Status").Not.Nullable();

            References(x => x.UnitOfMeasurement) // Many-to-one relationship with UnitOfMeasurement
                .Column("UnitOfMeasurementId")
                .Not.Insert()
                .Not.Update();

            References(x => x.ProductCategory) // Many-to-one relationship with ProductCategory
                .Column("ProductCategoryId")
                .Not.Insert()
                .Not.Update();

            // BaseEntity properties
            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreationDate).Column("CreationDate").Not.Nullable();
            Map(x => x.ModifiedBy).Column("ModifiedBy");
            Map(x => x.ModificationDate).Column("ModificationDate");
            Map(x => x.Rank).Column("Rank").Not.Nullable();
            Map(x => x.BusinessId).Column("BusinessId").Length(256);
            Map(x => x.Version).Column("Version").Not.Nullable();
        }
    }
}
