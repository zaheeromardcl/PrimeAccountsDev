CREATE TABLE [dbo].[tblApplicationUserClaim] (
    [ApplicationUserClaimId] UNIQUEIDENTIFIER NOT NULL,
    [ApplicationUserId]     UNIQUEIDENTIFIER NOT NULL,
    [ClaimType]  NVARCHAR (MAX)   NULL,
    [ClaimValue] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_tblApplicationUserClaim] PRIMARY KEY CLUSTERED ([ApplicationUserClaimId] ASC),
    CONSTRAINT [FK_tblApplicationUserClaim_tblApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[tblApplicationUser] ([ApplicationUserId])
);

