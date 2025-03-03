using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboAPI.Models;
using static RoboAPI.Models.Commands;

namespace RobotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotAPIController : ControllerBase
    {
        private readonly Commands _commands; // Inject Commands via DI

        // Constructor for injecting Commands
        public RobotAPIController(Commands commands)
        {
            _commands = commands;
        }

        [HttpPost("place")]
        public IActionResult Place([FromBody] Robot robot)
        {
            Direction direction;
            if (!Enum.TryParse(robot.direction.ToUpper(), out direction))
            {
                return BadRequest("Invalid direction.");
            }

            _commands.Place(robot.x, robot.y, direction);
            return Ok();
        }
        [HttpPost("move")]
        public IActionResult Move()
        {
            _commands.Move();
            return Ok();
        }
        [HttpPost("left")]
        public IActionResult Left()
        {
            _commands.TurnLeft();
            return Ok();
        }
        [HttpPost("right")]
        public IActionResult Right()
        {
            _commands.TurnRight();
            return Ok();
        }
        [HttpGet("report")]
        public IActionResult Report()
        {
            var report = _commands.Report();
            return Ok(report);
        }
    }
}
