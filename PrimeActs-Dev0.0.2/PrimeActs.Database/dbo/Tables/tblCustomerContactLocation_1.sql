CREATE TABLE [dbo].[tblCustomerContactLocation] (
    [CustomerContactLocationID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerContactID]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerLocationID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblCustomerContactLocation] PRIMARY KEY CLUSTERED ([CustomerContactLocationID] ASC),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerContact] FOREIGN KEY ([CustomerContactID]) REFERENCES [dbo].[tblCustomerContact] ([CustomerContactID]),
    CONSTRAINT [FK_tblCustomerContactLocation_tblCustomerLocation] FOREIGN KEY ([CustomerLocationID]) REFERENCES [dbo].[tblCustomerLocation] ([CustomerLocationID])
);

