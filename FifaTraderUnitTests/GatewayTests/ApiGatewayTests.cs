using FakeItEasy;
using FifaTrader.APIHandler;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace FifaTraderUnitTests.GatewayTests
{

    [TestFixture]
    public class ApiGatewayTests
    {
        private IApiGateway _ApiGateway;
        private IGetRequestHandler _getRequestHandler;
        private IBidViewModelBuilder _modelBuilder;
        private IPutRequestHandler _putRequestHandler;
        private IPostRequestHandler _postRequestHandler;
        private IDeleteHandler _deleteHandler;

        [SetUp]
        public void Setup()
        {
            _getRequestHandler = A.Fake<IGetRequestHandler>();
            _modelBuilder = A.Fake<IBidViewModelBuilder>();
            _putRequestHandler = A.Fake<IPutRequestHandler>();
            _postRequestHandler = A.Fake<IPostRequestHandler>();
            _deleteHandler = A.Fake<IDeleteHandler>();
            _ApiGateway = new ApiGateway(_getRequestHandler, _modelBuilder, _putRequestHandler,
                _postRequestHandler, _deleteHandler);
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
                    TimeRemaining=123,
                    TradeId="12345"
                },
                new PlayerSearchModel
                {
                    TimeRemaining=1234,
                    TradeId="12345789"
                }
            };
            var bidView = new List<BidViewModel>
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
                .Returns(bidView);
            //A.CallTo(() => _modelBuilder.ConvertSearchModelToBidView(searchView)).Returns(buildView);

            // Act
            var actual = await _ApiGateway.FetchPlayers(playerId, bidPrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<BidViewModel>>(actual);
            Assert.AreEqual(2, actual.Count);
        }

        [Test]
        public async Task BidOnPlayerCallsRequestHandlerAndReturnsABidViewModelAsync()
        {
            //Arrange
            var bidPrice = 1400;
            var tradeId = "12345";
            var accessToken = "ABC";
            var expected = "Success";
            A.CallTo(() => _putRequestHandler.PutBidOnPlayer(tradeId, bidPrice, accessToken))
                .Returns(expected);

            //Act
            var actual = await _ApiGateway.BidOnPlayer(tradeId, bidPrice, accessToken);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => _putRequestHandler.PutBidOnPlayer(tradeId, bidPrice, accessToken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetTransferTargetsCallsGetHandlerAndReturnsListOfBidViewModelAsync()
        {
            //Arrange
            var accessToken = "ABC";
            var initial = new auctionSearchModel
            {
                AuctionInfo = new List<BidViewModel>
                {
                    new BidViewModel
                    {
                        Status="Pending",
                        BidPrice=1300,
                        Pending=true,
                        TimeRemaining=123,
                        TradeId="123"
                    },
                    new BidViewModel
                    {
                        Status="Pending",
                        BidPrice=1300,
                        Pending=true,
                        TimeRemaining=-1,
                        TradeId="123"
                    }
                }
            };

            var expected = new auctionSearchModel
            {
                AuctionInfo = new List<BidViewModel>
                {
                    new BidViewModel
                    {
                        Status="Pending",
                        BidPrice=1300,
                        Pending=true,
                        TimeRemaining=123,
                        TradeId="123"
                    },
                    new BidViewModel
                    {
                        Status="Pending",
                        BidPrice=1300,
                        Pending=false,
                        TimeRemaining=-1,
                        TradeId="123"
                    }
                }
            };

            A.CallTo(() => _getRequestHandler.GetTransferTargets(accessToken))
                .Returns(initial);

            A.CallTo(() => _modelBuilder.PopulateDefaultFieldsOfBidViews(initial.AuctionInfo))
                .Returns(expected.AuctionInfo);

            //Act
            var actual = await _ApiGateway.GetTransferTargets(accessToken);

            //Assert
            A.CallTo(() => _getRequestHandler.GetTransferTargets(accessToken)).MustHaveHappenedOnceExactly();

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task SellPlayerCallsPutHandlerReturns200ThenCallsPostHandlerAndReturnsSellingAsync()
        {
            //Arrange
            var token = "ABC";
            var startPrice = 1100;
            var binPrice = 1200;
            var playerId = "12345";
            var tradeId = "54321";

            A.CallTo(() => _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, token))
                .Returns(HttpStatusCode.OK);
            A.CallTo(() => _postRequestHandler.SellPlayer(playerId, token, startPrice, binPrice))
                .Returns("Selling");

            //Act
            var actual = await _ApiGateway.SellPlayer(tradeId, playerId, token, startPrice, binPrice);

            //Assert
            Assert.AreEqual("Selling", actual);
            A.CallTo(() => _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, token)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestHandler.SellPlayer(playerId, token, startPrice, binPrice)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task SellPlayerCallsPutHandlerReturns400ErrorAsync()
        {
            //Arrange
            var token = "ABC";
            var startPrice = 1100;
            var binPrice = 1200;
            var playerId = "12345";
            var tradeId = "54321";

            A.CallTo(() => _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, token))
                .Returns(HttpStatusCode.BadRequest);

            //Act
            var actual = await _ApiGateway.SellPlayer(tradeId, playerId, token, startPrice, binPrice);

            //Assert
            Assert.AreEqual("Error", actual);
            A.CallTo(() => _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, token)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestHandler.SellPlayer(playerId, token, startPrice, binPrice)).MustNotHaveHappened();
        }

        [Test]
        public async Task CheckTokenCallsHttpHandlerAndReturnsAStringAsync()
        {
            //Arrange
            var token = "ABC";
            A.CallTo(() => _getRequestHandler.CheckToken(token)).Returns("Valid");

            //Act
            var actual = await _ApiGateway.CheckToken(token);

            //Assert
            Assert.AreEqual("Valid", actual);
        }

        [Test]
        public void ClearExpiredPlayersCallsDeleteHandlerAndDoesNotThrowError()
        {
            //Arrange
            var token = "ABC";
            var players = new List<BidViewModel>();

            //Act

            //Assert
            Assert.DoesNotThrow(() => _ApiGateway.ClearExpiredPlayers(token, players));
        }
    }
}
