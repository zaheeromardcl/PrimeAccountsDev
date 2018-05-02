CREATE TABLE [dbo].[tblMasterGroup] (
    [MasterGroupID]   UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]      UNIQUEIDENTIFIER NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [MasterGroupCode] NVARCHAR (10)    NOT NULL,
    [MasterGroupName] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedDate]     DATETIME         NULL,
    [CreatedByUserID] UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblMasterGroup_1] PRIMARY KEY CLUSTERED ([MasterGroupID] ASC),
    CONSTRAINT [FK_tblMasterGroup_tblDivision] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[tblDivision] ([DivisionID]),
    CONSTRAINT [FK_tblMasterGroupCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblMasterGroupUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

