<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tlkpCreditRating] (
    [CreditRatingID]          UNIQUEIDENTIFIER NOT NULL,
    [CreditRatingCode]        NVARCHAR (10)    NOT NULL,
    [CreditRatingDescription] NVARCHAR (200)   NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF_tlkpCreditRating_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]         UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpCreditRating] PRIMARY KEY CLUSTERED ([CreditRatingID] ASC),
    CONSTRAINT [FK_tlkpCreditRating_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCreditRatingCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCreditRatingUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

=======
﻿CREATE TABLE [dbo].[tlkpCreditRating] (
    [CreditRatingID]          UNIQUEIDENTIFIER NOT NULL,
    [CreditRatingCode]        NVARCHAR (10)    NOT NULL,
    [CreditRatingDescription] NVARCHAR (200)   NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF_tlkpCreditRating_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]         UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]         UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tlkpCreditRating] PRIMARY KEY CLUSTERED ([CreditRatingID] ASC),
    CONSTRAINT [FK_tlkpCreditRating_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tlkpCreditRatingCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tlkpCreditRatingUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
