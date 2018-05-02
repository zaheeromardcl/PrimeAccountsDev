CREATE TABLE [dbo].[tblIntrastatItem] (
    [IntrastatItemID]                  UNIQUEIDENTIFIER NOT NULL,
    [IntrastatCommodity]               NVARCHAR (50)    NOT NULL,
    [IntrastatValue]                   NUMERIC(18,4)            NOT NULL,
    [IntrastatTerms]                   NVARCHAR (50)    NOT NULL,
    [IntrastatNature]                  INT              NOT NULL,
    [IntrastatNetMassAmount]           FLOAT (53)       NOT NULL,
    [IntrastatCountry]                 NVARCHAR (50)    NOT NULL,
    [IntrastatID]                      UNIQUEIDENTIFIER NOT NULL,
    [InrastatConsignmentOriginCountry] NVARCHAR (50)    NOT NULL,
    [UpdatedBy]                        NVARCHAR (25)    NULL,
    [UpdatedDate]                      DATETIME         NULL,
    [CreatedBy]                        NVARCHAR (25)    NULL,
    [CreatedDate]                      DATETIME         NULL,
    [IsActive]                         BIT              CONSTRAINT [DF__tblIntras__IsAct__65F62111] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblIntrastatItem_1] PRIMARY KEY CLUSTERED ([IntrastatItemID] ASC),
    CONSTRAINT [FK_tblIntrastatItem_tblIntrastat] FOREIGN KEY ([IntrastatID]) REFERENCES [dbo].[tblIntrastat] ([IntrastatID])
);

