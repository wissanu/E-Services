
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/25/2015 11:50:03
-- Generated from EDMX file: C:\Users\PonG\Documents\git_e-commerce\Git_system\Models\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PermissionUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAdmins] DROP CONSTRAINT [FK_PermissionUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipPay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pays] DROP CONSTRAINT [FK_MembershipPay];
GO
IF OBJECT_ID(N'[dbo].[FK_PayPayItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayItems] DROP CONSTRAINT [FK_PayPayItem];
GO
IF OBJECT_ID(N'[dbo].[FK_PayTypePay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pays] DROP CONSTRAINT [FK_PayTypePay];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessStatusTypePay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pays] DROP CONSTRAINT [FK_ProcessStatusTypePay];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAdminLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logs] DROP CONSTRAINT [FK_UserAdminLog];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipRegisterTypeMembership]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [FK_MembershipRegisterTypeMembership];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_ProductCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CoursePayItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayItems] DROP CONSTRAINT [FK_CoursePayItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfirmPaymentBankTypeConfirmPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfirmPayments] DROP CONSTRAINT [FK_ConfirmPaymentBankTypeConfirmPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_PayConfirmPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfirmPayments] DROP CONSTRAINT [FK_PayConfirmPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_AmphurProvince]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Amphurs] DROP CONSTRAINT [FK_AmphurProvince];
GO
IF OBJECT_ID(N'[dbo].[FK_AmphurDistrict]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Districts] DROP CONSTRAINT [FK_AmphurDistrict];
GO
IF OBJECT_ID(N'[dbo].[FK_GeographyProvince]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Provinces] DROP CONSTRAINT [FK_GeographyProvince];
GO
IF OBJECT_ID(N'[dbo].[FK_MembershipQuestionList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [FK_MembershipQuestionList];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseProductSKU]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSKUs] DROP CONSTRAINT [FK_CourseProductSKU];
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

IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[UserAdmins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAdmins];
GO
IF OBJECT_ID(N'[dbo].[Permissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permissions];
GO
IF OBJECT_ID(N'[dbo].[ProcessStatusTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessStatusTypes];
GO
IF OBJECT_ID(N'[dbo].[PayTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayTypes];
GO
IF OBJECT_ID(N'[dbo].[PayItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayItems];
GO
IF OBJECT_ID(N'[dbo].[Pays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pays];
GO
IF OBJECT_ID(N'[dbo].[Memberships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Memberships];
GO
IF OBJECT_ID(N'[dbo].[MembershipRegisterTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MembershipRegisterTypes];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[ConfirmPayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConfirmPayments];
GO
IF OBJECT_ID(N'[dbo].[ConfirmPaymentBankTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConfirmPaymentBankTypes];
GO
IF OBJECT_ID(N'[dbo].[EmailMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailMessages];
GO
IF OBJECT_ID(N'[dbo].[ProductSKUs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSKUs];
GO
IF OBJECT_ID(N'[dbo].[Provinces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Provinces];
GO
IF OBJECT_ID(N'[dbo].[Amphurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Amphurs];
GO
IF OBJECT_ID(N'[dbo].[Districts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Districts];
GO
IF OBJECT_ID(N'[dbo].[Geographies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Geographies];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[QuestionLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionLists];
GO
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

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NULL,
    [Massage] nvarchar(max)  NULL,
    [Date] datetime  NULL,
    [Status] smallint  NULL,
    [UserAdminId] int  NULL
);
GO

-- Creating table 'UserAdmins'
CREATE TABLE [dbo].[UserAdmins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userName] nvarchar(max)  NOT NULL,
    [Password] nchar(512)  NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [JobTitle] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Tel] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [Permission_permissionId] int  NOT NULL,
    [LastSignIn] datetime  NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [permissionId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Detail] nvarchar(max)  NULL,
    [isAdmin] bit  NOT NULL,
    [isUser] bit  NOT NULL,
    [isProduct] bit  NOT NULL,
    [isPayment] bit  NOT NULL,
    [isLog] bit  NOT NULL
);
GO

-- Creating table 'ProcessStatusTypes'
CREATE TABLE [dbo].[ProcessStatusTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Detail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PayTypes'
CREATE TABLE [dbo].[PayTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Detail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PayItems'
CREATE TABLE [dbo].[PayItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] smallint  NOT NULL,
    [PayId] int  NOT NULL,
    [CourseId] int  NOT NULL
);
GO

-- Creating table 'Pays'
CREATE TABLE [dbo].[Pays] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [MembershipId] int  NOT NULL,
    [PayTypeId] smallint  NOT NULL,
    [ProcessStatusTypeId] smallint  NOT NULL,
    [Type] smallint  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Memberships'
CREATE TABLE [dbo].[Memberships] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TitleName] nvarchar(max)  NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [FirstnameTH] nvarchar(max)  NULL,
    [LastnameTH] nvarchar(max)  NULL,
    [FirstnameEN] nvarchar(max)  NULL,
    [LastnameEN] nvarchar(max)  NULL,
    [Company] nvarchar(max)  NULL,
    [JobTitle] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Tel] nvarchar(max)  NULL,
    [Mobile] nvarchar(max)  NULL,
    [Fax] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NOT NULL,
    [RegisterDate] datetime  NULL,
    [RegisterDateExp] datetime  NULL,
    [MembershipRegisterTypeId] smallint  NOT NULL,
    [IdCRM] nvarchar(max)  NULL,
    [GroupCRM] nvarchar(max)  NULL,
    [BirthDay] datetime  NULL,
    [Religion] nvarchar(max)  NULL,
    [Question] nvarchar(max)  NULL,
    [Answer] nvarchar(max)  NULL,
    [Active] bit  NOT NULL,
    [QuestionListId] int  NOT NULL,
    [MobileCRM] nvarchar(max)  NULL,
    [ExpCRM] datetime  NULL
);
GO

-- Creating table 'MembershipRegisterTypes'
CREATE TABLE [dbo].[MembershipRegisterTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Detail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TitleTH] nvarchar(max)  NOT NULL,
    [TitleEN] nvarchar(max)  NOT NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [LocationTH] nvarchar(max)  NULL,
    [LocationEN] nvarchar(max)  NULL,
    [Limit] smallint  NULL,
    [SupportTH] bit  NOT NULL,
    [SupportEN] bit  NOT NULL,
    [Price] float  NOT NULL,
    [PriceInter] float  NOT NULL,
    [PriceCorporate] float  NOT NULL,
    [PriceIndividual] float  NOT NULL,
    [PriceCorporateInter] float  NOT NULL,
    [PriceIndividualInter] float  NOT NULL,
    [Group] smallint  NOT NULL,
    [Active] bit  NOT NULL,
    [ProductSKUActive] bit  NOT NULL,
    [isCreditCard] bit  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TitleTH] nvarchar(max)  NOT NULL,
    [TitleEN] nvarchar(max)  NOT NULL,
    [DateStart] datetime  NULL,
    [DateEnd] datetime  NULL,
    [Active] bit  NOT NULL,
    [ProductId] int  NOT NULL,
    [ProductSKUActive] bit  NOT NULL
);
GO

-- Creating table 'ConfirmPayments'
CREATE TABLE [dbo].[ConfirmPayments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Invoice] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Total] float  NOT NULL,
    [Tel] nvarchar(max)  NULL,
    [Date] datetime  NULL,
    [Filename] nvarchar(max)  NULL,
    [Other] nvarchar(max)  NULL,
    [ConfirmPaymentBankTypeId] smallint  NOT NULL,
    [PayId] int  NOT NULL,
    [FilenameConfirm] nvarchar(max)  NULL
);
GO

-- Creating table 'ConfirmPaymentBankTypes'
CREATE TABLE [dbo].[ConfirmPaymentBankTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NULL,
    [NameEN] nvarchar(max)  NULL,
    [DetailTH] nvarchar(max)  NULL,
    [DetailEN] nvarchar(max)  NULL,
    [BanknameTH] nvarchar(max)  NOT NULL,
    [BanknameEN] nvarchar(max)  NOT NULL,
    [BranchTH] nvarchar(max)  NOT NULL,
    [BranchEN] nvarchar(max)  NOT NULL,
    [AccountNo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmailMessages'
CREATE TABLE [dbo].[EmailMessages] (
    [Id] smallint  NOT NULL,
    [TitleTH] nvarchar(max)  NOT NULL,
    [TitleEN] nvarchar(max)  NOT NULL,
    [MessageTH] nvarchar(max)  NOT NULL,
    [MessageEN] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductSKUs'
CREATE TABLE [dbo].[ProductSKUs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameTH] nvarchar(max)  NOT NULL,
    [NameEN] nvarchar(max)  NOT NULL,
    [Percent] float  NOT NULL,
    [isGuest] bit  NOT NULL,
    [isGuest_Inter] bit  NOT NULL,
    [isIndividual] bit  NOT NULL,
    [isIndividual_Inter] bit  NOT NULL,
    [isCorporate] bit  NOT NULL,
    [isCorporate_Inter] bit  NOT NULL,
    [Active] bit  NOT NULL,
    [Operator] int  NULL,
    [Type] int  NOT NULL,
    [CourseId] int  NULL
);
GO

-- Creating table 'Provinces'
CREATE TABLE [dbo].[Provinces] (
    [PROVINCE_ID] int  NOT NULL,
    [PROVINCE_CODE] nvarchar(max)  NOT NULL,
    [PROVINCE_NAME] nvarchar(max)  NOT NULL,
    [GEO_ID] int  NOT NULL
);
GO

-- Creating table 'Amphurs'
CREATE TABLE [dbo].[Amphurs] (
    [AMPHUR_ID] int  NOT NULL,
    [AMPHUR_CODE] nvarchar(max)  NOT NULL,
    [AMPHUR_NAME] nvarchar(max)  NOT NULL,
    [POSTCODE] nvarchar(max)  NOT NULL,
    [GEO_ID] int  NOT NULL,
    [PROVINCE_ID] int  NOT NULL
);
GO

-- Creating table 'Districts'
CREATE TABLE [dbo].[Districts] (
    [DISTRICT_ID] int  NOT NULL,
    [DISTRICT_CODE] nvarchar(max)  NOT NULL,
    [DISTRICT_NAME] nvarchar(max)  NOT NULL,
    [AMPHUR_ID] int  NOT NULL,
    [PROVINCE_ID] int  NOT NULL,
    [GEO_ID] int  NOT NULL
);
GO

-- Creating table 'Geographies'
CREATE TABLE [dbo].[Geographies] (
    [GEO_ID] int  NOT NULL,
    [GEO_NAME] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int  NOT NULL,
    [Code] nvarchar(2)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuestionLists'
CREATE TABLE [dbo].[QuestionLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionTH] nvarchar(max)  NOT NULL,
    [QuestionEN] nvarchar(max)  NOT NULL
);
GO

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
    [DescriptionEN] nvarchar(max)  NULL
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

-- Creating primary key on [Id] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAdmins'
ALTER TABLE [dbo].[UserAdmins]
ADD CONSTRAINT [PK_UserAdmins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [permissionId] in table 'Permissions'
ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([permissionId] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessStatusTypes'
ALTER TABLE [dbo].[ProcessStatusTypes]
ADD CONSTRAINT [PK_ProcessStatusTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayTypes'
ALTER TABLE [dbo].[PayTypes]
ADD CONSTRAINT [PK_PayTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayItems'
ALTER TABLE [dbo].[PayItems]
ADD CONSTRAINT [PK_PayItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [PK_Pays]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Memberships'
ALTER TABLE [dbo].[Memberships]
ADD CONSTRAINT [PK_Memberships]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MembershipRegisterTypes'
ALTER TABLE [dbo].[MembershipRegisterTypes]
ADD CONSTRAINT [PK_MembershipRegisterTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConfirmPayments'
ALTER TABLE [dbo].[ConfirmPayments]
ADD CONSTRAINT [PK_ConfirmPayments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConfirmPaymentBankTypes'
ALTER TABLE [dbo].[ConfirmPaymentBankTypes]
ADD CONSTRAINT [PK_ConfirmPaymentBankTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailMessages'
ALTER TABLE [dbo].[EmailMessages]
ADD CONSTRAINT [PK_EmailMessages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSKUs'
ALTER TABLE [dbo].[ProductSKUs]
ADD CONSTRAINT [PK_ProductSKUs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PROVINCE_ID] in table 'Provinces'
ALTER TABLE [dbo].[Provinces]
ADD CONSTRAINT [PK_Provinces]
    PRIMARY KEY CLUSTERED ([PROVINCE_ID] ASC);
GO

-- Creating primary key on [AMPHUR_ID] in table 'Amphurs'
ALTER TABLE [dbo].[Amphurs]
ADD CONSTRAINT [PK_Amphurs]
    PRIMARY KEY CLUSTERED ([AMPHUR_ID] ASC);
GO

-- Creating primary key on [DISTRICT_ID] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [PK_Districts]
    PRIMARY KEY CLUSTERED ([DISTRICT_ID] ASC);
GO

-- Creating primary key on [GEO_ID] in table 'Geographies'
ALTER TABLE [dbo].[Geographies]
ADD CONSTRAINT [PK_Geographies]
    PRIMARY KEY CLUSTERED ([GEO_ID] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionLists'
ALTER TABLE [dbo].[QuestionLists]
ADD CONSTRAINT [PK_QuestionLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

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

-- Creating foreign key on [Permission_permissionId] in table 'UserAdmins'
ALTER TABLE [dbo].[UserAdmins]
ADD CONSTRAINT [FK_PermissionUser]
    FOREIGN KEY ([Permission_permissionId])
    REFERENCES [dbo].[Permissions]
        ([permissionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionUser'
CREATE INDEX [IX_FK_PermissionUser]
ON [dbo].[UserAdmins]
    ([Permission_permissionId]);
GO

-- Creating foreign key on [MembershipId] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [FK_MembershipPay]
    FOREIGN KEY ([MembershipId])
    REFERENCES [dbo].[Memberships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipPay'
CREATE INDEX [IX_FK_MembershipPay]
ON [dbo].[Pays]
    ([MembershipId]);
GO

-- Creating foreign key on [PayId] in table 'PayItems'
ALTER TABLE [dbo].[PayItems]
ADD CONSTRAINT [FK_PayPayItem]
    FOREIGN KEY ([PayId])
    REFERENCES [dbo].[Pays]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayPayItem'
CREATE INDEX [IX_FK_PayPayItem]
ON [dbo].[PayItems]
    ([PayId]);
GO

-- Creating foreign key on [PayTypeId] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [FK_PayTypePay]
    FOREIGN KEY ([PayTypeId])
    REFERENCES [dbo].[PayTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayTypePay'
CREATE INDEX [IX_FK_PayTypePay]
ON [dbo].[Pays]
    ([PayTypeId]);
GO

-- Creating foreign key on [ProcessStatusTypeId] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [FK_ProcessStatusTypePay]
    FOREIGN KEY ([ProcessStatusTypeId])
    REFERENCES [dbo].[ProcessStatusTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessStatusTypePay'
CREATE INDEX [IX_FK_ProcessStatusTypePay]
ON [dbo].[Pays]
    ([ProcessStatusTypeId]);
GO

-- Creating foreign key on [UserAdminId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [FK_UserAdminLog]
    FOREIGN KEY ([UserAdminId])
    REFERENCES [dbo].[UserAdmins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAdminLog'
CREATE INDEX [IX_FK_UserAdminLog]
ON [dbo].[Logs]
    ([UserAdminId]);
GO

-- Creating foreign key on [MembershipRegisterTypeId] in table 'Memberships'
ALTER TABLE [dbo].[Memberships]
ADD CONSTRAINT [FK_MembershipRegisterTypeMembership]
    FOREIGN KEY ([MembershipRegisterTypeId])
    REFERENCES [dbo].[MembershipRegisterTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipRegisterTypeMembership'
CREATE INDEX [IX_FK_MembershipRegisterTypeMembership]
ON [dbo].[Memberships]
    ([MembershipRegisterTypeId]);
GO

-- Creating foreign key on [ProductId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_ProductCourse]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCourse'
CREATE INDEX [IX_FK_ProductCourse]
ON [dbo].[Courses]
    ([ProductId]);
GO

-- Creating foreign key on [CourseId] in table 'PayItems'
ALTER TABLE [dbo].[PayItems]
ADD CONSTRAINT [FK_CoursePayItem]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoursePayItem'
CREATE INDEX [IX_FK_CoursePayItem]
ON [dbo].[PayItems]
    ([CourseId]);
GO

-- Creating foreign key on [ConfirmPaymentBankTypeId] in table 'ConfirmPayments'
ALTER TABLE [dbo].[ConfirmPayments]
ADD CONSTRAINT [FK_ConfirmPaymentBankTypeConfirmPayment]
    FOREIGN KEY ([ConfirmPaymentBankTypeId])
    REFERENCES [dbo].[ConfirmPaymentBankTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfirmPaymentBankTypeConfirmPayment'
CREATE INDEX [IX_FK_ConfirmPaymentBankTypeConfirmPayment]
ON [dbo].[ConfirmPayments]
    ([ConfirmPaymentBankTypeId]);
GO

-- Creating foreign key on [PayId] in table 'ConfirmPayments'
ALTER TABLE [dbo].[ConfirmPayments]
ADD CONSTRAINT [FK_PayConfirmPayment]
    FOREIGN KEY ([PayId])
    REFERENCES [dbo].[Pays]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayConfirmPayment'
CREATE INDEX [IX_FK_PayConfirmPayment]
ON [dbo].[ConfirmPayments]
    ([PayId]);
GO

-- Creating foreign key on [PROVINCE_ID] in table 'Amphurs'
ALTER TABLE [dbo].[Amphurs]
ADD CONSTRAINT [FK_AmphurProvince]
    FOREIGN KEY ([PROVINCE_ID])
    REFERENCES [dbo].[Provinces]
        ([PROVINCE_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AmphurProvince'
CREATE INDEX [IX_FK_AmphurProvince]
ON [dbo].[Amphurs]
    ([PROVINCE_ID]);
GO

-- Creating foreign key on [AMPHUR_ID] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [FK_AmphurDistrict]
    FOREIGN KEY ([AMPHUR_ID])
    REFERENCES [dbo].[Amphurs]
        ([AMPHUR_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AmphurDistrict'
CREATE INDEX [IX_FK_AmphurDistrict]
ON [dbo].[Districts]
    ([AMPHUR_ID]);
GO

-- Creating foreign key on [GEO_ID] in table 'Provinces'
ALTER TABLE [dbo].[Provinces]
ADD CONSTRAINT [FK_GeographyProvince]
    FOREIGN KEY ([GEO_ID])
    REFERENCES [dbo].[Geographies]
        ([GEO_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GeographyProvince'
CREATE INDEX [IX_FK_GeographyProvince]
ON [dbo].[Provinces]
    ([GEO_ID]);
GO

-- Creating foreign key on [QuestionListId] in table 'Memberships'
ALTER TABLE [dbo].[Memberships]
ADD CONSTRAINT [FK_MembershipQuestionList]
    FOREIGN KEY ([QuestionListId])
    REFERENCES [dbo].[QuestionLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipQuestionList'
CREATE INDEX [IX_FK_MembershipQuestionList]
ON [dbo].[Memberships]
    ([QuestionListId]);
GO

-- Creating foreign key on [CourseId] in table 'ProductSKUs'
ALTER TABLE [dbo].[ProductSKUs]
ADD CONSTRAINT [FK_CourseProductSKU]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseProductSKU'
CREATE INDEX [IX_FK_CourseProductSKU]
ON [dbo].[ProductSKUs]
    ([CourseId]);
GO

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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------


INSERT INTO [dbo].[Permissions] VALUES ('Administrator','all access.',1,1,1,1,1);
INSERT INTO [dbo].[UserAdmins] VALUES (
    'Admin',
    '9D7A76757F6AF9A112029E766D2C589145A0B7E67880875732C4D322FF54E9D0F6A12AF5B7800A859BCFEF76B4F59EA1996069F8FE79C68F58FAE7C3B9C5DB27',
    'admin',
    'admin',
    'CM',
    'IT',
    'admin@git.org',
    '0891234312',
    1,
    1,
    null);


INSERT INTO [dbo].[MembershipRegisterTypes] VALUES 
('Guest','Guest'),
('Guest(inter)','Guest.'),
('Individual','Individual'),
('Individual(inter)','Individual'),
('Corporate','Corporate'),
('Corporate(inter)','Corporate');

INSERT INTO [dbo].[PayTypes] VALUES 
('Bank','Bank'),
('Credit card','Credit card');

INSERT INTO [dbo].[ProcessStatusTypes] VALUES 
('Waiting','Waiting'),
('Finished','Finished');

INSERT INTO [dbo].[ConfirmPaymentBankTypes] VALUES 
(N'ชื่อ','Name','','',N'ธนาคารไทยพานิชย์','The Siam Commercial Bank (SCB)',N'กทม','bangkok','245-207440-3');

INSERT INTO [dbo].[Products] VALUES (N'สมัครสมาชิกสถาบัน',N'Register membership',null,null,null,null,null,1,1,1000,40,1000,40,1000,40,0,1,0,1),
(N'สมัครสมาชิกสถาบัน',N'Register membership',null,null,null,null,null,1,1,2000,80,2000,80,2000,80,0,1,0,1),
(N'สมัครสมาชิกสถาบัน',N'Register membership',null,null,null,null,null,1,1,2000,80,2000,80,2000,80,0,1,0,1),
(N'สมัครสมาชิกสถาบัน',N'Register membership',null,null,null,null,null,1,1,4000,160,4000,160,4000,160,0,1,0,1);

INSERT INTO [dbo].[Courses] VALUES (N'บุคคลธรรมดา',N'บุคคลธรรมดา',NULL,NULL,1,1,0),
(N'นิติบุคคล',N'นิติบุคคล',NULL,NULL,1,2,0),
(N'individual',N'individual',NULL,NULL,1,3,0),
(N'corparate',N'corparate',NULL,NULL,1,4,0);

INSERT INTO [dbo].[EmailMessages] VALUES (1,N'You have registered with GIT eService System.',N'You have registered with GIT eService System.',
N'&lt;p&gt;คุณได้ลงทะเบียนเพื่อเข้าใช้งานระบบบริการออนไลน์&lt;/p&gt;

&lt;p&gt;Username : {email}&lt;/p&gt;

&lt;p&gt;คลิก {urllink0} เพื่อยืนยันการลงทะเบียน&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ในช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'&lt;p&gt;You have registered with GIT eService System&lt;/p&gt;

&lt;p&gt;Username : {email}&lt;/p&gt;

&lt;p&gt;Click {urllink0} to activate your registration.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;'),
(2,N'ยืนยันการลงทะเบียนอบรม',N'ยืนยันการลงทะเบียนอบรม',
N'&lt;p&gt;ท่านได้ลงทะเบียนอบรมในหลักสูตรดังต่อไปนี้&lt;/p&gt;

&lt;p&gt;{paymentList}&lt;/p&gt;

&lt;p&gt;กรุณาคลิก {urllink0} เพื่อยืนยันการชำระเงิน&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ใน ช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'&lt;p&gt;You have registered for the following course(s):&lt;/p&gt;

&lt;p&gt;{paymentList}&lt;/p&gt;

&lt;p&gt;Please click {urllink0} for payment confirmation.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;'),
(3,N'ยืนยันการลงทะเบียนอบรม',N'ยืนยันการลงทะเบียนอบรม',
N'&lt;p&gt;ท่านได้ลงทะเบียนเป็นสมาชิกของสถาบัน&lt;/p&gt;

&lt;p&gt;กรุณาคลิก {urllink0} เพื่อยืนยันการชำระเงิน&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ใน ช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'&lt;p&gt;You have registered for GIT Membership.&lt;/p&gt;

&lt;p&gt;Please click {urllink0} for payment confirmation.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;'),
(4,N'ตั้งค่ารหัสผ่านใหม่',N'Reset your password',
N'&lt;p&gt;กรุณาคลิก {urllink0} เพื่อตั้ง รหัสผ่านใหม่&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ใน ช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;',
N'&lt;p&gt;Please click {urllink0} for resetting your password.&lt;/p&gt;

&lt;p&gt;If you can&amp;#39;t open the site, please copy the URL and paste it into a Web Browser&amp;#39;s address bar.&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;The Gem and Jewelry Institute of Thailand (Public Organization)&lt;/p&gt;');
