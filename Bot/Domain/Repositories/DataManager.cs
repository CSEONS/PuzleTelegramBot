using Bot.Domain.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Domain.Repositories
{
    internal class DataManager
    {
        IPlayersRepository Players { get; set; }
        IPuzzleRepository Puzzles { get; set; }
        ISolvedPuzlesRepository SolvedPuzles { get; set; }

        public DataManager(IPlayersRepository players, IPuzzleRepository puzzles, ISolvedPuzlesRepository solvedPuzles)
        {
            Players = players;
            Puzzles = puzzles;
            SolvedPuzles = solvedPuzles;
        }
    }
}
