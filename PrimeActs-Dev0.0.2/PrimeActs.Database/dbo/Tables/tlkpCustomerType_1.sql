CREATE TABLE [dbo].[tlkpCustomerType] (
    [CustomerTypeID]          UNIQUEIDENTIFIER NOT NULL,
    [CustomerTypeCode]        NVARCHAR (10)    NOT NULL,
    [CustomerTypeDescription] NVARCHAR (50)    NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]               NVARCHAR (25)    NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedBy]               NVARCHAR (25)    NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF_tlkpCustomerType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpCustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeID] ASC),
    CONSTRAINT [FK_tlkpCustomerType_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

