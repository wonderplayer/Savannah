using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class MovingTest {
        private Gameplay gameplay;
        private Board boardManager;
        private Moving moving;

        [SetUp]
        public void SetUp() {
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            moving = new Moving();
        }

        [Test]
        public void Move_MovesToFreePlace_Correct() {
            CreateGame();
            moving.Move(boardManager, gameplay.Animals);
            Assert.IsTrue(MovedToFreeSpace());
        }

        [Test]
        public void Move_CanMove_Can() {
            CreateGame();
            int previousX = gameplay.Animals[0].PositionOnXAxis;
            int previousY = gameplay.Animals[0].PositionOnYAxis;
            moving.Move(boardManager, gameplay.Animals);
            Assert.IsFalse(DidNotMove(previousX, previousY));
        }

        private bool DidNotMove(int previousX, int previousY) {
            return gameplay.Animals[0].PositionOnXAxis == previousX && gameplay.Animals[0].PositionOnYAxis == previousY;
        }

        private bool MovedToFreeSpace() {
            int firstAnimalX = gameplay.Animals[0].PositionOnXAxis;
            int firstAnimalY = gameplay.Animals[0].PositionOnYAxis;
            int secondAnimalX = gameplay.Animals[1].PositionOnXAxis;
            int secondAnimalY = gameplay.Animals[1].PositionOnYAxis;
            return firstAnimalX != secondAnimalX && firstAnimalY != secondAnimalY;
        }

        private void CreateGame() {
            boardManager = new Board();
            boardManager.Create();
            gameplay.AddAnimal(new Antilope());
            gameplay.AddAnimal(new Antilope());
        }
    }
}