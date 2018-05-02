CREATE TABLE [dbo].[tblFile] (
    [FileID]      UNIQUEIDENTIFIER NOT NULL,
    [FileName]    NVARCHAR (50)    NOT NULL,
    [ContentType] NVARCHAR (50)    NULL,
    [FileContent] VARBINARY (MAX)  NOT NULL,
    [UpdatedBy]   NVARCHAR (25)    NULL,
    [UpdatedDate] DATETIME         NULL,
    [CreatedBy]   NVARCHAR (25)    NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_tblFile] PRIMARY KEY CLUSTERED ([FileID] ASC)
);

