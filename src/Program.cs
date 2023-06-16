using PowerCalculator.Domain.Services;
using PowerCalculator.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/productionplan", (ProductionPlanDto source) =>
{
    return (new Calculate()).Get(source);
})
.WithName("ProductionPlan");

app.Run();

internal class Calculate
{
    internal List<PowerPlantGenerationResponseDto> Get(ProductionPlanDto plan)
    {
        var powerPlantGenerationResponse = new List<PowerPlantGenerationResponseDto>();
        IPowerPlanService powerPlanService = new PowerPlanService();
        var domainPowerPlantList = powerPlanService.CreateForecastLoadProfile(plan);
        foreach (var plant in domainPowerPlantList)
        {
            powerPlantGenerationResponse.Add(new(plant.Name, Math.Round(plant.MwhToGenerateForPlan, 1)));
        }
        return powerPlantGenerationResponse;
    }
}