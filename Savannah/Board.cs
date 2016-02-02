using System;
using System.Collections.Generic;

namespace Savannah {
    public class Board {
        public char[,] Field;

        public void CreateBoard() {
            Field = new char[5, 5];
            for (var y = 0; y < Field.GetLength(0); y++) {
                for (var x = 0; x < Field.GetLength(1); x++) {
                    Field[y, x] = ' ';
                }
            }
        }

        public void FillBoardWithAnimals(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                InsertAnimalToArray(animal, IsLion(animal) ? 'L' : 'A');
            }
        }

        public void ShowBoard() {
            for (var y = 0; y < Field.GetLength(0); y++) {
                for (var x = 0; x < Field.GetLength(1); x++) {
                    Console.Write(Field[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void ClearBoard(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                Field[animal.PositionOnYAxis, animal.PositionOnXAxis] = ' ';
            }
        }

        private void InsertAnimalToArray(IAnimal animal, char animalSignature) {
            Field[animal.PositionOnYAxis, animal.PositionOnXAxis] = animalSignature;
        }

        private static bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }
    }
}