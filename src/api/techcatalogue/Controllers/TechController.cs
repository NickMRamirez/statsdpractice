
using System;
using System.Collections.Generic;
using System.Threading;
using JustEat.StatsD;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class TechController : ControllerBase
{
    private IStatsDPublisher statsPublisher;

    private IList<string> technologies;

    public TechController(IStatsDPublisher statsPublisher)
    {
        this.statsPublisher = statsPublisher;
        this.technologies = new List<string>() 
        {
            "Visual Studio Team Services",
            "Gitlab",
            "Jenkins"
        };
    }
    
    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        this.statsPublisher.Increment("techcatalogue.tech.getall.rate");

        using (this.statsPublisher.StartTimer("techcatalogue.tech.getall.time"))
        {
            SyntheticWait();
            return this.technologies;
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(404)]
    public IActionResult GetSingle(int id)
    {
        this.statsPublisher.Increment("techcatalogue.tech.getsingle.rate");

        using (this.statsPublisher.StartTimer("techcatalogue.tech.getsingle.time"))
        {
            SyntheticWait();
            if (id < this.technologies.Count) 
            {
                return Ok(this.technologies[id]);
            }

            return NotFound();
        }
    }

    private void SyntheticWait()
    {
        var rnd = new Random();
        int durationMs = rnd.Next(1, 5000);
        Thread.Sleep(durationMs);
    }
}