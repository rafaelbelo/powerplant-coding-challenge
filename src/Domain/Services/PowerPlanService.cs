using PowerCalculator.Domain.Models;
using PowerCalculator.Dto;

namespace PowerCalculator.Domain.Services
{
    public class PowerPlanService : IPowerPlanService
    {
        public List<PowerPlant> CreateForecastLoadProfile(ProductionPlanDto plan)
        {
            List<PowerPlant> plantList = CreatePowerPlantDomainListOrderedByCost(plan);

            var surplusLoad = plan.Load;
            for (int i = 0; i < plantList.Count && surplusLoad > 0d; i++)
            {
                if (surplusLoad < plantList[i].Pmin)
                {
                    var amountNeededToReachLastPlantPmin = plantList[i].Pmin - surplusLoad;

                    for (int j = i - 1; j == 0 && amountNeededToReachLastPlantPmin > 0d; j--)
                    {
                        var (subtracted, excess) = plantList[j].SubtractMwhToGenerateForPlan(amountNeededToReachLastPlantPmin);
                        surplusLoad += subtracted;
                        amountNeededToReachLastPlantPmin = excess;
                    }

                    if (surplusLoad < plantList[i].Pmin)
                    {
                        continue;
                    }
                }
                surplusLoad = plantList[i].AddMwhToGenerateForPlan(surplusLoad);
            }
            return plantList;
        }

        private List<PowerPlant> CreatePowerPlantDomainListOrderedByCost(ProductionPlanDto plan)
        {
            List<PowerPlant> plantList = new();

            foreach (var plant in plan.PowerPlants)
            {
                PowerPlant domainPlant = new(plant.Name, plant.Type, plant.Efficiency, plant.PMin, plant.PMax, plan.Fuels);
                plantList.Add(domainPlant);
            }
            return (plantList.Where(p => p.MwhAbleToGenerate > 0d).OrderBy(p => p.CostPerMwh)
                .Concat(plantList.Where(p => p.MwhAbleToGenerate == 0d).OrderBy(p => p.CostPerMwh))).ToList();
        }
    }
}
