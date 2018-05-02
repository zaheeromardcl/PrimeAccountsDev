Feature: F2.1_CreateCompany Setup 
As a superadmin user I need to be able to add a company.

@mytag
Scenario: Create a company 
Given I am in the role 'SuperAdminUser' 
And I have clicked on the menu option 'Company Settings'
When I press the 'Create Company' button 
Then the result should be that the 'Create Company Form" should be displayed.

Scenario: Create a company - select a parent company.
Given I am in the role 'SuperAdminUser' 
And I am on the 'Create Company' form
When I check the 'Has Parent Company' box
Then a list of existing companies should be displayed in a drop down list.

Scenario:  Create a company - validation with valid details.
Given I am in the role 'SuperAdminUser' 
And I am on the 'Create Company' form
When all fields are entered with valid values
Then the 'Green Tick icon' should be displayed.

Scenario: Create a company - validation with invalid details.
Given I am in the role 'SuperAdminUser'
And I am on the 'Create Company' form
When any of the fields are entered with invalid values or are incomplete
Then the 'Red Cross icon' should be displayed and the appropriate field highlighted in red until the user enters valid data.







