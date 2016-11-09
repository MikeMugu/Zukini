@example
Feature: Zukini Example
	In order to provide an example of Zukini
	As a user
	I want to try it out against Google

@google_search
Scenario: Perform a google search for Zukini returns Zukini in the search results
	Given I navigate to Google
	And I enter a search value of "Zukini"
	When I press Google Search
	Then I should see "https://github.com/MikeMugu/Zukini" in the results
