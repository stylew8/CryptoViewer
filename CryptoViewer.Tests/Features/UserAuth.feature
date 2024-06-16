Feature: User Authentication
  Scenario Outline: Successful login
    Given I have valid login credentials
    When I send a POST request to /api/UsersAuth/login with the credentials
    Then I should receive a successful login response with a token

  Scenario Outline: Failed login
    Given I have invalid login credentials
    When I send a POST request to /api/UsersAuth/login with the credentials
    Then I should receive an error response indicating incorrect credentials

  Scenario Outline: Successful registration
    Given I have unique registration details
    When I send a POST request to /api/UsersAuth/register with the details
    Then I should receive a successful registration response

  Scenario Outline: Failed registration due to existing username
    Given I have registration details with an existing username
    When I send a POST request to /api/UsersAuth/register with the details
    Then I should receive an error response indicating the username already exists

Examples:
  | Username      | Password       | Email               | ExpectedResult |
  | validUser     | validPass123   | valid@mail.com      | success        |
  | invalidUser   | invalidPass123 | invalid@mail.com    | fail           |
  | newUser       | newPass123     | newuser@mail.com    | success        |
  | existingUser  | pass123        | existing@mail.com   | fail           |
