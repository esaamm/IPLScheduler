using System;
using System.Collections.Generic;
using System.Linq;

namespace IPLScheduler.Services
{
    public class SchedulingService
    {
        public List<(string Team1, string Team2, DateTime MatchDate)> GenerateSchedule(List<string> teams)
        {
            var schedule = new List<(string Team1, string Team2, DateTime MatchDate)>();
            var random = new Random();
            var daysBetweenMatches = 2;
            var currentDate = DateTime.Now.Date;

            var teamMatches = teams.ToDictionary(team => team, team => new List<DateTime>());

            foreach (var team1 in teams)
            {
                foreach (var team2 in teams.Where(t => t != team1))
                {
                    var matchDate = currentDate;

                    while (teamMatches[team1].Any(date => (date - matchDate).TotalDays < daysBetweenMatches) ||
                           teamMatches[team2].Any(date => (date - matchDate).TotalDays < daysBetweenMatches))
                    {
                        matchDate = matchDate.AddDays(1);
                    }

                    schedule.Add((team1, team2, matchDate));
                    teamMatches[team1].Add(matchDate);
                    teamMatches[team2].Add(matchDate);
                }
            }

            return schedule;
        }
    }
}