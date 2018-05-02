Feature: AuthorisingPurchaseInvoices
	In order to avoid queries on purchase invoices
	As an administrator
	I want to be able to approve purchase invoices for payment

@PurchaseInvoice
Scenario: Incorrect purchase invoice entered, moderation required.
	Given I entered a purchase invoice incorrectly
	And I need to edit the values in one of the items
	When I click on "Save Purchase Invoice"
	Then the result should be "that this requires approval" on the screen.


	Scenario: Moderation of the purchase invoice.
	Given I entered a purchase invoice incorrectly
	And I need to edit the values in one of the items
	When I click on "Save Purchase Invoice"
	Then the result should be "that this requires approval" on the screen.