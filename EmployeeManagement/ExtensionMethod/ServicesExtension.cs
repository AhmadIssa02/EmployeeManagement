using EmployeeManagement.Data;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repositories.Implemintations;
using EmployeeManagement.Models.Repositories.Interfaces;
using EmployeeManagement.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace EmployeeManagement.ExtensionMethod
{
    public static class ServicesExtension
    {
        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EmployeeDB")));
        }
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");
            var key = Environment.GetEnvironmentVariable("key");
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
    }
}

