using FakeItEasy;
using FifaTrader.APIHandler;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FifaTraderUnitTests.GatewayTests
{

    [TestFixture]
    public class ApiGatewayTests
    {
        private IApiGateway _ApiGateway;
        private IGetRequestHandler _getRequestHandler;
        private IBidViewModelBuilder _modelBuilder;

        [SetUp]
        public void Setup()
        {
            _getRequestHandler = A.Fake<IGetRequestHandler>();
            _modelBuilder = A.Fake<IBidViewModelBuilder>();
            _ApiGateway = new ApiGateway(_getRequestHandler, _modelBuilder);
        }

        [Test]
        public async Task FetchPlayersCallsGetHandlerSuccessfullyAndReturnsListOfBidViewModelAsync()
        {
            // Arrange
            var bidPrice = 1400;
            var playerId = 12345;
            var accessToken = "ABC";
            var searchView = new List<PlayerSearchModel>
            {
                new PlayerSearchModel
                {
                    CurrentPrice=bidPrice-100,
                    TimeRemaining=123,
                    TradeId=12345
                },
                new PlayerSearchModel
                {
                    CurrentPrice=bidPrice-100,
                    TimeRemaining=1234,
                    TradeId=12345789
                }
            };
            var buildView = new List<BidViewModel>
            {
                new BidViewModel
                {
                    Pending=true
                },
                 new BidViewModel
                {
                     Pending=true
                }
            };

            A.CallTo(() => _getRequestHandler.SearchForSpecificPlayer(12345, 1400, "ABC"))
                .Returns(searchView);
            A.CallTo(() => _modelBuilder.ConvertSearchModelToBidView(searchView)).Returns(buildView);

            // Act
            var actual = await _ApiGateway.FetchPlayers(playerId, bidPrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<BidViewModel>>(actual);
            Assert.AreEqual(2, actual.Count);
        }
    }
}
