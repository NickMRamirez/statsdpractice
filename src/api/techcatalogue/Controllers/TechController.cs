
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
        this.statsPublisher.Increment("techcatalogue.count.techcontroller.getall");

        using (this.statsPublisher.StartTimer("techcatalogue.timer.techcontroller.getall"))
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
        this.statsPublisher.Increment("techcatalogue.count.techcontroller.getsingle");

        using (this.statsPublisher.StartTimer("techcatalogue.timer.techcontroller.getsingle"))
        {
            SyntheticWait();
            if (id < this.technologies.Count) 
            {
                this.statsPublisher.Increment("techcatalogue.count.techcontroller.getsingle.Ok");
                return Ok(this.technologies[id]);
            }

            this.statsPublisher.Increment("techcatalogue.count.techcontroller.getsingle.NotFound");
            return NotFound();
        }
    }

    private void SyntheticWait()
    {
        var rnd = new Random();
        int durationMs = rnd.Next(1, 3000);
        Thread.Sleep(durationMs);
    }
}