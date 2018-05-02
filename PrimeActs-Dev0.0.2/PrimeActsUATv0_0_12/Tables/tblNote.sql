CREATE TABLE [dbo].[tblNote] (
    [NoteID]          UNIQUEIDENTIFIER NOT NULL,
    [NoteDescription] NVARCHAR (250)   NOT NULL,
    [NoteText]        NTEXT            NOT NULL,
    CONSTRAINT [PK_tblNote] PRIMARY KEY CLUSTERED ([NoteID] ASC)
);

