using System;
using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class BoardTest {
        private BoardManager boardManager;

        [SetUp]
        public void SetUp() {
            boardManager = new BoardManager();
        }

        [Test]
        public void CreateBoard_CanCreateEmptyBoard_Can() {
            boardManager.CreateBoard();
            Assert.IsNotEmpty(boardManager.Board);
        }

        [Test]
        public void ShowBoard_CanShowNotEmptyBoard_Can() {
            boardManager.CreateBoard();
            boardManager.ShowBoard();
        }

        [Test]
        public void ShowBoard_CanShowEmptyBoard_Exception() {
            Assert.Throws<NullReferenceException>(() => boardManager.ShowBoard());
        }

        [Test]
        public void FillBoardWithAnimals_CanFillBoad_Can() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            boardManager.Board = new char[5, 5];
            var emptyField = new char[5, 5];
            boardManager.FillBoardWithAnimals(gameplay.Animals);
            Assert.AreNotEqual(emptyField, boardManager.Board);
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillBoardWithNoAnimals_Exception() {
            var gameplay = new Gameplay();
            boardManager.Board = new char[5, 5];
            Assert.Throws<NullReferenceException>(() => boardManager.FillBoardWithAnimals(gameplay.Animals));
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillNullBoaardWithAnimals_Exception() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            Assert.Throws<NullReferenceException>(() => boardManager.FillBoardWithAnimals(gameplay.Animals));
        }


        private static void AddAnimals(Gameplay gameplay) {
            gameplay.AddAnimal(new Antilope());
            gameplay.AddAnimal(new Lion());
        }
    }
}