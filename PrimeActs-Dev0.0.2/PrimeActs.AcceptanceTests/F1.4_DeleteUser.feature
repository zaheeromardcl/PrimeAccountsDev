Feature: F1.4_DeleteUser
In order to avoid lots of unused Users, the Admin role should be able to deactivate users.

@mytag

Scenario: 1.4a Navigate to Manage Users screen
Given I am an authorised user and a member of the Admin role
	When I click on the Manage Users link on the Settings menu
	Then the 'Manage User' screen is displayed.


Scenario: 1.4b User clicks on Delete User Icon on the Manage User Screen
	Given I have an authorised user and a member of the 'Admin' Role 
	When I click on the Delete icon on the Manage User screen for one user
	Then a confirmation prompt 'Are you sure you want to delete this user?'

Scenario: 1.4c User answers yes to the confirmation of deletion prompt  
Given I have answered yes to the confirmation prompt
When I click on the 'Delete User' button
	Then a deactivated flag should be marked to true in the database for that user
	And the user is prevented from authenticate from that point forward
	And the deletion confirmed screen should be displayed
	And the user is redirected to the Manager Users screen

Scenario: 1.4d User presses on Delete Users(multiple users) button 
Given I am an authorised user and a member of the Admin role
And I have browsed to the Manage Users screen
When I click on 'Delete Users' button 
Then a checkbox for each user should be displayed on the screen
And a 'Select All' option should be displayed at the top of the page

Scenario: 1.4e User selects multiple users to delete.
Given I am an authorised user and a member of the Admin role
And I have browsed to the Manager Users screen
When I click on 'Delete Users' button 
And I select multiple users from the list
Then a confirmation prompt 'Are you sure you want to delete these users?' should be displayed

Scenario: 1.4f User answers yes to the confirmation of deletion of multiple users prompt
Given I have selected multiple users from the Manage Users List
When I answer yes to the confirmation prompt of deletion of multiple users
Then the selected users are prevented from authenticating from that point forward
And the deletion confirmed screen should be displayed with a list of the deleted users
And the user is redirected to the Manager Users screen





