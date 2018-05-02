Feature: F1.3_Amend User Details
	In order to amend users, as a member of the admin role, I should be able 
	to amend users details.

@mytag
Scenario: 1.3a Navigate to Manage Users screen
Given I am an authorised user and a member of the Admin role
	When I click on the Manage Users link on the Settings menu
	Then the 'Manage User' screen is displayed.


Scenario: 1.3b Select a user to amend
Given I am an authorised user and a member of the Admin role
And I have clicked on the Edit icon for that user from the list
Then the result should be an 'Amend User form' screen displayed for that user.

Scenario: 1.3c Completes Amend User Form with valid entries
Given I am an authorised user and a member of the Admin role 
	And I enter valid details on the Amend User form
When I move focus away from any of the fields 
Then the fields should be highlighted green 
And the Amend User button should be activated

Scenario: 1.3d Completes Amend User Form with invalid entries
Given I am an authorised user and a member of the Admin role 
	And I enter invalid details on the Amend User form
When I move focus away from any of the fields 
Then the fields should be highlighted red
And the Amend User button should be deactivated

Scenario: 1.3e Completes Amend User Form
Given that I am an authorised user and a member of the Admin role
And I have clicked on a Username from the list of users on the 'Amend Users' screen
And I am on the Amend User form
When I complete all the fields 
And I click on 'Amend User' button
Then the result should be that the changes, previous values should be saved to the database with a record of the username.




