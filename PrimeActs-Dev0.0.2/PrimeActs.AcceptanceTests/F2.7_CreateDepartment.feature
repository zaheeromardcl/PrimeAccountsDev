Feature: F2.7_CreateDepartment
As a superadmin user I need to be able to add a department.

@myDepartment
Scenario: Create a department 
Given I am in the role 'SuperAdminUser' 
And I have clicked on the menu option 'Company Settings'
When I press the 'Create Department' button 
Then the result should be that the 'Create Department Form" should be displayed.

Scenario: Create a department - select a parent division.
Given I am in the role 'SuperAdminUser' 
And I am on the 'Create department' form
When the form loads 
Then a list of existing divisions should be displayed in a drop down list.

Scenario:  Create a department - validation with valid details.
Given I am in the role 'SuperAdminUser' 
And I am on the 'Create department' form
When all fields are entered with valid values
Then the 'Green Tick icon' should be displayed.

Scenario: Create a department - validation with invalid details.
Given I am in the role 'SuperAdminUser'
And I am on the 'Create department' form
When any of the fields are entered with invalid values or are incomplete
Then the 'Red Cross icon' should be displayed and the appropriate field highlighted in red until the user enters valid data.







