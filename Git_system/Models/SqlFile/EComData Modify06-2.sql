
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
ALTER TABLE [dbo].[EComDeliverPromotions]
ADD 
    [isGuest] bit  NOT NULL DEFAULT 1,
    [isGuestInter] bit  NOT NULL DEFAULT 1,
    [isIndividual] bit  NOT NULL DEFAULT 1,
    [isIndividualInter] bit  NOT NULL DEFAULT 1,
    [isCorporate] bit  NOT NULL DEFAULT 1,
    [isCorparateInter] bit  NOT NULL DEFAULT 1
GO

-- Add column
ALTER TABLE [dbo].[Permissions]
ADD 
    [isESlide] bit  NOT NULL DEFAULT 1,
    [isEProduct] bit  NOT NULL DEFAULT 1,
    [isEStock] bit  NOT NULL DEFAULT 1,
    [isEPayView] bit  NOT NULL DEFAULT 1,
    [isEPayConfirm] bit  NOT NULL DEFAULT 1,
    [isEDeliver] bit  NOT NULL DEFAULT 1,
    [isEPromotion] bit  NOT NULL DEFAULT 1,
    [isEStatistic] bit  NOT NULL DEFAULT 1
GO
