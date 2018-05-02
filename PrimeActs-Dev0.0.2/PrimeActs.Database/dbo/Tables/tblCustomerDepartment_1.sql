CREATE TABLE [dbo].[tblCustomerDepartment] (
    [CustomerDepartmentID]      UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]                UNIQUEIDENTIFIER NOT NULL,
    [CustomerDepartmentName]    NVARCHAR (50)    NOT NULL,
    [EmailAddress]              NVARCHAR (100)   NULL,
    [CreditTerms]               INT              NULL,
    [CreditLimit]               NUMERIC(18,4)            NULL,
    [Commission]                NUMERIC (6, 2)   NULL,
    [Handling]                  NUMERIC (6, 2)   NULL,
    [FactorRef]                 NVARCHAR (20)    NULL,
    [EDIType]                   TINYINT          NULL,
    [EDINumber]                 NVARCHAR (13)    NULL,
    [EDIIdent]                  NVARCHAR (20)    NULL,
    [InvoiceCustomerLocationID] UNIQUEIDENTIFIER NOT NULL,
    [InvoiceFrequency]          TINYINT          CONSTRAINT [DF_tblCustomerDepartment_InvoiceFrequency] DEFAULT ((1)) NOT NULL,
    [InvoiceEmailAddress]       NVARCHAR (250)   NULL,
    [NoteID]                    UNIQUEIDENTIFIER NULL,
    [SalesPersonUserID]         UNIQUEIDENTIFIER NULL,
    [CustomerTypeID]            UNIQUEIDENTIFIER NULL,
    [UpdatedBy]                 NVARCHAR (25)    NULL,
    [UpdatedDate]               DATETIME         NULL,
    [CreatedBy]                 NVARCHAR (25)    NULL,
    [CreatedDate]               DATETIME         NULL,
    [IsActive]                  BIT              CONSTRAINT [DF__tblCustom__IsAct__160F4887] DEFAULT ((1)) NOT NULL,
	[RebateType]				TINYINT			 NOT NULL DEFAULT(0),
	[RebateCustomerDepartmentID] UNIQUEIDENTIFIER NULL,
	[RebateRate]				NUMERIC (6, 2) NULL,
    CONSTRAINT [PK_tblCustomerDepartment_1] PRIMARY KEY CLUSTERED ([CustomerDepartmentID] ASC),
    CONSTRAINT [FK_tblCustomerDepartment_tblApplicationUser] FOREIGN KEY ([SalesPersonUserID]) REFERENCES [dbo].[tblApplicationUser] ([ApplicationUserId]),
    CONSTRAINT [FK_tblCustomerDepartment_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerDepartment_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID]),
    CONSTRAINT [FK_tblCustomerDepartment_tlkpCustomerType] FOREIGN KEY ([CustomerTypeID]) REFERENCES [dbo].[tlkpCustomerType] ([CustomerTypeID]),
	CONSTRAINT [FK_tblCustomerDepartment_tblCustomerDepartment] FOREIGN KEY ([RebateCustomerDepartmentID]) REFERENCES [dbo].[tblCustomerDepartment](CustomerDepartmentID)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tblCustomerDepartment', @level2type = N'COLUMN', @level2name = N'InvoiceFrequency';

