CREATE TABLE [dbo].[tblApplicationUserLogin] (
	[ApplicationUserLoginId] UNIQUEIDENTIFIER CONSTRAINT [DF_tblApplicationUserLogin_ApplicationUserLoginId] DEFAULT (newid()) NOT NULL,
    [LoginProvider] NVARCHAR (128)   NOT NULL,
    [ProviderKey]   UNIQUEIDENTIFIER NOT NULL,
    [ApplicationUserId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_tblApplicationUserLogin] PRIMARY KEY CLUSTERED ([ApplicationUserLoginId] ASC),
	CONSTRAINT [UQ_tblApplicationUserLogin] UNIQUE NONCLUSTERED ([LoginProvider], [ProviderKey], [ApplicationUserId]),
    CONSTRAINT [FK_tblApplicationUserLogin_tblApplicationUser_UserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[tblApplicationUser] ([ApplicationUserId]) ON DELETE CASCADE
);

