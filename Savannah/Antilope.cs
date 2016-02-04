using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Antilope : IAnimal {
        public Antilope() {
            HitPoints = 150;
            Name = "Antilope";
        }

        public int HitPoints { get; set; }
        public int PositionOnXAxis { get; set; }
        public int PositionOnYAxis { get; set; }
        public string Name { get; set; }
        private readonly Gameplay gameplay = new Gameplay();

        public bool TryToRunAway(List<IAnimal> animals, Board board) {
            IEnumerable<IAnimal> lionsAround = SearchForLions(animals);
            if (!lionsAround.Any()) {
                return false;
            }
            RunAway(animals, lionsAround, board);
            return true;
        }

        private void RunAway(List<IAnimal> animals, IEnumerable<IAnimal> lionsAround, Board board) {
            for (int y = -1; y <= 1; y++) {
                for (int x = -1; x <= 1; x++) {
                    if (!board.OutOfBounds(x, y, board.BoardLayout, this) && SpaceIsFree(animals, x, y) &&
                        IsSafePlaceToGo(lionsAround, x, y)) {
                        MoveTo(x, y);
                        return;
                    }
                }
            }
        }

        private void MoveTo(int x, int y) {
            PositionOnXAxis += x;
            PositionOnYAxis += y;
        }

        private bool SpaceIsFree(IEnumerable<IAnimal> animals, int x, int y) {
            var spaceIsNotTaken = true;
            foreach (IAnimal animal in animals) {
                if (IsNotFree(x, y, animal)) {
                    spaceIsNotTaken = false;
                }
            }
            return spaceIsNotTaken;
        }

        private bool IsNotFree(int x, int y, IAnimal animal) {
            return PositionOnXAxis + x == animal.PositionOnXAxis && PositionOnYAxis + y == animal.PositionOnYAxis;
        }

        private IEnumerable<IAnimal> SearchForLions(List<IAnimal> animals) {
            IEnumerable<IAnimal> lionsAround = gameplay.LookAround(animals, this);
            return lionsAround.Where(l => l.Name == "Lion");
        }

        private bool IsSafePlaceToGo(IEnumerable<IAnimal> lions, int x, int y) {
            foreach (IAnimal lion in lions) {
                if ((Math.Abs(PositionOnXAxis + x - lion.PositionOnXAxis) > 1) ||
                    (Math.Abs(PositionOnYAxis + y - lion.PositionOnYAxis) > 1)) {
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}