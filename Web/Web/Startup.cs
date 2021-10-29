using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Web.Managers;
using Web.Managers.Strategy;
using Web.Middlewares;
using Web.Repositories;
using Web.Repositories.Connection;

namespace Web
{
    public class Startup
    {
        const string SpecificOrigins = "SpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddCors(options =>
            {
                options.AddPolicy(name: SpecificOrigins,
                    configurePolice =>
                    {
                        configurePolice
                            .SetIsOriginAllowed(origin => true)
                            .AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod();

                    });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();})
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddMvc();
            
            services.AddSingleton<IConnection, MySqlDbConnection>();

            services.AddTransient<IGenaratorContext, GenaratorContext>();

            services.AddScoped<IGeneratorManager, GeneratorManagerAttr>();

#if MOCK
            services.AddScoped<ISmartRepository, MockSmartRepository>();
#else
            services.AddScoped<ISmartRepository, SmartRepository>();
#endif


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return Interceptor.Get(actionContext);
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(SpecificOrigins);

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
