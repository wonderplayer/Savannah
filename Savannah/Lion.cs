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
        readonly Gameplay gameplay = new Gameplay();

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
            HitPoints = 100;
            antilopeInRange.HitPoints = 0;
        }

        private IAnimal SearchForAntilope(List<IAnimal> animals) {
            IEnumerable<IAnimal> animalsAround = gameplay.LookAround(animals, this);
            return animalsAround.Where(a => a.HitPoints > 0).FirstOrDefault(a => a.Name == "Antilope");
        }
    }
}