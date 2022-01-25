using System.Collections.Generic;
using System.Text;

namespace CourseWork.Models.Models
{
    public class GameCell
    {
        private readonly List<Rabbit> _rabbits;
        private readonly List<Wolf> _wolves;
        private readonly List<SheWolf> _sheWolves;

        public List<Rabbit> Rabbits { get => _rabbits; }
        public List<Wolf> Wolves { get => _wolves; }
        public List<SheWolf> SheWolves { get => _sheWolves; }

        public GameCell()
        {
            _rabbits = new List<Rabbit>();
            _wolves = new List<Wolf>();
            _sheWolves = new List<SheWolf>();
        }
        public GameCell(List<Rabbit> rabbits, List<Wolf> wolves, List<SheWolf> sheWolves)
        {
            _rabbits = rabbits;
            _wolves = wolves;
            _sheWolves = sheWolves;
        }
        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            if (Rabbits.Count == 0 && Wolves.Count == 0 && SheWolves.Count == 0)
            {
                strBuilder.Append("('-')");
            }

            foreach (var rabbit in Rabbits)
            {
                strBuilder.Append(rabbit + " ");
            }
            foreach (var wolf in Wolves)
            {
                strBuilder.Append(wolf + " ");
            }
            foreach (var sheWolf in SheWolves)
            {
                strBuilder.Append(sheWolf + " ");
            }

            return strBuilder.ToString();
        }
    }
}
