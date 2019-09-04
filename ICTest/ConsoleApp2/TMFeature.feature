Feature: TMFeature
	As a tester of turn up module, I would like to create, edit and delete records
	in time and material table.
	
@automate @tm
Scenario: Create a time and material record in turn up portal
	Given Logged into the turn up portal
	And Navigate to the time and material page
	Then Create a time and material record.

@tm @automate
Scenario: Delete a time and material record
	Given Logged into the turn up portal
	And Navigate to the time and material page
	Then Delete a time and material record.

@tm @automate
Scenario: Edit a time and material record
	Given Logged into the turn up portal
	And Navigate to the time and material page
	Then Edit a time and material record.


