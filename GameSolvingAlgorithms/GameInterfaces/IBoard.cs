using System;
using System.Collections.Generic;
using System.Text;

namespace GameSolvingAlgorithms.GameInterfaces
{
    public interface IBoard
    {
        IMove GetBestMove();
        int GetBoardSize();
        Dictionary<IField, IFigure> GetBoard();
        List<IMove> GetPossibleMoves();
        List<IMove> GetOccupiedPlaces();
        double MakeMove(IMove move);
        double UndoMove(IMove move);
        IMove GetFirstPossibleMove();
        IMove GetFirstScoredMove();
        IBoard MakeMoveOnNewBoard(IMove move);
    }
}