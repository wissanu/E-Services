
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SlideshowSlideshowTranslate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SlideshowTranslates] DROP CONSTRAINT [FK_SlideshowSlideshowTranslate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Slideshows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slideshows];
GO
IF OBJECT_ID(N'[dbo].[SlideshowTranslates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SlideshowTranslates];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Slideshows'
CREATE TABLE [dbo].[Slideshows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [OrderBy] int  NOT NULL
);
GO

-- Creating table 'SlideshowTranslates'
CREATE TABLE [dbo].[SlideshowTranslates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_code] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [SlideshowId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Slideshows'
ALTER TABLE [dbo].[Slideshows]
ADD CONSTRAINT [PK_Slideshows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SlideshowTranslates'
ALTER TABLE [dbo].[SlideshowTranslates]
ADD CONSTRAINT [PK_SlideshowTranslates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SlideshowId] in table 'SlideshowTranslates'
ALTER TABLE [dbo].[SlideshowTranslates]
ADD CONSTRAINT [FK_SlideshowSlideshowTranslate]
    FOREIGN KEY ([SlideshowId])
    REFERENCES [dbo].[Slideshows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SlideshowSlideshowTranslate'
CREATE INDEX [IX_FK_SlideshowSlideshowTranslate]
ON [dbo].[SlideshowTranslates]
    ([SlideshowId]);
GO