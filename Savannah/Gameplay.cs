using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Gameplay {
        public List<IAnimal> Animals;
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

        //Maybe private. Only Play and some test calls this method
        public void Die(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = CheckForDead(animals);
            animals.RemoveAll(animal => deadAnimals.Contains(animal));
        }

        //Same as Die
        public void Move(Board boardManager, List<IAnimal> animals) {
            //Can do something like this: create field in IAnimals like DoTheAction and ech animal will have different actions.
            //So if I do this then checking for which animal it is is not needed. Can just leave checking for if action was done or not.
            foreach (IAnimal animal in animals) {
                if (IsLion(animal)) {
                    var lion = (Lion) animal;
                    bool ate = lion.TryToEat(animals);
                    if (ate) {
                        continue;
                    }
                }
                if (IsAntilope(animal)) {
                    var antilope = (Antilope) animal;
                    bool ranAway = antilope.TryToRunAway(animals, boardManager);
                    if (ranAway) {
                        continue;
                    }
                }
                MoveAnimal(boardManager, animals, animal);
            }
        }

        public IEnumerable<IAnimal> LookAround(IEnumerable<IAnimal> allAnimals, IAnimal currentAnimal) {
            IEnumerable<IAnimal> animalsAround = from animal in allAnimals
                where AnimalsInRange(currentAnimal, animal)
                select animal;
            return animalsAround;
        }

        //Maybe needed to be moved to Board
        public bool OutOfBounds(int x, int y, Board board, IAnimal animal) {
            return (animal.PositionOnXAxis + x >= board.BoardLayout.GetLength(0)) ||
                   (animal.PositionOnXAxis + x < 0) || (animal.PositionOnYAxis + y >= board.BoardLayout.GetLength(0)) ||
                   (animal.PositionOnYAxis + y < 0);
        }


        private void Play(Board board) {
            board.FillWithAnimals(Animals);
            board.Show();
            board.Clear(Animals);
            inGameMenu.Show(this);
            Console.Clear();
            Move(board, Animals);
            Die(Animals);
        }

        private bool AnimalsInRange(IAnimal currentAnimal, IAnimal animal) {
            return animal.PositionOnXAxis >= currentAnimal.PositionOnXAxis - 1 &&
                   animal.PositionOnXAxis <= currentAnimal.PositionOnXAxis + 1 &&
                   animal.PositionOnYAxis >= currentAnimal.PositionOnYAxis - 1 &&
                   animal.PositionOnYAxis <= currentAnimal.PositionOnYAxis + 1;
        }
       


        private IEnumerable<IAnimal> CheckForDead(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = from animal in animals
                where animal.HitPoints <= 0
                select animal;
            return deadAnimals;
        }

        

        private bool PlaceIsNotFree(int x, int y, IAnimal animal, List<IAnimal> animals) {
            int wantedX = animal.PositionOnXAxis + x;
            int wantedY = animal.PositionOnYAxis + y;
            IEnumerable<IAnimal> animalInWantedPlace = from anim in animals
                where anim.PositionOnXAxis == wantedX && anim.PositionOnYAxis == wantedY
                select anim;
            return animalInWantedPlace.Any();
        }

        private bool DidntMove(int x, int y) {
            return x == 0 && y == 0;
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


        //For Move
        private void MoveAnimal(Board board, List<IAnimal> animals, IAnimal animal)
        {
            int x = Program.Random.Next(-1, 2);
            int y = Program.Random.Next(-1, 2);
            while (OutOfBounds(x, y, board, animal) || PlaceIsNotFree(x, y, animal, animals) || DidntMove(x, y))
            {
                x = Program.Random.Next(-1, 2);
                y = Program.Random.Next(-1, 2);
            }

            animal.PositionOnXAxis += x;
            animal.PositionOnYAxis += y;
            animal.HitPoints -= 10;
        }

        private bool IsLion(IAnimal animal)
        {
            return animal.Name == "Lion";
        }

        private static bool IsAntilope(IAnimal animal)
        {
            return animal.Name == "Antilope";
        }
    }
}