using Bot.CommandsHandler.Commands;

namespace Bot.Data
{
    public class MuzzlePuzzleMessage
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
            NotSolvedPuzzles,
            WrongCommand,
            WrongPuzzle,
            NotPermissions,
            PuzzleAdded
        }

        private static Dictionary<InformationType, string> AlertInformation = new()
        {
            { InformationType.CorrectAnswer, "Это правильно!" },
            { InformationType.WrongAnswer, "Увы. Неправильно :("},
            { InformationType.Start, $"Привет! Я бот дающий загадки. Чтобы получить загадку введи команду {GetPuzzleCommand.CommandName}"},
            { InformationType.NoPuzzle, "Для вас пока нет загадок."},
            { InformationType.TryGetPuzzle, $"Пожалуйста получите загадку с помощью комманды {GetPuzzleCommand.CommandName}"},
            { InformationType.HasPuzzle, "У вас уже есть загадка:\n{0}"},
            { InformationType.GetPuzzle, "{0}"},
            { InformationType.NotSolvedPuzzles, "У вас пока нет решеных загадок." },
            { InformationType.WrongCommand, "Такой команды не существует."},
            { InformationType.WrongPuzzle, $"Неправильный вид комманды. Комманды должна быть вида: {AddPuzzleCommand.CommandName} [текст загадки] (ответ1 ответ2 ответ3)."},
            { InformationType.NotPermissions, "У вас нет разрешения на использование этой комманды."},
            { InformationType.PuzzleAdded, "Пазл удачно создан."}
        };

        private static Dictionary<ICommandProcessor, string> CommandProcessorsDescription = new()
        {
            {new StartCommand(), "Предназначена для инициализации игрока." },
            {new StatusCommand(), "Отображает ваши параметры. Id, Имя, Ранг, Рейтинг и т. д."},
            {new GetPuzzleCommand(), "Позволяет получить новую загадку если решена текущая или её нет."},
            {new DisplaySolvedPuzzlesCommand(), "Показывает решеные вами загадки." },
            {new AddPuzzleCommand(), $"Добавляет новую загадку по следующему виду: {AddPuzzleCommand.CommandName} [текст загадки] (ответ1 ответ2 ответ3)."},
            {new HelpCommand(), "Показывает все доступные комманды"}
        };

        public static string GetInformationString(InformationType informationType, params string[] args)
        {
            return string.Format(AlertInformation[informationType], args);
        }

        internal static string GetDescriptionString(ICommandProcessor commandProcessor)
        {
            return CommandProcessorsDescription.FirstOrDefault(x => x.Key.GetType() == commandProcessor.GetType()).Value;
        }
    }
}
