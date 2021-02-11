CREATE TABLE [dbo].[Branch] (
    [id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [name]       VARCHAR (100)    NOT NULL,
    [phone]    VARCHAR (20)     NOT NULL,
    [email]          VARCHAR (255)    NOT NULL,
    [description]    VARCHAR (2000)   NOT NULL,
    [country] VARCHAR (50)     NOT NULL,
    [address]  VARCHAR (200)    NOT NULL,
	[address2]  VARCHAR (200)    NOT NULL,
	[city]  VARCHAR (50)    NULL,
	[state]  VARCHAR (50)    NULL,
	[zip]  VARCHAR (20)    NOT NULL,
    [isMainBranch]     BIT              DEFAULT ((0)) NOT NULL,
    [businessId]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [baBranchFK] FOREIGN KEY ([businessId]) REFERENCES [dbo].[Business] ([id]),
    CHECK ([isMainBranch]=(1) OR [isMainBranch]=(0))
);

