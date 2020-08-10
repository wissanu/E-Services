
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Update table
ALTER TABLE [dbo].[Pays]
	ADD [Note] nvarchar(max)  NULL
GO
