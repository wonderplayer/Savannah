using System;
using System.Collections.Generic;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;
        private readonly InGameMenu inGameMenu = new InGameMenu();
        private readonly AnimalActions animalActions = new AnimalActions();

        public void NewGame() {
            Animals = new List<IAnimal>();
            var board = new Board();
            board.Create();
            do {
                Play(board);
            } while (Animals.Count > 0);
        }

        private void Play(Board board) {
            board.FillWithAnimals(Animals);
            board.Show();
            board.Clear(Animals);
            inGameMenu.Show(this);
            Console.Clear();
            animalActions.Move(board, Animals);
            animalActions.Die(Animals);
        }
    }
}