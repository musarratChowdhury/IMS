use MyInventoryDb;





use MyInventoryDb;
select * from SalesOrder;
select * from Customer;
delete from Customer where id = 38;
select * from Invoice;
select * from Bill;
select * from SalesOrderLine;
select * from Customer;
select * from Product;
select * from AspNetUsers;
select * from AspNetUserRoles;

select * from PurchaseOrder;
select * from PurchaseOrderLine;

delete from PurchaseOrderLine;
delete from PurchaseOrder;
delete from SalesOrderLine;
delete from SalesOrder;

delete from shipment;
delete from bill;
delete from invoice;
delete from PaymentReceived;
Update SalesOrder
	SET PaymentStatus = 1,
	ShipmentStatus = 1,
	InvoiceId = 4
WHERE Id =16; 
UPDATE SalesOrder
SET CreationDate = '2023-06-12'
WHERE Id = 49;
select * from SalesOrder;

ALTER TABLE SalesOrder

ADD DueAmount DECIMAL(10,2) NOT NULL DEFAULT(0);

use MyInventoryDb;
   SELECT
    MONTH(CreationDate) AS Month,
    SUM(TotalAmount) AS TotalSales
FROM
    SalesOrder
WHERE
    IsArchived = 0
GROUP BY
    MONTH(CreationDate)
ORDER BY
    Month;


Select * from Product;
Select * from ProductCategory;

SELECT ProductCategoryId, COUNT(*) AS NUMBEROFProducts
FROM Product 
GROUP BY ProductCategoryId;

use MyInventoryDb;
SELECT C.Name AS CategoryName, SUM(P.StockQuantity) AS TotalStockQuantity
FROM ProductCategory C
LEFT JOIN Product P ON C.Id = P.ProductCategoryId
GROUP BY C.Name;

select StockQuantity from Product;