
-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


GO
IF OBJECT_ID(N'[dbo].[FK_EComCategoryEComCategoryMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComCategoryMaps] DROP CONSTRAINT [FK_EComCategoryEComCategoryMap];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComCategoryMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComCategoryMaps] DROP CONSTRAINT [FK_EComProductEComCategoryMap];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipEComOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrders] DROP CONSTRAINT [FK_MembershipEComOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrderItems] DROP CONSTRAINT [FK_EComProductEComOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_EComOrderEComOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrderItems] DROP CONSTRAINT [FK_EComOrderEComOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_EComOrderEComConfirmOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComConfirmOrders] DROP CONSTRAINT [FK_EComOrderEComConfirmOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EComCategoryEComPromotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComPromotions] DROP CONSTRAINT [FK_EComCategoryEComPromotion];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComPromotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComPromotions] DROP CONSTRAINT [FK_EComProductEComPromotion];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipEComLogView]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComLogViews] DROP CONSTRAINT [FK_MembershipEComLogView];
GO
IF OBJECT_ID(N'[dbo].[FK_EComProductEComLogView]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComLogViews] DROP CONSTRAINT [FK_EComProductEComLogView];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipEComKeyword]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComKeywords] DROP CONSTRAINT [FK_MembershipEComKeyword];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfirmPaymentBankTypeEComConfirmOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComConfirmOrders] DROP CONSTRAINT [FK_ConfirmPaymentBankTypeEComConfirmOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentProcessStatusEComOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrders] DROP CONSTRAINT [FK_PaymentProcessStatusEComOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliverProcessStatusEComOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrders] DROP CONSTRAINT [FK_DeliverProcessStatusEComOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EComDeliverTypeEComOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EComOrders] DROP CONSTRAINT [FK_EComDeliverTypeEComOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


IF OBJECT_ID(N'[dbo].[EComProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComProducts];
GO
IF OBJECT_ID(N'[dbo].[EComCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComCategories];
GO
IF OBJECT_ID(N'[dbo].[EComCategoryMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComCategoryMaps];
GO
IF OBJECT_ID(N'[dbo].[EComOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComOrders];
GO
IF OBJECT_ID(N'[dbo].[EComOrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComOrderItems];
GO
IF OBJECT_ID(N'[dbo].[EComConfirmOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComConfirmOrders];
GO
IF OBJECT_ID(N'[dbo].[EComPromotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComPromotions];
GO
IF OBJECT_ID(N'[dbo].[EComLogViews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComLogViews];
GO
IF OBJECT_ID(N'[dbo].[EComKeywords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComKeywords];
GO
IF OBJECT_ID(N'[dbo].[PaymentProcessStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentProcessStatus];
GO
IF OBJECT_ID(N'[dbo].[DeliverProcessStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliverProcessStatus];
GO
IF OBJECT_ID(N'[dbo].[EComDeliverTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EComDeliverTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EComProducts'
CREATE TABLE [dbo].[EComProducts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [ImageFiles] nvarchar(max)  NULL,
    [OtherFiles] nvarchar(max)  NULL,
    [Price] float  NOT NULL,
    [PriceInter] float  NOT NULL,
    [PriceIndividual] float  NOT NULL,
    [PriceIndividualInter] float  NOT NULL,
    [PriceCorporate] float  NOT NULL,
    [PriceCorporateInter] float  NOT NULL,
    [ActiveStatus] bit  NOT NULL,
    [PinStatus] bit  NOT NULL,
    [PinWeight] float  NULL,
    [ElectronicFiles] nvarchar(max)  NULL,
    [ElectronicFileStatus] bit  NOT NULL,
    [DeliverStatus] bit  NOT NULL,
    [Quantity] int  NOT NULL,
    [InStock] bit  NOT NULL,
    [Vat] decimal(18,0)  NOT NULL,
    [DescriptionTH] nvarchar(max)  NULL,
    [DescriptionEN] nvarchar(max)  NULL,
    [PriceNormal] float  NULL,
    [PriceNormalInter] float  NULL
);
GO

-- Creating table 'EComCategories'
CREATE TABLE [dbo].[EComCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [ActiveStatus] bit  NOT NULL,
    [VisibleStatus] bit  NOT NULL,
    [OrderBy] int  NULL,
    [ShowOnPageStatus] bit  NOT NULL,
    [SortOrderStatus] bit  NOT NULL
);
GO

-- Creating table 'EComCategoryMaps'
CREATE TABLE [dbo].[EComCategoryMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EComCategoryId] int  NOT NULL,
    [EComProductId] int  NOT NULL,
    [OrderBy] int  NOT NULL
);
GO

-- Creating table 'EComOrders'
CREATE TABLE [dbo].[EComOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Datetime] datetime  NOT NULL,
    [MembershipId] int  NOT NULL,
    [Price] float  NOT NULL,
    [Currency] nvarchar(max)  NOT NULL,
    [PaymentType] int  NOT NULL,
    [PaymentProcessStatusId] int  NOT NULL,
    [DeliverProcessStatusId] int  NOT NULL,
    [DeliverNumber] nvarchar(max)  NULL,
    [SendFullname] nvarchar(max)  NULL,
    [SendAddress] nvarchar(max)  NULL,
    [SendProvince] nvarchar(max)  NULL,
    [SendCountry] nvarchar(max)  NULL,
    [SendPostcode] nvarchar(max)  NULL,
    [SendPhonenumber] nvarchar(max)  NULL,
    [SendEmail] nvarchar(max)  NULL,
    [ReceiptFullname] nvarchar(max)  NULL,
    [ReceiptAddress] nvarchar(max)  NULL,
    [ReceiptProvince] nvarchar(max)  NULL,
    [ReceiptCountry] nvarchar(max)  NULL,
    [ReceiptPostcode] nvarchar(max)  NULL,
    [ReceiptPhonenumber] nvarchar(max)  NULL,
    [ReceiptEmail] nvarchar(max)  NULL,
    [EComDeliverTypeId] int  NOT NULL
);
GO

-- Creating table 'EComOrderItems'
CREATE TABLE [dbo].[EComOrderItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [EComProductId] int  NOT NULL,
    [EComOrderId] int  NOT NULL
);
GO

-- Creating table 'EComConfirmOrders'
CREATE TABLE [dbo].[EComConfirmOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EComOrderId] int  NOT NULL,
    [Price] float  NOT NULL,
    [Datetime] datetime  NOT NULL,
    [FilenamePayment] nvarchar(max)  NOT NULL,
    [FilenameConfirm] nvarchar(max)  NOT NULL,
    [ConfirmPaymentBankTypeId] smallint  NOT NULL
);
GO

-- Creating table 'EComPromotions'
CREATE TABLE [dbo].[EComPromotions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [isGuest] bit  NOT NULL,
    [isGuestInter] bit  NOT NULL,
    [isIndividual] bit  NOT NULL,
    [isIndividualInter] bit  NOT NULL,
    [isCorporate] bit  NOT NULL,
    [isCorporateInter] bit  NOT NULL,
    [Percent] float  NOT NULL,
    [Operator] int  NOT NULL,
    [Type] int  NOT NULL,
    [ActiveStatus] bit  NOT NULL,
    [EComCategoryId] int  NULL,
    [EComProductId] int  NULL
);
GO

-- Creating table 'EComLogViews'
CREATE TABLE [dbo].[EComLogViews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MembershipId] int  NULL,
    [EComProductId] int  NOT NULL,
    [Datetime] datetime  NOT NULL
);
GO

-- Creating table 'EComKeywords'
CREATE TABLE [dbo].[EComKeywords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MembershipId] int  NULL,
    [Keyword] nvarchar(max)  NOT NULL,
    [Datetime] datetime  NOT NULL
);
GO

-- Creating table 'PaymentProcessStatus'
CREATE TABLE [dbo].[PaymentProcessStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DeliverProcessStatus'
CREATE TABLE [dbo].[DeliverProcessStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL
);
GO


-- Creating table 'EComDeliverTypes'
CREATE TABLE [dbo].[EComDeliverTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [PriceTH] float  NOT NULL,
    [PriceUS] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EComProducts'
ALTER TABLE [dbo].[EComProducts]
ADD CONSTRAINT [PK_EComProducts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComCategories'
ALTER TABLE [dbo].[EComCategories]
ADD CONSTRAINT [PK_EComCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComCategoryMaps'
ALTER TABLE [dbo].[EComCategoryMaps]
ADD CONSTRAINT [PK_EComCategoryMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComOrders'
ALTER TABLE [dbo].[EComOrders]
ADD CONSTRAINT [PK_EComOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComOrderItems'
ALTER TABLE [dbo].[EComOrderItems]
ADD CONSTRAINT [PK_EComOrderItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComConfirmOrders'
ALTER TABLE [dbo].[EComConfirmOrders]
ADD CONSTRAINT [PK_EComConfirmOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComPromotions'
ALTER TABLE [dbo].[EComPromotions]
ADD CONSTRAINT [PK_EComPromotions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComLogViews'
ALTER TABLE [dbo].[EComLogViews]
ADD CONSTRAINT [PK_EComLogViews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComKeywords'
ALTER TABLE [dbo].[EComKeywords]
ADD CONSTRAINT [PK_EComKeywords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaymentProcessStatus'
ALTER TABLE [dbo].[PaymentProcessStatus]
ADD CONSTRAINT [PK_PaymentProcessStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeliverProcessStatus'
ALTER TABLE [dbo].[DeliverProcessStatus]
ADD CONSTRAINT [PK_DeliverProcessStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EComDeliverTypes'
ALTER TABLE [dbo].[EComDeliverTypes]
ADD CONSTRAINT [PK_EComDeliverTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EComCategoryId] in table 'EComCategoryMaps'
ALTER TABLE [dbo].[EComCategoryMaps]
ADD CONSTRAINT [FK_EComCategoryEComCategoryMap]
    FOREIGN KEY ([EComCategoryId])
    REFERENCES [dbo].[EComCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComCategoryEComCategoryMap'
CREATE INDEX [IX_FK_EComCategoryEComCategoryMap]
ON [dbo].[EComCategoryMaps]
    ([EComCategoryId]);
GO

-- Creating foreign key on [EComProductId] in table 'EComCategoryMaps'
ALTER TABLE [dbo].[EComCategoryMaps]
ADD CONSTRAINT [FK_EComProductEComCategoryMap]
    FOREIGN KEY ([EComProductId])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComCategoryMap'
CREATE INDEX [IX_FK_EComProductEComCategoryMap]
ON [dbo].[EComCategoryMaps]
    ([EComProductId]);
GO

-- Creating foreign key on [MembershipId] in table 'EComOrders'
ALTER TABLE [dbo].[EComOrders]
ADD CONSTRAINT [FK_MembershipEComOrder]
    FOREIGN KEY ([MembershipId])
    REFERENCES [dbo].[Memberships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipEComOrder'
CREATE INDEX [IX_FK_MembershipEComOrder]
ON [dbo].[EComOrders]
    ([MembershipId]);
GO

-- Creating foreign key on [EComProductId] in table 'EComOrderItems'
ALTER TABLE [dbo].[EComOrderItems]
ADD CONSTRAINT [FK_EComProductEComOrderItem]
    FOREIGN KEY ([EComProductId])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComOrderItem'
CREATE INDEX [IX_FK_EComProductEComOrderItem]
ON [dbo].[EComOrderItems]
    ([EComProductId]);
GO

-- Creating foreign key on [EComOrderId] in table 'EComOrderItems'
ALTER TABLE [dbo].[EComOrderItems]
ADD CONSTRAINT [FK_EComOrderEComOrderItem]
    FOREIGN KEY ([EComOrderId])
    REFERENCES [dbo].[EComOrders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComOrderEComOrderItem'
CREATE INDEX [IX_FK_EComOrderEComOrderItem]
ON [dbo].[EComOrderItems]
    ([EComOrderId]);
GO

-- Creating foreign key on [EComOrderId] in table 'EComConfirmOrders'
ALTER TABLE [dbo].[EComConfirmOrders]
ADD CONSTRAINT [FK_EComOrderEComConfirmOrder]
    FOREIGN KEY ([EComOrderId])
    REFERENCES [dbo].[EComOrders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComOrderEComConfirmOrder'
CREATE INDEX [IX_FK_EComOrderEComConfirmOrder]
ON [dbo].[EComConfirmOrders]
    ([EComOrderId]);
GO

-- Creating foreign key on [EComCategoryId] in table 'EComPromotions'
ALTER TABLE [dbo].[EComPromotions]
ADD CONSTRAINT [FK_EComCategoryEComPromotion]
    FOREIGN KEY ([EComCategoryId])
    REFERENCES [dbo].[EComCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComCategoryEComPromotion'
CREATE INDEX [IX_FK_EComCategoryEComPromotion]
ON [dbo].[EComPromotions]
    ([EComCategoryId]);
GO

-- Creating foreign key on [EComProductId] in table 'EComPromotions'
ALTER TABLE [dbo].[EComPromotions]
ADD CONSTRAINT [FK_EComProductEComPromotion]
    FOREIGN KEY ([EComProductId])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComPromotion'
CREATE INDEX [IX_FK_EComProductEComPromotion]
ON [dbo].[EComPromotions]
    ([EComProductId]);
GO

-- Creating foreign key on [MembershipId] in table 'EComLogViews'
ALTER TABLE [dbo].[EComLogViews]
ADD CONSTRAINT [FK_MembershipEComLogView]
    FOREIGN KEY ([MembershipId])
    REFERENCES [dbo].[Memberships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipEComLogView'
CREATE INDEX [IX_FK_MembershipEComLogView]
ON [dbo].[EComLogViews]
    ([MembershipId]);
GO

-- Creating foreign key on [EComProductId] in table 'EComLogViews'
ALTER TABLE [dbo].[EComLogViews]
ADD CONSTRAINT [FK_EComProductEComLogView]
    FOREIGN KEY ([EComProductId])
    REFERENCES [dbo].[EComProducts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComProductEComLogView'
CREATE INDEX [IX_FK_EComProductEComLogView]
ON [dbo].[EComLogViews]
    ([EComProductId]);
GO

-- Creating foreign key on [MembershipId] in table 'EComKeywords'
ALTER TABLE [dbo].[EComKeywords]
ADD CONSTRAINT [FK_MembershipEComKeyword]
    FOREIGN KEY ([MembershipId])
    REFERENCES [dbo].[Memberships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipEComKeyword'
CREATE INDEX [IX_FK_MembershipEComKeyword]
ON [dbo].[EComKeywords]
    ([MembershipId]);
GO

-- Creating foreign key on [ConfirmPaymentBankTypeId] in table 'EComConfirmOrders'
ALTER TABLE [dbo].[EComConfirmOrders]
ADD CONSTRAINT [FK_ConfirmPaymentBankTypeEComConfirmOrder]
    FOREIGN KEY ([ConfirmPaymentBankTypeId])
    REFERENCES [dbo].[ConfirmPaymentBankTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfirmPaymentBankTypeEComConfirmOrder'
CREATE INDEX [IX_FK_ConfirmPaymentBankTypeEComConfirmOrder]
ON [dbo].[EComConfirmOrders]
    ([ConfirmPaymentBankTypeId]);
GO

-- Creating foreign key on [PaymentProcessStatusId] in table 'EComOrders'
ALTER TABLE [dbo].[EComOrders]
ADD CONSTRAINT [FK_PaymentProcessStatusEComOrder]
    FOREIGN KEY ([PaymentProcessStatusId])
    REFERENCES [dbo].[PaymentProcessStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentProcessStatusEComOrder'
CREATE INDEX [IX_FK_PaymentProcessStatusEComOrder]
ON [dbo].[EComOrders]
    ([PaymentProcessStatusId]);
GO

-- Creating foreign key on [DeliverProcessStatusId] in table 'EComOrders'
ALTER TABLE [dbo].[EComOrders]
ADD CONSTRAINT [FK_DeliverProcessStatusEComOrder]
    FOREIGN KEY ([DeliverProcessStatusId])
    REFERENCES [dbo].[DeliverProcessStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliverProcessStatusEComOrder'
CREATE INDEX [IX_FK_DeliverProcessStatusEComOrder]
ON [dbo].[EComOrders]
    ([DeliverProcessStatusId]);
GO

-- Creating foreign key on [EComDeliverTypeId] in table 'EComOrders'
ALTER TABLE [dbo].[EComOrders]
ADD CONSTRAINT [FK_EComDeliverTypeEComOrder]
    FOREIGN KEY ([EComDeliverTypeId])
    REFERENCES [dbo].[EComDeliverTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EComDeliverTypeEComOrder'
CREATE INDEX [IX_FK_EComDeliverTypeEComOrder]
ON [dbo].[EComOrders]
    ([EComDeliverTypeId]);
GO


--------------------------Default--------------------------
INSERT INTO [dbo].[DeliverProcessStatus] VALUES (N'เตรียมการจัดส่ง',N'เตรียมการจัดส่ง'),
(N'จัดส่งเรียบร้อยแล้ว',N'จัดส่งเรียบร้อยแล้ว');
INSERT INTO [dbo].[PaymentProcessStatus] VALUES (N'รอการยืนยันการชำระ',N'รอการยืนยันการชำระ'),
(N'รอการตรวจสอบ',N'รอการตรวจสอบ'),
(N'ชำระเรียบร้อยแล้ว',N'ชำระเรียบร้อยแล้ว'),
(N'ยกเลิก',N'ยกเลิก');