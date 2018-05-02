CREATE TABLE [dbo].[tblAuditAddress]
(
	[AuditAddressID] UNIQUEIDENTIFIER NOT NULL , 
    [AddressID] UNIQUEIDENTIFIER NOT NULL, 
    [AddressLine1] NVARCHAR(50) NOT NULL, 
    [AddressLine2] NVARCHAR(50) NULL, 
    [AddressLine3] NVARCHAR(50) NOT NULL, 
    [PostalTown] NVARCHAR(50) NOT NULL, 
    [CountyCity] NVARCHAR(50) NOT NULL, 
    [Postcode] NVARCHAR(10) NOT NULL,
	[CreatedBy] NVARCHAR(25) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(25) NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [RevisionNumber] NUMERIC NOT NULL, 
    CONSTRAINT [PK_tblAuditAddress] PRIMARY KEY CLUSTERED (AuditAddressID ASC),
	 CONSTRAINT [FK_tblAuditAddress_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].tblAddress (AddressID)
)
