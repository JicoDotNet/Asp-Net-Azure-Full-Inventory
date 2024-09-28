﻿INSERT INTO [SingleIB].[mBranch] (BranchName, State, IsActive, TransactionDate, RequestId)
VALUES ('Head Branch', '29' , 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mWareHouse] (BranchId, WareHouseName, IsRetailCounter, IsActive, TransactionDate, RequestId)
VALUES (CAST(SCOPE_IDENTITY() as BIGINT), 'Default Warehouse', 1, 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mUnitOfMeasure]( UnitOfMeasureName, Description, IsActive, TransactionDate, RequestId)
VALUES ('Pc', 'Piece', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mProductType] (ProductTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Product Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mVendorType] (VendorTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Vendor Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mCustomerType] (CustomerTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Customer Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mPaymentType] (PaymentTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Payment Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mShipmentType] (ShipmentTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Shipment Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mBillType] (BillTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Bill Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mInvoiceType] (InvoiceTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Invoice Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mPurchaseType] (PurchaseTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Purchase Type', 1, GETDATE(), 'DefaultDataInsert');

INSERT INTO [SingleIB].[mSalesType] (SalesTypeName, IsActive, TransactionDate, RequestId)
VALUES ('Default Sales Type', 1, GETDATE(), 'DefaultDataInsert');