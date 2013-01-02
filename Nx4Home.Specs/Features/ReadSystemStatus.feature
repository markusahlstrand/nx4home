Feature: ReadSystemStatus
	In order to know if my wife set the alarm or not
	As the house owner
	I want to be told the current status of the alarm

@mytag
Scenario: Read system status
	When I request the system status
	Then the result shoud be disarmed
