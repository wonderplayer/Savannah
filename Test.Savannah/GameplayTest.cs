using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class GameplayTest {
        private Gameplay gameplay;

        [SetUp]
        public void SetUp() {
            gameplay = new Gameplay();
        }

        [Test]
        public void NewGame_CanCreateNewGame_Can() {
            gameplay.NewGame();
            Assert.IsFalse(gameplay.Lions.Count == 0);
            Assert.IsFalse(gameplay.Antilopes.Count == 0);
        }

        [Test]
        [Ignore("Doesn't work without debugging.")]
        public void AddAntilope_AddTwoAntilopesToSamePosition_Cannot() {
            var antilopes = new List<Antilope>();
            gameplay.AddAntilope(antilopes);
            gameplay.AddAntilope(antilopes);
            Assert.AreNotEqual(antilopes[0].PositionOnXAxis, antilopes[1].PositionOnXAxis);
            Assert.AreNotEqual(antilopes[0].PositionOnYAxis, antilopes[1].PositionOnYAxis);
        }
    }
}