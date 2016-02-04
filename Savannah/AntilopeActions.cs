using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class AntilopeActions {

        private readonly AnimalActions animalActions = new AnimalActions();

        public bool TryToRunAway(List<IAnimal> animals, Board board, Antilope antilope)
        {
            IEnumerable<IAnimal> lionsAround = SearchForLions(animals, antilope);
            IEnumerable<IAnimal> enumeratedLionsAround = lionsAround as IList<IAnimal> ?? lionsAround.ToList();
            if (!enumeratedLionsAround.Any())
            {
                return false;
            }
            RunAway(animals, enumeratedLionsAround, board, antilope);
            return true;
        }

        private void RunAway(List<IAnimal> animals, IEnumerable<IAnimal> lionsAround, Board board, Antilope antilope)
        {
            IEnumerable<IAnimal> enumeratedLionsAround = lionsAround as IList<IAnimal> ?? lionsAround.ToList();
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (board.OutOfBounds(x, y, board.Layout, antilope) || !SpaceIsFree(animals, x, y, antilope) ||
                        !IsSafePlaceToGo(enumeratedLionsAround, x, y, antilope))
                    {
                        continue;
                    }
                    MoveTo(x, y, antilope);
                    return;
                }
            }
        }

        private void MoveTo(int x, int y, Antilope antilope)
        {
            antilope.PositionOnXAxis += x;
            antilope.PositionOnYAxis += y;
        }

        private bool SpaceIsFree(IEnumerable<IAnimal> animals, int x, int y, Antilope antilope)
        {
            var spaceIsNotTaken = true;
            foreach (IAnimal animal in animals)
            {
                if (IsNotFree(x, y, animal, antilope))
                {
                    spaceIsNotTaken = false;
                }
            }
            return spaceIsNotTaken;
        }

        private bool IsNotFree(int x, int y, IAnimal animal, Antilope antilope)
        {
            return antilope.PositionOnXAxis + x == animal.PositionOnXAxis && antilope.PositionOnYAxis + y == animal.PositionOnYAxis;
        }

        private IEnumerable<IAnimal> SearchForLions(List<IAnimal> animals, Antilope antilope)
        {
            IEnumerable<IAnimal> lionsAround = animalActions.LookAround(animals, antilope);
            return lionsAround.Where(l => l.Name == "Lion");
        }

        private bool IsSafePlaceToGo(IEnumerable<IAnimal> lions, int x, int y, Antilope antilope)
        {
            foreach (IAnimal lion in lions)
            {
                if ((Math.Abs(antilope.PositionOnXAxis + x - lion.PositionOnXAxis) > 1) ||
                    (Math.Abs(antilope.PositionOnYAxis + y - lion.PositionOnYAxis) > 1))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}