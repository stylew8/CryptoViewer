Feature: Update Cryptocurrency
Scenario: Admin updates an existing cryptocurrency
    And a cryptocurrency with ID 1 exists
    When the admin sends a PUT request to "api/TrackerApi/1" with the following data:
      | Name          | Bitcoin Updated |
      | LogoPath      | Images/CryptoLogos/btc_updated.png |
      | TrackerAction | Track Updated   |
      | BorderColor   | #FF0000         |
    Then the response code should be 200
    And the response should contain the updated cryptocurrency