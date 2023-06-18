using FluentAssertions;
using Moq.AutoMock;
using PowerCalculator.Domain.Services;
using PowerCalculator.Dto;
using System.Collections.Generic;
using Xunit;

namespace PowerCalculatorTests.Domain.Services
{
    public class PowerPlanServiceTests
    {
        // Tests named as Given_when_then
        // Order of given: Efficiency, Cost, when applicable

        [Fact]
        public void CostWindZeroAndGasMedium_CreateForecastLoadProfile_WindGeneratesItsMaxAndGasTheRestAndOilIsOff()
        {
            // Arrange - given
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<PowerPlanService>();

            var fuel = new FuelCostDto() { Co2 = 20d, GasCost = 13.4d, KersosineCost = 50.8d, WindEfficiency = 60d };
            var powerPlants = new List<PowerPlantDto>
            {
                new PowerPlantDto("gas1", "gasfired", 1d, 100d, 400d),
                new PowerPlantDto("oil1", "turbojet", 1d, 100d, 400d),
                new PowerPlantDto("wind1", "windturbine", 1d, 0d, 400d)
            };
            var load = 400d;
            var plan = new ProductionPlanDto(load, fuel, powerPlants);

            // Act - when
            var result = service.CreateForecastLoadProfile(plan);

            // Assert - then
            result[0].Name.Should().Be("wind1");
            result[0].MwhToGenerateForPlan.Should().Be(result[0].MwhAbleToGenerate);
            result[1].Name.Should().Be("gas1");
            result[1].MwhToGenerateForPlan.Should().Be(load - result[0].MwhToGenerateForPlan);
            result[2].MwhToGenerateForPlan.Should().Be(0d);
        }

        [Fact]
        public void EfficiencyWindZeroAndCostGasBetter_CreateForecastLoadProfile_GasGeneratesMaxAndOilTheRestAndWindIsOff()
        {
            // Arrange - given
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<PowerPlanService>();

            var fuel = new FuelCostDto() { Co2 = 20d, GasCost = 13.4d, KersosineCost = 50.8d, WindEfficiency = 0d };
            var powerPlants = new List<PowerPlantDto>
            {
                new PowerPlantDto("gas1", "gasfired", 0.53d, 100d, 400d),
                new PowerPlantDto("oil1", "turbojet", 0.3d, 100d, 400d),
                new PowerPlantDto("wind1", "windturbine", 1d, 0d, 400d)
            };
            var load = 400d;
            var plan = new ProductionPlanDto(load, fuel, powerPlants);

            // Act - when
            var result = service.CreateForecastLoadProfile(plan);

            // Assert - then
            result[0].Name.Should().Be("gas1");
            result[0].MwhToGenerateForPlan.Should().Be(result[0].MwhAbleToGenerate);
            result[1].Name.Should().Be("oil1");
            result[1].MwhToGenerateForPlan.Should().Be(load - result[0].MwhToGenerateForPlan);
            result[2].MwhToGenerateForPlan.Should().Be(0d);
        }
    }
}
