using System;
using System.Collections.Generic;
using System.Text;

namespace GameSolvingAlgorithms.GameInterfaces
{
    public interface IGame
    {
        IPlayer[] GetPlayers();
        IBoard GetBoard();
        void MakeMove(IMove move);
        void UndoMove(IMove move);
        double CountResult(int resultPlayer, IPlayer player);
        int GetPlayerWithMove();
    }
}