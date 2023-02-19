using Bot.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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
        public Permissions Permission { get; set; }

        public enum Permissions
        {
            Manager,
            Administrator,
            User
        }

        public static void CreateNew(string? username, long id)
        {
            using(var context = new MuzzlePuzzleDBContext())
            {
                Player? player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == id);

                if (player is not null)
                    return;

                player = new Player()
                {
                    Name = username,
                    TelegramIdentifier = id,
                    Permission = Permissions.User
                };

                if (InManagerList(player))
                    player.Permission = Permissions.Manager;


                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        private static bool InManagerList(Player player)
        {
            if (Directory.Exists(BotConfiguration.ManagerListPath) is false)
                Directory.CreateDirectory(BotConfiguration.ManagerListPath);

            if (System.IO.File.Exists(BotConfiguration.ManagerListFullPath) is false)
                System.IO.File.CreateText(BotConfiguration.ManagerListFullPath).Dispose();

            using (var sr = new StreamReader(BotConfiguration.ManagerListFullPath))
            {
                string[] managersId = sr.ReadToEnd().Split();

                foreach (var item in managersId)
                {
                    if (player.TelegramIdentifier.ToString() == item)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override string ToString()
        {
            return
                $"Id: {Id}\n" +
                $"Name: {Name}\n" +
                $"Rank: {Rank}\n" +
                $"Rating: {Rating}\n"+
                $"Permission: {Permission}";
        }

        public static bool Exist(User user)
        {
            using (var context = new MuzzlePuzzleDBContext())
            {
                Player? player = context.Players.FirstOrDefault(x => x.TelegramIdentifier == user.Id);

                return (player is null) is false;
            }
        }
    }
}