@api
Feature: ReqRes Scenarios


Scenario: Create User using "/api/users"
	Given I create request body for endpoint
	When I requested "POST" for "/api/users" 
	Then I validate the response for "/api/users" 
	And I validate response status should be 201

	Scenario: Get User using "/api/users/2"
	When I requested "GET" for "/api/users/2" 
	Then I validate the response for /api/users/2
	And I validate response status should be 200

	Scenario: User Not Found using "/api/users/23"
	When I requested "GET" for "/api/users/23" 
	Then I validate the response for /api/users/23
	And I validate response status should be 404

	
	Scenario: Update User using "/api/users/2"
	Given I create request body for endpoint
	When I requested "PUT" for "/api/users/2" 
	Then I validate the response for updated /api/users/2
	And I validate response status should be 200

	Scenario: Delete User using "/api/users/2"
	When I requested "DELETE" for "/api/users/2" 
	Then I validate the response for /api/users/2 after delete 
	And I validate response status should be 204