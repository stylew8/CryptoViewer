Feature: User Login Authentication
Scenario: User Login
  Given John Doe is on the login page
  When he enters the following credentials:
    | field    | value             |
    | Username | johndoe           |
    | Password | strongpassword123 |
  And he clicks "Login" button
  Then he should see a message saying "Login successful"
  And he should be redirected to the dashboard