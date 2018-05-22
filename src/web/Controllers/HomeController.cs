using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JustEat.StatsD;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private static Queue<string> registrationQueue = new Queue<string>(100);
        
        private IStatsDPublisher statsd;

        public HomeController(IStatsDPublisher statsdPublisher)
        {
            this.statsd = statsdPublisher;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("home/register/{level=starter}")]
        public IActionResult Register(string level) 
        {
            registrationQueue.Enqueue(level);
            
            this.statsd.Increment($"count.homecontroller.register.level.{level}");
            this.statsd.Gauge(registrationQueue.Count, "gauge.homecontroller.registrationqueue");
            
            using (this.statsd.StartTimer("timer.homecontroller.register"))
            {
                GetFeaturesList();
                this.statsd.Time("timer.homecontroller.getprices", t => GetPrices());

                if (registrationQueue.Any())
                {
                    registrationQueue.Dequeue();
                }

                return View("~/Views/Home/register.cshtml");
            } 
        }

        private void GetFeaturesList()
        {
             SyntheticWait(1000);
        }

        private void GetPrices()
        {
            SyntheticWait(3000);
        }

        private void SyntheticWait(int durationMs)
        {
            var rnd = new Random();
            int duration = rnd.Next(1, durationMs);
            Thread.Sleep(durationMs);
        }
    }
}
