
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
ALTER TABLE [dbo].[EComOrders]
ADD 
    [JOrderDetail] nvarchar(max)  NULL
GO
