
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO


-- Creating table 'BaseDevices'
CREATE TABLE [dbo].[BaseDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceIdentifier] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [MembershipId] int  NOT NULL
);
GO

-- Creating primary key on [Id] in table 'BaseDevices'
ALTER TABLE [dbo].[BaseDevices]
ADD CONSTRAINT [PK_BaseDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO


-- Creating foreign key on [MembershipId] in table 'BaseDevices'
ALTER TABLE [dbo].[BaseDevices]
ADD CONSTRAINT [FK_MembershipDevice]
    FOREIGN KEY ([MembershipId])
    REFERENCES [dbo].[Memberships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MembershipDevice'
CREATE INDEX [IX_FK_MembershipDevice]
ON [dbo].[BaseDevices]
    ([MembershipId]);
GO
