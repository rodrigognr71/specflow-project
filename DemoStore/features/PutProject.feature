Feature: Put a prodcut

As a user, I wanto to update a product by API testing

@negative @regression @finalProject
Scenario: Update a product by ID
	Given I use the Gatling service client
	When I have product id with value 17
	And I send a PUT request to product/{ID} with the following json body
		"""
		{
		      "id": 17,
	   		  "name": "Curved Brown234",
			  "slug": "curved-brown234",
			  "description": "<p>Curved brown glasses case perfect for him or her</p>",
			  "image": "curve-brown-open.jpg",
		      "price": "199999985415423",
	          "categoryId": "15157",
			  "createdAt": "2020-11-25T07:16:45",
			  "updatedAt": "2020-11-25T07:16:46"
		}
		"""
	Then I validate that the response status is "400"
	And I validate that the response body contais the following values
	    | status | 400         |
	    | error  | Bad Request |
