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
    }
}