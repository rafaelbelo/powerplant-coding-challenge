namespace PowerCalculator.Dto
{
    public class ProductionPlanDto
    {
        public ProductionPlanDto(double load, FuelCostDto fuels, List<PowerPlantDto> powerplants)
        {
            Load = load;
            Fuels = fuels;
            PowerPlants = powerplants;
        }

        public double Load { get; set; }
        public FuelCostDto Fuels { get; set; }
        public List<PowerPlantDto> PowerPlants { get; set; }
    }
}
