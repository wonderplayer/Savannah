using System;

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

        public void Move() {
            int x = Program.Random.Next(-1, 2);
            int y = Program.Random.Next(-1, 2);
            PositionOnXAxis += x;
            PositionOnYAxis += y;
            HitPoints -= 10;
        }

        public void Die() {
            HitPoints = 0;
        }

        public void LookAround() {
            throw new NotImplementedException();
        }

        public void Eat() {
            throw new NotImplementedException();
        }
    }
}