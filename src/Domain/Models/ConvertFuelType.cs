namespace PowerCalculator.Domain.Models
{
    public static class ConvertFuelType
    {
        public static FuelType ToDomainFuelType(this string fuel)
        {
            return fuel switch
            {
                "gasfired" => FuelType.Gas,
                "turbojet" => FuelType.Kerosine,
                "windturbine" => FuelType.Wind,
                _ => throw new ArgumentException($"Invalid fuel type: {fuel}")
            };
        }

        public static string ToResponseString(this FuelType energy)
        {
            return energy switch
            {
                FuelType.Gas => "gasfired",
                FuelType.Kerosine => "turbojet",
                FuelType.Wind => "windturbine",
                _ => throw new ArgumentException($"Invalid fuel type: {energy}")
            };
        }
    }
}
