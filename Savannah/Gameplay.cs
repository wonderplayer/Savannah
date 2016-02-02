using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;
        private AnimalActions animalActions = new AnimalActions();

        public void NewGame() {
            Animals = new List<IAnimal>();
            AddAnimal(new Lion());
            AddAnimal(new Antilope());
            var boardManager = new BoardManager();
            boardManager.CreateBoard();
            while (Animals.Count > 0) {
                Play(boardManager);
            }
        }

        public void AddAnimal(IAnimal newAnimal) {
            int x = Program.Random.Next(1, 5);
            int y = Program.Random.Next(1, 5);
            if (PositionIsNotFree(x, y).Any()) {
                throw new Exception(
                    "Sorry, but something went wrong and animal on this position already exists. PLease try again.");
            }
            AddAnimalToList(newAnimal, x, y);
        }

        public void AddAnimalToList(IAnimal newAnimal) {
            int x = newAnimal.PositionOnXAxis;
            int y = newAnimal.PositionOnYAxis;
            if (PositionIsNotFree(x, y).Any()) {
                throw new Exception(
                    "Sorry, but something went wrong and animal on this position already exists. PLease try again.");
            }
            Animals.Add(newAnimal);
        }


        private void AddAnimalToList(IAnimal newAnimal, int x, int y) {
            newAnimal.PositionOnXAxis = x;
            newAnimal.PositionOnYAxis = y;
            Animals.Add(newAnimal);
        }

        private IEnumerable<IAnimal> PositionIsNotFree(int x, int y) {
            return from animal in Animals
                where animal.PositionOnXAxis.Equals(x) && animal.PositionOnYAxis.Equals(y)
                select animal;
        }

        private void Play(BoardManager board) {
            board.FillBoardWithAnimals(Animals);
            board.ShowBoard();
            board.ClearBoard(Animals);
            Console.ReadLine();
            Console.Clear();
            animalActions.Move(board, Animals);
        }
    }
}