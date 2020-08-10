
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TranslateMapTranslate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Translates] DROP CONSTRAINT [FK_TranslateMapTranslate];
GO
IF OBJECT_ID(N'[dbo].[FK_TranslateMapVatType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VatTypes] DROP CONSTRAINT [FK_TranslateMapVatType];
GO
IF OBJECT_ID(N'[dbo].[FK_VatTypeProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_VatTypeProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Translates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Translates];
GO
IF OBJECT_ID(N'[dbo].[VatTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VatTypes];
GO
IF OBJECT_ID(N'[dbo].[TranslateMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TranslateMaps];
GO

-- Update table
ALTER TABLE [dbo].[Products]
ADD [VatTypeId] int
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Translates'
CREATE TABLE [dbo].[Translates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TranslateMapId] int  NOT NULL,
    [Language_code] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VatTypes'
CREATE TABLE [dbo].[VatTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTranslate] int  NOT NULL
);
GO

-- Creating table 'TranslateMaps'
CREATE TABLE [dbo].[TranslateMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Note] nvarchar(max)
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Translates'
ALTER TABLE [dbo].[Translates]
ADD CONSTRAINT [PK_Translates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VatTypes'
ALTER TABLE [dbo].[VatTypes]
ADD CONSTRAINT [PK_VatTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TranslateMaps'
ALTER TABLE [dbo].[TranslateMaps]
ADD CONSTRAINT [PK_TranslateMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TranslateMapId] in table 'Translates'
ALTER TABLE [dbo].[Translates]
ADD CONSTRAINT [FK_TranslateMapTranslate]
    FOREIGN KEY ([TranslateMapId])
    REFERENCES [dbo].[TranslateMaps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO


-- Creating non-clustered index for FOREIGN KEY 'FK_TranslateMapTranslate'
CREATE INDEX [IX_FK_TranslateMapTranslate]
ON [dbo].[Translates]
    ([TranslateMapId]);
GO

-- Creating foreign key on [NameTranslate] in table 'VatTypes'
ALTER TABLE [dbo].[VatTypes]
ADD CONSTRAINT [FK_TranslateMapVatType]
    FOREIGN KEY ([NameTranslate])
    REFERENCES [dbo].[TranslateMaps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TranslateMapVatType'
CREATE INDEX [IX_FK_TranslateMapVatType]
ON [dbo].[VatTypes]
    ([NameTranslate]);
GO

-- Creating foreign key on [VatTypeId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_VatTypeProduct]
    FOREIGN KEY ([VatTypeId])
    REFERENCES [dbo].[VatTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VatTypeProduct'
CREATE INDEX [IX_FK_VatTypeProduct]
ON [dbo].[Products]
    ([VatTypeId]);
GO

INSERT INTO [dbo].[TranslateMaps] VALUES 
(null), (null), (null);
GO

INSERT INTO [dbo].[Translates] VALUES 
(1, N'th', N'รวมภาษีมูลค่าเพิ่มแล้ว'),
(2, N'th', N'ยังไม่รวมภาษีมูลค่าเพิ่ม'),
(3, N'th', N'ไม่มีภาษีมูลค่าเพิ่ม');
GO

INSERT INTO [dbo].[VatTypes] VALUES 
(1), (2), (3);
GO

UPDATE [dbo].[Products]
SET [VatTypeId] = 1
Where [Group] = 0;
GO

UPDATE [dbo].[Products]
SET [VatTypeId] = 2
Where [Group] = 1;
GO

