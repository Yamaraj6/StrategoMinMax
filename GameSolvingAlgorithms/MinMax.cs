using System;
using System.Collections.Generic;
using System.Text;

namespace GameSolvingAlgorithms.GameInterfaces
{
    internal class MinMax
    {
        private int resultPlayer;
        
        private int ChangePlayer(int activePlayer)
        {
            return activePlayer == 0 ? 1 : 0;
        }

        public void SetResultPlayer(IGame game, IPlayer activePlayer)
        {
            resultPlayer = game.GetPlayers()[0].Equals(activePlayer) ? 0 : 1;
        }

        public IMove GetMinMaxMove(IGame game, IMove lastMove, int depth)
        {
            var _moves = game.GetBoard().GetPossibleMoves();
            if (depth == 0 || _moves.Count == 0)
            {
                if (lastMove != null)
                {
                    lastMove.SetPoints(game.CountResult(resultPlayer, game.GetPlayers()[resultPlayer]));
                }
                return lastMove;
            }
            
            foreach (var _move in _moves)
            {
                game.MakeMove(_move);
                GetMinMaxMove(game, _move, depth - 1);
                game.UndoMove(_move);                
            }

            if (lastMove != null)
            {
                if (game.GetPlayerWithMove() == resultPlayer)
                {
                    lastMove.SetPoints(GetMax(_moves).GetPoints());
                    return lastMove;
                }
                else
                {
                    lastMove.SetPoints(GetMin(_moves).GetPoints());
                    return lastMove;
                }
            }

            return GetMax(_moves);
        }

        public IMove GetAlfaBetaMoveNew(IGame game, IMove lastMove, int depth, double α, double β)
        {
            IMove _bestMove = null;
            var _moves = game.GetBoard().GetPossibleMoves();
            if (depth == 0 || _moves.Count == 0)
            {
                if (lastMove != null)
                {
                    lastMove.SetPoints(game.CountResult(resultPlayer, game.GetPlayers()[resultPlayer]));
                }
                return lastMove;
            }

            foreach (var _move in _moves)
            {
                game.MakeMove(_move);
                GetAlfaBetaMoveNew(game, _move, depth - 1, α, β);
                game.UndoMove(_move);

                if (game.GetPlayerWithMove() == resultPlayer)
                {
                    if (_bestMove == null || _bestMove.GetPoints() < _move.GetPoints())
                    {
                        _bestMove = _move;
                    }
                    if (α < _bestMove.GetPoints())
                    {
                        α = _bestMove.GetPoints();
                    }
                    if (_bestMove.GetPoints() > β)
                    {
                        lastMove.SetPoints(_bestMove.GetPoints());
                        return lastMove;
                    }
                }
                else
                {
                    if (_bestMove == null || _bestMove.GetPoints() > _move.GetPoints())
                    {
                        _bestMove = _move;
                    }
                    if (β > _bestMove.GetPoints())
                    {
                        β = _bestMove.GetPoints();
                    }
                    if (_bestMove.GetPoints() < α)
                    {
                        lastMove.SetPoints(_bestMove.GetPoints());
                        return lastMove;
                    }
                }

            }

            if (lastMove != null)
            {
                if (game.GetPlayerWithMove() == resultPlayer)
                {
                    lastMove.SetPoints(GetMax(_moves).GetPoints());
                    return lastMove;
                }
                else
                {
                    lastMove.SetPoints(GetMin(_moves).GetPoints());
                    return lastMove;
                }
            }

            return GetMax(_moves);
        }

        private IMove GetMax(List<IMove> list)
        {
            IMove _maxMove = list[0];
            double _maxNumber = list[0].GetPoints();
            foreach (var _move in list)
            {
                if (_maxNumber < _move.GetPoints())
                {
                    _maxNumber = _move.GetPoints();
                    _maxMove = _move;
                }
            }
            return _maxMove;
        }

        private IMove GetMin(List<IMove> list)
        {
            IMove _minMove = list[0];
            double _maxNumber = list[0].GetPoints();
            foreach (var _move in list)
            {
                if (_maxNumber > _move.GetPoints())
                {
                    _maxNumber = _move.GetPoints();
                    _minMove = _move;
                }
            }
            return _minMove;
        }
    }
}
