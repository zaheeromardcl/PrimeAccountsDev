Feature: 2.10_Create Produce
	In order to use the system with current stock
	there is a need to add produce to the stock using the application.

@myProduce
Scenario: Navigate to Manage Produce Screen
Given the user has logged in as a member with SuperAdmin role
When the produce link is clicked
Then the produce screen should be displayed.
	


Scenario:  Navigate to Create Produce Screen
Given the user has logged in as a member with SuperAdmin role
When the Create Produce link is clicked
Then the Create Produce form should be displayed.



Scenario: Successfully create the new produce
Given the user has logged in as a member with SuperAdmin role
And valid produce details are entered in the Create Produce form 
When the Create Produce button is pressed 
Then the user is redirected to details page with produce creation confirmation


Scenario: Invalid attempt to add produce as an admin
Given the user has logged in as a member with SuperAdmin role
And invalid produce details are entered in the Create Produce form 
When the Create Produce button is pressed
Then a validation error message Please enter valid details should be displayed.
