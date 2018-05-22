using System;
using System.Collections.Concurrent;
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
        private static ConcurrentQueue<string> registrationQueue = new ConcurrentQueue<string>();
        
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
            this.statsd.Gauge(registrationQueue.Count, "gauge.homecontroller.registrationqueue");
            
            this.statsd.Increment($"count.homecontroller.register.level.{level}");
            
            
            using (this.statsd.StartTimer("timer.homecontroller.register"))
            {
                GetFeaturesList();
                this.statsd.Time("timer.homecontroller.getprices", t => GetPrices());

                if (registrationQueue.Any())
                {
                    string result;
                    registrationQueue.TryDequeue(out result);
                    this.statsd.Gauge(registrationQueue.Count, "gauge.homecontroller.registrationqueue");
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
            SyntheticWait(2000);
        }

        private void SyntheticWait(int durationMs)
        {
            var rnd = new Random();
            int duration = rnd.Next(1, durationMs);
            Thread.Sleep(durationMs);
        }
    }
}
