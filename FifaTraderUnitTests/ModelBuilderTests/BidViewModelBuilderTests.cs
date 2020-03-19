using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace FifaTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class BidViewModelBuilderTests
    {
        private IBidViewModelBuilder _bidViewModelBuilder;
        [SetUp]
        public void Setup()
        {
            _bidViewModelBuilder = new BidViewModelBuilder();
        }

        [Test]
        public void ConvertSearchModelToBidViewTakesSearchModelAndReturnsListOfBidViewModel()
        {
            // Arrange
            var initial = new List<PlayerSearchModel>
            {
                new PlayerSearchModel
                {
                    TimeRemaining=123,
                    TradeId="12345"
                }
            };

            var expected = new List<BidViewModel>
            {
                new BidViewModel
                {
                    TimeRemaining=123,
                    TradeId="12345",
                    Pending=true,
                    Status="Pending"
                }
            };

            // Act
            var actual = _bidViewModelBuilder.ConvertSearchModelToBidView(initial);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ConverSearchModelToBidViewWhereEmptyListReturnsOneExpectedItem()
        {
            // Arrange
            var initial = new List<PlayerSearchModel>();
            var expected = new List<BidViewModel>
            {
                new BidViewModel
                {
                    Status="Error",
                    Pending=false
                }
            };

            // Act
            var actual = _bidViewModelBuilder.ConvertSearchModelToBidView(initial);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PopulateDefaultFieldsTakesListOfBidViewAndReturnsCorrectModel()
        {
            //Arrange
            var initial = new List<BidViewModel>
            {
                new BidViewModel
                {
                    TimeRemaining=-1,
                    Status = "Outbid",
                    Pending = true
                },
                new BidViewModel
                {
                    TimeRemaining=10,
                    Status="Winning",
                    Pending = true
                }
            };
            var expected = new List<BidViewModel>
            {
                new BidViewModel
                {
                    TimeRemaining=-1,
                    Status = "Outbid",
                    Pending = false
                },
                new BidViewModel
                {
                    TimeRemaining=10,
                    Status="Winning",
                    Pending = true
                }
            };

            //Act
            var actual = _bidViewModelBuilder.PopulateDefaultFieldsOfBidViews(initial);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
