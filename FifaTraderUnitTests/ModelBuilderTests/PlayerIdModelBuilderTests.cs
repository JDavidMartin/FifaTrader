using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class PlayerIdModelBuilderTests
    {
        private IPlayerIdModelBuilder _playerIdModelBuilder;
        [SetUp]
        public void Setup()
        {
            _playerIdModelBuilder = new PlayerIdModelBuilder();
        }
        
        [Test]
        public void ReadPlayerStoreReturnsListOfPlayerIdModel()
        {
            var actual = _playerIdModelBuilder.ReadStoredPlayers();

            Assert.IsInstanceOf<List<PlayerIdModel>>(actual);
        }
    }
}
