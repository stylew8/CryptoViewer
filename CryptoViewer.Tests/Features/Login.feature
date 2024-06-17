Feature: User Login

  Scenario: Successful login
    Given a user with username "string" and password "string" exists
    When the user attempts to log in with username "string" and password "string"
    Then the response status should be 200


  Scenario: Login with incorrect password
    Given a user with username "string" and password "asdllasd" exists
    When the user attempts to log in with username "fgdjbifhgd" and password "ahsjibfibs"
    Then the response status should be 200


  Scenario: Login with non-existent username
    When the user attempts to log in with username "sadbbkahj" and password "string"
    Then the response status should be 200

