Feature: User Login Feature
  Scenario: User Login Success Check
    When User enters valid username and password
    And User clicks on the login button
    Then User should be redirected to the home page

