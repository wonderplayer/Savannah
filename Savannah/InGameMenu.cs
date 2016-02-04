using System;

namespace Savannah {
    public class InGameMenu {
        private AnimalActions animalActions = new AnimalActions();
        public void Show(Gameplay gameplay) {
            Console.Write("L - add lion  ");
            Console.WriteLine("A - add antilope");
            Console.Write("Press any other key to resume...");
            SelectAction(Console.ReadKey(true).Key, gameplay);
            Console.WriteLine();
        }

        private void SelectAction(ConsoleKey key, Gameplay gameplay) {
            switch (key) {
                case ConsoleKey.L:
                    animalActions.AddAnimal(new Lion(), gameplay.Animals);
                    break;
                case ConsoleKey.A:
                    animalActions.AddAnimal(new Antilope(), gameplay.Animals);
                    break;
            }
        }
    }
}