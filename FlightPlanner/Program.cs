using FlightPlaner.Data;
using FlightPlaner.Services;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Helpers;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// For cors
// var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddDbContext<FlightPlannerDbContext>();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddScoped<IFlightService, FlightService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For cors
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//         policy  =>
//         {
//             policy.WithOrigins("http://example.com",
//                 "http://www.contoso.com");
//         });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// For cors
// app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
