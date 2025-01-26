using EmployeeManagement.Data;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repositories.Implemintations;
using EmployeeManagement.Models.Repositories.Interfaces;
using EmployeeManagement.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB")));
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
    //developerExceptionPageOptions.SourceCodeLineCount = 1;
    //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}

//FileServerOptions options = new FileServerOptions();
//options.DefaultFilesOptions.DefaultFileNames.Clear();
//options.DefaultFilesOptions.DefaultFileNames.Add("foo.html");


//app.UseFileServer(options);

app.UseStaticFiles();

//app.UseMvcWithDefaultRoute();

app.MapControllers();

app.Run();
