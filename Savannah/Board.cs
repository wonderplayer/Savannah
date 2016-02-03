using System;
using System.Collections.Generic;

namespace Savannah {
    public class Board {
        public char[,] BoardLayout;

        public void Create() {
            BoardLayout = new char[10, 10];
            for (var y = 0; y < BoardLayout.GetLength(0); y++) {
                for (var x = 0; x < BoardLayout.GetLength(1); x++) {
                    BoardLayout[y, x] = ' ';
                }
            }
        }

        public void FillWithAnimals(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                PlaceAnimalOnBoard(animal, IsLion(animal) ? 'L' : 'A');
            }
        }

        public void Show() {
            for (var y = 0; y < BoardLayout.GetLength(0); y++) {
                for (var x = 0; x < BoardLayout.GetLength(1); x++) {
                    Console.Write(BoardLayout[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void Clear(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                BoardLayout[animal.PositionOnYAxis, animal.PositionOnXAxis] = ' ';
            }
        }

        private void PlaceAnimalOnBoard(IAnimal animal, char animalSignature) {
            BoardLayout[animal.PositionOnYAxis, animal.PositionOnXAxis] = animalSignature;
        }

        private static bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }
    }
}