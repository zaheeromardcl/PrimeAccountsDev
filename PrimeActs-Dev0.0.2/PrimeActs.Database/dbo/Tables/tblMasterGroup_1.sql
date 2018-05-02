CREATE TABLE [dbo].[tblMasterGroup] (
    [MasterGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]      UNIQUEIDENTIFIER NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [MasterGroupCode] NVARCHAR (10)    NOT NULL,
    [MasterGroupName] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]       NVARCHAR (25)    NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedBy]       NVARCHAR (25)    NULL,
    [CreatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblMasterGroup_1] PRIMARY KEY CLUSTERED ([MasterGroupID] ASC),
    CONSTRAINT [FK_tblMasterGroup_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID])
);

