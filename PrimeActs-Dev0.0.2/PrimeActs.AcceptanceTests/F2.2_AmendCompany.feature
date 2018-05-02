Feature: F2.2_AmendCompany
	In order to amend company title, as a member of the admin role, I should be able 
	to amend company title.


@mytag
Scenario: Select user to amend 
	Given I am an authorised user and a member of the Admin role
	When I select a user from the list of users
	Then the result should be an 'Amend User form' screen displayed for that user.

Scenario: Amend user form
Given that I am an authorised user and a member of the Admin role
And I have clicked on a username from the list of users on the 'Display Users' screen
And I have edited the user details on the 'Amend User Form' screen
When I click on 'Submit User Details' button
Then the result should be that the details are updated in the database 
And  the 'Amended Details' should be displayed on the 'Confirm Amended details' screen.

Scenario: Validation for user form
Given that I am an authorised user and a member of the Admin role.
And I have clicked on a Username from the list of users on the 'Display Users' screen
And I have edited the User details with invalid entries on the 'Amend User Form' screen
When I click on 'Submit User Details' button
Then the result should be that a validation error message 'The entry is invalid, please enter another'.


