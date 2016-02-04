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
            IAnimal antilopeToEat = SearchForAntilope(animals);
            if (antilopeToEat == null) {
                return false;
            }
            Eat(antilopeToEat);
            return true;
        }

        private void Eat(IAnimal antilope) {
            MoveToAntilopesPosition(antilope);
            HitPoints = 100;
            antilope.HitPoints = 0;
        }

        private void MoveToAntilopesPosition(IAnimal antilopeInRange) {
            PositionOnXAxis = antilopeInRange.PositionOnXAxis;
            PositionOnYAxis = antilopeInRange.PositionOnYAxis;
        }

        private IAnimal SearchForAntilope(List<IAnimal> animals) {
            IEnumerable<IAnimal> animalsAround = gameplay.LookAround(animals, this);
            return animalsAround.Where(a => a.HitPoints > 0).FirstOrDefault(a => a.Name == "Antilope");
        }
    }
}