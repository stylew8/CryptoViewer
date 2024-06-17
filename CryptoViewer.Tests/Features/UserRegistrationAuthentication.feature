Feature: User Registration Authetication
Scenario: User Registration
  Given John Doe is on the registration page
  When he enters the following details:
    | field     | value |
    | Username  | johndoe |
    | Password  | strongpassword123 |
    | FirstName    | John |
    | LastName  | Belingham |
    | Email  | johnbelinham@gmail.com |
    | Address  | Taikos 2 |
  And he clicks the "Register" button
  Then he should see a success message saying "User registered successfully."
