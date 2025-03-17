using Microsoft.AspNetCore.Mvc;
using IPLScheduler.Models;
using IPLScheduler.Services;
using System.Linq;

namespace IPLScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly SchedulingService _schedulingService;

        public MatchController(DataService dataService, SchedulingService schedulingService)
        {
            _dataService = dataService;
            _schedulingService = schedulingService;
        }

        [HttpPost]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            var team1Matches = _dataService.Matches.Where(m => m.Team1Id == match.Team1Id || m.Team2Id == match.Team1Id);
            var team2Matches = _dataService.Matches.Where(m => m.Team1Id == match.Team2Id || m.Team2Id == match.Team2Id);

            if (team1Matches.Any(m => (match.MatchDate - m.MatchDate).TotalDays < 2) ||
                team2Matches.Any(m => (match.MatchDate - m.MatchDate).TotalDays < 2))
            {
                return BadRequest("Match cannot be scheduled within 2 days of previous match for either team.");
            }

            _dataService.Matches.Add(match);
            return Ok(match);
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            return Ok(_dataService.Matches);
        }

        [HttpGet("schedule")]
        public IActionResult GetSchedule()
        {
            var teams = _dataService.Teams.Select(t => t.Name).ToList();
            var schedule = _schedulingService.GenerateSchedule(teams);
            return Ok(schedule);
        }
    }
}