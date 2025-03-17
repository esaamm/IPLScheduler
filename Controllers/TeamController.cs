using Microsoft.AspNetCore.Mvc;
using IPLScheduler.Models;
using IPLScheduler.Services;
using System.Collections.Generic;

namespace IPLScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly DataService _dataService;
        private static int nextId = 1;

        public TeamController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            team.Id = nextId++;
            _dataService.Teams.Add(team);
            return Ok(team);
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            return Ok(_dataService.Teams);
        }
    }
}