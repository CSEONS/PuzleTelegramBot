using Bot.Data;
using System.ComponentModel.DataAnnotations.Schema;
using Telegram.Bot.Types;

namespace Bot.Models
{
    public class Player
    {
        public int Id { get; set; }
        public long TelegramIdentifier { get; set; }
        public string? Name { get; set; }
        public int Rating { get; set; }
        public int Rank { get; set; }
        public int? CurrentPuzzleId { get; set; }
        public virtual Puzzle? CurrentPuzzle { get; set; }
        public virtual ICollection<Puzzle>? SolvedPuzzles { get; set; }

        public static void TryCreateNew(string? username, long id)
        {
            using(var context = new PlayerDBContext())
            {
                Player? player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == id);

                if (player is not null)
                    return;


                player = new Player()
                {
                    Name = username,
                    TelegramIdentifier = id,
                };

                context.Players.Add(player);
                context.SaveChanges();
            }
        }
    }
}