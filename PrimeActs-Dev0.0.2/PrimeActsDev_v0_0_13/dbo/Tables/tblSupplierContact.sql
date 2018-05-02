<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSupplierContact] (
    [SupplierContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CreatedDate]       DATETIME         NULL,
    [SupplierID]        UNIQUEIDENTIFIER NOT NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblSuppli__IsAct__2FCF1A8A] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSupplierContact] PRIMARY KEY CLUSTERED ([SupplierContactID] ASC),
    CONSTRAINT [FK_tblSupplierContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblSupplierContact_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierContactCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierContactUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSupplierContact] (
    [SupplierContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CreatedDate]       DATETIME         NULL,
    [SupplierID]        UNIQUEIDENTIFIER NOT NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblSuppli__IsAct__2FCF1A8A] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSupplierContact] PRIMARY KEY CLUSTERED ([SupplierContactID] ASC),
    CONSTRAINT [FK_tblSupplierContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblSupplierContact_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierContactCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierContactUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
