using System;
using System.Collections.Generic;

namespace Savannah {
    public class InGameMenu {
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
                    gameplay.AddAnimal(new Lion());
                    break;
                case ConsoleKey.A:
                    gameplay.AddAnimal(new Antilope());
                    break;
            }
        } 
    }
}