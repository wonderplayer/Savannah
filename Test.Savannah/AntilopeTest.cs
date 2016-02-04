using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AntilopeTest {
        private Antilope antilope;
        private Board board;
        private Gameplay gameplay;

        [SetUp]
        public void SetUp() {
            antilope = new Antilope();
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            board = new Board();
            board.Create();
        }

        [Test]
        public void Antilope_CanCreateNewAntilope_Can() {
            Assert.AreEqual(150, antilope.HitPoints);
        }

        [Test]
        public void TryToRunAway_RunsAwayFromLion_ReturnsTrue() {
            var newAntilope = new Antilope {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 3,
                PositionOnYAxis = 3
            };
            var newLion = new Lion {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 4,
                PositionOnYAxis = 4
            };
            gameplay.Animals.Add(newAntilope);
            gameplay.Animals.Add(newLion);
            Assert.IsTrue(newAntilope.TryToRunAway(gameplay.Animals, board));
        }

        [Test]
        public void TryToRunAway_DoesNotRunAway_ReturnsFalse() {
            Assert.IsFalse(antilope.TryToRunAway(gameplay.Animals, board));
        }
    }
}