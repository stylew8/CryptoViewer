Feature: Guest Access to Register Page
  Scenario: Guest successfully views the register page
    Given I am a guest
    When I navigate to the register page
    Then I should see the register page content
    And the register page should display fields for "Username", "Email", "Phone number" and "Password"
    And the register page should include a "Register" button