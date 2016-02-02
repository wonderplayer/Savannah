using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class AnimalActions {
        //TODO Make animal die so, but don`t remove him from list just now, because error will occur.
        //TODO So maybe I need to pass a null value to an element and when I`m done with foreach find and remove all elements with null from list
        public void Die(IAnimal animal) {
            
        }

        public void Move(BoardManager boardManager, List<IAnimal> animals) {
            foreach (IAnimal animal in animals) {
                MoveAnimal(boardManager, animals, animal);
            }
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

        private bool OutOfBounds(int x, int y, BoardManager board, IAnimal animal) {
            return (animal.PositionOnXAxis + x >= board.Board.GetLength(0)) ||
                   (animal.PositionOnXAxis + x <= 0) || (animal.PositionOnYAxis + y >= board.Board.GetLength(0)) ||
                   (animal.PositionOnYAxis + y <= 0);
        }

        private bool DidntMove(int x, int y) {
            return x == 0 && y == 0;
        }
    }
}