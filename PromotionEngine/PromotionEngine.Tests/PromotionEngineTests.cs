using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace PromotionEngine.Tests
{
    public class PromotionEngineTests
    {
        private readonly ServiceProvider _serviceProvider;

        public PromotionEngineTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<PromotionEngine>();
            serviceCollection.AddTransient<IPriceRepository, PriceRepository>();
            serviceCollection.AddTransient<IPromotionsRepository, PromotionsRepository>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void PromotionEngine_ScenarioA()
        {
            //Arrange
            var orders = new List<Order>
            {
                new Order {SkuName = "A", Quantity = 1},
                new Order {SkuName = "B", Quantity = 1},
                new Order {SkuName = "C", Quantity = 1}
            };
            var engine = _serviceProvider.GetRequiredService<PromotionEngine>();

            //Act
            var actualTotalOrderValue = engine.Execute(orders);

            //Assert
            Assert.Equal(100, actualTotalOrderValue);
        }

        [Fact]
        public void PromotionEngine_ScenarioB()
        {
            //Arrange
            var orders = new List<Order>
            {
                new Order {SkuName = "A", Quantity = 5},
                new Order {SkuName = "B", Quantity = 5},
                new Order {SkuName = "C", Quantity = 1}
            };
            var engine = _serviceProvider.GetRequiredService<PromotionEngine>();

            //Act
            var actualTotalOrderValue = engine.Execute(orders);

            //Assert
            Assert.Equal(370, actualTotalOrderValue);
        }

        [Fact]
        public void PromotionEngine_ScenarioC()
        {
            //Arrange
            var orders = new List<Order>
            {
                new Order {SkuName = "A", Quantity = 3},
                new Order {SkuName = "B", Quantity = 5},
                new Order {SkuName = "C", Quantity = 1},
                new Order {SkuName = "D", Quantity = 1}
            };
            var engine = _serviceProvider.GetRequiredService<PromotionEngine>();

            //Act
            var actualTotalOrderValue = engine.Execute(orders);

            //Assert
            Assert.Equal(280, actualTotalOrderValue);
        }
    }
}
