CREATE TABLE [dbo].[tlkpCreditRating] (
    [CreditRatingID]          UNIQUEIDENTIFIER NOT NULL,
    [CreditRatingCode]        NVARCHAR (10)    NOT NULL,
    [CreditRatingDescription] NVARCHAR (200)   NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]               NVARCHAR (25)    NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedBy]               NVARCHAR (25)    NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF_tlkpCreditRating_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpCreditRating] PRIMARY KEY CLUSTERED ([CreditRatingID] ASC),
    CONSTRAINT [FK_tlkpCreditRating_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

