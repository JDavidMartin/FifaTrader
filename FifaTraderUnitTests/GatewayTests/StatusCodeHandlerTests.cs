using FifaTrader.APIHandler.HttpHandlers;
using FifaTrader.APIHandler.Interfaces;
using NUnit.Framework;
using System.Net;

namespace FifaTraderUnitTests.GatewayTests
{
    [TestFixture]
    public class StatusCodeHandlerTests
    {
        private IStatusCodeHandler _statusCodeHandler;

        [SetUp]
        public void Setup()
        {
            _statusCodeHandler = new StatusCodeHandler();
        }

        [Test]
        public void OkStatusCodeReturnsSuccess()
        {
            //Arrange
            var code = HttpStatusCode.OK;

            //Act
            var actual = _statusCodeHandler.HandleBiddingStatusCode(code);

            //Assert
            Assert.AreEqual("Success", actual);
        }

        [Test]
        public void TooManyRequestsStatusCodeReturnsTooManyRequests()
        {
            //Arrange
            var code = HttpStatusCode.TooManyRequests;

            //Act
            var actual = _statusCodeHandler.HandleBiddingStatusCode(code);

            //Assert
            Assert.AreEqual("TooManyRequests", actual);
        }

        [Test]
        public void HandleTokenCheckStatusCode200ReturnsValid()
        {
            //Arrange
            var status = HttpStatusCode.OK;

            //Act
            var actual = _statusCodeHandler.HandleTokenCheckStatusCode(status);

            //Assert
            Assert.AreEqual("Valid", actual);
        }

        [Test]
        public void HandleTokenCheckStatusNot200ReturnsInvalid()
        {
            //Arrange
            var status = HttpStatusCode.Unauthorized;

            //Act
            var actual = _statusCodeHandler.HandleTokenCheckStatusCode(status);

            //Assert
            Assert.AreEqual("Invalid", actual);
        }
    }
}
