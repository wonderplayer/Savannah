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
    }
}