using PowerCalculator.Domain.Models;
using PowerCalculator.Dto;

namespace PowerCalculator.Domain.Services
{
    public interface IPowerPlanService
    {
        List<PowerPlant> CreateForecastLoadProfile(ProductionPlanDto plan);
    }
}