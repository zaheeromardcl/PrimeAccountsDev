CREATE TABLE [dbo].[tblGroup] (
    [GroupID]     UNIQUEIDENTIFIER NOT NULL,
    [GroupName]   NVARCHAR (50)    NOT NULL,
    [UpdatedBy]   NVARCHAR (25)    NULL,
    [UpdatedDate] DATETIME         NULL,
    [CreatedBy]   NVARCHAR (25)    NULL,
    [CreatedDate] DATETIME         NULL,
    [IsActive]    BIT              CONSTRAINT [DF__tblGroup__IsActi__640DD89F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblGroup_1] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

