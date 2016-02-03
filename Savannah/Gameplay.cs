using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;
        private readonly AnimalActions animalActions = new AnimalActions();
        private readonly InGameMenu inGameMenu = new InGameMenu();

        public void NewGame() {
            Animals = new List<IAnimal>();
            var board = new Board();
            board.Create();
            do {
                Play(board);
            } while (Animals.Count > 0);
        }

        public void AddAnimal(IAnimal newAnimal) {
            int x;
            int y;
            do {
                x = Program.Random.Next(1, 10);
                y = Program.Random.Next(1, 10);
                if (!PositionIsNotFree(x, y).Any()) {
                    AddAnimalToList(newAnimal, x, y);
                }
            } while (!PositionIsNotFree(x, y).Any());
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
            board.FillWithAnimals(Animals);
            board.Show();
            board.Clear(Animals);
            inGameMenu.Show(this);
            animalActions.Move(board, Animals);
            animalActions.Die(Animals);
        }
    }
}