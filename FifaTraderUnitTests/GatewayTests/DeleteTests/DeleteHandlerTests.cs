using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.DeleteRequests;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using NUnit.Framework;
using System.Collections.Generic;

namespace FifaTraderUnitTests.GatewayTests.DeleteTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private IDeleteHandler _deleteHandler;
        private IDeleteMaker _deleteMaker;
        private ITradeIdsBuilder _tradeIdsBuilder;
        private IUrlBuilder _urlBuilder;

        [SetUp]
        public void Setup()
        {
            _deleteMaker = A.Fake<IDeleteMaker>();
            _tradeIdsBuilder = A.Fake<ITradeIdsBuilder>();
            _urlBuilder = A.Fake<IUrlBuilder>();
            _deleteHandler = new DeleteHandler(_deleteMaker, _tradeIdsBuilder, _urlBuilder);
        }

        [Test]
        public void DeleteExpiredPlayersCallsDeleteMakerAndDoesNotThrowError()
        {
            //Arrange
            var token = "Abc";
            var players = new List<BidViewModel>();
            var ids = new TradeIdsModel
            {
                TradeIds = "12345"
            };
            A.CallTo(() => _tradeIdsBuilder.GetTradeIds(players)).Returns(ids);
            A.CallTo(() => _urlBuilder.BuildDeletePlayerUrl(ids.TradeIds)).Returns("DeleteUrl");
            //Act

            //Assert
            Assert.DoesNotThrow(() => _deleteHandler.DeleteExpiredPlayers(token, players));
            A.CallTo(() => _deleteMaker.MakeDeleteRequest(token, "DeleteUrl")).MustHaveHappenedOnceExactly();
        }
    }
}
