using FakeItEasy;
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
		private IHttpWrapper _wrapper;
		[SetUp]
		public void Setup()
		{
			_wrapper = A.Fake<IHttpWrapper>();
			_getRequestMaker = new GetRequestMaker(_wrapper);
		}

		[Test]
		public void MakeGetRequestMakesASuccessfulCall()
		{
			// Arrange
			var url = "http://google.co.uk";
			var accessToken = "ABC";
			var expected = new HttpResponseMessage();
			expected.Content = new StringContent("No Exception");
			A.CallTo(() => _wrapper.GetAsync(url))
				.Returns(expected);

			// Act

			// Assert
			Assert.DoesNotThrowAsync(() => _getRequestMaker.MakeGetRequest(url, accessToken));
		}


		[Test]
		public void MakeGetRequestWhichIsUnsuccessfulThrowsExpectedError()
		{
			// Arrange
			var url = "https://fake.url.co.uk";
			var accessToken = "ABC";
			A.CallTo(() => _wrapper.GetAsync(url))
				.Throws(new HttpRequestException());
				
			// Act
			// Assert
			var ex = Assert.ThrowsAsync<HttpRequestException>(() => _getRequestMaker.MakeGetRequest(url, accessToken));
		}

		[Test]
		public async Task MakeGetRequestToReturnStatusCodeReturnsHttpCode()
		{
			//Arrange
			var url = "https://AnyUrl.com";
			var token = "ABC";
			var expectedMessage = new HttpResponseMessage();
			expectedMessage.StatusCode = HttpStatusCode.OK;
			A.CallTo(() => _wrapper.GetAsync(url)).Returns(expectedMessage);

			//Act
			var actual = await _getRequestMaker.MakeGetRequestStatusCode(url, token);

			//Assert
			Assert.AreEqual(HttpStatusCode.OK, actual);
		}
	}
}
