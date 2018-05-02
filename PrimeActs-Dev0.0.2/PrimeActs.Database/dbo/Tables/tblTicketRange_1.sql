CREATE TABLE [dbo].[tblTicketRange] (
    [TicketRangeID]    UNIQUEIDENTIFIER NOT NULL,
    [TicketRangeStart] INT              NOT NULL,
    [TicketRangeEnd]   INT              NOT NULL,
    [UpdatedBy]        NVARCHAR (25)    NULL,
    [UpdatedDate]      DATETIME         NULL,
    [CreatedBy]        NVARCHAR (25)    NULL,
    [CreatedDate]      DATETIME         NULL,
    [IsActive]         BIT              CONSTRAINT [DF__tblTicket__IsAct__7AF13DF7] DEFAULT ((1)) NOT NULL,
    [DepartmentID]     UNIQUEIDENTIFIER NULL,
    [TicketPrefix]     VARCHAR (1)      NULL,
    CONSTRAINT [PK_tblTicketRange_1] PRIMARY KEY CLUSTERED ([TicketRangeID] ASC),
    CONSTRAINT [FK_tblTicketRange_tblDepartment] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[tblDepartment] ([DepartmentID])
);

