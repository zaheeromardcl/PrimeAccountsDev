CREATE TABLE [dbo].[tblNote] (
    [NoteID]          UNIQUEIDENTIFIER NOT NULL,
    [NoteDescription] NVARCHAR (250)   NOT NULL,
    [NoteText]        TEXT             NOT NULL,
    [NoteAuthor]      UNIQUEIDENTIFIER NOT NULL,
    [NoteCreated]     DATETIME         NOT NULL,
    [NoteAmended]     DATETIME         CONSTRAINT [DF_tblNote_NoteAmended] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]       NVARCHAR (25)    NULL,
    [UpdatedDate]     DATETIME         NULL,
    [CreatedBy]       NVARCHAR (25)    NULL,
    [CreatedDate]     DATETIME         NULL,
    CONSTRAINT [PK_tblNote] PRIMARY KEY CLUSTERED ([NoteID] ASC)
);

