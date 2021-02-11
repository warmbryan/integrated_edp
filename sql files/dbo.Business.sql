CREATE TABLE [dbo].[Business] (
    [id]                 UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [name]               VARCHAR (200)    NOT NULL,
    [registrationNumber] VARCHAR (20)     NOT NULL,
    [type]               VARCHAR (50)     NOT NULL,
    [url]                VARCHAR (150)    NULL,
    [logoId]             UNIQUEIDENTIFIER NULL,
    [acraCertificate]    UNIQUEIDENTIFIER NOT NULL,
    [verified]           BIT              DEFAULT ((0)) NOT NULL,
    [customerId]         UNIQUEIDENTIFIER NULL,
    [adminId]            UNIQUEIDENTIFIER NULL,
    [categoryId]         UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([customerId] ASC),
    CONSTRAINT [baBusinessFK] FOREIGN KEY ([customerId]) REFERENCES [dbo].[Customer] ([id]),
    CONSTRAINT [bbBusinessFK] FOREIGN KEY ([adminId]) REFERENCES [dbo].[Admin] ([id]),
    CONSTRAINT [bcBusinessFK] FOREIGN KEY ([categoryId]) REFERENCES [dbo].[BusinessCategory] ([id]),
    CHECK ([verified]=(1) OR [verified]=(0))
);

