using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AnimalActionsTest {
        private AnimalActions animalAction;
        private Gameplay gp;
        private BoardManager boardManager;

        [SetUp]
        public void SetUp() {
            animalAction = new AnimalActions();
            gp = new Gameplay
            {
                Animals = new List<IAnimal>()
            };
        }

        [Test]
        public void Move_MovesToCorrectPlace_Correct() {
            CreateGame();
            animalAction.Move(boardManager,gp.Animals);
            Assert.AreNotEqual(gp.Animals[0].PositionOnXAxis, gp.Animals[1].PositionOnXAxis);
        }

        [Test]
        public void Move_CanMove_Can() {
            CreateGame();
            int previousX = gp.Animals[0].PositionOnXAxis;
            animalAction.Move(boardManager,gp.Animals);
            Assert.AreNotEqual(previousX, gp.Animals[0].PositionOnXAxis);
        }

        [Test]
        public void Die_IsAnimalRemovedFromList_Yes() {
            var deadLion = new Lion() {
                HitPoints = 0
            };
            gp.AddAnimal(deadLion);
            gp.AddAnimal(new Lion());
            animalAction.Die(gp.Animals);
            Assert.IsFalse(gp.Animals.Contains(deadLion));
        }

        [Test]
        public void Die_TryToRemoveAliveAnimals_CannotRemove() {  
            var lionToRemove = new Lion()
            {
                HitPoints = 10
            };
            gp.AddAnimal(lionToRemove);
            gp.AddAnimal(new Lion());
            animalAction.Die(gp.Animals);
            Assert.IsTrue(gp.Animals.Contains(lionToRemove));
        }

        private void CreateGame() {
            boardManager = new BoardManager();
            boardManager.CreateBoard();
            gp.AddAnimal(new Lion());
            gp.AddAnimal(new Antilope());
        }
    }
}