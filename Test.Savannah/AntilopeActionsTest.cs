using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AntilopeActionsTest {
        private Antilope antilope;
        private Board board;
        private Gameplay gameplay;
        private AntilopeActions antilopeActions;

        [SetUp]
        public void SetUp() {
            antilope = new Antilope();
            antilopeActions = new AntilopeActions();
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            board = new Board();
            board.Create();
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
            Assert.IsTrue(antilopeActions.TryToRunAway(gameplay.Animals, board, newAntilope));
        }

        [Test]
        public void TryToRunAway_DoesNotRunAway_ReturnsFalse() {
            Assert.IsFalse(antilopeActions.TryToRunAway(gameplay.Animals, board, antilope));
        }
    }
}