﻿CREATE TABLE [dbo].[tlkpCustomerType] (
    [CustomerTypeID]          UNIQUEIDENTIFIER NOT NULL,
    [CustomerTypeCode]        NVARCHAR (10)    NOT NULL,
    [CustomerTypeDescription] NVARCHAR (50)    NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF_tlkpCustomerType_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]         UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpCustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeID] ASC),
    CONSTRAINT [FK_tlkpCustomerType_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCustomerTypeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCustomerTypeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

