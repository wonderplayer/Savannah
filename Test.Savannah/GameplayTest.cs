using System;
using System.Collections.Generic;
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
        public void AddAnima_AddsAnimalToSamePosition_ThrowsException() {
            var lion = new Lion {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 2,
                PositionOnYAxis = 2
            };
            var lion2 = new Lion {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 2,
                PositionOnYAxis = 2
            };
            gameplay.AddAnimalToList(lion);
            Assert.Throws<Exception>(() => gameplay.AddAnimalToList(lion2));
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