using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly TestDBContext _context;

        public RecordController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/record
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords()
        {
            //return null
            var records = await _context.Records.ToListAsync();
            if (records == null || records.Count == 0)
            {
                return NotFound();
            }
            return records;
        }

        // Endpoint to retrieve call cost statistics
        [HttpGet("call-cost-statistics")]
        public async Task<IActionResult> GetCallCostStatistics()
        {
            try
            {
                // Retrieve the call costs from the database
                var callCosts = await _context.Records.Select(r => r.Cost).ToListAsync();

                // Calculate statistics
                decimal? averageCost = callCosts.Average();
                decimal? maxCost = callCosts.Max();
                decimal? minCost = callCosts.Min();

                // Create an anonymous object to hold the statistics
                var statistics = new
                {
                    AverageCost = averageCost,
                    MaxCost = maxCost,
                    MinCost = minCost
                };

                // Return the statistics as JSON
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
            [HttpGet("duration-statistics")]
        public IActionResult GetCallDurationStatistics()
        {
            try
            {
                // Calculate statistics
                var statistics = new
                {
                    AverageDuration = _context.Records.Average(r => r.Duration),
                    LongestDuration = _context.Records.Max(r => r.Duration),
                    ShortestDuration = _context.Records.Min(r => r.Duration)
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving call duration statistics: {ex.Message}");
            }
        }


        [HttpGet("count-statistics")]
        public IActionResult GetCallCountStatistics()
        {
            try
            {
                // Calculate statistics
                var statistics = new
                {
                    TotalCalls = _context.Records.Count(),
                    AverageCallsPerDay = _context.Records
                        .GroupBy(r => r.Call_date)
                        .Select(g => new
                        {
                            Date = g.Key,
                            AverageCalls = g.Count()
                        })
                        .Average(r => r.AverageCalls)
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving call count statistics: {ex.Message}");
            } 
        }
            
       [HttpGet("call-details")]
        public IActionResult GetCallDetails([FromQuery] long? callerId, [FromQuery] long? recipient)
        {
            try
            {
                // Start with all records in the database
                IQueryable<Record> query = _context.Records.AsQueryable();

                // Apply caller ID filter if provided
                if (callerId.HasValue)
                {
                    query = query.Where(r => r.Caller_id == callerId.Value);
                }

                // Apply recipient filter if provided
                if (recipient.HasValue)
                {
                    query = query.Where(r => r.Recipient == recipient.Value);
                }

                // Execute the query to retrieve filtered call details
                List<Record> callDetails = query.ToList();

                // Check if any call details were found
                if (callDetails.Count == 0)
                {
                    return NotFound();
                }

                // Return the filtered call details
                return Ok(callDetails);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 Internal Server Error response
                return StatusCode(500, $"An error occurred while retrieving call details: {ex.Message}");
            }
        }
    }
}