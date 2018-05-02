CREATE TABLE [dbo].[tblContact] (
    [ContactID]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName]        NVARCHAR (50)    NOT NULL,
    [LastName]         NVARCHAR (50)    NOT NULL,
    [Title]            NVARCHAR (50)    NULL,
    [ContactType]      NVARCHAR (50)    NOT NULL,
    [ContactReference] NVARCHAR (50)    NOT NULL,
    [NoteID]           UNIQUEIDENTIFIER NULL,
    [EmailAddress]     NVARCHAR (100)   NULL,
    [DDITelephoneNo]   NVARCHAR (30)    NULL,
    [MobileNo]         NVARCHAR (20)    NULL,
    [UpdatedBy]        NVARCHAR (25)    NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedBy]        NVARCHAR (25)    NULL,
    [CreatedDate]      DATETIME         NULL,
    [IsActive]         BIT              CONSTRAINT [DF__tblContac__IsAct__1332DBDC] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblContact_1] PRIMARY KEY CLUSTERED ([ContactID] ASC),
    CONSTRAINT [FK_tblContact_tblNote] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[tblNote] ([NoteID])
);

