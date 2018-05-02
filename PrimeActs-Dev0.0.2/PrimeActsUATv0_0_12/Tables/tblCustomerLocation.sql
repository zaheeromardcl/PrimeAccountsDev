CREATE TABLE [dbo].[tblCustomerLocation] (
    [CustomerLocationID]   UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]           UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationName] NVARCHAR (50)    NOT NULL,
    [AddressID]            UNIQUEIDENTIFIER NOT NULL,
    [TelephoneNumber]      NVARCHAR (30)    NULL,
    [FaxNumber]            NVARCHAR (30)    NULL,
    [NoteID]               UNIQUEIDENTIFIER NULL,
    [IsActive]             BIT              CONSTRAINT [DF__tblCustom__IsAct__17036CC0] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]      UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]          DATETIME         NULL,
    [UpdatedByUserID]      UNIQUEIDENTIFIER NULL,
    [UpdatedDate]          DATETIME         NULL,
    CONSTRAINT [PK_tblCustomerLocation] PRIMARY KEY CLUSTERED ([CustomerLocationID] ASC),
    CONSTRAINT [FK_tblCustomerLocation_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblCustomerLocation_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerLocation_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblCustomerLocationCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerLocationUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

