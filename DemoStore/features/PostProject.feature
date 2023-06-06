Feature: Post a product

As a user, I wanto to create a product by API test

@negative @regression @finalProject
Scenario: Create a product 
	Given I use the Gatling service client
	When I send a POST request to products with the following json body
		"""
		{
            "id": 33,
            "name": "Curved Brown",
            "slug": "curved-brown",
            "description": "<p>Curved brown glasses case perfect for him or her</p>",
            "image": "curve-brown-open.jpg",
            "price": "19.99",
            "categoryId": "7",
        }
        """
	Then I validate that the response status is "400"
    And I validate that the response body contais the following values
	    | status | 400         |
        | error | Bad Request  |
