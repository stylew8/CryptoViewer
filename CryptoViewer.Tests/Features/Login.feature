Feature: User Login Feature
  Scenario: User Login Success Check
    Given User is on the login page
    When User enters valid username and password
    And User clicks on the login button
    Then User should be redirected to the dashboard
    And a welcome message is displayed
