using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Data.Models
{
    public class PuzzleInformationMessage
    {
        public enum InformationType
        {
            CorrectAnswer,
            WrongAnswer,
            Start,
            ChangeMessage
        }

        public static Dictionary<InformationType, string> PuzzleInformation = new()
        {
            { InformationType.CorrectAnswer, "Это правильно! Ответ {0}" },
            { InformationType.WrongAnswer, "Увы. Неправильно :("},
            { InformationType.Start, "Привет!\nВот тебе загадка\n{0}"},
            { InformationType.ChangeMessage, "Сообщение изменено."}
        };
    }
}
