using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.GetRequests;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

namespace FifaTraderUnitTests.GatewayTests.GetTests
{

	[TestFixture]
	public class GetRequestHandlerTests
	{
		private IGetRequestHandler _getRequestHandler;
		private IGetRequestMaker _getRequestMaker;
		private IUrlBuilder _urlBuilder;
		[SetUp]
		public void Setup()
		{
			_getRequestMaker = A.Fake<IGetRequestMaker>();
			_urlBuilder = A.Fake<IUrlBuilder>();
			_getRequestHandler = new GetRequestHandler(_getRequestMaker, _urlBuilder);
		}

		[Test]
		public async Task SearchForSpecificPlayerBuildUrlCallsRequestMakerAndReturnsListSearchViewAsync()
		{
			// Arrange
			var bidPrice = 1400;
			var playerId = 12345;
			var accessToken = "ABC";
			var expectedPlayerSearch = new List<PlayerSearchModel>();
			var expected = new auctionSearchModel
			{
				AuctionInfo = expectedPlayerSearch
			};
			A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
			A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).Returns(JsonSerializer.Serialize(expected));

			// Act
			var actual = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);

			// Assert
			Assert.IsInstanceOf<List<PlayerSearchModel>>(actual);
			A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
			A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
		}
		[Test]
		public async Task SearchForSpecificPlayerWhereExpiredTokenThrowsExpectedErrorAndReturnsExpectedEmptyListAsync()
		{
			// Arrange
			// Arrange
			var bidPrice = 1400;
			var playerId = 12345;
			var accessToken = "ABC";
			var expectedPlayerSearch = new List<PlayerSearchModel>();
			var expected = new auctionSearchModel
			{
				AuctionInfo = expectedPlayerSearch
			};
			A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
			A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).Throws(new HttpRequestException());

			// Act
			var actual = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);

			// Assert
			Assert.IsInstanceOf<List<PlayerSearchModel>>(actual);
			A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
			A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
			Assert.AreEqual(0, actual.Count);
		}
	}
}
