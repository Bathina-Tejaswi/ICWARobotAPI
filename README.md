## RobotAPI Solution
This repository contains two main components:
Web API (`RobotAPI`) - – A RESTful API for managing robot commands
xUnit Tests (`RobotAPI.Tests`) - Designed to test the API's functionality using xUnit.

The Web API provides a set of endpoints for controlling a robot, and the xUnit tests ensure that the API functions correctly.

## Prerequisites
Make sure you have the following installed:
Visual Studio 
.NET SDK (Version 8.0 or higher)
xUnit for testing (can be installed through the solution)
Postman or any other API client (optional for testing endpoints manually)

## How to Run

1. Clone the repository and install dependencies
    a)create a folder in file explorer and open git bash
    b)clone the repository using command
       git clone  https://github.com/Bathina-Tejaswi/ICWARobotAPI.git
2. Open the project in Visual Studio
3. Run the RobotAPI project
   a)By default, the API will run on https://localhost:7173(or another port if configured). Alternatively you can test the API using Postman


## Commands

- `PLACE X,Y,FACING`: Places the robot at the specified position and facing.
- `MOVE`: Moves the robot forward in the current direction.
- `LEFT`: Rotates the robot 90 degrees counterclockwise.
- `RIGHT`: Rotates the robot 90 degrees clockwise.
- `REPORT`: Outputs the robot's current position and facing.

## Tests

Unit tests are included in the `RobotAPI.Tests` project. You can run them using `dotnet test`.

