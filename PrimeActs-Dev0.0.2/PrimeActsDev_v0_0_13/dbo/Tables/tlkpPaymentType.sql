<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpPaymentType] (
    [PaymentTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentTypeName] NVARCHAR (100)   NOT NULL,
    [PaymentTypeCode] NVARCHAR (10)    NOT NULL,
    [DisplayOrder]    INT              DEFAULT ((0)) NOT NULL,
    [IsDefault]       BIT              DEFAULT ((0)) NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPaymentType] PRIMARY KEY CLUSTERED ([PaymentTypeID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tlkpPaymentType] (
    [PaymentTypeID]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentTypeName] NVARCHAR (100)   NOT NULL,
    [PaymentTypeCode] NVARCHAR (10)    NOT NULL,
    [DisplayOrder]    INT              DEFAULT ((0)) NOT NULL,
    [IsDefault]       BIT              DEFAULT ((0)) NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPaymentType] PRIMARY KEY CLUSTERED ([PaymentTypeID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
