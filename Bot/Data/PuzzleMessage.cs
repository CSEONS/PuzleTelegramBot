using Bot.CommandsHandler.Commands;

namespace Bot.Data
{
    public class PuzzleMessage
    {
        public enum InformationType
        {
            CorrectAnswer,
            WrongAnswer,
            Start,
            NoPuzzle,
            TryGetPuzzle,
            HasPuzzle,
            GetPuzzle,
            NotSolvedPuzzles
        }

        private static Dictionary<InformationType, string> Information = new()
        {
            { InformationType.CorrectAnswer, "Это правильно!" },
            { InformationType.WrongAnswer, "Увы. Неправильно :("},
            { InformationType.Start, $"Привет! Я бот дающий загадки. Чтобы получить загадку введи команду {GetPuzzleCommand.CommandName}"},
            { InformationType.NoPuzzle, "Для вас пока нет загадок."},
            { InformationType.TryGetPuzzle, $"Пожалуйста получите загадку с помощью комманды {GetPuzzleCommand.CommandName}"},
            { InformationType.HasPuzzle, "У вас уже есть загадка.\n{0}"},
            { InformationType.GetPuzzle, "Вы успешно получили новую загадку.\n{0}"},
            { InformationType.NotSolvedPuzzles, "У вас пока нет решеных загадок." }
        };

        public static string GetInformationString(InformationType informationType, params string[] args)
        {
            return string.Format(Information[informationType], args);
        }
    }
}
