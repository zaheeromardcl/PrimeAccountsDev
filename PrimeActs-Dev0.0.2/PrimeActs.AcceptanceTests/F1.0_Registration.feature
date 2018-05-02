Feature: Registration
	In order to use the applicationa as an authenticated user
	I want to register a new account

@mytag
Scenario: 1.0a Register button is clicked on Login Page
	Given I browse to Application Home Page 
	When I click on the Register Button
	Then the blank Register Page should be displayed.


Scenario Outline:  1.0b User enters details on Register page
Given I complete the Register Page with valid data
	When I enter <username> in the Username field
		And I enter <password> in the password field
		And I enter the <confirmpassword> in the confirm password field
		And I enter the <email> in the email field
		And I enter the <Firstname> in the Firstname field
		And I enter the <Lastname> in the Lastname field
		And I enter the <Nickname> in the Nickname field
		And the Register button is pressed
Then the Home page should be displayed with a 'Welcome <firstmame>' message should be displayed and an email should be sent to all users in the 'Administrators' Role confirming the user has registered
But an error message 'A user already exists with that <username>' should be displayed if the username is already in use.
But an error message 'Password is not valid. Please enter a strong password' should be displayed if the <password> is not a strong password
But an error message 'Passwords do not match. Please re-enter your password' should be displayed if the <confirmpassword> does not match the <password>
But an error message 'Email is not valid.  Please re-enter your email address' should be displayed if the <email> is not of the correct format
But an error message 'Please complete all the fields' if any of the fields <username>,<password>, <confirmpassword>, <email>,<Firstname>,<Lastname>,<Nickname> are left blank


Examples: 
| username | password | confirmpassword | email             | Firstname  | Lastname   | Nickname   |
| User1    | Plummy78 | Plummy78        | user1@company.com | user1fname | user1lname | user1nname |




	