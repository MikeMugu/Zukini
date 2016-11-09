@api
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

@post_example
Scenario: Post to an API and validate that the post data returned is correct
	Given I post the following data to the API
		| title                  | body                                                | userId |
		| Checkout this new POST | Here is a test title to demonstrate testing a post. | 123    |
	Then the post data should return "Checkout this new POST" in the "title" field
	And the post data should return "Here is a test title to demonstrate testing a post." in the "body" field
	And the post data should return "123" in the "userId" field

@patch_example
Scenario: Patch to an API and validate that the patch data returned is correct
	Given I "Patch" a record with id "1"
		| title               | body                                                 | userId |
		| Checkout this PATCH | Here is a test title to demonstrate testing a patch. | 123    |
	Then the "Patch" data should return "Checkout this PATCH" in the "title" field
	Then the "Patch" data should return "Here is a test title to demonstrate testing a patch." in the "body" field
	Then the "Patch" data should return "123" in the "userId" field


@put_example
Scenario: Put to an API and validate that the put data returned is correct
	Given I "Put" a record with id "2"
		| title           | body                                               | userId |
		| Checkout my PUT | Here is a test title to demonstrate testing a put. | 456    |
	Then the "Put" data should return "Checkout my PUT" in the "title" field
	Then the "Put" data should return "Here is a test title to demonstrate testing a put." in the "body" field
	Then the "Put" data should return "456" in the "userId" field


@delete_example
Scenario: Delete a post from our fake API
	Given I perform a DELETE for postId "1"
	Then I should get a status code of "OK"
	