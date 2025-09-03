using Microsoft.AspNetCore.Mvc;
using StatsApi.Data;
using StatsApi.Models;
using StatsApi.Services;
using Microsoft.Extensions.Logging; 

namespace StatsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly StatsDbContext _context;
        private readonly StatsService _statsService;
        private readonly ILogger<StatsController> _logger;

        public StatsController(StatsDbContext context, StatsService statsService, ILogger<StatsController> logger)
        {
            _context = context;
            _statsService = statsService;
            _logger = logger;
        }

        // POST api/stats
        [HttpPost]
        public async Task<IActionResult> PostStats([FromBody] MatchStats stats)
        {
            _context.MatchStats.Add(stats);
            await _context.SaveChangesAsync();
            return Ok(stats);
        }

        // GET api/stats/summary
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var summary = new
            {
                ID = _context.MatchStats.Any() ? _context.MatchStats.Max(s => s.Id) : 0,
                TotalMatches = _context.MatchStats.Count(),
                TotalWins = _context.MatchStats.Count(s => s.Placement == "first"),
                TotalElims = _context.MatchStats.Sum(s => s.Elims),
                TotalAssists = _context.MatchStats.Sum(s => s.Assists),
                TotalDeaths = _context.MatchStats.Sum(s => s.Deaths),
                TotalRevives = _context.MatchStats.Sum(s => s.Revives),
                KD = _context.MatchStats.Any() ? _context.MatchStats.Sum(s => s.Elims) / (double)_context.MatchStats.Sum(s => s.Deaths) : 0,
                KDA = _context.MatchStats.Any() ? (_context.MatchStats.Sum(s => s.Elims) + _context.MatchStats.Sum(s => s.Assists)) / (double)_context.MatchStats.Sum(s => s.Deaths) : 0,
                KPG = _context.MatchStats.Any() ? _context.MatchStats.Sum(s => s.Elims) / (double)_context.MatchStats.Count() : 0,
                WinRate = _statsService.CalculateWinPercentage(_context.MatchStats.Count(s => s.Placement == "first"), _context.MatchStats.Count()),
                AvgElims = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Elims) : 0,
                AvgAssists = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Assists) : 0,
                AvgDeaths = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Deaths) : 0,
                AvgRevives = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Revives) : 0
            };

            _logger.LogInformation("Summary response: {@Summary}", summary);
            return Ok(summary);
        }
    }
}
