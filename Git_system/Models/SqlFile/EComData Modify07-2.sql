
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

INSERT INTO [dbo].[TranslateMaps] VALUES 
(N'ค่าจัดส่งตามหมวดหมู่');
GO

INSERT INTO [dbo].[Translates] VALUES 
(7, N'th', N'ค่าจัดส่งตามหมวดหมู่'),
(7, N'en', N'Price by category');
GO

INSERT INTO [dbo].[DeliverTypes] VALUES 
(7);
GO

-- Add column
ALTER TABLE [dbo].[EComCategories]
ADD 
    [Price] float  NOT NULL DEFAULT 0,
    [PriceInter] float  NOT NULL DEFAULT 0
GO
