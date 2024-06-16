Feature: User Login

  Scenario: Successful login
    Given a user with username "testuser" and password "password123" exists
    When the user attempts to log in with username "testuser" and password "password123"
    Then the response status should be 200
    And the response should contain a user ID

  Scenario: Login with incorrect password
    Given a user with username "testuser" and password "password123" exists
    When the user attempts to log in with username "testuser" and password "wrongpassword"
    Then the response status should be 400


  Scenario: Login with non-existent username
    When the user attempts to log in with username "nonexistent" and password "password"
    Then the response status should be 400

