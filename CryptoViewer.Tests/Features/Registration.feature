Feature: User Registration

  Scenario: Successful registration
    Given I have a valid registration request
    When I send a POST request to "api/Auth/register" with the following details
      | Username   | Password  | Email          | Address      | FirstName | LastName |
      | testuser   | Test@1234 | test@test.com  | 123 Test St  | Test      | User     |
    Then the registration response status should be 200
