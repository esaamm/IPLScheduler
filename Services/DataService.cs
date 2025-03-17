using System.Collections.Generic;
using IPLScheduler.Models;

namespace IPLScheduler.Services
{
    public class DataService
    {
        public List<Team> Teams { get; } = new List<Team>();
        public List<Match> Matches { get; } = new List<Match>();
    }
}