using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RefactoringChallenge.Core.Entities;
using RefactoringChallenge.Core.Models.Exceptions;
using RefactoringChallenge.Core.Repositories.Interfaces;
using RefactoringChallenge.Core.Services;
using RefactoringChallenge.Core.Services.Interfaces;
using RefactoringChallenge.Filters;
using RefactoringChallenge.Middlewares;

namespace RefactoringChallenge
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
            services.AddDbContext<NorthwindDbContext>(options => options.UseInMemoryDatabase("Technical-Challenge"));

            services.AddSingleton(TypeAdapterConfig.GlobalSettings);

            services.AddScoped<IMapper, ServiceMapper>();
            services.AddScoped(typeof(IRepository<,>), typeof(RefactoringChallenge.Core.Repositories.Repository<,>));
            services.AddScoped<IOrderService, OrderService>();
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ValidationFiller>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<AppException>();
                    options.ImplicitlyValidateChildProperties = true;
                });

            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
