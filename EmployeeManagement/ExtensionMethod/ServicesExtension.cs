using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repositories.Implemintations;
using EmployeeManagement.Models.Repositories.Interfaces;
using EmployeeManagement.Models.Repository;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace EmployeeManagement.ExtensionMethod
{
    public static class ServicesExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<APIUser>(q => { q.User.RequireUniqueEmail = true; });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddTokenProvider("EmployeeManagement", typeof(DataProtectorTokenProvider<APIUser>));
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }
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
            services.AddScoped<IAuthManager, AuthManager>();
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
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
              Enter 'Bearer' [space] and then your token in the text input below.
              Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "0auth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
            });
            });

        }

        //middleware
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later."
                        }.ToString());
                    }
                });
            });
        }
    }
}

