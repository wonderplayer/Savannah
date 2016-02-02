using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;

        public void NewGame() {
            Animals = new List<IAnimal>();
            AddAnimal(new Lion());
            AddAnimal(new Antilope());
            var board = new Board();
            board.CreateBoard();
            while (Animals.Count > 0) {
                Play(board);
            }
        }

        private void Move(List<IAnimal> animals, Board board) {
            var animalFunc = new AnimalFunctionality();
            foreach (IAnimal animal in animals) {
                animalFunc.Move(board, animal, animals);
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

        private void Play(Board board) {
            board.FillBoardWithAnimals(Animals);
            board.ShowBoard();
            board.ClearBoard(Animals);
            Console.ReadLine();
            Console.Clear();
            Move(Animals, board);
            CheckForDeadAnimals();
        }

        private void CheckForDeadAnimals() {}
    }
}