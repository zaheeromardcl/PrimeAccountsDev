CREATE TABLE [dbo].[tblSetupGlobal] (
    [SetupName]                  NVARCHAR (50)    NOT NULL,
    [SetupValueType]             INT              NOT NULL,
    [SetupValueInt]              INT              NULL,
    [SetupValueNumeric]          NUMERIC (38, 9)  NULL,
    [SetupValueBit]              BIT              NULL,
    [SetupValueNvarchar]         NVARCHAR (50)    NULL,
    [SetupValueUniqueIdentifier] UNIQUEIDENTIFIER NULL,
    [CompanyID]                  UNIQUEIDENTIFIER NULL,
    [DivisionID]                 UNIQUEIDENTIFIER NULL,
    [DepartmentID]               UNIQUEIDENTIFIER NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedDate]                DATETIME         NULL,
    [CreatedByUserID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]            UNIQUEIDENTIFIER NULL,
    [SetupID]                    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([SetupID] ASC),
    CONSTRAINT [FK_tblSetupGlobal_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblSetupGlobal_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblSetupGlobal_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblSetupGlobalCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSetupGlobalUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblSetupGlobal_NameCompDivDept]
    ON [dbo].[tblSetupGlobal]([SetupName] ASC, [CompanyID] ASC, [DivisionID] ASC, [DepartmentID] ASC);

