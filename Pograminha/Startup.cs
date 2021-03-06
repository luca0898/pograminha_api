using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pograminha.Domain.Contracts;
using Pograminha.Infra.SQL;
using Pograminha.Infra.SQL.Repositories;
using Pograminha.Model;
using Pograminha.Services;

namespace Pograminha
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("pograminhaDb")));

            services
                .AddScoped<ApplicationDbContext>()
                .AddScoped<DbContext>((x) => x.GetService<ApplicationDbContext>())
                .AddScoped<IUnitOfWorkFactory<UnitOfWork>, UnitOfWorkFactory>();

            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            services.AddTransient<ITodoItemService, TodoItemService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pograminha", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pograminha v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
