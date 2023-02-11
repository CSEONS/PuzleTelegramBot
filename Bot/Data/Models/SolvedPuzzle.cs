using Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Data.Models
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
