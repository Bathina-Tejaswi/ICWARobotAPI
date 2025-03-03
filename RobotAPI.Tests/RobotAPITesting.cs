using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;

namespace RobotAPI.Tests
{
    public class RobotAPITesting  : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        
        public RobotAPITesting(WebApplicationFactory<Program> factory)
        {
            //_client = factory.CreateClient(new WebApplicationFactoryClientOptions
            //{
            //    BaseAddress = new Uri("https://localhost:7173") 
            //});
            _factory = factory;
            _client = factory.CreateClient();
        }
        

        [Fact]
        public async Task TestPlaceCommand()
        {
            // Arrange: Place robot at (0,0) facing NORTH
            var placeRequest = new
            {
                X = 0,
                Y = 0,
                Direction = "NORTH"
            };
            
            var placeResponse = await _client.PostAsync("/api/RobotAPI/place",
                new StringContent(JsonConvert.SerializeObject(placeRequest), Encoding.UTF8, "application/json"));
            placeResponse.EnsureSuccessStatusCode();

            // Act: Get robot's report
            var reportResponse = await _client.GetStringAsync("/api/RobotAPI/report");

            // Assert: Verify the report matches the expected result
            Assert.Equal("0, 0, NORTH", reportResponse);
        }

        // Test for MOVE command after valid PLACE command
        [Fact]
        public async Task TestMoveCommand()
        {
            // Arrange: Place robot at (0,0) facing NORTH
            var placeRequest = new
            {
                X = 0,
                Y = 0,
                Direction = "NORTH"
            };
            await _client.PostAsync("/api/RobotAPI/place", new StringContent(JsonConvert.SerializeObject(placeRequest), Encoding.UTF8, "application/json"));

            // Act: Move the robot
            var moveResponse = await _client.PostAsync("/api/RobotAPI/move", null);
            moveResponse.EnsureSuccessStatusCode();

            // Get robot's report
            var reportResponse = await _client.GetStringAsync("/api/RobotAPI/report");

            // Assert: Verify the robot moved correctly
            Assert.Equal("0, 1, NORTH", reportResponse);
        }

        // Test for LEFT command
        [Fact]
        public async Task TestLeftCommand()
        {
            // Arrange: Place robot at (0,0) facing NORTH
            var placeRequest = new
            {
                X = 0,
                Y = 0,
                Direction = "NORTH"
            };
            await _client.PostAsync("/api/RobotAPI/place", new StringContent(JsonConvert.SerializeObject(placeRequest), Encoding.UTF8, "application/json"));

            // Act: Turn the robot left
            var leftResponse = await _client.PostAsync("/api/RobotAPI/left", null);
            leftResponse.EnsureSuccessStatusCode();

            // Get robot's report
            var reportResponse = await _client.GetStringAsync("/api/RobotAPI/report");

            // Assert: Verify the robot's direction after turning left
            Assert.Equal("0, 0, WEST", reportResponse);
        }

        // Test for RIGHT command
        [Fact]
        public async Task TestRightCommand()
        {
            // Arrange: Place robot at (0,0) facing NORTH
            var placeRequest = new
            {
                X = 0,
                Y = 0,
                Direction = "NORTH"
            };
            await _client.PostAsync("/api/RobotAPI/place", new StringContent(JsonConvert.SerializeObject(placeRequest), Encoding.UTF8, "application/json"));

            // Act: Turn the robot right
            var rightResponse = await _client.PostAsync("/api/RobotAPI/right", null);
            rightResponse.EnsureSuccessStatusCode();

            // Get robot's report
            var reportResponse = await _client.GetStringAsync("/api/RobotAPI/report");

            // Assert: Verify the robot's direction after turning right
            Assert.Equal("0, 0, EAST", reportResponse);
        }

        // Test all commands in sequence
        [Fact]
        public async Task TestAllCommands()
        {
            // Arrange: Place robot at (0,0) facing NORTH
            var placeRequest = new
            {
                X = 1,
                Y = 2,
                Direction = "EAST"
            };
            await _client.PostAsync("/api/RobotAPI/place", new StringContent(JsonConvert.SerializeObject(placeRequest), Encoding.UTF8, "application/json"));

            // Act: Execute a series of commands
            await _client.PostAsync("/api/RobotAPI/move", null); // Move
            await _client.PostAsync("/api/RobotAPI/move", null); // Move
            await _client.PostAsync("/api/RobotAPI/left", null); // Turn Left
            await _client.PostAsync("/api/RobotAPI/move", null); // Move

            // Get robot's final report
            var reportResponse = await _client.GetStringAsync("/api/RobotAPI/report");

            // Assert: Verify the robot's position and direction after all commands
            Assert.Equal("3, 3, NORTH", reportResponse);
        }
    }
}