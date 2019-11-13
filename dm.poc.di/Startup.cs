using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using dm.poc.core;
using dm.poc.iservice;
using dm.poc.iservice.Helper;
using dm.poc.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dm.poc.di
{
    public class Startup
    {
        public List<Type> TypesToRegister { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            TypesToRegister=new List<Type>();
            foreach (var file in Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)))
            {
                if (System.IO.Path.GetFileName(file).StartsWith("dm.poc.service") && System.IO.Path.GetFileName(file).EndsWith(".dll"))
                {
                    TypesToRegister.AddRange(Assembly.LoadFrom(file)
                        .GetTypes()
                        .Where(x => !string.IsNullOrEmpty(x.Namespace))
                        .Where(x => x.IsClass)
                        .Where(x => x.Namespace.StartsWith("dm.poc.service")).ToList());
                }
            }
            //TypesToRegister = Assembly.LoadFrom(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\dm.poc.service.dll")
            //    .GetTypes()
            //    .Where(x => !string.IsNullOrEmpty(x.Namespace))
            //    .Where(x => x.IsClass)
            //    .Where(x => x.Namespace.StartsWith("dm.poc.service")).ToList();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            TypesToRegister.ForEach(x => { if (x.DeclaringType == null) { services.AddScoped(x); } });
            RegisterService.registerServicesDI(ref services, TypesToRegister);
            services.AddScoped(typeof(IServicesProvider<>), typeof(ServicesProvider<>));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
