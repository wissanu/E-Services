
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Delete column
ALTER TABLE [dbo].[EComProducts]
DROP COLUMN [PriceNormal], [PriceNormalInter]
GO

-- Add column
ALTER TABLE [dbo].[EComProducts]
ADD [PriceNormal] float  NULL,
    [PriceNormalInter] float  NULL
GO

-- Update data
UPDATE [dbo].[EComProducts]
SET [PriceNormal] = [Price],
    [PriceNormalInter] = [PriceInter]
GO

-- Update table
ALTER TABLE [dbo].[EComProducts]
ADD [VatTypeId] int,
    [DemoElectronicFiles] nvarchar(max)  NULL
GO

-- Creating foreign key on [VatTypeId] in table 'EComProducts'
ALTER TABLE [dbo].[EComProducts]
ADD CONSTRAINT [FK_VatTypeEComProduct]
    FOREIGN KEY ([VatTypeId])
    REFERENCES [dbo].[VatTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VatTypeEComProduct'
CREATE INDEX [IX_FK_VatTypeEComProduct]
ON [dbo].[EComProducts]
    ([VatTypeId]);
GO

UPDATE [dbo].[EComProducts]
SET [VatTypeId] = 2
GO
