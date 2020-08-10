
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EComDeliverPromotionEComDeliverPromotionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComDeliverPromotionMaps] DROP CONSTRAINT [FK_EComDeliverPromotionEComDeliverPromotionMap];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComDeliverPromotionMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComDeliverPromotionMaps] DROP CONSTRAINT [FK_EComProductEComDeliverPromotionMap];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EComDeliverPromotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComDeliverPromotions];
GO
IF OBJECT_ID(N'[dbo].[EComDeliverPromotionMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComDeliverPromotionMaps];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EComDeliverPromotions'
CREATE TABLE [dbo].[EComDeliverPromotions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTh] nvarchar(max)  NOT NULL,
    [NameEn] nvarchar(max)  NOT NULL,
    [Percent] float  NOT NULL,
    [Type] int  NOT NULL,
    [Operator] int  NULL,
    [Operator2] int  NULL,
    [ActiveStatus] bit  NOT NULL
);
GO

-- Creating table 'EComDeliverPromotionMaps'
CREATE TABLE [dbo].[EComDeliverPromotionMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EComDeliverPromotionId] int  NOT NULL,
    [EComProductId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EComDeliverPromotions'
ALTER TABLE [dbo].[EComDeliverPromotions]
ADD CONSTRAINT [PK_EComDeliverPromotions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComDeliverPromotionMaps'
ALTER TABLE [dbo].[EComDeliverPromotionMaps]
ADD CONSTRAINT [PK_EComDeliverPromotionMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EComDeliverPromotionId] in table 'EComDeliverPromotionMaps'
ALTER TABLE [dbo].[EComDeliverPromotionMaps]
ADD CONSTRAINT [FK_EComDeliverPromotionEComDeliverPromotionMap]
    FOREIGN KEY ([EComDeliverPromotionId])
    REFERENCES [dbo].[EComDeliverPromotions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComDeliverPromotionEComDeliverPromotionMap'
CREATE INDEX [IX_FK_EComDeliverPromotionEComDeliverPromotionMap]
ON [dbo].[EComDeliverPromotionMaps]
    ([EComDeliverPromotionId]);
GO

-- Creating foreign key on [EComProductId] in table 'EComDeliverPromotionMaps'
ALTER TABLE [dbo].[EComDeliverPromotionMaps]
ADD CONSTRAINT [FK_EComProductEComDeliverPromotionMap]
    FOREIGN KEY ([EComProductId])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComDeliverPromotionMap'
CREATE INDEX [IX_FK_EComProductEComDeliverPromotionMap]
ON [dbo].[EComDeliverPromotionMaps]
    ([EComProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
