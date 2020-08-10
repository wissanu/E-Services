ALTER TABLE [dbo].[Memberships]
ADD [IdentificationNo] nvarchar(max)

ALTER TABLE [dbo].[Memberships]
ADD [Occupation] nvarchar(max)

ALTER TABLE [dbo].[Memberships]
ADD [Education] nvarchar(max)

ALTER TABLE [dbo].[Memberships]
ADD [ExperienceInGem] bit

ALTER TABLE [dbo].[Products]
ADD [isShortTerm] bit

ALTER TABLE [dbo].[Products]
ADD [isLongTerm] bit

ALTER TABLE [dbo].[Memberships]
ADD [WorkTel] nvarchar(max) NULL

ALTER TABLE [dbo].[Memberships]
ADD [WorkFax] nvarchar(max) NULL

ALTER TABLE [dbo].[EmailMessages]
ADD [EmailAlert] nvarchar(max) NULL

-- Creating table 'ProductNews'
CREATE TABLE [dbo].[ProductNews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DetailTH] nvarchar(max)  NOT NULL,
    [DetailEN] nvarchar(max)  NOT NULL
);
GO

-- Creating primary key on [Id] in table 'ProductNews'
ALTER TABLE [dbo].[ProductNews]
ADD CONSTRAINT [PK_ProductNews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Pays]
ADD [Fullname] nvarchar(max)  NULL

ALTER TABLE [dbo].[Pays]
ADD [Address] nvarchar(max)  NULL

-- Creating table 'Benefits'
CREATE TABLE [dbo].[Benefits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL
);
GO

-- Creating primary key on [Id] in table 'Benefits'
ALTER TABLE [dbo].[Benefits]
ADD CONSTRAINT [PK_Benefits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Pays]
ADD [Currency] nvarchar(max)  NULL

ALTER TABLE [dbo].[ConfirmPayments]
ADD [Currency] nvarchar(max)  NULL
