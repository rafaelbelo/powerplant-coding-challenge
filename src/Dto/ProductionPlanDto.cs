namespace PowerCalculator.Dto
{
    public class ProductionPlanDto
    {
        public ProductionPlanDto(int load, FuelCostDto fuels, List<PowerPlantDto> powerPlants)
        {
            Load = load;
            Fuels = fuels;
            PowerPlants = powerPlants;
        }

        public int Load { get; set; }
        public FuelCostDto Fuels { get; set; }
        public List<PowerPlantDto> PowerPlants { get; set; }
    }
}
