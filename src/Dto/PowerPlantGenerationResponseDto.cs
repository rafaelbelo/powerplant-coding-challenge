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

        [JsonProperty(PropertyName = "p")]
        public double Power { get; set; }
    }
}
