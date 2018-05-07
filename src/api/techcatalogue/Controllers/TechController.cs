
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class TechController : Controller 
{
    public IEnumerable<string> Catalogue()
    {
        return new List<string>() 
        {
            "Puppet"
        };
    }
}