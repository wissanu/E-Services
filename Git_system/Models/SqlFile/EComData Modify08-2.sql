SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

INSERT INTO [dbo].[TranslateMaps] VALUES 
(N'ค่าจัดแบบกลุ่ม');
GO

INSERT INTO [dbo].[Translates] VALUES 
(8, N'th', N'ค่าจัดส่งแบบกลุ่ม'),
(8, N'en', N'Price from group');
GO

INSERT INTO [dbo].[DeliverTypes] VALUES 
(8);
GO

UPDATE [dbo].[EComProductDelivers]
    SET [DeliverTypeId] = 5
    WHERE [DeliverTypeId] = 4
GO