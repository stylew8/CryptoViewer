Feature: Registration success check

  Scenario: Successful User Registration
    Given a new user with username "testuser" and email "testuser@example.com"
    When the user registers with password "Test@1234"
    Then the user should be successfully registered
    And a session should be created for the user

  Scenario: Duplicate Email Registration
    Given a new user with username "anotheruser" and email "testuser@example.com"
    When the user registers with password "Another@1234"
    Then a duplicate email error should be thrown

  Scenario: Duplicate Username Registration
    Given a new user with username "testuser" and email "uniqueuser@example.com"
    When the user registers with password "Unique@1234"
    Then a duplicate username error should be thrown