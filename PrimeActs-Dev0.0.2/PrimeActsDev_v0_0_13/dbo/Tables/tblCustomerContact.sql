<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblCustomerContact] (
    [CustomerContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CustomerID]        UNIQUEIDENTIFIER CONSTRAINT [DF_tblCustomerContact_CustomerID] DEFAULT ('68144060-6063-7000-0076-000000000096') NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblCustom__IsAct__151B244E] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCustomerContact_1] PRIMARY KEY CLUSTERED ([CustomerContactID] ASC),
    CONSTRAINT [FK_tblCustomerContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblCustomerContact_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerContactCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerContactUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tblCustomerContact] (
    [CustomerContactID] UNIQUEIDENTIFIER NOT NULL,
    [ContactID]         UNIQUEIDENTIFIER NOT NULL,
    [SortOrder]         UNIQUEIDENTIFIER NULL,
    [UpdatedDate]       DATETIME         NULL,
    [CustomerID]        UNIQUEIDENTIFIER CONSTRAINT [DF_tblCustomerContact_CustomerID] DEFAULT ('68144060-6063-7000-0076-000000000096') NOT NULL,
    [CreatedDate]       DATETIME         NULL,
    [IsActive]          BIT              CONSTRAINT [DF__tblCustom__IsAct__151B244E] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblCustomerContact_1] PRIMARY KEY CLUSTERED ([CustomerContactID] ASC),
    CONSTRAINT [FK_tblCustomerContact_tblContact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[tblContact] ([ContactID]),
    CONSTRAINT [FK_tblCustomerContact_tblCustomer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[tblCustomer] ([CustomerID]),
    CONSTRAINT [FK_tblCustomerContactCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblCustomerContactUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
