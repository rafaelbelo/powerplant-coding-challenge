using PowerCalculator.Domain.Models;
using PowerCalculator.Dto;

namespace PowerCalculator.Domain.Services
{
    public class PowerPlantLoadCalculator
    {
        public void Calculate(ProductionPlanDto plan)
        {
            List<PowerPlant> plantsList;

            foreach (var plant in plan.PowerPlants)
            {
                // calculate the list
            }
        }
    }
}
