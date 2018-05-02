Feature: F4_1_Ticket
	In order to make a sale of produce,
	As a salesperson
	I want to enter the produce and see 
	a list of the produce with the total, Transaction tax and Porterage calculated.

@mytag
Scenario: 
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
