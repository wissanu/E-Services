
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

UPDATE [dbo].[DeliverProcessStatus]
SET NameEN = N'Preparing Shipping'
WHERE Id = 1;
UPDATE [dbo].[DeliverProcessStatus]
SET NameEN = N'Preparing Shipped'
WHERE Id = 2;

UPDATE [dbo].[PaymentProcessStatus]
SET NameEN = N'Waiting for confirmation'
WHERE Id = 1;
UPDATE [dbo].[PaymentProcessStatus]
SET NameEN = N'Waiting for verify'
WHERE Id = 2;
UPDATE [dbo].[PaymentProcessStatus]
SET NameEN = N'Success'
WHERE Id = 3;
UPDATE [dbo].[PaymentProcessStatus]
SET NameEN = N'Canceled'
WHERE Id = 4;