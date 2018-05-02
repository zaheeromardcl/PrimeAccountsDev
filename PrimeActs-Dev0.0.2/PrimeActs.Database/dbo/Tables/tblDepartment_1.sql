CREATE TABLE [dbo].[tblDepartment] (
    [DepartmentID]   UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]     UNIQUEIDENTIFIER NULL,
    [DepartmentName] NVARCHAR (50)    NULL,
	[DepartmentCode] Nvarchar (2)	  not Null,
    [UpdatedBy]      NVARCHAR (25)    NULL,
    [UpdatedDate]    DATETIME         NULL,
    [CreatedBy]      NVARCHAR (25)    NULL,
    [CreatedDate]    DATETIME         NULL,
    [AddressID]      UNIQUEIDENTIFIER NULL,
    [IsActive]       BIT              CONSTRAINT [DF__tblDepart__IsAct__403A8C7D] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblDepartment_1] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
    CONSTRAINT [FK_tblDepartment_tblAddress] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[tblAddress] ([AddressID]),
    CONSTRAINT [FK_tblDepartment_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);

