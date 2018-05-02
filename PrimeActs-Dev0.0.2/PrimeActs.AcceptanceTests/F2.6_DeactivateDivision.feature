Feature: F2.6 Deactivate Division 
	In order to prevent a division from being displayed
	I want to be able to deactivate the division.

@mytag
Scenario: Deactivate division
	Given I am an authorised user and a member of the Admin role
	And I have navigated to the 'Deactivate Division' Screen
	And A list of active divisions are displayed	
	When I click on the checkbox next to the selected company 'TestDivision'
	Then the result should be the selected division is marked as deactivated in the database
	And a Deactivation confirmation screen should be displayed\.


