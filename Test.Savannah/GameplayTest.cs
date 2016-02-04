using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class GameplayTest {
        private Gameplay gameplay;

        [SetUp]
        public void SetUp() {
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
        }

        [Test]
        public void AddAnimal_AddsAnimalToDifferentPosition_Can() {
            AddLionsOnDifferentLocations(gameplay.Animals);
            Assert.AreNotEqual(gameplay.Animals[0].PositionOnXAxis, gameplay.Animals[1].PositionOnXAxis);
            Assert.AreNotEqual(gameplay.Animals[0].PositionOnYAxis, gameplay.Animals[1].PositionOnYAxis);
        }

        [Test]
        public void LookAround_FindsAllAnimalsAround_FindsOne() {
            AddLionsOnDifferentLocations(gameplay.Animals);
            IEnumerable<IAnimal> foundAnimals = gameplay.LookAround(gameplay.Animals, gameplay.Animals[0]);
            Assert.AreEqual(1, foundAnimals.Count());
        }

        private void AddLionsOnDifferentLocations(List<IAnimal> animals) {
            var lion = new Lion {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 2,
                PositionOnYAxis = 2
            };
            var lion2 = new Lion {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 3,
                PositionOnYAxis = 3
            };
            animals.Add(lion);
            animals.Add(lion2);
        }
    }
}