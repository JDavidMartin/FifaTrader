﻿using FifaTrader.APIHandler.HttpHandlers.UrlBuilder;
using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models.EnvVariables;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace FifaTraderUnitTests.GatewayTests
{
    [TestFixture]
	public class UrlBuilderTests
	{
		private IUrlBuilder _urlBuilder;
		private string _fifaYear;
		[SetUp]
		public void Setup()
		{
			_fifaYear = "fifa22";

			var myOptions = Options.Create(new FifaYear { Year = _fifaYear});
			
			_urlBuilder = new UrlBuilder(myOptions);
		}

		[Test]
		public void BuildSearchUrlBidMoreThan1000BuildsExpectedUrl()
		{
			// Arrange
			var bidPrice = 1400;
			var playedId = 1234;
			var expected = $"https://utas.mob.v1.fut.ea.com/ut/game/fifa22/transfermarket?type=player&maskedDefId=1234&macr=1300&num=21&start=0";

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
			var expected = "https://utas.mob.v1.fut.ea.com/ut/game/fifa22/transfermarket?type=player&maskedDefId=1234&macr=850&num=21&start=0";

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
			var expected = "https://utas.mob.v1.fut.ea.com/ut/game/fifa22/trade/12345/bid";

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
			var expected = "https://utas.mob.v1.fut.ea.com/ut/game/fifa22/watchlist?tradeId=12345,54321";

			//Act
			var actual = _urlBuilder.BuildDeletePlayerUrl(tradeIds);

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}
