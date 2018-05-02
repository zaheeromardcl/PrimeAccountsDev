CREATE TABLE [dbo].[tblApplicationRole] (
    [ApplicationRoleID] UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (256)   NOT NULL,
    [Description]   NVARCHAR (MAX)   NULL,    
    CONSTRAINT [PK_tblApplicationRole] PRIMARY KEY CLUSTERED ([ApplicationRoleID] ASC)
);

