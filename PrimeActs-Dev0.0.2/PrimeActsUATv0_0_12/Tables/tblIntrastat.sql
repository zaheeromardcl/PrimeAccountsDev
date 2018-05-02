CREATE TABLE [dbo].[tblIntrastat] (
    [IntrastatID]           UNIQUEIDENTIFIER NOT NULL,
    [IntrastatDate]         DATETIME         NOT NULL,
    [IntrastatDescription]  NVARCHAR (50)    NOT NULL,
    [IntrastatValue]        NUMERIC (18, 4)  NOT NULL,
    [IntrastatCompanyID]    INT              NOT NULL,
    [IntrastatVATNumber]    NVARCHAR (50)    NOT NULL,
    [IntrastatBranchNumber] NVARCHAR (50)    NOT NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsActive]              BIT              DEFAULT ((1)) NOT NULL,
    [CreatedByUserID]       UNIQUEIDENTIFIER NOT NULL,
    [UpdatedByUserID]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tblInterstat] PRIMARY KEY CLUSTERED ([IntrastatID] ASC),
    CONSTRAINT [FK_tblIntrastatCreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    CONSTRAINT [FK_tblIntrastatUpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

