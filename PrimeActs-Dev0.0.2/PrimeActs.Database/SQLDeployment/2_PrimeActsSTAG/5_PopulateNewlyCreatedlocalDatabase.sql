--use [PrimeActsSTAGING]
--insert into tblAddress select * from PrimeActsDEV..tblAddress

--Insert into tblCompany select * from PrimeActsDEV..tblCompany
--Insert into tblDivision select * from PrimeActsDEV..tblDivision

--insert into AspNetUsers select * from PrimeActsDEV..AspNetUsers
--insert into AspNetRoles select * from PrimeActsDEV..AspNetRoles
--insert into AspNetUserRoles select * from PrimeActsDEV..AspNetUserRoles
--insert into AspNetPermissions select * from PrimeActsDEV..AspNetPermissions
--insert into AspNetRolePermission select * from PrimeActsDEV..AspNetRolePermission
--insert into tblSetuplocal select *  from PrimeActsDEV..tblSetupLocal

--set identity_insert tblSetupGlobal on
--insert into tblSetupGlobal([SetupID]
--      ,[SetupName]
--      ,[SetupValueType]
--      ,[SetupValueInt]
--      ,[SetupValueNumeric]
--      ,[SetupValueBit]
--      ,[SetupValueNvarchar]
--      ,[SetupValueUniqueIdentifier]
--      ,[CompanyID]
--      ,[DivisionID]
--      ,[DepartmentID]
--      ,[UpdatedBy]
--      ,[UpdatedDate]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[IsActive]) select  * from PrimeActsDEV..tblSetupGlobal
--set identity_insert tblSetupGlobal off


--set identity_insert tblbatchnumberlog on  
--insert into tblBatchNumberLog(
--[BatchNumberLogID]
--      ,[CompanyID]
--      ,[DivisionID]
--      ,[ServerPrefix]
--      ,[BatchNumber]
--      ,[TransactionDateTime]
--      ,[UpdatedBy]
--      ,[UpdatedDate]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[IsActive]
--	  )
--	  select * from PrimeActsDEV..tblBatchNumberLog
--set identity_insert tblbatchnumberlog off

--Insert into tblDepartment select * from PrimeActsDEV..tblDepartment
--Insert into tlkpCountry select * from PrimeActsDEV..tlkpCountry
--INSERT into tlkpPackWtUnit SELECT * FROM PrimeActsDEV..tlkpPackWtUnit
--INSERT into tlkpPorterage SELECT * FROM PrimeActsDEV..tlkpPorterage
--INSERT into tlkpPort SELECT * FROM PrimeActsDEV..tlkpPort
--INSERT into tlkpDespatchLocation SELECT * FROM PrimeActsDEV..tlkpDespatchLocation
--INSERT into tlkpPurchaseType SELECT * FROM PrimeActsDEV..tlkpPurchaseType
--INSERT into tlkpVATCode SELECT * FROM PrimeActsDEV..tlkpVATCode
--INSERT into tlkpVATRate SELECT * FROM PrimeActsDEV..tlkpVATRate
--INSERT into tlkpCreditRating SELECT * FROM PrimeActsDEV..tlkpCreditRating
--INSERT INTO tlkpTransferType SELECT * FROM PrimeActsDEV..tlkpTransferType
--insert into tlkpCurrency select * from PrimeActsDEV..tlkpCurrency
--insert into tlkpLedgerEntryType select * from PrimeActsDEV..tlkpLedgerEntryType
--insert into tlkpStockLocation select * from PrimeActsDEV..tlkpStockLocation
--INSERT into tblNote SELECT * FROM PrimeActsDEV..tblNote
--INSERT into tlkpCustomerType SELECT * FROM PrimeActsDEV..tlkpCustomerType
--INSERT into tblContact SELECT * FROM PrimeActsDEV..tblContact
--INSERT into tblCustomer SELECT * FROM [PrimeActsDeployment0_0_1b]..tblCustomer
--INSERT into tblCustomerLocation SELECT * FROM PrimeActsDEV..tblCustomerLocation
--INSERT into tblCustomerDepartment SELECT * FROM PrimeActsDEV..tblCustomerDepartment
--INSERT into tblCustomerDepartmentLocation SELECT * FROM PrimeActsDEV..tblCustomerDepartmentLocation
--INSERT into tblCustomerContact SELECT * FROM PrimeActsDEV..tblCustomerContact
--insert into tblBankAccount select * from PrimeActsDEV..tblBankAccount
--INSERT into tblCustomerBankAccount SELECT * FROM PrimeActsDEV..tblCustomerBankAccount
--INSERT into tblSupplier SELECT * FROM PrimeActsDEV..tblSupplier
--INSERT into tblSupplierLocation SELECT * FROM PrimeActsDEV..tblSupplierLocation
--INSERT into tblSupplierDepartment SELECT * FROM PrimeActsDEV..tblSupplierDepartment
--INSERT into tblSupplierContact SELECT * FROM PrimeActsDEV..tblSupplierContact
--INSERT into tblSupplierBankAccount SELECT * FROM PrimeActsDEV..tblSupplierBankAccount
--INSERT into tblSupplierDepartmentLocation SELECT * FROM PrimeActsDEV..tblSupplierDepartmentLocation
--INSERT into tblProduceGroup SELECT * FROM PrimeActsDEV..tblProduceGroup
--INSERT into tblMasterGroup SELECT * FROM PrimeActsDEV..tblMasterGroup
--INSERT into tblProduce SELECT * FROM PrimeActsDEV..tblProduce
--INSERT into tblConsignment SELECT * FROM PrimeActsDEV..tblConsignment
--insert into tblFile select * from PrimeActsDEV..tblFile--already inserted.
--insert into tblConsignmentFile SELECT * FROM PrimeActsDEV..tblConsignmentFile
--INSERT into tblConsignmentItem SELECT * FROM PrimeActsDEV..tblConsignmentItem
--INSERT into tblTicket SELECT * FROM PrimeActsDEV..tblTicket
--INSERT into tblTicketItem SELECT * FROM PrimeActsDEV..tblTicketItem
--insert into tblNominalAccount select * from PrimeActsDEV..tblNominalAccount
--insert into tblNominalLedgerEntry select * from PrimeActsDEV..tblNominalLedgerEntry
--insert into tgenSalesInvoiceNumber select * from PrimeActsDEV..tgenSalesInvoiceNumber
--insert into tblSalesLedgerEntry select * from PrimeActsDEV..tblSalesLedgerEntry
--insert into tblSalesInvoice select * from PrimeActsDEV..tblSalesInvoice
--insert into tblSalesInvoiceitem select * from PrimeActsDEV..tblSalesInvoiceItem
--insert into tblSalesAllocation select * from PrimeActsDEV..tblSalesAllocation