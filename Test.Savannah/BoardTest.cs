using System;
using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class BoardTest {
        private Board boardManager;

        [SetUp]
        public void SetUp() {
            boardManager = new Board();
        }

        [Test]
        public void CreateBoard_CanCreateEmptyBoard_Can() {
            boardManager.Create();
            Assert.IsNotEmpty(boardManager.BoardLayout);
        }

        [Test]
        public void ShowBoard_CanShowNotEmptyBoard_Can() {
            boardManager.Create();
            boardManager.Show();
        }

        [Test]
        public void ShowBoard_CanShowEmptyBoard_Exception() {
            Assert.Throws<NullReferenceException>(() => boardManager.Show());
        }

        [Test]
        public void FillBoardWithAnimals_CanFillBoad_Can() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            boardManager.BoardLayout = new char[10, 10];
            var emptyField = new char[10, 10];
            boardManager.FillWithAnimals(gameplay.Animals);
            Assert.AreNotEqual(emptyField, boardManager.BoardLayout);
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillBoardWithNoAnimals_Exception() {
            var gameplay = new Gameplay();
            boardManager.BoardLayout = new char[5, 5];
            Assert.Throws<NullReferenceException>(() => boardManager.FillWithAnimals(gameplay.Animals));
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillNullBoaardWithAnimals_Exception() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            Assert.Throws<NullReferenceException>(() => boardManager.FillWithAnimals(gameplay.Animals));
        }


        private static void AddAnimals(Gameplay gameplay) {
            gameplay.AddAnimal(new Antilope());
            gameplay.AddAnimal(new Lion());
        }
    }
}