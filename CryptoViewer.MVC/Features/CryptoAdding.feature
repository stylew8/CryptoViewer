Feature: Adding Crypto Feature
  Scenario: User selects a crypto currency to monitor
    Given User is on the available crypto page 
    When User selects crypto from the list of available currencies
    And User clicks on the add button
    Then Crypto should be added to users profile wallet
