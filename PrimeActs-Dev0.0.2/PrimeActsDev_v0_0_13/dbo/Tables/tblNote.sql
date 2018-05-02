<<<<<<< HEAD
﻿CREATE TABLE [dbo].[tblNote] (
    [NoteID]          UNIQUEIDENTIFIER NOT NULL,
    [NoteDescription] NVARCHAR (250)   NOT NULL,
    [NoteText]        NTEXT            NOT NULL,
    CONSTRAINT [PK_tblNote] PRIMARY KEY CLUSTERED ([NoteID] ASC)
);

=======
﻿CREATE TABLE [dbo].[tblNote] (
    [NoteID]          UNIQUEIDENTIFIER NOT NULL,
    [NoteDescription] NVARCHAR (250)   NOT NULL,
    [NoteText]        NTEXT            NOT NULL,
    CONSTRAINT [PK_tblNote] PRIMARY KEY CLUSTERED ([NoteID] ASC)
);

>>>>>>> 77f919d1351ae84bebcf6a9db529e9dcfbd09019
