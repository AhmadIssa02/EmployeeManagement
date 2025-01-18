using EmployeeManagement.Models.Repositories.Implemintations;
using EmployeeManagement.Models.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddControllers();
builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
builder.Services.AddSwaggerGen();



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
