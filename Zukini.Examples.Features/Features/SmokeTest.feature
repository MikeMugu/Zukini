Feature: SmokeTest
	In order to provide an example of Zukini
	As a user
	I want to try it out against Google

@mytag
Scenario: Perform a google search for SpecFlow returns specflow.org site
	Given I navigate to Google
	And I enter a search value of "SpecFlow"
	When I press Google Search
	Then I should see "www.specflow.org" in the results
