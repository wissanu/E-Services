
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


IF OBJECT_ID(N'[dbo].[FK_TranslateMapDeliverType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliverTypes] DROP CONSTRAINT [FK_TranslateMapDeliverType];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliverTypeEComProductDeliver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComProductDelivers] DROP CONSTRAINT [FK_DeliverTypeEComProductDeliver];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComProductDeliver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComProducts] DROP CONSTRAINT [FK_EComProductEComProductDeliver];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DeliverTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliverTypes];
GO
IF OBJECT_ID(N'[dbo].[EComProductDelivers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComProductDelivers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DeliverTypes'
CREATE TABLE [dbo].[DeliverTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTranslateId] int  NOT NULL
);
GO

-- Creating table 'EComProductDelivers'
CREATE TABLE [dbo].[EComProductDelivers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Price] float  NOT NULL,
    [PriceInter] float  NOT NULL,
    [DeliverTypeId] int  NOT NULL,
    [EComProduct_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DeliverTypes'
ALTER TABLE [dbo].[DeliverTypes]
ADD CONSTRAINT [PK_DeliverTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComProductDelivers'
ALTER TABLE [dbo].[EComProductDelivers]
ADD CONSTRAINT [PK_EComProductDelivers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NameTranslateId] in table 'DeliverTypes'
ALTER TABLE [dbo].[DeliverTypes]
ADD CONSTRAINT [FK_TranslateMapDeliverType]
    FOREIGN KEY ([NameTranslateId])
    REFERENCES [dbo].[TranslateMaps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TranslateMapDeliverType'
CREATE INDEX [IX_FK_TranslateMapDeliverType]
ON [dbo].[DeliverTypes]
    ([NameTranslateId]);
GO

-- Creating foreign key on [DeliverTypeId] in table 'EComProductDelivers'
ALTER TABLE [dbo].[EComProductDelivers]
ADD CONSTRAINT [FK_DeliverTypeEComProductDeliver]
    FOREIGN KEY ([DeliverTypeId])
    REFERENCES [dbo].[DeliverTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliverTypeEComProductDeliver'
CREATE INDEX [IX_FK_DeliverTypeEComProductDeliver]
ON [dbo].[EComProductDelivers]
    ([DeliverTypeId]);
GO

-- Creating foreign key on [EComProduct_Id] in table 'EComProductDelivers'
ALTER TABLE [dbo].[EComProductDelivers]
ADD CONSTRAINT [FK_EComProductEComProductDeliver]
    FOREIGN KEY ([EComProduct_Id])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComProductDeliver'
CREATE INDEX [IX_FK_EComProductEComProductDeliver]
ON [dbo].[EComProductDelivers]
    ([EComProduct_Id]);
GO
