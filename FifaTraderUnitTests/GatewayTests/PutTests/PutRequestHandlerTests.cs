using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.PutRequests;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FifaTraderUnitTests.GatewayTests.PutTests
{
    [TestFixture]
    public class PutRequestHandlerTests
    {
        private IPutRequestHandler _putRequestHandler;
        private IPutRequestMaker _putRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;
        [SetUp]
        public void Setup()
        {
            _urlBuilder = A.Fake<IUrlBuilder>();
            _putRequestMaker = A.Fake<IPutRequestMaker>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();
            _putRequestHandler = new PutRequestHandler(_urlBuilder, _putRequestMaker, _statusCodeHandler);
        }

        [Test]
        public async Task PutBidOnPlayerCallsUrlBuilderAndRequestMakerSuccessfullyAndReturnsString()
        {
            //Arrange
            var tradeId = "12344";
            var bidPrice = 12345;
            var accessToken = "ABC";
            A.CallTo(() => _urlBuilder.BuildBidUrl(tradeId)).Returns("DaveUrl");
            A.CallTo(() => _putRequestMaker.PutBidOnPlayer("DaveUrl", accessToken, bidPrice))
                .Returns(HttpStatusCode.OK);
            A.CallTo(() => _statusCodeHandler.HandleBiddingStatusCode(HttpStatusCode.OK)).Returns("Success");

            //Act
            var actual = await _putRequestHandler.PutBidOnPlayer(tradeId, bidPrice, accessToken);

            //Assert
            Assert.AreEqual("Success", actual);
        }

        [Test]
        public async Task PutBidOnPlayerCallsUrlBuilderAndRequestMakerFailsAndReturnsString()
        {
            //Arrange
            var tradeId = "12344";
            var bidPrice = 12345;
            var accessToken = "ABC";
            A.CallTo(() => _urlBuilder.BuildBidUrl(tradeId)).Returns("DaveUrl");
            A.CallTo(() => _putRequestMaker.PutBidOnPlayer("DaveUrl", accessToken, bidPrice))
                .Returns(HttpStatusCode.Unauthorized);
            A.CallTo(() => _statusCodeHandler.HandleBiddingStatusCode(HttpStatusCode.Unauthorized)).Returns("Expired");

            //Act
            var actual = await _putRequestHandler.PutBidOnPlayer(tradeId, bidPrice, accessToken);

            //Assert
            Assert.AreEqual("Expired", actual);
        }

        [Test]
        public async Task MovePlayerToTradePileBuildsModelAndCallsPutMakerThenReturnsString()
        {
            //Arrange
            var accessToken = "Abc";
            var playerId = "123";
            var tradeId = "321";
            var url = "itemUrl";
            var expectedModel = new MovePlayerBodyModel
            {
                ItemData = new List<MovePlayerDataModel>
                {
                    new MovePlayerDataModel
                    {
                        Pile = "trade",
                        PlayerId = "123",
                        tradeId = "321"
                    }
                }
            };

            A.CallTo(() => _urlBuilder.GetItemUrl()).Returns(url);
            A.CallTo(() => _putRequestMaker.MovePlayerToTradePile(url, accessToken, A<MovePlayerBodyModel>.Ignored))
                .Returns(HttpStatusCode.OK);

            //Act
            var actual = await _putRequestHandler.MovePlayerToTradePile(tradeId, playerId, accessToken);

            //Assert
            actual.Should().Be(HttpStatusCode.OK);
        }
    }
}
