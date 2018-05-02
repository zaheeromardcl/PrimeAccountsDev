<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblCustomerContactLocation] (
    [CustomerContactLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerContactID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationID]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]               DATETIME         NULL,
    [UpdatedByUserID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]               DATETIME         NULL,
    CONSTRAINT [PK_tblCustomerContactLocation] PRIMARY KEY CLUSTERED ([CustomerContactLocationID] ASC),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerContact] FOREIGN KEY ([CustomerContactID]) REFERENCES [dbo].[tblCustomerContact] ([CustomerContactID]),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID]),
    CONSTRAINT [FK_tblCustomerContactLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerContactLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblCustomerContactLocation] (
    [CustomerContactLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerContactID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationID]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedByUserID]           UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]               DATETIME         NULL,
    [UpdatedByUserID]           UNIQUEIDENTIFIER NULL,
    [UpdatedDate]               DATETIME         NULL,
    CONSTRAINT [PK_tblCustomerContactLocation] PRIMARY KEY CLUSTERED ([CustomerContactLocationID] ASC),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerContact] FOREIGN KEY ([CustomerContactID]) REFERENCES [dbo].[tblCustomerContact] ([CustomerContactID]),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID]),
    CONSTRAINT [FK_tblCustomerContactLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerContactLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
