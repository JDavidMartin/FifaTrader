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
		public void BuildSearchUrlTakesParametersAndBuildsExpectedUrl()
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
	}

}
