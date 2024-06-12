Feature: Registration success check
Scenario: User registration successfull 
When User enters information 
And User click button Register
Then User should be registered to system
And  Welcome message is displayed