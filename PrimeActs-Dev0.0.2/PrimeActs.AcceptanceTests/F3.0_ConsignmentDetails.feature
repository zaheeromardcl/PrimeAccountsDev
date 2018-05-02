Feature: F3.0_ConsignmentDetails
As a user, it should be possible to add inventory items to a consignment.

@myConsignmentDetails
Scenario Outline: Consignment #123434 created with no items
	Given that the user is an authenticated user with the appropriate role
	And the user has navigated to the CreateConsignment Items page
	And the user has completed the <Produce> field
	And the user has comppleted the <EstimatedPurchaseCost> field
	And the user has completed the <EstimatedChargeCostPerPack> field
	And the user has comppleted the <EstimatedPercentageProfit> field
	And the user has completed the <Returns> field
	And the user has completed the <Brand> field
	And the user has completed the <Pack> field
	And the user has completed the <PackSize> field
	And the user has completed the <PackWeight> field
	And the user has completed the <PackPall> field
	And the user has completed the <PackWtUnit> field
	And the user has completed the <Porterage> field
	And the user has completed the <ExpectedQuantity> field
	And the user has completed the <ReceivedQuantity> field
	And the user has clicked on the <CreateConsignmentItem> button
	Then the consignment items details should be saved to the database 
	And the consignment details page updated to show the new items.
	Examples: 
	| Produce | estimatedpurchasecost | EstimatedChargeCostPerPack | EstimatedPercentageProfit | Returns | Brand   | Pack | PackSize | PackWeight | PackPall | PackWtUnit | Porterage | ExpectedQuantity | RecievedQuantity |
	| AARD    | 1.23                  | 1.34                       | 2.34                      | 2.34    | Purleys | 4    | 8        | 1.23       | 1.24     | 2          | 1         | 10               | 10               |