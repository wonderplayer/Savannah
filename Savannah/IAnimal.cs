namespace Savannah {
    public interface IAnimal {
        int HitPoints { get; set; }
        int PositionOnXAxis { get; set; }
        int PositionOnYAxis { get; set; }
        string Name { get; set; }
        void Move();
        void Die();
        void LookAround();
    }
}