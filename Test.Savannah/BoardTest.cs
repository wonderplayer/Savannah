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
        public void Create_CanCreateEmptyBoard_Can() {
            board.Create();
            Assert.IsNotEmpty(board.Layout);
        }

        [Test]
        public void Show_CanShowNotEmptyBoard_Can() {
            board.Create();
            board.Show();
        }

        [Test]
        public void Show_CanShowEmptyBoard_Exception() {
            Assert.Throws<NullReferenceException>(() => board.Show());
        }

        [Test]
        public void FillWithAnimals_CanFillBoad_Can() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            board.Layout = new char[10, 10];
            var emptyField = new char[10, 10];
            board.FillWithAnimals(gameplay.Animals);
            Assert.AreNotEqual(emptyField, board.Layout);
        }

        [Test]
        public void FillWithAnimals_CannotFillBoardWithNoAnimals_Exception() {
            var gameplay = new Gameplay();
            board.Layout = new char[5, 5];
            Assert.Throws<NullReferenceException>(() => board.FillWithAnimals(gameplay.Animals));
        }

        [Test]
        public void FillWithAnimals_CannotFillNullBoaardWithAnimals_Exception() {
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            Assert.Throws<NullReferenceException>(() => board.FillWithAnimals(gameplay.Animals));
        }

        [Test]
        public void Clear_ClearsBoardFormAnimals_ClearBoard() {
            board.Create();
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            AddAnimals(gameplay);
            board.FillWithAnimals(gameplay.Animals);
            object filledBoard = board.Layout.Clone();
            board.Clear(gameplay.Animals);
            Assert.AreNotEqual(filledBoard, board.Layout);
        }

        [Test]
        public void OutOfBounds_ValueIsNotOutOfBounds_ReturnsFalse() {
            Gameplay gameplay = InitializeAllNecessaryForOutOfBounds();
            Assert.IsFalse(board.OutOfBounds(1, 1, board.Layout, gameplay.Animals[0]));
        }

        [Test]
        public void OutOfBounds_ValueIsOutOfBounds_ReturnTrue() {
            Gameplay gameplay = InitializeAllNecessaryForOutOfBounds();
            Assert.IsTrue(board.OutOfBounds(-1, -1, board.Layout, gameplay.Animals[0]));
        }

        private Gameplay InitializeAllNecessaryForOutOfBounds() {
            board.Create();
            var gameplay = new Gameplay {
                Animals = new List<IAnimal>()
            };
            gameplay.Animals.Add(new Antilope());
            return gameplay;
        }

        private static void AddAnimals(Gameplay gameplay) {
            gameplay.AddAnimal(new Antilope());
            gameplay.AddAnimal(new Antilope());
        }
    }
}