using Microsoft.OpenApi.Models;
using Ta9_Assignment.Repositories;
using Neo4jClient;

namespace Ta9_Assignment
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

            services.AddControllers();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { Title = "Ta9 Assignment",
                  Version = "v1",
                  Description = "An ASP.Net Core WebAPI for managing Departments & Employees in an Organization"
                });
            });

            var config = this.Configuration.GetSection("NeO4jConnectionSettings");
            var client = new BoltGraphClient(new Uri(config["ServerDB"]),config["User"],config["Password"]);
            //var client = new BoltGraphClient(new Uri("neo4j+s://c979ddd2.databases.neo4j.io"),"neo4j", "kdVgJkfCly0xr82fIwtH1-OC59skFNwFzMmKzKEnSig");
            client.ConnectAsync();
            services.AddSingleton<IGraphClient>(client);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "";
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ta9 /Assignment v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}