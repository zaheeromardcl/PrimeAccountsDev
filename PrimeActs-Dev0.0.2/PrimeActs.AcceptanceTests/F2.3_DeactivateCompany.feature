Feature: F2.3 Deactivate Company 
	In order to remove a company from being displayed
	I want to be able to deactivate the company.

@mytag
Scenario: Deactivate company
	Given I am an authorised user and a member of the Admin role
	And I have navigated to the 'Deactivate Company' Screen
	And A list of active companies are displayed	
	When I click on the checkbox next to the selected company 'TestCompany'
	Then the result should be the selected company is marked as deactivated in the database
	And a Deactivation confirmation screen should be displayed\.


