@web
Feature: Automation Practice Test Scenarios

Background:
	When User logged into application

Scenario: Purchase 2 Items
	Given I select "Women" from the menu
	When I select a product from catalogue
	And I select size "M" from size
	And I get the product details
	Then I click on Add To Cart
	Then I selected "Continue shopping" action
	Given I select "Women" from the menu
	When I select a product from catalogue which is not already added to cart
	And I get the product details
	Then I click on Add To Cart
	Then I get the details of Cart
	Then I selected "Proceed to checkout" action
	When I get the cart details
	Then I validate cart details
	When I placed order using "By Bank Wire"
	Then I validate order conformation
	Then I sign out of the application

	Scenario: Review previous order and add a message
	When I select Order history and details
	When I select a random order from existing orders
	Then I validate order details
	When I enter a message
	Then I validate the message
	Then I sign out of the application

	
	Scenario: Capture Image
	When I select Order history and details
	When I select a random order from existing orders
	Then I validate order details
	When I enter a message
	Then I validate the message for negative scenario
	Then I sign out of the application

	