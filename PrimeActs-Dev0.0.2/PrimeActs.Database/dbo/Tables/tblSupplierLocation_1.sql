CREATE TABLE [dbo].[tblSupplierLocation] (
    [SupplierLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]           UNIQUEIDENTIFIER NOT NULL,
    [SupplierLocationName] VARCHAR (50)     NOT NULL,
    [AddressID]            UNIQUEIDENTIFIER NOT NULL,
    [NoteID]               UNIQUEIDENTIFIER NULL,
    [TelephoneNumber]      NVARCHAR (30)    NULL,
    [FaxNumber]            NVARCHAR (30)    NULL,
    [UpdatedBy]            NVARCHAR (25)    NULL,
    [UpdatedDate]          DATETIME         NULL,
    [CreatedBy]            NVARCHAR (25)    NULL,
    [CreatedDate]          DATETIME         NULL,
    [IsActive]             BIT              CONSTRAINT [DF__tblSuppli__IsAct__31B762FC] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSupplierLocation] PRIMARY KEY CLUSTERED ([SupplierLocationID] ASC),
    CONSTRAINT [FK_tblSupplierLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblSupplierLocation_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSupplierLocation_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID])
);

