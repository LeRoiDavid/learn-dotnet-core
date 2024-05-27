using LearnDotnet.AppDataContext;
using LearnDotnet.Interfaces;
using LearnDotnet.Middleware;
using LearnDotnet.Models;
using LearnDotnet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoServices, TodoServices>();


// Add  This to in the Program.cs file
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Add this line
builder.Services.AddSingleton<TodoDbContext>(); // Add this line

// Add  This to in the Program.cs file
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Add this line

var app = builder.Build();

// Use the global exception handler middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
