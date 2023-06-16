namespace PowerCalculator.Dto
{
    public class PowerPlantDto
    {
        public PowerPlantDto(string name, string type, double efficiency, double pmin, double pmax)
        {
            Name = name;
            Type = type;
            Efficiency = efficiency;
            PMin = pmin;
            PMax = pmax;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public double Efficiency { get; set; }
        public double PMin { get; set; }
        public double PMax { get; set; }
    }
}
