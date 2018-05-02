CREATE TABLE [dbo].[tblSupplierContact] (
    [SupplierContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedBy]         NVARCHAR (25)    NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CreatedBy]         NVARCHAR (25)    NULL,
    [CreatedDate]       DATETIME         NULL,
    [SupplierID]        UNIQUEIDENTIFIER NOT NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblSuppli__IsAct__2FCF1A8A] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSupplierContact] PRIMARY KEY CLUSTERED ([SupplierContactID] ASC),
    CONSTRAINT [FK_tblSupplierContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblSupplierContact_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID])
);

