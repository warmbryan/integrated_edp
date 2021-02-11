CREATE TABLE [dbo].[BusinessEmployeeAccess]
(
	[id] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
	[userId] UNIQUEIDENTIFIER NOT NULL,
	[businessId] UNIQUEIDENTIFIER NOT NULL,
	[readAppointment] BIT NOT NULL,
	[writeAppointment] BIT NOT NULL,
	[readCustomerChat] BIT NOT NULL,
	[writeCustomerChat] BIT NOT NULL,
	[role] VARCHAR(20) NOT NULL,
	CONSTRAINT [beaUserIdFK] FOREIGN KEY ([userId]) REFERENCES [dbo].[BusinessUser] ([id]),
	CONSTRAINT [beaBusinessIdFK] FOREIGN KEY ([userId]) REFERENCES [dbo].[Business] ([id])
)
