using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FormFactory.AspNetCore.Example
    
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(options => options.FileProviders.Add(
                    new EmbeddedFileProvider(typeof(FormFactory.FF).GetTypeInfo().Assembly, nameof(FormFactory))
                ));
            
            var embeddedProvider = new EmbeddedFileProvider(typeof(FF).Assembly,nameof(FormFactory));
            services.AddSingleton<IFileProvider>(embeddedProvider);
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            var embeddedProvider = new EmbeddedFileProvider(typeof(FF).Assembly,nameof(FormFactory));
            var physicalFileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot"));
            var compositeProvider = new CompositeFileProvider( embeddedProvider, physicalFileProvider);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = compositeProvider,
                RequestPath = new PathString("")
            });

            app.UseRouting();

            app.UseAuthorization();
            
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}