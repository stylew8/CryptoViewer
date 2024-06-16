Feature: Cryptocurrency Management
  Scenario Outline: Get all cryptocurrencies
    When I send a GET request to /api/trackerapi
    Then I should receive a list of cryptocurrencies

  Scenario Outline: Get a specific cryptocurrency
    Given a cryptocurrency with ID <id> exists
    When I send a GET request to /api/trackerapi/<id>
    Then I should receive the details of the cryptocurrency with ID <id>
    And the response should include a self-link

  Scenario Outline: Add a new cryptocurrency
    Given I have a valid cryptocurrency model
    When I send a POST request to /api/trackerapi with the model
    Then I should receive a created response with the new cryptocurrency details
    And the response should include a self-link to the new cryptocurrency

  Scenario Outline: Fail to add an invalid cryptocurrency
    Given I have an invalid cryptocurrency model
    When I send a POST request to /api/trackerapi with the model
    Then I should receive a bad request response

Examples:
  | id | name       | logoPath | trackerAction | borderColor  | valid |
  | 1  | Bitcoin    | /path1   | track         | #FFD700      | true  |
  | 2  | Ethereum   | /path2   | track         | #3C3C3D      | true  |
  | 3  | Invalid    |          | invalidAction | invalidColor | false |
