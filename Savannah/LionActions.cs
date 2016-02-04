using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class LionActions {
        private readonly Gameplay gameplay = new Gameplay();
        private AnimalActions animalActions = new AnimalActions();

        public bool TryToEat(List<IAnimal> animals, Lion lion)
        {
            IAnimal antilopeToEat = SearchForAntilope(animals, lion);
            if (antilopeToEat == null)
            {
                return false;
            }
            Eat(antilopeToEat, lion);
            return true;
        }

        private void Eat(IAnimal antilope, Lion lion)
        {
            MoveToAntilopesPosition(antilope, lion);
            lion.HitPoints = 100;
            antilope.HitPoints = 0;
        }

        private void MoveToAntilopesPosition(IAnimal antilopeInRange, Lion lion)
        {
            lion.PositionOnXAxis = antilopeInRange.PositionOnXAxis;
            lion.PositionOnYAxis = antilopeInRange.PositionOnYAxis;
        }

        private IAnimal SearchForAntilope(List<IAnimal> animals, Lion lion)
        {
            IEnumerable<IAnimal> animalsAround = animalActions.LookAround(animals, lion);
            return animalsAround.Where(a => a.HitPoints > 0).FirstOrDefault(a => a.Name == "Antilope");
        }
    }
}