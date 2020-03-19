using FakeItEasy;
using FifaTrader.APIHandler.HttpHandlers.UrlBuilder;
using FifaTrader.APIHandler.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaTraderUnitTests.GatewayTests
{

	[TestFixture]
	public class UrlBuilderTests
	{
		private IUrlBuilder _urlBuilder;
		[SetUp]
		public void Setup()
		{
			_urlBuilder = new UrlBuilder();
		}

		[Test]
		public void BuildSearchUrlBidMoreThan1000BuildsExpectedUrl()
		{
			// Arrange
			var bidPrice = 1400;
			var playedId = 1234;
			var expected = "https://utas.external.s2.fut.ea.com/ut/game/fifa20/transfermarket?type=player&maskedDefId=1234&macr=1300&num=21&start=0";

			// Act
			var actual = _urlBuilder.BuildSearchUrl(playedId, bidPrice);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildSearchWithBidLessThan1000BuildsExpectedUrl()
		{
			// Arrange
			var bidPrice = 900;
			var playedId = 1234;
			var expected = "https://utas.external.s2.fut.ea.com/ut/game/fifa20/transfermarket?type=player&maskedDefId=1234&macr=850&num=21&start=0";

			// Act
			var actual = _urlBuilder.BuildSearchUrl(playedId, bidPrice);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildBidUrlTakesParametersAndBuildsExpectedUrl()
		{
			//Arrange
			var tradeId = "12345";
			var expected = "https://utas.external.s2.fut.ea.com/ut/game/fifa20/trade/12345/bid";

			//Act
			var actual = _urlBuilder.BuildBidUrl(tradeId);

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildDeleteUrlTakesTradeIdsAndReturnsExpectedUrl()
		{
			//Arrange
			var tradeIds = "12345,54321";
			var expected = "https://utas.external.s2.fut.ea.com/ut/game/fifa20/watchlist?tradeId=12345,54321";

			//Act
			var actual = _urlBuilder.BuildDeletePlayerUrl(tradeIds);

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}
