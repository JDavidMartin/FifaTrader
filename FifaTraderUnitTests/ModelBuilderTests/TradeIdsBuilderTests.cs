using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class TradeIdsBuilderTests
    {
        private ITradeIdsBuilder _tradeIdsBuilder;

        [SetUp]
        public void setup()
        {
            _tradeIdsBuilder = new TradeIdsBuilder();
        }

        [Test]
        public void GetTradeIdsTakesPlayersAndReturnsStringWithIds()
        {
            //Arrange
            var players = new List<BidViewModel>
            {
                new BidViewModel
                {
                    TradeId = "12345",
                    Status="outbid"
                },
                new BidViewModel
                {
                    TradeId = "54321",
                    Status = "outbid"
                },
                new BidViewModel
                {
                    TradeId = "9876",
                    Status = "highest"
                }

            };
            var expected = new TradeIdsModel
            {
                TradeIds = "12345,54321"
            };

            //Act
            var actual = _tradeIdsBuilder.GetTradeIds(players);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
