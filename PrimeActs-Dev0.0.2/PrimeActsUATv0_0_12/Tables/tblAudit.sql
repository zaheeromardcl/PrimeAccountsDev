CREATE TABLE [dbo].[tblAudit] (
    [AuditID]        UNIQUEIDENTIFIER NOT NULL,
    [JsonDataBefore] NVARCHAR (MAX)   NULL,
    [JsonDataAfter]  NVARCHAR (MAX)   NULL,
    [ContentType]    NVARCHAR (50)    NOT NULL,
    [UserID]         UNIQUEIDENTIFIER NOT NULL,
    [CompanyID]      UNIQUEIDENTIFIER NOT NULL,
    [DivisionID]     UNIQUEIDENTIFIER NULL,
    [DepartmentID]   UNIQUEIDENTIFIER NULL,
    [EditDate]       DATETIME         NOT NULL,
    [ReferenceID]    UNIQUEIDENTIFIER NULL,
    [Reference]      NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([AuditID] ASC)
);

