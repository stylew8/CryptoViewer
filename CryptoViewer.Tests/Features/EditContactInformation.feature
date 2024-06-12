Feature: Update User Contact Information
  Scenario: User successfully updates contact information
    Given User is logged in and is on the "Profile" page
    When User clicks on the "Edit Contact Information" button
    And User updates the "Email" field to "newemail@example.com"
    And User updates the "Phone Number" field to "123-456-7890"
    And User updates the "Username" field to "Username1"
    And User clicks on the "Save" button
    Then User's contact information should be updated
    And a message "Your contact information has been successfully updated" should be displayed
    And the updated contact information should be displayed