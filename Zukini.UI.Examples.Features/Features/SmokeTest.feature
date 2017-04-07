@ui
Feature: SmokeTest
	In order to provide an example of Zukini
	As a user
	I want to try it out against Google

@google_search
Scenario: Perform a google search for SpecFlow returns specflow.org site
	Given I navigate to Google
	And I enter a search value of "SpecFlow"
	When I press Google Search
	Then I should see "specflow.org" in the results

@table_example
Scenario: I want to show how to use row and cell helpers
	Given I navigate to W3Schools table reference page
	Then I should see that the table tag is supported in "Chrome"
	And I should see that the table tag is supported in "IE"
	And I should see that the table tag is supported in "FireFox"
	And I should see that the table tag is supported in "Safari"

@property_bucket
Scenario: I want to demonstrate how to use the property bucket
	Given I navigate to W3Schools table reference page
	And I remember the sub-header text
	Then the sub-header text should have been "THE WORLD'S LARGEST WEB DEVELOPER SITE"

@ignore
Scenario: Performing a search for SpecFlow and expecting random text should fail and give me a screenshot
	Given I navigate to Google
	And I enter a search value of "SpecFlow"
	When I press Google Search
	Then I should see "ZZZXXXYYYGGGJJJPPPP" in the results	

@table_example
Scenario Outline: I want to demonstrate how to use SpecFlow data tables
	Given I navigate to W3Schools table reference page
	Then I should see that the table tag is supported for the following
		| Browser   |
		| <Browser> |
	Examples:
		| Browser |
		| Chrome  |
		| IE      |
		| FireFox |
		| Safari  |

@browser_session_extension
Scenario: I want to demonstrate how to wait for a button to appear
	Given I create a delayed button
	Then the delayed button should eventually exist
		And the delayed button has a size and location

@browser_session_extension
Scenario: I want to demonstrate how to try until a button appears
	Given I create a button that creates a delayed button
	When I use TryUntil on the button
	Then the second button should exist

@browser_session_extension
Scenario: WaitForNavigation does timeout
	Given I try to navigate to a url that changes the browser location
	Then navigation does timeout

@browser_session_extension
Scenario: WaitForNavigation does not timeout
	Given I try to navigate to Google
	Then navigation does not timeout