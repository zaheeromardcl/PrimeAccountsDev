Feature: F2.8 Amend Department
	In order to amend department details, as a member of the admin role, I should be able 
	to amend department details.


@mytag
Scenario: Select department to amend 
	Given I am an authorised user and a member of the Admin role
	When I select a department from the list of departments
	Then the result should be an 'Amend Department form' screen is displayed for that department.

Scenario: Amend department form
Given that I am an authorised user and a member of the Admin role
And I have clicked on a department name from the list of departments on the 'Display Departments' screen
And I have edited the department details on the 'Amend Department Form' screen
When I press the 'Submit Department Details' button
Then the result should be that the details are updated in the database 
And  the 'Amended Department Details' should be displayed on the 'Confirm Amended Department details' screen.

Scenario: Validation for amend division form
Given that I am an authorised user and a member of the Admin role.
And I have clicked on a department name from the list of departments on the 'Display departments' screen
And I have edited the department details with invalid entries on the 'Amend department Form' screen
When I press on the  'Submit department Details' button
Then the result should be that a validation error message 'The entry is invalid, please enter another'.


