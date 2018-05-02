CREATE TABLE [dbo].[tlkpCountry] (
    [CountryID]   UNIQUEIDENTIFIER NOT NULL,
    [CountryName] NVARCHAR (50)    NOT NULL,
    [CountryCode] NVARCHAR (10)    NOT NULL,
    [UpdatedBy]   NVARCHAR (25)    NULL,
    [UpdatedDate] DATETIME         NULL,
    [CreatedBy]   NVARCHAR (25)    NULL,
    [CreatedDate] DATETIME         NULL,
    [IsActive]    BIT              CONSTRAINT [DF__tlkpCount__IsAct__3CF40B7E] DEFAULT ((1)) NOT NULL,
    [CompanyID]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tlkpCountry] PRIMARY KEY CLUSTERED ([CountryID] ASC),
    CONSTRAINT [FK_tlkpCountry_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

