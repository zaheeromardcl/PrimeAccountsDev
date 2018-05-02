CREATE TABLE [dbo].[tblIntrastat] (
    [IntrastatID]           UNIQUEIDENTIFIER NOT NULL,
    [IntrastatDate]         DATETIME         NOT NULL,
    [IntrastatDescription]  NVARCHAR (50)    NOT NULL,
    [IntrastatValue]        NUMERIC(18,4)            NOT NULL,
    [IntrastatCompanyID]    INT              NOT NULL,
    [IntrastatVATNumber]    NVARCHAR (50)    NOT NULL,
    [IntrastatBranchNumber] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]             NVARCHAR (25)    NULL,
    [UpdatedDate]           DATETIME         NULL,
    [CreatedBy]             NVARCHAR (25)    NULL,
    [CreatedDate]           DATETIME         NULL,
    [IsActive]              BIT              CONSTRAINT [DF__tblIntras__IsAct__6501FCD8] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblInterstat] PRIMARY KEY CLUSTERED ([IntrastatID] ASC)
);

