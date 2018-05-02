CREATE TABLE [dbo].[tblTicketRange] (
    [TicketRangeID]    UNIQUEIDENTIFIER NOT NULL,
    [TicketRangeStart] INT              NOT NULL,
    [TicketRangeEnd]   INT              NOT NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedDate]      DATETIME         NULL,
    [IsActive]         BIT              DEFAULT ((1)) NOT NULL,
    [DepartmentID]     UNIQUEIDENTIFIER NULL,
    [TicketPrefix]     NVARCHAR (1)     NULL,
    [CreatedByUserID]  UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]  UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblTicketRange_1] PRIMARY KEY CLUSTERED ([TicketRangeID] ASC),
    CONSTRAINT [FK_tblTicketRange_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID]),
    CONSTRAINT [FK_tblTicketRangeCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblTicketRangeUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

