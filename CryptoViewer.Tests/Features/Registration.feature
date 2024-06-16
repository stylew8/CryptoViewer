Feature: User Registration

  Scenario: Successful registration
    Given no user with username "newuser" exists
    And no user with email "newuser@example.com" exists
    When the admin attempts to register a user with the following details:
      | username  | password   | email               | address       | firstName | lastName |
      | newuser   | password123| newuser@example.com | 123 Main St   | New       | User     |
    Then the response status should be 200
    And the response should contain a user ID

  Scenario: Registration with existing username
    Given a user with username "existinguser" exists
    When the admin attempts to register a user with the following details:
      | username      | password   | email                   | address       | firstName | lastName |
      | existinguser  | password123| newemail@example.com    | 123 Main St   | New       | User     |
    Then the response status should be 400
    And the response should contain "Username already exist."

  Scenario: Registration with existing email
    Given a user with email "existinguser@example.com" exists
    When the admin attempts to register a user with the following details:
      | username  | password   | email                     | address       | firstName | lastName |
      | newuser   | password123| existinguser@example.com  | 123 Main St   | New       | User     |
    Then the response status should be 400
    And the response should contain "Email already exist."

  Scenario: Registration with invalid input
    When the admin attempts to register a user with the following details:
      | username  | password | email           | address       | firstName | lastName |
      | newuser   |          | invalidemail    |               | New       | User     |
    Then the response status should be 400
    And the response should contain "Registration failed."