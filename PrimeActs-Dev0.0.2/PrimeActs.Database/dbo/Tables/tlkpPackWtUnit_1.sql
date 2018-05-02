CREATE TABLE [dbo].[tlkpPackWtUnit] (
    [PackWtUnitID] UNIQUEIDENTIFIER NOT NULL,
    [WtUnit]       NVARCHAR (10)    NULL,
    [KgMultiple]   NUMERIC (10, 4)  NULL,
    [CompanyID]    UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]    NVARCHAR (25)    NULL,
    [UpdatedDate]  DATETIME         NULL,
    [CreatedBy]    NVARCHAR (25)    NULL,
    [CreatedDate]  DATETIME         NULL,
    [IsActive]     BIT              CONSTRAINT [DF_tlkpPackWtUnit_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPackWtUnit] PRIMARY KEY CLUSTERED ([PackWtUnitID] ASC),
    CONSTRAINT [FK_tlkpPackWtUnit_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tlkpPackWtUnit', @level2type = N'COLUMN', @level2name = N'KgMultiple';

