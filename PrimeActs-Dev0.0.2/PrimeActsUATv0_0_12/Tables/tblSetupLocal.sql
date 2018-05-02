CREATE TABLE [dbo].[tblSetupLocal] (
    [SetupName]                  NVARCHAR (50)    NOT NULL,
    [SetupValueType]             INT              NOT NULL,
    [SetupValueInt]              INT              NULL,
    [SetupValueNumeric]          NUMERIC (38, 9)  NULL,
    [SetupValueBit]              BIT              NULL,
    [SetupValueNvarchar]         NVARCHAR (MAX)   NULL,
    [UpdatedDate]                DATETIME         NULL,
    [CreatedDate]                DATETIME         NULL,
    [CreatedByUserID]            UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]            UNIQUEIDENTIFIER NULL,
    [SetupID]                    UNIQUEIDENTIFIER NOT NULL,
    [SetupValueUniqueidentifier] UNIQUEIDENTIFIER NULL,
    [CompanyID]                  UNIQUEIDENTIFIER NULL,
    [DivisionID]                 UNIQUEIDENTIFIER NULL,
    [DepartmentID]               UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblSetup] PRIMARY KEY CLUSTERED ([SetupName] ASC),
    CONSTRAINT [FK_tblSetupLocal_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID]),
    CONSTRAINT [FK_tblSetupLocal_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblSetupLocal_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblSetupLocalCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblSetupLocalUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    UNIQUE NONCLUSTERED ([SetupID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ__tblSetup__C9C734B21771AD66]
    ON [dbo].[tblSetupLocal]([SetupID] ASC);

