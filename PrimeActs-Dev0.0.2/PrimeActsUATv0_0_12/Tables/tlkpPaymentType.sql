﻿CREATE TABLE [dbo].[tlkpPaymentType] (
    [PaymentTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentTypeName] NVARCHAR (100)   NOT NULL,
    [PaymentTypeCode] NVARCHAR (10)    NOT NULL,
    [DisplayOrder]    INT              DEFAULT ((0)) NOT NULL,
    [IsDefault]       BIT              DEFAULT ((0)) NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPaymentType] PRIMARY KEY CLUSTERED ([PaymentTypeID] ASC)
);

