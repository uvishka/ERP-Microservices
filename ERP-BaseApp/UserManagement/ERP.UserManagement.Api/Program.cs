using ERP.UserManagement.DataServices;
using Microsoft.EntityFrameworkCore;
using ERP.UserManagement.DataServices.Repositories;
using ERP.UserManagement.DataServices.Repositories.Interfaces;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Get Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Intilizing the DB Context inside the Dependency Injection Container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();


var app = builder.Build();

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