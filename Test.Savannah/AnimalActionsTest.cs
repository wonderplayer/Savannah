using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AnimalActionsTest {
        private AnimalActions animalFunc;
        private Gameplay gp;
        private BoardManager boardManager;

        [SetUp]
        public void SetUp() {
            animalFunc = new AnimalActions();
        }

        [Test]
        public void Move_MovesToCorrectPlace_Correct() {
            CreateGame();
            animalFunc.Move(boardManager,gp.Animals);
            Assert.AreNotEqual(gp.Animals[0].PositionOnXAxis, gp.Animals[1].PositionOnXAxis);
        }

        [Test]
        public void Move_CanMove_Can() {
            CreateGame();
            int previousX = gp.Animals[0].PositionOnXAxis;
            animalFunc.Move(boardManager,gp.Animals);
            Assert.AreNotEqual(previousX, gp.Animals[0].PositionOnXAxis);
        }

        [Test]
        [Ignore("Method is not complete")]
        public void Die_IsAnimalRemovedFromList_Yes() {
            gp = new Gameplay {
                Animals = new List<IAnimal>()
            };
            var lionToRemove = new Lion();
            gp.AddAnimal(lionToRemove);
            gp.AddAnimal(new Antilope());
            Assert.IsFalse(gp.Animals.Contains(lionToRemove));
        }

        private void CreateGame() {
            boardManager = new BoardManager();
            boardManager.CreateBoard();
            gp = new Gameplay {
                Animals = new List<IAnimal>()
            };
            gp.Animals.Add(new Lion());
            gp.Animals.Add(new Antilope());
        }
    }
}