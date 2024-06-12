Feature: Guest Access to Login Page
  Scenario: Guest successfully views the Login page
    Given I am a guest
    When I navigate to the Login page
    Then I should see the Login page content
    And the Login page should display fields for "Username" and "Password"
    And the Login page should include a "Login" button