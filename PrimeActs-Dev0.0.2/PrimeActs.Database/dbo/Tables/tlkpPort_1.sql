CREATE TABLE [dbo].[tlkpPort] (
    [PortID]      UNIQUEIDENTIFIER NOT NULL,
    [PortName]    NVARCHAR (100)   NOT NULL,
    [PortCode]    NVARCHAR (10)    NOT NULL,
    [CompanyID]   UNIQUEIDENTIFIER NOT NULL,
    [UpdatedBy]   NVARCHAR (25)    NULL,
    [UpdatedDate] DATETIME         NULL,
    [CreatedBy]   NVARCHAR (25)    NULL,
    [CreatedDate] DATETIME         NULL,
    [IsActive]    BIT              CONSTRAINT [DF_tlkpPort_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tlkpPort] PRIMARY KEY CLUSTERED ([PortID] ASC),
    CONSTRAINT [FK_tlkpPort_tblCompany] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[tblCompany] ([CompanyID])
);

