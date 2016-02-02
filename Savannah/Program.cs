using System;

namespace Savannah {
    public static class Program {
        public static readonly Random Random = new Random();

        private static void Main() {
            var g = new Gameplay();
            g.NewGame();
        }
    }
}