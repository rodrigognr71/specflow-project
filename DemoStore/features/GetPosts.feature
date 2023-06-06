Feature: Get posts

As a user, I wanto to see a product by API testing

//@positive @smoke @regression @integration @JIRA-7777 
//Scenario: Get by id
	Given I have an id with value 1
	When I send a get request
	Then I expected a valid code response

@positive @regression @finalProject
Scenario: Get a product by id
	Given I have product id with value 17
	When I send a GET request to Gatling
	Then I validate that the response status is "200"
	And I validate that the response body contais the following values
	  | id          | 17                                          |
	  | name        | Casual Black-Blue                           |
	  | description | <p>Some casual black &amp; blue glasses</p> |
	  | price       | 24.99                                       |
	  | categoryId  | 5                                           |

@positive @regression @finalProject
Scenario: Get a list of all products filter by categoty
	Given I have category id with value 5
	When I send a GET by category ID request to Gatling
	Then I validate that the response status is "200"
