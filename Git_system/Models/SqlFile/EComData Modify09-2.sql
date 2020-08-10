
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
ALTER TABLE [dbo].[ConfirmPayments]
ADD 
    [CreditCardRefNo] nvarchar(max)  NULL
GO

ALTER TABLE [dbo].[EComConfirmOrders]
ADD 
    [CreditCardRefNo] nvarchar(max)  NULL,
    [Currency] nvarchar(max)  NULL
GO
