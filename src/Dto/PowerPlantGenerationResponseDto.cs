using Newtonsoft.Json;

namespace PowerCalculator.Dto
{
    public class PowerPlantGenerationResponseDto
    {
        public PowerPlantGenerationResponseDto(string name, double power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; set; }

        // Should be round to .1
        [JsonProperty(PropertyName = "p")]
        public double Power { get; set; }
    }
}
