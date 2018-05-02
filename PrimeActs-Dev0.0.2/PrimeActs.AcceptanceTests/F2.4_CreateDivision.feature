Feature: Division Setup 
As a superadmin user I need to be able to add a division.

@myDivision
Scenario: Create a division 
Given user is registered as SuperAdmin User 
When the user goes to the create division screen
Then the result should be that the Create Division Form screen is displayed 
And a default 'Select a company' is selected in the Company dropdown.

@myDivisionName
Scenario: Create division shoud return error if division name is missing
Given user is registered as SuperAdmin User 
And the user hasn't entered division name
When the user goes to the create division with missing division name
Then User should be shown the error message ''Division Name' should not be empty.'

@myDivisionCompany
Scenario: Create division shoud return error if company is not selected
Given user is registered as SuperAdmin User 
And the user hasn't selected company
When the user goes to the create division with missing company
Then User should be shown the error message ''Company' should not be empty.'

Scenario: On successfull creation of division, user redirected to index 
Given user is registered as SuperAdmin User 
And the user enteres valid division details.
When the user goes to the create division
Then User should be shown redirected to division index

Scenario: Create division not accessible other then super admin
Given user is registered as Admin User 
When the user tries to access create division page
Then User should be shown redirected to not authorized page.
