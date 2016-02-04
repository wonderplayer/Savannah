using System;
using System.Collections.Generic;

namespace Savannah {
    public class Board {
        public char[,] Layout;

        public void Create() {
            Layout = new char[10, 10];
            for (var y = 0; y < Layout.GetLength(0); y++) {
                for (var x = 0; x < Layout.GetLength(1); x++) {
                    Layout[y, x] = ' ';
                }
            }
        }

        public void FillWithAnimals(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                PlaceAnimalOnBoard(animal, IsLion(animal) ? 'L' : 'A');
            }
        }

        public void Show() {
            for (var y = 0; y < Layout.GetLength(0); y++) {
                for (var x = 0; x < Layout.GetLength(1); x++) {
                    Console.Write(Layout[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void Clear(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                Layout[animal.PositionOnYAxis, animal.PositionOnXAxis] = ' ';
            }
        }

        public bool OutOfBounds(int x, int y, char[,] boardLayout, IAnimal animal) {
            return (animal.PositionOnXAxis + x >= boardLayout.GetLength(0)) ||
                   (animal.PositionOnXAxis + x < 0) || (animal.PositionOnYAxis + y >= boardLayout.GetLength(1)) ||
                   (animal.PositionOnYAxis + y < 0);
        }

        private void PlaceAnimalOnBoard(IAnimal animal, char animalSignature) {
            Layout[animal.PositionOnYAxis, animal.PositionOnXAxis] = animalSignature;
        }

        private static bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }
    }
}