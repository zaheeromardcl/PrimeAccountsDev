﻿CREATE TABLE [dbo].[tblSupplierDepartment] (
    [SupplierDepartmentID]       UNIQUEIDENTIFIER NOT NULL,
    [SupplierID]                 UNIQUEIDENTIFIER NOT NULL,
    [SupplierDepartmentName]     NVARCHAR (50)    NOT NULL,
    [CreditTerms]                INT              NULL,
    [CreditLimit]                NUMERIC(18,4)            NULL,
    [Commission]                 NUMERIC (6, 2)   NULL,
    [Handling]                   NUMERIC (6, 2)   NULL,
    [FactorSupplierDepartmentID] UNIQUEIDENTIFIER NULL,
    [NoteID]                     UNIQUEIDENTIFIER NULL,
    [Rebate]                     NUMERIC (6, 2)   NULL,
    [EmailAddress]               NVARCHAR (100)   NULL,
	[CountryID]					uniqueidentifier  NOT null,
    [EDIType]                    TINYINT          NULL,
    [EDINumber]                  NVARCHAR (13)    NULL,
    [EDIIdent]                   NVARCHAR (20)    NULL,
    [UpdatedBy]                  NVARCHAR (25)    NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedBy]                  NVARCHAR (25)    NULL,
    [CreatedDate]                DATETIME         NULL,
    [IsActive]                   BIT              CONSTRAINT [DF__tblSuppli__IsAct__30C33EC3] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblSupplierDepartment] PRIMARY KEY CLUSTERED ([SupplierDepartmentID] ASC),
    CONSTRAINT [FK_tblSupplierDepartment_tlkpCountry] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[tlkpCountry] ([CountryID]),
	CONSTRAINT [FK_tblSupplierDepartment_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblSupplierDepartment_tblSupplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[tblSupplier] ([SupplierID]),
    CONSTRAINT [FK_tblSupplierDepartment_tblSupplierDepartment] FOREIGN KEY ([FactorSupplierDepartmentID]) REFERENCES [dbo].[tblSupplierDepartment] ([SupplierDepartmentID])
);

