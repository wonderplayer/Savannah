using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class AnimalActions {
        public void Die(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = CheckForDead(animals);
            animals.RemoveAll(animal => deadAnimals.Contains(animal));
        }

        public void Move(BoardManager boardManager, List<IAnimal> animals) {
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

        private bool AnimalsInRange(IAnimal currentAnimal, IAnimal animal) {
            return animal.PositionOnXAxis >= currentAnimal.PositionOnXAxis - 1 &&
                   animal.PositionOnXAxis <= currentAnimal.PositionOnXAxis + 1 &&
                   animal.PositionOnYAxis >= currentAnimal.PositionOnYAxis - 1 &&
                   animal.PositionOnYAxis <= currentAnimal.PositionOnYAxis + 1;
        }

        private bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }

        private static bool IsAntilope(IAnimal animal) {
            return animal.Name == "Antilope";
        }

        private IEnumerable<IAnimal> CheckForDead(List<IAnimal> animals) {
            IEnumerable<IAnimal> deadAnimals = from animal in animals
                where animal.HitPoints <= 0
                select animal;
            return deadAnimals;
        }

        private void MoveAnimal(BoardManager board, List<IAnimal> animals, IAnimal animal) {
            int x = Program.Random.Next(-1, 2);
            int y = Program.Random.Next(-1, 2);
            while (OutOfBounds(x, y, board, animal) || PlaceIsNotFree(x, y, animal, animals) || DidntMove(x, y)) {
                x = Program.Random.Next(-1, 2);
                y = Program.Random.Next(-1, 2);
            }

            animal.PositionOnXAxis += x;
            animal.PositionOnYAxis += y;
            animal.HitPoints -= 10;
        }

        private bool PlaceIsNotFree(int x, int y, IAnimal animal, List<IAnimal> animals) {
            int wantedX = animal.PositionOnXAxis + x;
            int wantedY = animal.PositionOnYAxis + y;
            IEnumerable<IAnimal> animalInWantedPlace = from anim in animals
                where anim.PositionOnXAxis == wantedX && anim.PositionOnYAxis == wantedY
                select anim;
            return animalInWantedPlace.Any();
        }

        public bool OutOfBounds(int x, int y, BoardManager board, IAnimal animal) {
            return (animal.PositionOnXAxis + x >= board.Board.GetLength(0)) ||
                   (animal.PositionOnXAxis + x < 0) || (animal.PositionOnYAxis + y >= board.Board.GetLength(0)) ||
                   (animal.PositionOnYAxis + y < 0);
        }

        private bool DidntMove(int x, int y) {
            return x == 0 && y == 0;
        }
    }
}