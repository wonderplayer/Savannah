using System;

namespace Savannah {
    public class Antilope : IAnimal {
        public Antilope(int xAxis, int yAxis) {
            HitPoints = 150;
            MovementSpeed = 1;
            PositionOnXAxis = xAxis;
            PositionOnYAxis = yAxis;
        }

        public Antilope() {
            HitPoints = 150;
        }

        public int HitPoints { get; set; }
        public int PositionOnXAxis { get; set; }
        public int PositionOnYAxis { get; set; }
        public int MovementSpeed { get; set; }

        public void Move() {
            throw new NotImplementedException();
        }

        public void Die() {
            HitPoints = 0;
        }

        public void LookAround() {
            throw new NotImplementedException();
        }
    }
}