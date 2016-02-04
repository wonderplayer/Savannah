using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AnimalActionsTest {

        private Gameplay gameplay;
        private AnimalActions animalActions;
        private Board board;

        [SetUp]
        public void SetUp() {
            gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            animalActions= new AnimalActions();
        }

        [Test]
        public void LookAround_FindsAllAnimalsAround_FindsOne()
        {
            AddLionsOnDifferentLocations(gameplay.Animals);
            IEnumerable<IAnimal> foundAnimals = animalActions.LookAround(gameplay.Animals, gameplay.Animals[0]);
            Assert.AreEqual(1, foundAnimals.Count());
        }

        [Test]
        public void Move_MovesToFreePlace_Correct()
        {
            CreateGame();
            animalActions.Move(board, gameplay.Animals);
            Assert.IsTrue(MovedToFreeSpace());
        }

        [Test]
        public void Move_CanMove_Can()
        {
            CreateGame();
            int previousX = gameplay.Animals[0].PositionOnXAxis;
            int previousY = gameplay.Animals[0].PositionOnYAxis;
            animalActions.Move(board, gameplay.Animals);
            Assert.IsFalse(DidNotMove(previousX, previousY));
        }

        private bool DidNotMove(int previousX, int previousY)
        {
            return gameplay.Animals[0].PositionOnXAxis == previousX && gameplay.Animals[0].PositionOnYAxis == previousY;
        }

        private bool MovedToFreeSpace()
        {
            int firstAnimalX = gameplay.Animals[0].PositionOnXAxis;
            int firstAnimalY = gameplay.Animals[0].PositionOnYAxis;
            int secondAnimalX = gameplay.Animals[1].PositionOnXAxis;
            int secondAnimalY = gameplay.Animals[1].PositionOnYAxis;
            return firstAnimalX != secondAnimalX && firstAnimalY != secondAnimalY;
        }

        private void CreateGame()
        {
            board = new Board();
            board.Create();
            var newAntilope = new Antilope
            {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 3,
                PositionOnYAxis = 3
            };
            var newAntilope2 = new Antilope
            {
                HitPoints = 150,
                Name = "Antilope",
                PositionOnXAxis = 4,
                PositionOnYAxis = 4
            };
            gameplay.Animals.Add(newAntilope);
            gameplay.Animals.Add(newAntilope2);
        }

        private void AddLionsOnDifferentLocations(List<IAnimal> animals)
        {
            var lion = new Lion
            {
                HitPoints = 100,
                Name = "Lion",
                PositionOnXAxis = 2,
                PositionOnYAxis = 2
            };
            var lion2 = new Lion
            {
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