<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblSupplierLocation] (
    [SupplierLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationName] NVARCHAR (50)    NOT NULL,
    [AddressID]            UNIQUEIDENTIFIER NOT NULL,
    [NoteID]               UNIQUEIDENTIFIER NULL,
    [TelephoneNumber]      NVARCHAR (30)    NULL,
    [FaxNumber]            NVARCHAR (30)    NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF__tblSuppli__IsAct__31B762FC] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSupplierLocation] PRIMARY KEY CLUSTERED ([SupplierLocationID] ASC),
    CONSTRAINT [FK_tblSupplierLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSupplierLocation_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSupplierLocation_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblSupplierLocation] (
    [SupplierLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationName] NVARCHAR (50)    NOT NULL,
    [AddressID]            UNIQUEIDENTIFIER NOT NULL,
    [NoteID]               UNIQUEIDENTIFIER NULL,
    [TelephoneNumber]      NVARCHAR (30)    NULL,
    [FaxNumber]            NVARCHAR (30)    NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF__tblSuppli__IsAct__31B762FC] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSupplierLocation] PRIMARY KEY CLUSTERED ([SupplierLocationID] ASC),
    CONSTRAINT [FK_tblSupplierLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSupplierLocation_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSupplierLocation_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSupplierLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
