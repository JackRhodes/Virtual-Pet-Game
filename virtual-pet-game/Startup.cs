using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Mappings;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Areas.v1.Repository.Implementation;
using virtual_pet_game_Areas.v1.Repository.Contracts;
using virtual_pet_game_Areas.v1.Repository.Implementation;

namespace virtual_pet_game
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
            RegisterDIServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            Mapper.Initialize(x =>
            {
                x.AddProfile(new DTOMappings());
            }

            );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        /// <summary>
        /// Added for readibility
        /// </summary>
        /// <param name="services"></param>
        private void RegisterDIServices(IServiceCollection services)
        {
            //This needs to remain constant across multiple requests as it's replicating a database. 
            //Therefore needs to be singleton
            services.AddSingleton<IContext, StubDataContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAnimalTypeRepository, AnimalTypeRepository>();
            services.AddTransient<IAnimalTypeManager, AnimalTypeManager>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddTransient<IAnimalManager, AnimalManager>();
            services.AddAutoMapper();
        }

    }
}
