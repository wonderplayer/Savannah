using System;
using System.Collections.Generic;
using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class BoardTest {
        private Board board;

        [SetUp]
        public void SetUp() {
            board = new Board();
        }

        [Test]
        public void CreateBoard_CanCreateEmptyBoard_Can() {
            board.CreateBoard();
            Assert.IsNotEmpty(board.Field);
        }

        [Test]
        public void ShowBoard_CanShowNotEmptyBoard_Can() {
            board.CreateBoard();
            board.ShowBoard();
        }

        [Test]
        public void ShowBoard_CanShowEmptyBoard_Exception() {
            Assert.Throws<NullReferenceException>(() => board.ShowBoard());
        }

        [Test]
        public void FillBoardWithAnimals_CanFillBoad_Can() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            board.Field = new char[5, 5];
            var emptyField = new char[5, 5];
            board.FillBoardWithAnimals(gameplay.Animals);
            Assert.AreNotEqual(emptyField, board.Field);
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillBoardWithNoAnimals_Exception() {
            var gameplay = new Gameplay();
            board.Field = new char[5, 5];
            Assert.Throws<NullReferenceException>(() => board.FillBoardWithAnimals(gameplay.Animals));
        }

        [Test]
        public void FillBoardWithAnimals_CannotFillNullBoaardWithAnimals_Exception() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            Assert.Throws<NullReferenceException>(() => board.FillBoardWithAnimals(gameplay.Animals));
        }


        private static void AddAnimals(Gameplay gameplay) {
            gameplay.AddAnimal(new Antilope());
            gameplay.AddAnimal(new Lion());
        }
    }
}