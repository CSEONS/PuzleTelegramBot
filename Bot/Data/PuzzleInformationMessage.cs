using Bot.CommandsHandler.Commands;

namespace Bot.Data
{
    public class PuzzleInformationMessage
    {
        public enum InformationType
        {
            CorrectAnswer,
            WrongAnswer,
            Start,
            NoPuzzle
        }

        public static Dictionary<InformationType, string> Information = new()
        {
            { InformationType.CorrectAnswer, "Это правильно!" },
            { InformationType.WrongAnswer, "Увы. Неправильно :("},
            { InformationType.Start, $"Привет! Я бот дающий загадки. Чтобы получить загадку введи команду /{GetPuzzleCommand.CommandName}"},
            { InformationType.NoPuzzle, "Для вас пока нет загадок."}
        };
    }
}
