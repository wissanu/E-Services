
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Add column
ALTER TABLE [dbo].[Slideshows]
ADD 
    [Link] nvarchar(max)  NULL
GO
