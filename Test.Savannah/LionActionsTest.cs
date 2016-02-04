using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class LionActionsTest {
        private Lion lion;
        private Gameplay gameplay;
        private LionActions lionActions;

        [SetUp]
        public void Setup() {
            lion = new Lion();
            lionActions = new LionActions();
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
        }

        [Test]
        public void TryToEat_NotFullHealthLionEatsOneAntilope_ReturnsTrue() {
            var newLion = new Lion {
                HitPoints = 10,
                Name = "Lion",
                PositionOnXAxis = 2,
                PositionOnYAxis = 2
            };
            var antilopeToEat = new Antilope {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 1,
                PositionOnYAxis = 2
            };
            gameplay.Animals.Add(newLion);
            gameplay.Animals.Add(antilopeToEat);
            Assert.IsTrue(lionActions.TryToEat(gameplay.Animals, newLion));
            Assert.AreEqual(100, newLion.HitPoints);
            Assert.AreEqual(0, antilopeToEat.HitPoints);
        }

        [Test]
        public void TryToEat_NoAntilopesAround_ReturnsFalse() {
            gameplay.Animals.Add(lion);
            Assert.IsFalse(lionActions.TryToEat(gameplay.Animals, lion));
        }
    }
}