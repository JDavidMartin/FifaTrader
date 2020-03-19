using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.PostRequests;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FifaTraderUnitTests.GatewayTests.PostTests
{
    [TestFixture]
    public class PostRequestHandlerTests
    {
        private IPostRequestHandler _postRequestHandler;
        private IPostRequestMaker _postRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;
        private string _token;
        private string _playerId;
        private int _startPrice;
        private int _binPrice;

        [SetUp]
        public void Setup()
        {
            _urlBuilder = A.Fake<IUrlBuilder>();
            _postRequestMaker = A.Fake<IPostRequestMaker>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();
            _postRequestHandler = new PostRequestHandler(_postRequestMaker, _urlBuilder, _statusCodeHandler);
            _token = "ABC";
            _playerId = "12345";
            _startPrice = 1200;
            _binPrice = 1100;
        }

        [Test]
        public async Task SellPlayerCallsUrlBuilderThenRequestMakerReturns200ThenStatusCodeHandlerReturnsSellingAsync()
        {
            //Arrange
            
            A.CallTo(() => _urlBuilder.GetAuctionUrl()).Returns("Jeff");
            A.CallTo(() => _postRequestMaker.SellPlayer("Jeff", _token, A<SellPlayerModel>.Ignored)).Returns(HttpStatusCode.OK);
            A.CallTo(() => _statusCodeHandler.HandleSellingStatusCode(HttpStatusCode.OK)).Returns("Selling");

            //Act
            var actual = await _postRequestHandler.SellPlayer(_playerId, _token, _startPrice, _binPrice);

            //Assert
            Assert.AreEqual("Selling", actual);
            A.CallTo(() => _urlBuilder.GetAuctionUrl()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestMaker.SellPlayer("Jeff", _token, A<SellPlayerModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _statusCodeHandler.HandleSellingStatusCode(HttpStatusCode.OK)).MustHaveHappenedOnceExactly();
        }
    }
}
