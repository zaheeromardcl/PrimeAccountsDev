Feature: F2
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Deactivate division
	Given I am an authorised user and a member of the Admin role
	And I have navigated to the 'Deactivate Department' Screen
	And A list of active departments are displayed	
	When I click on the checkbox next to the selected department 'TestDepartment'
	Then the result should be the selected department is marked as deactivated in the database
	And a Deactivation confirmation screen should be displayed\.


