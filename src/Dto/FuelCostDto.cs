using Newtonsoft.Json;

namespace PowerCalculator.Dto
{
    public class FuelCostDto
    {
        [JsonProperty(PropertyName = @"gas(euro/MWh)")]
        public double GasCost { get; set; }

        [JsonProperty(PropertyName = @"kerosine(euro/MWh)")]
        public double KersosineCost { get; set; }

        [JsonProperty(PropertyName = @"co2(euro/ton)")]
        public double Co2 { get; set; }

        [JsonProperty(PropertyName = @"wind(%)")]
        public double WindEfficiency { get; set; }

        [JsonProperty(PropertyName = @"co2(ton/MWh)")]
        public double CO2TonPerMw { get; set; }
    }
}
