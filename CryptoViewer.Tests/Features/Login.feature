Feature: Authentication
  Scenario Outline: Successful login
    Given I have valid credentials
    When I POST to /api/auth/login with my credentials
    Then I should receive a successful response with my user ID

  Scenario Outline: Failed login
    Given I have invalid credentials
    When I POST to /api/auth/login with my credentials
    Then I should receive an invalid login attempt response

  Scenario Outline: Successful registration
    Given I have valid registration details
    When I POST to /api/auth/register with my details
    Then I should receive a successful response with my new user ID

  Scenario Outline: Failed registration
    Given I have invalid registration details
    When I POST to /api/auth/register with my details
    Then I should receive a registration failure response

Examples:
  | Username    | Password    | Email                | Address      | FirstName | LastName  | ExpectedResult |
  | validUser   | validPass   | valid@mail.com       | 123 Street   | John      | Doe       | success        |
  | invalidUser | invalidPass | invalid@mail.com     | 456 Avenue   | Jane      | Smith     | fail           |
  | newUser     | newPass     | newuser@mail.com     | 789 Road     | Alice     | Johnson   | success        |
  |             |             | invalid@address.com  |              | Bob       |           | fail           |
