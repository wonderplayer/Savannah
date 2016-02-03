using System;
using System.Collections.Generic;

namespace Savannah {
    public class BoardManager {
        public char[,] Board;

        public void CreateBoard() {
            Board = new char[10, 10];
            for (var y = 0; y < Board.GetLength(0); y++) {
                for (var x = 0; x < Board.GetLength(1); x++) {
                    Board[y, x] = ' ';
                }
            }
        }

        public void FillBoardWithAnimals(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                InsertAnimalToArray(animal, IsLion(animal) ? 'L' : 'A');
            }
        }

        public void ShowBoard() {
            for (var y = 0; y < Board.GetLength(0); y++) {
                for (var x = 0; x < Board.GetLength(1); x++) {
                    Console.Write(Board[y, x]);
                }
                Console.WriteLine();
            }
        }

        public void ClearBoard(List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                Board[animal.PositionOnYAxis, animal.PositionOnXAxis] = ' ';
            }
        }

        private void InsertAnimalToArray(IAnimal animal, char animalSignature) {

            Board[animal.PositionOnYAxis, animal.PositionOnXAxis] = animalSignature;
        }

        private static bool IsLion(IAnimal animal) {
            return animal.Name == "Lion";
        }
    }
}