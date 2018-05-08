
using System.Collections.Generic;
using JustEat.StatsD;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class TechController : Controller 
{
    private IStatsDPublisher statsPublisher;

    public TechController(IStatsDPublisher statsPublisher)
    {
        this.statsPublisher = statsPublisher;
    }
    
    [HttpGet]
    public IEnumerable<string> Get()
    {
        this.statsPublisher.Increment("tech");
        
        return new List<string>() 
        {
            "Puppet"
        };
    }
}