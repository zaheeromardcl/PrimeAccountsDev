Feature: F1.1
	In order to avoid authenticate the user
	As a user with an associated role
	I want to be told the username and password 

@mytag
Scenario: 1.1a User loads the login page
	Given I browse to the the Login page
	When I load the page
	Then the focus should be on the username field.

Scenario: 1.1b User logs in with valid credentials.
	Given I complete the Login form with valid credentials
	When I press on the Login button
	Then I will be logged in as valid user 
		And my account will be assigned to an role 
		#And a record of the login will be stored in the database.

	#Examples: 
	#|username |password	|role	|
	#| ArunaC | roses2212C!          |User      |
	#| ArunaB | Roses2212B!        |Admin      |
	#| ArunaA | Roses2212A!          |SuperAdmin      |

Scenario Outline:  1.1c User enters invalid credentials
Given I complete the Login form with <username> and <password>
When I move focus off either field
	Then the fields should be highlighted red 
	And I will be shown an error message 'Please enter valid credentials'
	And a record of the login will be stored in the database.

	Examples: 
	|username |password	|
	| Aruna.invalidC | roses2212C          |
	| Aruna.invalidB | Roses2212B         |
	| Aruna.invalid | Roses2212A          |

Scenario Outline:  1.1d User enters valid credentials that are not in the database.
Given I am on the Login page
	And I have completed the form with valid <username> and <password>
When I press the Login button 
Then I will be shown an error message 'User details not found.  Please enter valid details or Register'


Examples: 
	|username |password	|
	| Aruna.notpresentA| roses2212C          |
	| Aruna.notpresentB| Roses2212B         |
	| Aruna.notpresentC | Roses2212A          |

Scenario Outline: 1.1e User leaves either field blank
Given I am on the Login page
And I have left the <username> or <password> fields blank
When I move focus away from either field
Then I will be shown an error message 'Please enter a value for <username> or <password> field.'

Examples:
	|username |password	|
	| '' | password1!|
	| username3| ''|
	