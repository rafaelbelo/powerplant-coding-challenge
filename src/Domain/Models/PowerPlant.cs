using PowerCalculator.Dto;

namespace PowerCalculator.Domain.Models
{
    //"powerplants": [
    //{
    //  "name": "gasfiredbig1",
    //  "type": "gasfired",
    //  "efficiency": 0.53,
    //  "pmin": 100,
    //  "pmax": 460
    //},
    public class PowerPlant
    {
        public PowerPlant(string name, string fuelType, double efficiency, double pmin, double pmax, FuelCostDto fuelCostDto)
        {
            Name = name;
            FuelType = fuelType.ToDomainFuelType();
            Efficiency = efficiency;
            Pmin = pmin;
            Pmax = pmax;

            CalculateMetricsBasedOnFuel(fuelCostDto);
        }

        public string Name { get; private set; }
        public FuelType FuelType { get; private set; }
        public double Efficiency { get; private set; }
        public double Pmin { get; private set; }
        public double Pmax { get; private set; }
        public double CostPerMwh { get; private set; }
        public double MwhAbleToGenerate { get; private set; }
        public double MwhToGenerateForPlan { get; private set; }

        public double GetMwhThatCanBeSubtracted() => MwhToGenerateForPlan - Pmin;
        public double AddMwhToGenerateForPlan(double mhwToAdd)
        {
            MwhToGenerateForPlan += mhwToAdd;
            if (MwhToGenerateForPlan > MwhAbleToGenerate)
            {
                var surplus = MwhToGenerateForPlan - MwhAbleToGenerate;
                MwhToGenerateForPlan = MwhAbleToGenerate;
                return surplus;
            }
            return 0d; ;
        }

        public (double subtracted, double excess) SubtractMwhToGenerateForPlan(double mhwToSubtract)
        {
            MwhToGenerateForPlan -= mhwToSubtract;
            if (MwhToGenerateForPlan < Pmin)
            {
                var excess = Pmin - MwhToGenerateForPlan;
                var subtracted = mhwToSubtract - excess;
                MwhToGenerateForPlan = Pmin;
                return (subtracted, excess);
            }
            return (mhwToSubtract, 0d);
        }

        // Relation to the merit order: the lower the cost, the higher priority
        private void CalculateMetricsBasedOnFuel(FuelCostDto fuelCostDto)
        {
            // For the purpose of this exercise, not considering maintenance of any plant, wind costs zero.
            if (FuelType == FuelType.Wind)
            {
                CostPerMwh = 0;
                MwhAbleToGenerate = (fuelCostDto.WindEfficiency / 100) * Pmax;
            }
            else
            {
                MwhAbleToGenerate = Pmax;

                if (FuelType == FuelType.Kerosine)
                {
                    CostPerMwh = fuelCostDto.KersosineCost / Efficiency;

                }
                else if (FuelType == FuelType.Gas)
                {
                    CostPerMwh = fuelCostDto.GasCost / Efficiency;
                }
            }
        }
    }
}
