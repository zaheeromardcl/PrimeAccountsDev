CREATE TABLE [dbo].[tblCompanyNominalAccount] (
    [CompanyNominalAccountID] UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]               UNIQUEIDENTIFIER NOT NULL,
    [NominalAccountID]        UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]               NVARCHAR (25)    NULL,
    [UpdatedDate]             DATETIME         NULL,
    [CreatedBy]               NVARCHAR (25)    NULL,
    [CreatedDate]             DATETIME         NULL,
    [IsActive]                BIT              CONSTRAINT [DF__tblCompan__IsAct__5D60DB10] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCompanyNominalAccount] PRIMARY KEY CLUSTERED ([CompanyNominalAccountID] ASC),
    CONSTRAINT [FK_tblCompanyNominalAccount_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblCompanyNominalAccount_tblNominalAccount] FOREIGN KEY ([NominalAccountID]) REFERENCES [dbo].[tblNominalAccount] ([NominalAccountID])
);

