Feature: API Example Features
	In order to provide an example of Zukini.API
	As a user
	I want to try it out against a prototype API

@api_example
Scenario: Make a post to an API and check the response returned
	Given I make a fake API call with the data
		| email         | name       | address     | city      | state | zip   |
		| test@test.com | Joe Tester | 123 Main St | Somewhere | CA    | 90210 |
	Then I should see that the "email" field returned a value of "test@test.com"
	And I should see that the "name" field returned a value of "Joe Tester"
	And I should see that the "address" field returned a value of "123 Main St"
	And I should see that the "city" field returned a value of "Somewhere"
	And I should see that the "state" field returned a value of "CA"
	And I should see that the "zip" field returned a value of "90210"
