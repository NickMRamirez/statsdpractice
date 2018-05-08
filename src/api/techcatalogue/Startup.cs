using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustEat.StatsD;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace techcatalogue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var statsDOptions = new StatsDOptions();
            Configuration.Bind("StatsD", statsDOptions); 
            services.AddSingleton(statsDOptions);

            services.AddMvc();
            services.AddStatsD(provider =>
            {
                var options = provider.GetRequiredService<StatsDOptions>();

                return new StatsDConfiguration()
                {
                    Host = options.HostName,
                    Port = options.Port,
                    Prefix = options.Prefix// ,
                    // OnError = ex => LogError(ex)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
