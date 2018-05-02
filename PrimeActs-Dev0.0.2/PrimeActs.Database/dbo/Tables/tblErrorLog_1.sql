CREATE TABLE [dbo].[tblErrorLog] (
    [ErrorID]          UNIQUEIDENTIFIER NOT NULL,
    [ErrorDescription] NVARCHAR (50)    NOT NULL,
    [ErrorCategory]    NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_tblErrorLog_1] PRIMARY KEY CLUSTERED ([ErrorID] ASC)
);

