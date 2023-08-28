use MyInventoryDb;

CREATE TABLE PaymentType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE ShipmentType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE CustomerType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE VendorType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE InvoiceType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE BillType(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE UnitOfMeasurement(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE ProductCategory(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);

CREATE TABLE CashBank(
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(25) NOT NULL,
	Description NVARCHAR(255),
	Rank INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
	CreatedBy BIGINT NOT NULL,
	CreationDate DATETIME NOT NULL,
	ModifiedBy BIGINT,
	ModificationDate DATETIME,
	Version INT NOT NULL DEFAULT(1),
	BusinessId NVARCHAR(MAX)
);


use MyInventoryDb;


CREATE TABLE Customer (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    CustomerTypeId BIGINT NOT NULL,
    FirstName NVARCHAR(55) NOT NULL,
    LastName NVARCHAR(55) NOT NULL,
    Address NVARCHAR(255),
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(100),
    Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATETIME NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATETIME,
    Rank INT NOT NULL DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_Customer_CustomerType FOREIGN KEY (CustomerTypeId) REFERENCES CustomerType(Id)
);

use MyInventoryDb;
-- Create SalesOrder table with foreign key constraints

-- Create Invoice table with foreign key constraints
CREATE TABLE Invoice (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    InvoiceDate DATE NOT NULL,
    InvoiceDueDate DATE NOT NULL,
    SalesOrderId BIGINT NOT NULL,
    InvoiceTypeId BIGINT NOT NULL,
	PaymentStatus INT NOT NULL DEFAULT(0),
	IsArchived BIT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL,
    CONSTRAINT FK_Invoice_SalesOrder FOREIGN KEY (SalesOrderId) REFERENCES SalesOrder(Id),
    CONSTRAINT FK_Invoice_InvoiceType FOREIGN KEY (InvoiceTypeId) REFERENCES InvoiceType(Id)
);

use MyInventoryDb;
CREATE TABLE Shipment (
    Id BIGINT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    ShipmentDate DATE NOT NULL,
	ShippingAddress NVARCHAR(100) NOT NULL,
    SalesOrderId BIGINT NOT NULL,
    ShipmentTypeId BIGINT NOT NULL,
    Status INT NOT NULL,
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL,
    CONSTRAINT FK_Shipment_SalesOrder FOREIGN KEY (SalesOrderId) REFERENCES SalesOrder(Id), 
    CONSTRAINT FK_Shipment_ShipmentType FOREIGN KEY (ShipmentTypeId) REFERENCES ShipmentType(Id)
);

-- Create PaymentReceived table with foreign key constraints
CREATE TABLE PaymentReceived (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    PaymentDate DATE NOT NULL,
    PaymentAmount DECIMAL(10, 2) NOT NULL,
    Status INT NOT NULL,
    InvoiceId BIGINT,
    PaymentTypeId BIGINT,
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL,
    CONSTRAINT FK_PaymentReceived_Invoice FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id),
    CONSTRAINT FK_PaymentReceived_PaymentType FOREIGN KEY (PaymentTypeId) REFERENCES PaymentType(Id)
);

-- Create Vendor table with foreign key constraint
CREATE TABLE Vendor (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    VendorTypeId BIGINT NOT NULL,
    FirstName NVARCHAR(55) NOT NULL,
    LastName NVARCHAR(55) NOT NULL,
    Address NVARCHAR(255),
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(100),
    Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_Vendor_VendorType FOREIGN KEY (VendorTypeId) REFERENCES VendorType(Id)
);

CREATE TABLE SalesOrder (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    CustomerId BIGINT NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
	DueAmount DECIMAL(10,2) NOT NULL DEFAULT(0),
    IsArchived BIT NOT NULL DEFAULT(0),
	InvoiceId BIGINT,
	ShipmentStatus INT NOT NULL DEFAULT(0),
	PaymentStatus  INT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL,
    CONSTRAINT FK_SalesOrder_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);


-- Create PurchaseOrder table with foreign key constraint
CREATE TABLE PurchaseOrder (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    VendorId BIGINT NOT NULL,
	BillId BIGINT,
	PaymentStatus  INT NOT NULL DEFAULT(0),
    TotalAmount DECIMAL(10, 2) NOT NULL,
	DueAmount DECIMAL(10,2) NOT NULL DEFAULT(0),
    IsArchived BIT NOT NULL DEFAULT(0),
	Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_PurchaseOrder_Vendor FOREIGN KEY (VendorId) REFERENCES Vendor(Id)
);

-- Create Bill table with foreign key constraint
CREATE TABLE Bill (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    BillDate DATE NOT NULL,
    BillDueDate DATE NOT NULL,
    BillTypeId BIGINT NOT NULL,
	PaymentStatus Int NOT NULL DEFAULT(0),
	IsArchived BIT NOT NULL DEFAULT(0),
	PurchaseOrderId BIGINT NOT NULL,
	Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_Bill_BillType FOREIGN KEY (BillTypeId) REFERENCES BillType(Id),
	CONSTRAINT FK_Bill_PuchaseOrder FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrder(Id)
);



-- Create PaymentVoucher table with foreign key constraints
CREATE TABLE PaymentVoucher (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    PaymentDate DATE NOT NULL,
    PaymentAmount DECIMAL(10, 2) NOT NULL,
    Status INT NOT NULL,
    BillId BIGINT,
    PaymentTypeId BIGINT,
    CashBankId BIGINT,
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_PaymentVoucher_Bill FOREIGN KEY (BillId) REFERENCES Bill(Id),
    CONSTRAINT FK_PaymentVoucher_PaymentType FOREIGN KEY (PaymentTypeId) REFERENCES PaymentType(Id),
    CONSTRAINT FK_PaymentVoucher_CashBank FOREIGN KEY (CashBankId) REFERENCES CashBank(Id)
);


-- Create Product table with foreign key constraints
CREATE TABLE Product (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(55) NOT NULL,
    ProductImageUrl NVARCHAR(100),
    UnitOfMeasurementId BIGINT NOT NULL,
    ProductCategoryId BIGINT NOT NULL,
    BuyingUnitPrice DECIMAL(10, 2),
    SellingUnitPrice DECIMAL(10, 2),
    StockQuantity INT NOT NULL,
    SKU INT NOT NULL,
    ManufacturingDate DATE,
    Status INT NOT NULL DEFAULT(1),
    CreatedBy BIGINT NOT NULL,
    CreationDate DATE NOT NULL,
    ModifiedBy BIGINT,
    ModificationDate DATE,
    Rank INT NOT NULL  DEFAULT(0),
    BusinessId NVARCHAR(MAX),
    Version INT NOT NULL DEFAULT(1),
    CONSTRAINT FK_Product_UnitOfMeasurement FOREIGN KEY (UnitOfMeasurementId) REFERENCES UnitOfMeasurement(Id),
    CONSTRAINT FK_Product_ProductCategory FOREIGN KEY (ProductCategoryId) REFERENCES ProductCategory(Id)
);
use MyInventoryDb;
CREATE TABLE SalesOrderLine (
    SalesOrderId BIGINT NOT NULL,
    ProductId BIGINT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2),
    Amount DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (SalesOrderId, ProductId),
    FOREIGN KEY (SalesOrderId) REFERENCES SalesOrder(Id),
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);
CREATE TABLE PurchaseOrderLine (
    PurchaseOrderId BIGINT NOT NULL,
    ProductId BIGINT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (PurchaseOrderId, ProductId),
    FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrder(Id),
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);



-- Create UserProfile table with foreign key constraint
CREATE TABLE UserProfile (
    UserProfileId BIGINT PRIMARY KEY NOT NULL,
    AspNetUserId BIGINT NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    ProfilePicture NVARCHAR(MAX),
    CONSTRAINT FK_UserProfile_AspNetUser FOREIGN KEY (AspNetUserId) REFERENCES AspNetUsers(Id)
);



