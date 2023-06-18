using FluentAssertions;
using PowerCalculator.Domain.Models;
using PowerCalculator.Dto;
using Xunit;

namespace PowerCalculatorTests.Domain.Models
{
    public class PowerPlantTests
    {
        private FuelCostDto _fuelCostDto = new() { Co2 = 20d, GasCost = 13.4d, KersosineCost = 50.8d, WindEfficiency = 60d };

        [Fact]
        public void PowerPlantGas_CalculateMetricsBasedOnFuel_ShouldBeZero()
        {
            // Arrange and Act
            var powerPlant = new PowerPlant("gas1", "gasfired", 0.5d, 100d, 500d, _fuelCostDto);

            // Assert - then
            powerPlant.MwhAbleToGenerate.Should().Be(500d);
            powerPlant.CostPerMwh.Should().Be(26.8d);
        }

        [Fact]
        public void PowerPlantGasTakesMoreLoadThanPmax_AddMwhToGenerateForPlan_ShouldReturnSurplus()
        {
            // Arrange - given
            var powerPlant = new PowerPlant("gas1", "gasfired", 0.5d, 100d, 500d, _fuelCostDto);
            double mwhToAdd = 600d;

            // Act - when
            var surplus = powerPlant.AddMwhToGenerateForPlan(mwhToAdd);

            // Assert - then
            surplus.Should().Be(100d);
        }

        [Fact]
        public void PowerPlantGasWithNoAmountToGenerate_SubtractMwhToGenerateForPlan_ShouldBeSubtractedZeroAndExcessTheSameRequested()
        {
            // Arrange - given
            var powerPlant = new PowerPlant("gas1", "gasfired", 0.5d, 100d, 500d, _fuelCostDto);
            double mwhToSubtract = 50d;

            // Act - when
            var result = powerPlant.SubtractMwhToGenerateForPlan(mwhToSubtract);

            // Assert - then
            result.subtracted.Should().Be(0d);
            result.excess.Should().Be(mwhToSubtract);
        }
    }
}
