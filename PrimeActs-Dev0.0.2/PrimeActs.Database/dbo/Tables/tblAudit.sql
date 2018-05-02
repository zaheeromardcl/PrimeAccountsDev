CREATE TABLE [dbo].tblAudit (
       AuditID uniqueidentifier PRIMARY KEY,
       JsonDataBefore nvarchar(max) Null, 
       JsonDataAfter nvarchar(max) Null, 
       ContentType nvarchar(50) Not Null,
       UserID uniqueidentifier Not Null,
       CompanyID uniqueidentifier Not Null,
       DivisionID uniqueidentifier, 
       DepartmentID uniqueidentifier,
       EditDate datetime Not Null,
       [ReferenceID] uniqueidentifier Null,
       [Reference] nvarchar(50) Null 
)



