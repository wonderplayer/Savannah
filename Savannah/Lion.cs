using System.Collections.Generic;
using System.Linq;

namespace Savannah {
    public class Lion : IAnimal {
        public Lion() {
            HitPoints = 100;
            Name = "Lion";
        }

        public int HitPoints { get; set; }
        public int PositionOnXAxis { get; set; }
        public int PositionOnYAxis { get; set; }
        public string Name { get; set; }
        readonly AnimalActions animalAction = new AnimalActions();

        public bool TryToEat(List<IAnimal> animals) {
            IAnimal antilopeInRange = SearchForAntilope(animals);
            if (antilopeInRange == null) {
                return false;
            }
            Eat(antilopeInRange);
            return true;
        }

        private void Eat(IAnimal antilopeInRange) {
            PositionOnXAxis = antilopeInRange.PositionOnXAxis;
            PositionOnYAxis = antilopeInRange.PositionOnYAxis;
            HitPoints += antilopeInRange.HitPoints;
            antilopeInRange.HitPoints = 0;
        }

        private IAnimal SearchForAntilope(List<IAnimal> animals) {
            IEnumerable<IAnimal> animalsAround = animalAction.LookAround(animals, this);
            return animalsAround.FirstOrDefault(a => a.Name == "Antilope");
        }
    }
}