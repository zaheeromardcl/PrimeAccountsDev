Feature: AuthenticateUser
In order to ensure only authenticated users are able to access the application the user must be authenticated prior to accessing any application pages.


@mytag
Scenario Outline: Authenticate 
	Given I have entered a valid <username> and <password> 
	When I  press the Login button
	Then the result should be that the Home Page is displayed and the record of the login should be saved
But an error message 'Invalid details. Please re-enter your details' should be displayed if invalid details are entered
But an error message 'This user does not exist, would you like to Register?' should be displayed if the username does not exist in the database
But an error message 'The password does not match the one we have saved.  Please re-enter your password' if the password is not correct

  Examples: 
| username | password | 
| User1    | Plummy78 | 