using EmployeeManagement.Data;
using EmployeeManagement.ExtensionMethod;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.IRepository;
using EmployeeManagement.Models.Repositories.Implemintations;
using EmployeeManagement.Models.Repositories.Interfaces;
using EmployeeManagement.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.ConfigureController();
builder.Services.ConfigureDbContext(builder.Configuration);


builder.Services.AddIdentity<APIUser, IdentityRole>(op =>
{
    op.User.RequireUniqueEmail = true;
    op.Password.RequiredUniqueChars = 0;
    op.Password.RequiredLength = 5;
    op.Password.RequireNonAlphanumeric=false;
    op.Password.RequireUppercase=false;
    op.Password.RequireDigit =false;
}
).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureAppServices();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureJWT(builder.Configuration);


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
app.UseAuthentication();
app.UseAuthorization();

//app.UseMvcWithDefaultRoute();

app.MapControllers();

app.Run();
