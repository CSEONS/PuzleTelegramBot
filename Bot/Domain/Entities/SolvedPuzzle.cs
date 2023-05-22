using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Domain.Entities
{
    public class SolvedPuzzle
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? PuzzleId { get; set; }
        public virtual Player? Player { get; set; }
        public virtual Puzzle? Puzzle { get; set; }
    }
}
