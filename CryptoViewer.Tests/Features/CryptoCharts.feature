Feature: Crypto Monitoring Feature
  Scenario: User selects a crypto currency to monitor
    Given User is on the crypto monitoring dashboard
    When User selects crypto from the list of available currencies
    And User clicks on the button
    Then Crypto should be listed in the monitored currencies
    And the current price and trend for crypto is displayed