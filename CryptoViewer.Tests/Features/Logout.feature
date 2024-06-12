Feature: User Logout Feature
  Scenario: User logout success check
    Given User is logged in and is on the dashboard page
    When User clicks on the "Logout" button
    Then User should be redirected to the login page
    And a message "You have been successfully logged out" should be displayed