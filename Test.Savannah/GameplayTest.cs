using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class GameplayTest {
        private Gameplay gameplay;
        private Board boardManager;

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
        public void Move_MovesToCorrectPlace_Correct() {
            CreateGame();
            gameplay.Move(boardManager, gameplay.Animals);
            Assert.AreNotEqual(gameplay.Animals[0].PositionOnXAxis, gameplay.Animals[1].PositionOnXAxis);
        }

        [Test]
        public void Move_CanMove_Can() {
            CreateGame();
            int previousX = gameplay.Animals[0].PositionOnXAxis;
            int previousY = gameplay.Animals[0].PositionOnYAxis;
            gameplay.Move(boardManager, gameplay.Animals);
            Assert.IsFalse(DidNotMove(previousX, previousY));
        }

        [Test]
        public void Die_IsAnimalRemovedFromList_Yes() {
            var deadLion = new Lion {
                HitPoints = 0
            };
            gameplay.AddAnimal(deadLion);
            gameplay.AddAnimal(new Lion());
            gameplay.Die(gameplay.Animals);
            Assert.IsFalse(gameplay.Animals.Contains(deadLion));
        }

        [Test]
        public void Die_TryToRemoveAliveAnimals_CannotRemove() {
            var lionToRemove = new Lion {
                HitPoints = 10
            };
            gameplay.AddAnimal(lionToRemove);
            gameplay.AddAnimal(new Lion());
            gameplay.Die(gameplay.Animals);
            Assert.IsTrue(gameplay.Animals.Contains(lionToRemove));
        }

        private void CreateGame() {
            boardManager = new Board();
            boardManager.Create();
            gameplay.AddAnimal(new Lion());
            gameplay.AddAnimal(new Antilope());
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

        private bool DidNotMove(int previousX, int previousY) {
            return gameplay.Animals[0].PositionOnXAxis == previousX && gameplay.Animals[0].PositionOnYAxis == previousY;
        }
    }
}