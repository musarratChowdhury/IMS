using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Dto.DashBoard;
using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Dto.Product;
using IMS.BusinessModel.Entity;
using IMS.Dao;
using IMS.Services.BaseServices;
using NHibernate;

namespace IMS.Services.SecondaryServices
{
    public class ProductService : BaseSecondaryService<Product>
    {
         private readonly IBaseDao<Product> _baseDao = new BaseDao<Product>();

         public async Task<List<ProductDto>> GetAll(ISession session, DataRequest dataRequest)
        {
            try
            {
                var entities = await _baseDao.GetAll(session, dataRequest);
                return (from t in entities let dto = new ProductDto() select MapToDto(t, dto)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ProductDto> GetProductById(ISession session, long id)
        {
            var product = await _baseDao.GetById(id, session);
            var productDto = new ProductDto();
            var result = MapToDto(product, productDto);
            return result;
        }

        public async Task Create(ProductFormDto productFormDto, long userId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var product = new Product();
                    var mappedProduct = MapToEntity(productFormDto, product);
                    mappedProduct.Rank = GetNextRank(session);
                    mappedProduct.CreatedBy = userId;
                    mappedProduct.CreationDate = DateTime.Now;
                    mappedProduct.SKU = new Random().Next(1000, 9999) * 100 + mappedProduct.Rank;
                    await _baseDao.Create(mappedProduct, session);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        
        public async Task<List<DropDownDto>> GetDropDownList(ISession session)
        {
            try
            {
                var entities = await _baseDao.GetAll(session);
                return (from t in entities let dto = new DropDownDto() select MapToDropDownDto(t, dto)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<CategoryBasedProductCount>> GetCategoryBasedProductsCount(ISession session)
        {
            try
            {
                var sql = @"
                        SELECT C.Name AS CategoryName, SUM(P.StockQuantity) AS NumberOfProducts
                        FROM ProductCategory C
                        LEFT JOIN Product P ON C.Id = P.ProductCategoryId
                        GROUP BY C.Name";

                var result = await _baseDao.GetCategoryBasedProductCount(session, sql);
                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private DropDownDto MapToDropDownDto(Product entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.ProductName;

            return dto;
        }

        public async Task Update(ProductDto productDto, long modifiedById, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var product = new Product();
                    var mappedProduct = MapToEntity(productDto, product);
                    mappedProduct.ModificationDate = DateTime.Now;
                    mappedProduct.ModifiedBy = modifiedById;
                    
                    await _baseDao.Update(mappedProduct, sess);
                    
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private ProductDto MapToDto(Product entity, ProductDto dto)
        {

            dto.Id = entity.Id;
            dto.ProductName = entity.ProductName;
            dto.ProductImageUrl = entity.ProductImageUrl;
            dto.ProductCategoryName = entity.ProductCategory.Name;
            dto.UnitOfMeasurementId = entity.UnitOfMeasurementId;
            dto.UnitOfMeasurementName = entity.UnitOfMeasurement.Name;
            dto.ProductCategoryId = entity.ProductCategoryId;
            dto.BuyingUnitPrice = entity.BuyingUnitPrice;
            dto.SellingUnitPrice = entity.SellingUnitPrice;
            dto.SKU = entity.SKU;
            dto.StockQuantity = entity.StockQuantity;
            dto.ManufacturingDate = entity.ManufacturingDate;
            dto.Status = entity.Status;
            dto.Rank = entity.Rank;
            dto.CreatedBy = entity.CreatedBy;
            dto.CreationDate = entity.CreationDate;
            dto.ModifiedBy = entity.ModifiedBy;
            dto.ModificationDate = entity.ModificationDate;
            dto.Version = entity.Version;
            dto.BusinessId = entity.BusinessId;

            return dto;
        }

        //for create operation
        private Product MapToEntity(ProductFormDto dto, Product product)
        {
            product.ProductName = dto.ProductName;
            product.ProductImageUrl = dto.ProductImageUrl;
            product.ProductCategoryId = dto.ProductCategoryId;
            product.UnitOfMeasurementId = dto.UnitOfMeasurementId;
            product.BuyingUnitPrice = dto.BuyingUnitPrice;
            product.SellingUnitPrice = dto.SellingUnitPrice;
            product.StockQuantity = dto.StockQuantity;
            product.ManufacturingDate = dto.ManufacturingDate;
            product.Version = 1;
            product.BusinessId = "IMS-1";
            product.Status = 1;

            return product;
        }

        //for update action
        private Product MapToEntity(ProductDto dto, Product product)
        {
            product.Id = dto.Id;
            product.ProductName = dto.ProductName;
            product.ProductImageUrl = dto.ProductImageUrl;
            product.BuyingUnitPrice = dto.BuyingUnitPrice;
            product.SellingUnitPrice = dto.SellingUnitPrice;
            product.SKU = dto.SKU;
            product.StockQuantity = dto.StockQuantity;
            product.ManufacturingDate = dto.ManufacturingDate;
            product.CreatedBy = dto.CreatedBy;
            product.CreationDate = dto.CreationDate;
            product.ProductCategoryId = dto.ProductCategoryId;
            product.UnitOfMeasurementId = dto.UnitOfMeasurementId;
            product.Rank = dto.Rank;
            product.Version = 1;
            product.BusinessId = "IMS-1";
            product.Status = dto.Status;

            return product;
        }
    }
}