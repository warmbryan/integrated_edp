﻿CREATE TABLE [dbo].[BusinessUser]
(
	[id] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
	[name] VARCHAR(100) NOT NULL,
	[email] VARCHAR(300) NOT NULL,
	[password] VARCHAR(100) NOT NULL,
	[verified] BIT NOT NULL DEFAULT 0,
	[phone] VARCHAR(20) NULL
)
