using Application;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ProductsApi
{
    public class Startup
    {
        readonly string CorsAllowedOrigins = "AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbPath = Configuration.GetSection("ConnectionStrings:ProductDbContext");
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(dbPath.Value));

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsAllowedOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins(Configuration.GetValue<string>("CorsOrigins"))
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                                    });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsApi", Version = "v1" });
            });

            services.AddScoped<ProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsApi v1"));
            }

            app.UseRouting();

            app.UseCors(CorsAllowedOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
