
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
ALTER TABLE [dbo].[EComOrders]
ADD 
    [DeliverSendName] nvarchar(max)  NULL,
    [DeliverSendDate] datetime  NULL
GO
