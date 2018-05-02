CREATE TABLE [dbo].[tblCustomerContact] (
    [CustomerContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedBy]         NVARCHAR (25)    NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CreatedBy]         NVARCHAR (25)    NULL,
    [CustomerID]        UNIQUEIDENTIFIER CONSTRAINT [DF_tblCustomerContact_CustomerID] DEFAULT ('68144060-6063-7000-0076-000000000096') NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblCustom__IsAct__151B244E] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCustomerContact_1] PRIMARY KEY CLUSTERED ([CustomerContactID] ASC),
    CONSTRAINT [FK_tblCustomerContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblCustomerContact_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID])
);

