using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class AnimalActions {
        public IEnumerable<IAnimal> LookAround(IEnumerable<IAnimal> allAnimals, IAnimal currentAnimal)
        {
            IEnumerable<IAnimal> animalsAround = from animal in allAnimals
                                                 where AnimalsInRange(currentAnimal, animal)
                                                 select animal;
            return animalsAround;
        }

        public void Die(List<IAnimal> animals)
        {
            IEnumerable<IAnimal> deadAnimals = CheckForDead(animals);
            animals.RemoveAll(animal => deadAnimals.Contains(animal));
        }

        private IEnumerable<IAnimal> CheckForDead(List<IAnimal> animals)
        {
            IEnumerable<IAnimal> deadAnimals = from animal in animals
                                               where animal.HitPoints <= 0
                                               select animal;
            return deadAnimals;
        }

        private bool AnimalsInRange(IAnimal currentAnimal, IAnimal animal)
        {
            bool himself = animal.PositionOnYAxis == currentAnimal.PositionOnYAxis &&
                           animal.PositionOnXAxis == currentAnimal.PositionOnXAxis;
            return animal.PositionOnXAxis >= currentAnimal.PositionOnXAxis - 1 &&
                   animal.PositionOnXAxis <= currentAnimal.PositionOnXAxis + 1 &&
                   animal.PositionOnYAxis >= currentAnimal.PositionOnYAxis - 1 &&
                   animal.PositionOnYAxis <= currentAnimal.PositionOnYAxis + 1 &&
                   !himself;
        }

        public void AddAnimal(IAnimal newAnimal, List<IAnimal> animals )
        {
            int x;
            int y;
            do
            {
                x = Program.Random.Next(1, 10);
                y = Program.Random.Next(1, 10);
                if (!PositionIsNotFree(x, y, animals).Any())
                {
                    AddAnimalToList(newAnimal, x, y, animals);
                }
            } while (!PositionIsNotFree(x, y, animals).Any());
        }

        private int adderForXPosition;
        private int adderForYPosition;

        public void Move(Board boardManager, List<IAnimal> animals)
        {
            foreach (IAnimal animal in animals)
            {
                if (IsLion(animal))
                {
                    var lion = (Lion)animal;
                    var lionActions = new LionActions();
                    bool ate = lionActions.TryToEat(animals, lion);
                    if (ate)
                    {
                        continue;
                    }
                }
                if (IsAntilope(animal))
                {
                    var antilope = (Antilope)animal;
                    var antilopeActions = new AntilopeActions();
                    bool ranAway = antilopeActions.TryToRunAway(animals, boardManager, antilope);
                    if (ranAway)
                    {
                        continue;
                    }
                }
                MoveCurrentAnimal(boardManager, animals, animal);
            }
        }

        private void MoveCurrentAnimal(Board board, List<IAnimal> animals, IAnimal animal)
        {
            CalculateCorrectPosition(board, animals, animal);
            MoveToNewPosition(animal);
        }

        private void MoveToNewPosition(IAnimal animal)
        {
            animal.PositionOnXAxis += adderForXPosition;
            animal.PositionOnYAxis += adderForYPosition;
            animal.HitPoints -= 10;
        }

        private void CalculateCorrectPosition(Board board, List<IAnimal> animals, IAnimal animal)
        {
            adderForXPosition = Program.Random.Next(-1, 2);
            adderForYPosition = Program.Random.Next(-1, 2);
            while (board.OutOfBounds(adderForXPosition, adderForYPosition, board.Layout, animal) ||
                   PlaceIsNotFree(animal, animals) ||
                   DidntMove())
            {
                adderForXPosition = Program.Random.Next(-1, 2);
                adderForYPosition = Program.Random.Next(-1, 2);
            }
        }

        private bool PlaceIsNotFree(IAnimal currentAnimal, List<IAnimal> animals)
        {
            int wantedPositionX = currentAnimal.PositionOnXAxis + adderForXPosition;
            int wantedPositionY = currentAnimal.PositionOnYAxis + adderForYPosition;
            IEnumerable<IAnimal> animalInWantedPlace = from animal in animals
                                                       where animal.PositionOnXAxis == wantedPositionX && animal.PositionOnYAxis == wantedPositionY
                                                       select animal;
            return animalInWantedPlace.Any();
        }

        private bool DidntMove()
        {
            return adderForXPosition == 0 && adderForYPosition == 0;
        }

        private bool IsLion(IAnimal animal)
        {
            return animal.Name == "Lion";
        }

        private bool IsAntilope(IAnimal animal)
        {
            return animal.Name == "Antilope";
        }

        private IEnumerable<IAnimal> PositionIsNotFree(int x, int y, List<IAnimal> animals)
        {
            return from animal in animals
                   where animal.PositionOnXAxis.Equals(x) && animal.PositionOnYAxis.Equals(y)
                   select animal;
        }

        private void AddAnimalToList(IAnimal newAnimal, int x, int y, List<IAnimal> animals)
        {
            newAnimal.PositionOnXAxis = x;
            newAnimal.PositionOnYAxis = y;
            animals.Add(newAnimal);
        }
    }
}