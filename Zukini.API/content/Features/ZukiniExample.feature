@example
Feature: API Example Features
	In order to provide an example of Zukini.API
	As a user
	I want to try it out against a prototype API

@get_example
Scenario: Get a resource from an API and validate the return data
	Given I perform a GET for post "1"
	Then the Get response should contain the following data
	| userId | id | title                                                                      | body                                                                                                                                                              |
	| 1      | 1  | sunt aut facere repellat provident occaecati excepturi optio reprehenderit | quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto |
