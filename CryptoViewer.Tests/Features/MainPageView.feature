Feature: Guest Access to Main Page
  Scenario: Guest successfully views the main page
    Given I am a guest
    When I navigate to the main page
    Then I should see the main page content
    And the main page should display general information about the site
    And the main page should include a "Login" button
    And the main page should include a "Register" button