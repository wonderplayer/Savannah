using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;
        private readonly InGameMenu inGameMenu = new InGameMenu();
        private readonly Moving moving = new Moving();

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

        public IEnumerable<IAnimal> LookAround(IEnumerable<IAnimal> allAnimals, IAnimal currentAnimal) {
            IEnumerable<IAnimal> animalsAround = from animal in allAnimals
                where AnimalsInRange(currentAnimal, animal)
                select animal;
            return animalsAround;
        }

        private void Die(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = CheckForDead(animals);
            animals.RemoveAll(animal => deadAnimals.Contains(animal));
        }

        private void Play(Board board) {
            board.FillWithAnimals(Animals);
            board.Show();
            board.Clear(Animals);
            inGameMenu.Show(this);
            Console.Clear();
            moving.Move(board, Animals);
            Die(Animals);
        }

        private IEnumerable<IAnimal> PositionIsNotFree(int x, int y) {
            return from animal in Animals
                where animal.PositionOnXAxis.Equals(x) && animal.PositionOnYAxis.Equals(y)
                select animal;
        }

        private void AddAnimalToList(IAnimal newAnimal, int x, int y) {
            newAnimal.PositionOnXAxis = x;
            newAnimal.PositionOnYAxis = y;
            Animals.Add(newAnimal);
        }

        private IEnumerable<IAnimal> CheckForDead(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = from animal in animals
                where animal.HitPoints <= 0
                select animal;
            return deadAnimals;
        }

        private bool AnimalsInRange(IAnimal currentAnimal, IAnimal animal) {
            bool himself = animal.PositionOnYAxis == currentAnimal.PositionOnYAxis &&
                           animal.PositionOnXAxis == currentAnimal.PositionOnXAxis;
            return animal.PositionOnXAxis >= currentAnimal.PositionOnXAxis - 1 &&
                   animal.PositionOnXAxis <= currentAnimal.PositionOnXAxis + 1 &&
                   animal.PositionOnYAxis >= currentAnimal.PositionOnYAxis - 1 &&
                   animal.PositionOnYAxis <= currentAnimal.PositionOnYAxis + 1 &&
                   !himself;
        }
    }
}