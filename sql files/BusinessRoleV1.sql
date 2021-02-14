CREATE TABLE [dbo].[BusinessRole]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [name] NCHAR(30) NULL, 
    [businessId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_BusinessRole_ToBusiness] FOREIGN KEY ([businessId]) REFERENCES [dbo].[Business]([id])
)
