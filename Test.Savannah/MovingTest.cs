using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class MovingTest {
        private Gameplay gameplay;
        private Board board;
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
            moving.Move(board, gameplay.Animals);
            Assert.IsTrue(MovedToFreeSpace());
        }

        [Test]
        public void Move_CanMove_Can() {
            CreateGame();
            int previousX = gameplay.Animals[0].PositionOnXAxis;
            int previousY = gameplay.Animals[0].PositionOnYAxis;
            moving.Move(board, gameplay.Animals);
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
            board = new Board();
            board.Create();
            var newAntilope = new Antilope {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 3,
                PositionOnYAxis = 3
            };
            var newAntilope2 = new Antilope {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 4,
                PositionOnYAxis = 4
            };
            gameplay.Animals.Add(newAntilope);
            gameplay.Animals.Add(newAntilope2);
        }
    }
}