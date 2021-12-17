using Expert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly ILogger<ConcertsController> _logger;

        public ConcertsController(ILogger<ConcertsController> logger, DBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

            if (!dbContext.Concerts.Any())
            {
                dbContext.Concerts.Add(new Concert("ROCK", "19.01.2022", "STEREO PLAZA", "TILL LINDEMANN", "UKRAINE", "KYIV", 2050));
                dbContext.Concerts.Add(new Concert("ROCK", "03.01.2022", "NSC OLYMPIC", "IMAGINE DRAGONS", "UKRAINE", "KYIV", 1300));
                dbContext.Concerts.Add(new Concert("ROCK", "29.05.2022", "VDNH", "IRON MAIDEN", "UKRAINE", "KYIV", 1700));
                dbContext.Concerts.Add(new Concert("INSTRUMENTAL", "03.01.2022", "NC KPI", "LORDS OF THE SOUND", "UKRAINE", "KYIV", 650));
                dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concert>>> Get()
        {
            return await _dbContext.Concerts.ToListAsync();
        }

        // GET api/concerts/0
        [HttpGet("{id}")]
        public async Task<ActionResult<Concert>> Get(int id)
        {
            var concert = await _dbContext.Concerts.FirstOrDefaultAsync(x => x.Id == id);

            if (concert == null)
            {
                return NotFound();
            }

            return new ObjectResult(concert);
        }

        [HttpPost]
        public async Task<ActionResult<Concert>> Post(Concert concert)
        {
            if (concert == null)
            {
                return BadRequest();
            }

            _dbContext.Concerts.Add(concert);
            await _dbContext.SaveChangesAsync();

            return Ok(concert);
        }

        [HttpPut]
        public async Task<ActionResult<Concert>> Put(Concert concert)
        {
            if (concert == null)
            {
                return BadRequest();
            }

            if (!_dbContext.Concerts.Any(x => x.Id == concert.Id))
            {
                return NotFound();
            }

            _dbContext.Update(concert);
            await _dbContext.SaveChangesAsync();

            return Ok(concert);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Concert>> Delete(int id)
        {
            var user = _dbContext.Concerts.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Concerts.Remove(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet("style/{style}")]
        public async Task<ActionResult<IEnumerable<Concert>>> GetConcertsByStyle(string style)
        {
            var concerts = await _dbContext.Concerts.Where(x => x.Style == style).ToListAsync();

            if (concerts == null)
            {
                return NotFound();
            }

            return concerts;
        }

        [HttpGet("priceRange/{priceFrom}/{priceTo}")]
        public async Task<ActionResult<IEnumerable<Concert>>> GetConcertsFromDateRange(int priceFrom, int priceTo)
        {
            var concerts = await _dbContext.Concerts.Where(x => x.Price >= priceFrom && x.Price <= priceTo).ToListAsync();

            if (concerts == null)
            {
                return NotFound();
            }

            return concerts;
        }

        [HttpGet("averagePrice")]
        public async Task<ActionResult<IEnumerable<Concert>>> GetAveragePrice()
        {
            var prices = await _dbContext.Concerts.Select(x => x.Price).ToListAsync();
            double sum = 0;

            foreach (var price in prices)
            {
                sum += price;
            }

            return new ObjectResult(sum / prices.Count);
        }
    }
}
