
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StatsdClient;

[Route("api/[controller]")]
public class TechController : Controller 
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        using (DogStatsd.StartTimer("Catalogue"))
        {
            return new List<string>() 
            {
                "Puppet"
            };
        }
    }
}