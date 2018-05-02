Feature: F1
	In order to avoid authenticate the user
	As a user with an associated role
	I want to be told the username and password 

@mytag
Scenario Outline: Login with valid details
	Given I am on the Login page
	And I have completed the form with valid <username> <password>
	When I press the Login button
	Then I will be logged in as <username>
	And my account will be assigned the role of <role>

	Examples: 
	|username |password	|role	|
	| ArunaC | roses2212C!|User      |
	| ArunaB | Roses2212B!|Admin      |
	| ArunaA | Roses2212A!|SuperAdmin      |

	Scenario: Login with invalid details
	Given I am on the Login page
	And I have completed the form with invalid <username> <password>
	When I press the Login button
	Then I will get an error message.
	

	Examples: 
	|username |password	|
	| Aruna.invalidC | roses2212C          |
	| Aruna.invalidB | Roses2212B         |
	| Aruna.invalid | Roses2212A          |
