using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapNavigator;
using MapNavigatorWeb.OutputTranslators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MapNavigatorWeb
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
            services.AddMvc();

            //=========== Register classes needed for map navigation ======== //
            AddMapServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Not ideal for an api, but
            // proper error handling takes time.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        /// <summary>
        /// Adds all map services at once.
        /// </summary>
        /// <param name="services"></param>
        private static void AddMapServices(IServiceCollection services)
        {
            // The root navigator class.
            services.AddTransient<INavigator, Navigator>();
            // The class responsible for parsing an input string into a series of instructions.
            services.AddTransient<IFullnstructionParser, RelativeInstructionParser>();
            // The class responsible for taking a set of instructions, simulating map travel, and returning a result.
            services.AddTransient<IInstructionSimulator, InstructionSimulator>();
            // The class responsible for parsing a single instruction string from a series of instructions.
            services.AddTransient<ISingleInstructionParser, SingleNamedInstructionParser>();

            // Class responsible for interpreting a map result and converting it to an API output model.
            services.AddTransient<IMapTranslator, MapTranslator>();
        }
    }
}
