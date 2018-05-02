Feature: F2.5_AmendDivision
	In order to amend division details, as a member of the admin role, I should be able 
	to amend division details.


@mytag
Scenario: Select division to amend 
	Given I am an authorised user and a member of the Admin role
	When I select a division from the list of divisons
	Then the result should be an 'Amend Division form' screen is displayed for that company.

Scenario: Amend division form
Given that I am an authorised user and a member of the Admin role
And I have clicked on a division name from the list of divisions on the 'Display Divisions' screen
And I have edited the division details on the 'Amend Division Form' screen
When I press the 'Submit Division Details' button
Then the result should be that the details are updated in the database 
And  the 'Amended Division Details' should be displayed on the 'Confirm Amended Division details' screen.

Scenario: Validation for amend division form
Given that I am an authorised user and a member of the Admin role.
And I have clicked on a division name from the list of division on the 'Display divisions' screen
And I have edited the division details with invalid entries on the 'Amend division Form' screen
When I press on the  'Submit division Details' button
Then the result should be that a validation error message 'The entry is invalid, please enter another'.


