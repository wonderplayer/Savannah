using System;
using System.Collections.Generic;

namespace Savannah {
    public class Gameplay {
        public List<Lion> Lions;
        public List<Antilope> Antilopes;

        public void NewGame() {
            Lions = new List<Lion>();
            Antilopes = new List<Antilope>();

            AddLion(Lions);
            AddAntilope(Antilopes);
        }

        public void AddAntilope(List<Antilope> antilopes) {
            var r = new Random();
            int x, y;
            var isNeededToChangePositions = false;
            do {
                x = r.Next(1, 80);
                y = r.Next(1, 30);
                foreach (Antilope antilope in antilopes)
                {
                    if (IsAntilopeOnSamePosition(x, y, antilope)) {
                        isNeededToChangePositions = true;
                        break;
                    }
                }
            } while (isNeededToChangePositions);
            
            antilopes.Add(new Antilope(x,y));
        }

        private static bool IsAntilopeOnSamePosition(int x, int y, Antilope antilope)
        {
            return (antilope.PositionOnXAxis == x) && (antilope.PositionOnYAxis == y);
        }

        private void AddLion(List<Lion> lions) {
            var r = new Random();
            int x, y;
            var isNeededToChangePositions = false;
            do
            {
                x = r.Next(1, 80);
                y = r.Next(1, 30);
                foreach (Lion lion in lions)
                {
                    if (IsLionOnSamePosition(x, y, lion))
                    {
                        isNeededToChangePositions = true;
                        break;
                    }
                }
            } while (isNeededToChangePositions);
            lions.Add(new Lion());
        }

        private bool IsLionOnSamePosition(int x, int y, Lion lion) {
            return (lion.PositionOnXAxis == x) && (lion.PositionOnYAxis == y);
        }
    }
}