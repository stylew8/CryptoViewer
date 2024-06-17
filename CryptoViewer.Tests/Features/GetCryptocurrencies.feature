Feature: Get Cryptocurrencies
  Scenario: User retrieves a list of all available cryptocurrencies
    Given the API is running
    When the user sends a GET request to "http://localhost:5004/api/TrackerApi"
    Then the response status code should be 200
    And the response should contain a list of cryptocurrencies
