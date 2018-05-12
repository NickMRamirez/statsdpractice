
using System.Collections.Generic;
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
    public IEnumerable<string> Get()
    {
        this.statsPublisher.Increment("techcatalogue.tech.get");

        return this.technologies;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(404)]
    public IActionResult Get(int id)
    {
        this.statsPublisher.Increment("techcatalogue.tech.get.id");

        if (id < this.technologies.Count) 
        {
            return Ok(this.technologies[id]);
        }

        return NotFound();
    }
}