CREATE TABLE [dbo].[tblDivision] (
    [DivisionID]   UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]    UNIQUEIDENTIFIER NOT NULL,
    [DivisionName] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]    NVARCHAR (25)    NULL,
    [UpdatedDate]  DATETIME         NULL,
    [CreatedBy]    NVARCHAR (25)    NULL,
    [CreatedDate]  DATETIME         NULL,
    [AddressID]    UNIQUEIDENTIFIER NULL,
    [IsActive]     BIT              CONSTRAINT [DF__tblDivisi__IsAct__4316F928] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblDivision_1] PRIMARY KEY CLUSTERED ([DivisionID] ASC),
    CONSTRAINT [FK_tblDivision_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblDivision_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

