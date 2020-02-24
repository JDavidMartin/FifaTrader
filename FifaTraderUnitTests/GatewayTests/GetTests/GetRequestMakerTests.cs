using FifaTrader.APIHandler.HttpHandlers.GetRequests;
using FifaTrader.APIHandler.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FifaTraderUnitTests.GatewayTests.GetTests
{

	[TestFixture]
	public class GetRequestMakerTests
	{
		private IGetRequestMaker _getRequestMaker;
		[SetUp]
		public void Setup()
		{
			_getRequestMaker = new GetRequestMaker();
		}

		[Test]
		public async Task MakeGetRequestMakesASuccessfulCall()
		{
			// Arrange
			var url = "http://google.co.uk";
			var accessToken = "ABC";

			// Act

			// Assert
			Assert.DoesNotThrowAsync(() => _getRequestMaker.MakeGetRequest(url, accessToken));
		}


		[Test]
		public async Task MakeGetRequestWhichIsUnsuccessfulThrowsExpectedError()
		{
			// Arrange
			var url = "https://fake.url.co.uk";
			var accessToken = "ABC";

			// Act
			// Assert
			var ex = Assert.ThrowsAsync<HttpRequestException>(() => _getRequestMaker.MakeGetRequest(url, accessToken));
		}
	}
}
