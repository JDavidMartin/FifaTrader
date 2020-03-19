using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.GetRequests;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;

namespace FifaTraderUnitTests.GatewayTests.GetTests
{

    [TestFixture]
    public class GetRequestHandlerTests
    {
        private IGetRequestHandler _getRequestHandler;
        private IGetRequestMaker _getRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;

        [SetUp]
        public void Setup()
        {
            _getRequestMaker = A.Fake<IGetRequestMaker>();
            _urlBuilder = A.Fake<IUrlBuilder>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();

            _getRequestHandler = new GetRequestHandler(_getRequestMaker, _urlBuilder, _statusCodeHandler);
        }

        [Test]
        public async Task SearchForSpecificPlayerBuildUrlCallsRequestMakerAndReturnsListSearchViewAsync()
        {
            // Arrange
            var bidPrice = 1400;
            var playerId = 12345;
            var accessToken = "ABC";
            var expectedPlayerSearch = new List<BidViewModel>();
            var expected = new auctionSearchModel
            {
                AuctionInfo = expectedPlayerSearch
            };
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken))
                .Returns(JsonConvert.SerializeObject(expected));

            // Act
            var actual = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<BidViewModel>>(actual);
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public async Task SearchForSpecificPlayerWhereExpiredTokenThrowsExpectedErrorAndReturnsExpectedEmptyListAsync()
        {
            // Arrange
            var bidPrice = 1400;
            var playerId = 12345;
            var accessToken = "ABC";
            var expectedPlayerSearch = new List<BidViewModel>();
            var expected = new auctionSearchModel
            {
                AuctionInfo = expectedPlayerSearch
            };
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).Throws(new HttpRequestException());

            // Act
            var actual = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<BidViewModel>>(actual);
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public async Task GetTransferTargetsGetsUrlMakesRequestThenReturnsListBidViewModel()
        {
            //Arrange
            var accessToken = "ABC";
            var expectedPlayers = new List<BidViewModel>
            {
                new BidViewModel
                {
                    Status="Pending",
                    BidPrice=1400,
                    Pending=true,
                    TimeRemaining=123,
                    TradeId="12234"
                }
            };

            var expected = new auctionSearchModel
            {
                AuctionInfo = expectedPlayers
            };

            A.CallTo(() => _urlBuilder.GetTransferTargetsUrl()).Returns("TargetsUrl");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("TargetsUrl", accessToken))
                .Returns(JsonConvert.SerializeObject(expected));

            //Act
            var actual = await _getRequestHandler.GetTransferTargets(accessToken);

            //Assert
            actual.Should().BeEquivalentTo(expectedPlayers);
            A.CallTo(() => _getRequestMaker.MakeGetRequest("TargetsUrl", accessToken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task CheckUrlMakesRequestWhichReturnsSuccessfullyThenReturnsValid()
        {
            //Arrange
            var token = "ABC";
            var url = "TokenUrl";
            var expectedCode = HttpStatusCode.OK;
            A.CallTo(() => _urlBuilder.GetCheckTokenUrl()).Returns(url);
            A.CallTo(() => _getRequestMaker.MakeGetRequestStatusCode(url, token)).Returns(expectedCode);
            A.CallTo(() => _statusCodeHandler.HandleTokenCheckStatusCode(expectedCode)).Returns("Valid");

            //Act
            var actual = await _getRequestHandler.CheckToken(token);

            //Assert
            Assert.AreEqual("Valid", actual);
        }

        [Test]
        public async Task CheckUrlMakesRequestWhichReturnsNOTSuccesfulThenReturnsInvalid()
        {
            //Arrange
            var token = "ABC";
            var url = "TokenUrl";
            var expectedCode = HttpStatusCode.Unauthorized;
            A.CallTo(() => _urlBuilder.GetCheckTokenUrl()).Returns(url);
            A.CallTo(() => _getRequestMaker.MakeGetRequestStatusCode(url, token)).Returns(expectedCode);
            A.CallTo(() => _statusCodeHandler.HandleTokenCheckStatusCode(expectedCode)).Returns("Invalid");

            //Act
            var actual = await _getRequestHandler.CheckToken(token);

            //Assert
            Assert.AreEqual("Invalid", actual);
        }
    }
}
