Feature: View User Contact Information
  Scenario: User successfully views contact information
    Given User is logged in and is on the dashboard page
    When User navigates to the "Profile" page
    And User clicks on the "View Contact Information" button
    Then User's contact information should be displayed
    And the displayed contact information should include "Email", "Phone Number" and "Username"