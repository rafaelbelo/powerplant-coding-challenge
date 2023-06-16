using Microsoft.AspNetCore.Mvc;
using PowerCalculator.Domain.Services;
using PowerCalculator.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPowerPlanService, PowerPlanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/productionplan", ([FromBody] ProductionPlanDto source, IPowerPlanService service) =>
{
    return (new Calculation(service)).Execute(source);
})
.WithName("ProductionPlan");

app.Run();

internal class Calculation
{
    private readonly IPowerPlanService _powerPlanService;
    public Calculation(IPowerPlanService service)
    {
        _powerPlanService = service;
    }
    internal List<PowerPlantGenerationResponseDto> Execute(ProductionPlanDto plan)
    {
        var powerPlantGenerationResponse = new List<PowerPlantGenerationResponseDto>();
        var domainPowerPlantList = _powerPlanService.CreateForecastLoadProfile(plan);
        foreach (var plant in domainPowerPlantList)
        {
            powerPlantGenerationResponse.Add(new(plant.Name, Math.Round(plant.MwhToGenerateForPlan, 1)));
        }
        return powerPlantGenerationResponse;
    }
}