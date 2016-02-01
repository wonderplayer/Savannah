namespace Savannah {
    public class Lion : IAnimal {
        public Lion(int xAxis, int yAxis) {
            HitPoints = 100;
            MovementSpeed = 1;
            PositionOnXAxis = xAxis;
            PositionOnXAxis = yAxis;
        }

        public Lion() {
            HitPoints = 100;
        }

        public int HitPoints { get; set; }
        public int PositionOnXAxis { get; set; }
        public int PositionOnYAxis { get; set; }
        public int MovementSpeed { get; set; }

        public void Move() {
            throw new System.NotImplementedException();
        }

        public void Die() {
            HitPoints = 0;
        }

        public void LookAround() {
            throw new System.NotImplementedException();
        }
    }
}