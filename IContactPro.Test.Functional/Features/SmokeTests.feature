
Feature: SmokeTests
	As an iContactPro user
	I want to make sure that most of the application works after a build
	
@smokeTests
Scenario: Login to iContactPro works as expected
	Given I log in to iContactPro with the smoke test account
	Then I should be on the Home page
	
