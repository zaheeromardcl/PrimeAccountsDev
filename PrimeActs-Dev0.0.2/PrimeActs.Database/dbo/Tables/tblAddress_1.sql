﻿CREATE TABLE [dbo].[tblAddress] (
    [AddressID]    UNIQUEIDENTIFIER NOT NULL,
    [AddressLine1] NVARCHAR (50)    NOT NULL,
    [AddressLine2] NVARCHAR (50)    NULL,
    [AddressLine3] NVARCHAR (50)    NULL,
    [PostalTown]   NVARCHAR (50)    NULL,
    [CountyCity]   NVARCHAR (50)    NOT NULL,
    [Postcode]     NVARCHAR (10)    NULL,
    CONSTRAINT [PK_tblAddress] PRIMARY KEY CLUSTERED ([AddressID] ASC)
);

