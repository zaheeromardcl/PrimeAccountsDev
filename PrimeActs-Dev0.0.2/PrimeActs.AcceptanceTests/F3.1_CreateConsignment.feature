Feature: F3.1_CreateConsignment
In order to add consignments to the system, we need a create consignment view.

@myConsignments
Scenario Outline: 1a User completes the Consignment form part 1 with valid entries.
	Given that the user is an authenticated user with the appropriate role
	And the system has generated a <ConsignmentReference>
	And the user has selected a valid <Department>
	And the user has uploaded a valid <file>
	And the user has filled in the <Consignment Description>
	And the user has selected the <Supplier Code> field
	And the system has generated a <Supplier Name> 
	And the user has completed the <Supplier Ref> field
	And the user has completed the <Despatch Code> field
	And the user has completed the <Despatch Date> field
	And the user has completed the <Consignment Type> field
	And the user has completed the <CountryOfOrigin> field
	And the user has completed the <Shipment> field
	And the user has completed the <Vehicle Name> field
	And the user has completed the <Vehicle Detail> field
	And the user has completed the <Port> field
	And the user has completed the <Contract Date> field
	And the user has completed the <Recieved Date> field
	When the user presses the Create Consignment button 
	Then all the form values should be saved to the database
	And the details panel for the Consignmeent Created should be displayed.

	Examples:
	| Consignment Reference | Department  | File      | Consignment Description | Supplier Code | Supplier Name   | Supplier Reference | Despatch Code | Despatch Date | Consignment Type | CountryOfOrigin | Shipment        | Commission | Handling | Vehicle/Shipment | Vehicle Detail | Port | ContractDate | RecievedDate |
	| 123456                | Department1 | input.txt | This is a consignment   | SUP01         | Supplier 1 Name | SupplierRef        | DCOD1         | 01/01/2015    | OP               | UK              | Shipmentdetails |0.00       | 0.00     | Vehicle01        | this is a vehicle description | PORTCODE1 | 01/02/2015   | 01/03/2015   |




