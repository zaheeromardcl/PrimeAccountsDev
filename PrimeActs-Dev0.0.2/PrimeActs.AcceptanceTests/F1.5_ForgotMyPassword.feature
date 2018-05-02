Feature: Forgot my password
If the user has forgotten their password 
they should be able to reset it using the application.

@mytag
Scenario: 1.5a On logging in the user has forgotten their password.
	Given I have forgotten my password
	When I click on the Forgot My Password link on the Login Form	
	Then the Forgot my Password Form should be displayed.

Scenario Outline: 1.5b  When the user starts the forget my password process.
Given that I have clicked on the 'Forgot my password' link on the 'Login Form' 
When I enter <username> in the Username field 
And I press the Reset my password button
Then an automatically generated email with a link to enable me to 'reset my password' should be sent to my email address.

Examples: 
| Username  |
| username1 |

Scenario: 1.5c when the user has recieved the 'Reset My password Email'
Given that the user has recieved the auto-generated email
When the user clicks on the link in the email
Then the result should be that the 'Reset My Password Form' screen should be displayed.
 
 Scenario Outline: 1.5d when the user enters a new valid password and confirms it.
Given that the user has clicked on the link in the email 
And the 'Reset My Password Form' screen is displayed
When the user enters the new <password> into the password fields 
And clicks on the 'Reset my Password' button 
Then the password should be updated in the database 
And the 'Password Reset' confirmation screen should be displayed.
Examples: 
| password   |
| Password1! |

Scenario Outline: 1.5e when the user enters a new Invalid password.
Given that the user has clicked on the link in the email 
And the 'Reset My Password Form' screen is displayed
When the user enters a new invalid  <passwprd> into the password fields 
And clicks on the 'Reset my Password' button 
Then the result should be that the validation error message 'Invalid password, please enter a valid one' should be displayed
And the Password and Confirm Password fields on the 'Reset my Password form' should be reset to blank.
Examples: 
| Password |
| password |         