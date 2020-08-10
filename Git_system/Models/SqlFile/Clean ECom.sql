SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

delete from EComOrderItems
go
dbcc checkident(EComOrderItems, reseed, 0)
go

delete from EComConfirmOrders
go
dbcc checkident(EComConfirmOrders, reseed, 0)
go

--delete from EComDeliverTypes
delete from EComPromotions
go
dbcc checkident(EComPromotions, reseed, 0)
go

delete from EComCategoryMaps
go
dbcc checkident(EComCategoryMaps, reseed, 0)
go

delete from EComCategories
go
dbcc checkident(EComCategories, reseed, 0)
go

delete from EComOrders
go
dbcc checkident(EComOrders, reseed, 0)
go

delete from EComDeliverTypes
go
dbcc checkident(EComDeliverTypes, reseed, 0)
go

delete from EComLogViews
go
dbcc checkident(EComLogViews, reseed, 0)
go

delete from EComProductDelivers
go
dbcc checkident(EComProductDelivers, reseed, 0)
go

delete from EComKeywords
go
dbcc checkident(EComKeywords, reseed, 0)
go

delete from EComDeliverPromotionMaps
go
dbcc checkident(EComDeliverPromotionMaps, reseed, 0)
go

delete from EComDeliverPromotions
go
dbcc checkident(EComDeliverPromotions, reseed, 0)
go

delete from EComProducts
go
dbcc checkident(EComProducts, reseed, 0)
go

delete from SlideshowTranslates
go
dbcc checkident(SlideshowTranslates, reseed, 0)
go

delete from Slideshows
go
dbcc checkident(Slideshows, reseed, 0)
go