using System;
using System.Collections.Generic;
using System.Text;
using GameSolvingAlgorithms.GameInterfaces;

namespace GameStratego
{
    public class StrategoBoard : IBoard
    {
        Dictionary<IField,IFigure> gameBoard;

        public StrategoBoard(int boardSize)
        {
            gameBoard = new Dictionary<IField, IFigure>();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var _field = new Field(i, j);
                    gameBoard.Add(_field, null);                  
                }
            }
        }

        public StrategoBoard(Dictionary<IField, IFigure> gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public Dictionary<IField, IFigure> GetBoard()
        {
            return gameBoard;
        }
        
        public int GetBoardSize()
        {
            return (int)Math.Pow(gameBoard.Count, 1f / 2f);
        }
   
        public List<IMove> GetPossibleMoves()
        {
            var _moves = new List<IMove>();
            foreach(var field in gameBoard)
            {
                if(field.Value==null)
                {
                    _moves.Add(new Move(field.Key, new StrategoFigure()));
                }
            }
            return _moves;
        }

        public List<IMove> GetOccupiedPlaces()
        {
            var _moves = new List<IMove>();
            foreach (var field in gameBoard)
            {
                if (field.Value != null)
                {
                    _moves.Add(new Move(field.Key, new StrategoFigure()));
                }
            }
            return _moves;

        }

        public IMove GetFirstPossibleMove()
        {
            foreach (var move in GetPossibleMoves())
            {
                return move;
            }
            return null;
        }

        public IMove GetFirstScoredMove()
        {
            var moves = GetPossibleMoves();
            IMove firstMove = null;
            foreach (var move in moves)
            {
                if (firstMove == null)
                {
                    firstMove = move;
                }
                if(Evaluator.RateMove(move.GetField(), this)>0)
                {
                    return move;
                }
            }
            return firstMove;
        }

        public IMove GetBestMove()
        {
            var moves = GetPossibleMoves();
            IMove bestMove = null;
            foreach (var move in moves)
            {
                if (bestMove == null || 
                        Evaluator.RateMove(move.GetField(), this) > Evaluator.RateMove(bestMove.GetField(), this))
                {
                    bestMove = move;
                }
            }
            return bestMove;
        }

        public double MakeMove(IMove move)
        {
            move.SetPointsForMove(Evaluator.RateMove(move.GetField(), this));
            gameBoard[move.GetField()] = move.GetFigure();
            return move.GetPointsForMove();
        }

        public double UndoMove(IMove move)
        {
            gameBoard[move.GetField()] = null;
            return -move.GetPointsForMove();
        }

        public IBoard MakeMoveOnNewBoard(IMove move)
        {
            var _board = new Dictionary<IField, IFigure>();
            foreach (var _place in gameBoard)
            {
                _board.Add(_place.Key, _place.Value);
            }
            _board[move.GetField()] = move.GetFigure();
            return new StrategoBoard(_board);
        }
    }
}