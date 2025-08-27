using Microsoft.AspNetCore.Mvc;
using StatsApi.Data;
using StatsApi.Models;

namespace StatsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly StatsDbContext _context;

        public StatsController(StatsDbContext context)
        {
            _context = context;
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
                TotalMatches = _context.MatchStats.Count(),
                AvgElims = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Elims) : 0,
                AvgAssists = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Assists) : 0,
                AvgDeaths = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Deaths) : 0,
                AvgRevives = _context.MatchStats.Any() ? _context.MatchStats.Average(s => s.Revives) : 0
            };

            return Ok(summary);
        }
    }
}
