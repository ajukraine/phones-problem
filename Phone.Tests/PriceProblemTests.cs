using FluentAssertions;
using NUnit.Framework;
using Phone.App;

namespace Phone.Tests
{
    public class PriceProblemTests
    {
        [Test]
        public void PriceProblem_Should_Calculate_TotalPrice()
        {
            // Given
            var phones = new APhone[]
            {
                new MobilePhone { Price = 100m },
                new RadioPhone { Price = 150m }
            };

            // When
            var solution = PriceProblem.Solve(phones);

            // Then
            solution.TotalPrice.Should().Be(250m);
        }

        [Test]
        public void PriceProblem_Should_Sort_By_Price()
        {
            // Given
            var phones = new APhone[]
            {
                new RadioPhone { Price = 280m },
                new MobilePhone { Price = 100m },
                new RadioPhone { Price = 150m }
            };

            // When
            var solution = PriceProblem.Solve(phones);

            // Then
            solution.PhonesSortedByPrice.Should().BeInAscendingOrder(p => p.Price);
        }
    }
}