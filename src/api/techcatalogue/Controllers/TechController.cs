
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class TechController : Controller 
{
    [HttpGet]
    public IEnumerable<string> Get()
    {

            return new List<string>() 
            {
                "Puppet"
            };

    }
}