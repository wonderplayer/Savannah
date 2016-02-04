using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Moving {
        private int adderForXPosition;
        private int adderForYPosition;

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
                MoveCurrentAnimal(boardManager, animals, animal);
            }
        }

        private void MoveCurrentAnimal(Board board, List<IAnimal> animals, IAnimal animal) {
            CalculateCorrectPosition(board, animals, animal);
            MoveToNewPosition(animal);
        }

        private void MoveToNewPosition(IAnimal animal) {
            animal.PositionOnXAxis += adderForXPosition;
            animal.PositionOnYAxis += adderForYPosition;
            animal.HitPoints -= 10;
        }

        private void CalculateCorrectPosition(Board board, List<IAnimal> animals, IAnimal animal) {
            adderForXPosition = Program.Random.Next(-1, 2);
            adderForYPosition = Program.Random.Next(-1, 2);
            while (board.OutOfBounds(adderForXPosition, adderForYPosition, board.BoardLayout, animal) ||
                   PlaceIsNotFree(animal, animals) ||
                   DidntMove()) {
                adderForXPosition = Program.Random.Next(-1, 2);
                adderForYPosition = Program.Random.Next(-1, 2);
            }
        }

        private bool PlaceIsNotFree(IAnimal currentAnimal, List<IAnimal> animals) {
            int wantedPositionX = currentAnimal.PositionOnXAxis + adderForXPosition;
            int wantedPositionY = currentAnimal.PositionOnYAxis + adderForYPosition;
            IEnumerable<IAnimal> animalInWantedPlace = from animal in animals
                where animal.PositionOnXAxis == wantedPositionX && animal.PositionOnYAxis == wantedPositionY
                select animal;
            return animalInWantedPlace.Any();
        }

        private bool DidntMove() {
            return adderForXPosition == 0 && adderForYPosition == 0;
        }

        private bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }

        private bool IsAntilope(IAnimal animal) {
            return animal.Name == "Antilope";
        }
    }
}