CREATE TABLE [dbo].[tblSetupGlobal] (
	[SetupID]					INT,
    [SetupName]                  NVARCHAR (50)    NOT NULL,
    [SetupValueType]             TINYINT          NOT NULL,
    [SetupValueInt]              INT              NULL,
    [SetupValueNumeric]          NUMERIC (38, 9)  NULL,
    [SetupValueBit]              BIT              NULL,
    [SetupValueNvarchar]         NVARCHAR (50)    NULL,
    [SetupValueUniqueIdentifier] UNIQUEIDENTIFIER NULL,
    [CompanyID]                  UNIQUEIDENTIFIER NULL,
    [DivisionID]                 UNIQUEIDENTIFIER NULL,
    [DepartmentID]               UNIQUEIDENTIFIER NULL,
    [UpdatedBy]                  NVARCHAR (25)    NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedBy]                  NVARCHAR (25)    NULL,
    [CreatedDate]                DATETIME         NULL,
    CONSTRAINT [PK_tblSetupGlobal] PRIMARY KEY CLUSTERED ([SetupName] ASC),
    CONSTRAINT [FK_tblSetupGlobal_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblSetupGlobal_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblSetupGlobal_NameCompDivDept]
    ON [dbo].[tblSetupGlobal]([SetupName] ASC, [CompanyID] ASC, [DivisionID] ASC, [DepartmentID] ASC);

